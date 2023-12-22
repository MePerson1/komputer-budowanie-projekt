using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/cpu")]
    public class CpuController : Controller
    {
        private readonly IGenericRepository<Cpu> _cpuRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public CpuController(IGenericRepository<Cpu> cpuRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _cpuRepository = cpuRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpus()
        {
            var cpus = await _cpuRepository.GetAllAsync();
            if (cpus is null || !cpus.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CpuDto>>(cpus));
        }

        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllCpuScraper()
        {
            var cpus = await _cpuRepository.GetAllAsync();
            if (cpus is null || !cpus.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cpus));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCpuById(int id)
        {
            var cpu = await _cpuRepository.GetByIdAsync(id);
            if (cpu is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CpuDto>(cpu));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var cpus = await _cpuRepository.GetAllAsync();
                if (cpus is null || !cpus.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.CpuFilter(configuration, ref cpus);

                return Ok(cpus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpPost]
        public async Task<IActionResult> CreateCpu([FromBody] CpuDto cpu)
        {
            var newCpu = _mapper.Map<Cpu>(cpu);
            try
            {
                await _cpuRepository.Create(newCpu);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpPut]
        public async Task<IActionResult> UpdateCpu([FromBody] CpuDto cpu)
        {
            var newCpu = _mapper.Map<Cpu>(cpu);
            try
            {
                await _cpuRepository.Update(newCpu);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices == null || newPrices.Prices == null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                Cpu cpu = await _cpuRepository.GetByIdAsync(newPrices.Id);

                if (cpu is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = cpu.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        cpu.Prices.Add(price);
                    }
                }

                await _cpuRepository.Update(cpu);

                return Ok(cpu);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Error updating prices. Please try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCpu(int id)
        {
            var cpu = await _cpuRepository.GetByIdAsync(id);
            if (cpu is null)
            {
                return NotFound();
            }

            try
            {
                await _cpuRepository.Delete(cpu);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

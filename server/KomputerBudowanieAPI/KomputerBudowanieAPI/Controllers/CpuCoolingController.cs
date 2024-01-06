using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/cpu-cooling")]
    public class CpuCoolingController : Controller
    {
        private readonly IPcPartsRepository<CpuCooling> _cpuCoolingRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public CpuCoolingController(IPcPartsRepository<CpuCooling> cpuCoolingRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _cpuCoolingRepository = cpuCoolingRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpusCollings()
        {
            var cpuCoolings = await _cpuCoolingRepository.GetAllAsync();
            if (cpuCoolings is null || !cpuCoolings.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CpuCoolingDto>>(cpuCoolings));
        }

        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllCpusCoolingsScraper()
        {
            var cases = await _cpuCoolingRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCpuCoolingById(int id)
        {
            var cpuCooling = await _cpuCoolingRepository.GetByIdAsync(id);
            if (cpuCooling is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CpuCoolingDto>(cpuCooling));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var cpuCoolings = await _cpuCoolingRepository.GetAllAsync();
                if (cpuCoolings is null || !cpuCoolings.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.CpuCoolingFilter(configuration, ref cpuCoolings);

                return Ok(cpuCoolings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateCpuCooling([FromBody] CpuCoolingDto cpuCooling)
        {
            try
            {
                var newCpuCooling = _mapper.Map<CpuCooling>(cpuCooling);
                await _cpuCoolingRepository.Create(newCpuCooling);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCpuCooling(int id)
        {
            var cpuCooling = await _cpuCoolingRepository.GetByIdAsync(id);
            if (cpuCooling is null)
            {
                return NotFound();
            }

            try
            {
                await _cpuCoolingRepository.Delete(cpuCooling);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut]
        public async Task<IActionResult> UpdateCase([FromBody] CpuCoolingDto cpuCooling)
        {
            try
            {
                var newCpuCooling = _mapper.Map<CpuCooling>(cpuCooling);
                await _cpuCoolingRepository.Update(newCpuCooling);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices == null || newPrices.Prices == null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                CpuCooling cpuCooling = await _cpuCoolingRepository.GetByIdAsync(newPrices.Id);

                if (cpuCooling is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = cpuCooling.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        ShopPrice newPrice = new ShopPrice
                        {
                            ShopName = price.ShopName,
                            Link = price.Link,
                            Price = price.Price
                        };

                        cpuCooling.Prices.Add(price);
                    }
                    await _cpuCoolingRepository.Update(cpuCooling);
                }
                return Ok(cpuCooling);
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

    }
}

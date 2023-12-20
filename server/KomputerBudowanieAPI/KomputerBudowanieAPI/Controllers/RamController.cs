using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/ram")]
    public class RamController : Controller
    {
        private readonly IGenericRepository<Ram> _ramRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public RamController(IGenericRepository<Ram> ramRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _ramRepository = ramRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRam()
        {
            var rams = await _ramRepository.GetAllAsync();
            if (rams is null || !rams.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RamDto>>(rams));
        }

        [HttpGet("scrapper")]
        public async Task<IActionResult> GetAllRamsScraper()
        {
            var cases = await _ramRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            var ram = await _ramRepository.GetByIdAsync(id);
            if (ram is null)
                return NotFound();
            return Ok(_mapper.Map<RamDto>(ram));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var rams = await _ramRepository.GetAllAsync();
                if (rams is null || !rams.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.RamFilter(configuration, ref rams);

                return Ok(rams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRam([FromBody] RamDto ram)
        {
            var newRam = _mapper.Map<Ram>(ram);
            try
            {
                await _ramRepository.Create(newRam);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRam([FromBody] RamDto ram)
        {
            var newRam = _mapper.Map<Ram>(ram);
            try
            {
                await _ramRepository.Update(newRam);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices == null || newPrices.Prices == null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                Ram ram = await _ramRepository.GetByIdAsync(newPrices.Id);

                if (ram is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = ram.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        ram.Prices.Add(price);
                    }
                }

                await _ramRepository.Update(ram);

                return Ok(ram);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            var ram = await _ramRepository.GetByIdAsync(id);
            if (ram is null)
                return NotFound();
            try
            {
                await _ramRepository.Delete(ram);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

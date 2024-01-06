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
    [Route("api/water-cooling")]
    public class WaterCoolingController : Controller
    {
        private readonly IGenericRepository<WaterCooling> _waterCoolingRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public WaterCoolingController(IGenericRepository<WaterCooling> waterCoolingRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _waterCoolingRepository = waterCoolingRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFans()
        {
            var fans = await _waterCoolingRepository.GetAllAsync();
            if (fans is null || !fans.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<WaterCoolingDto>>(fans));
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllCpusCoolingsScraper()
        {
            var cases = await _waterCoolingRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFanById(int id)
        {
            var fan = await _waterCoolingRepository.GetByIdAsync(id);
            if (fan is null)
                return NotFound();
            return Ok(_mapper.Map<WaterCoolingDto>(fan));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var waterCoolings = await _waterCoolingRepository.GetAllAsync();
                if (waterCoolings is null || !waterCoolings.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.WaterCoolingFilter(configuration, ref waterCoolings);

                return Ok(waterCoolings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateFan([FromBody] WaterCoolingDto fan)
        {
            var newFan = _mapper.Map<WaterCooling>(fan);
            try
            {
                await _waterCoolingRepository.Create(newFan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut]
        public async Task<IActionResult> UpdateFan([FromBody] WaterCoolingDto fan)
        {
            var newFan = _mapper.Map<WaterCooling>(fan);
            try
            {
                await _waterCoolingRepository.Update(newFan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices is null || newPrices.Prices is null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                WaterCooling waterCooling = await _waterCoolingRepository.GetByIdAsync(newPrices.Id);

                if (waterCooling is null)
                {
                    return BadRequest("WaterCooling with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = waterCooling.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        waterCooling.Prices.Add(price);
                    }
                    await _waterCoolingRepository.Update(waterCooling);
                }
                return Ok(waterCooling);
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

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFan(int id)
        {
            var fan = await _waterCoolingRepository.GetByIdAsync(id);
            if (fan is null)
                return NotFound();
            try
            {
                await _waterCoolingRepository.Delete(fan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

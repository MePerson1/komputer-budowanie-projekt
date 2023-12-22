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
    [Route("api/power-supply")]
    public class PowerSupplyController : Controller
    {
        private readonly IGenericRepository<PowerSupply> _powerSupplyRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public PowerSupplyController(IGenericRepository<PowerSupply> powerSupplyRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _powerSupplyRepository = powerSupplyRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPowerSupplies()
        {
            var powerSupplies = await _powerSupplyRepository.GetAllAsync();
            if (powerSupplies is null || !powerSupplies.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PowerSupplyDto>>(powerSupplies));
        }

        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllCpusCoolingsScraper()
        {
            var cases = await _powerSupplyRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPowerSupplyById(int id)
        {
            var powerSupply = await _powerSupplyRepository.GetByIdAsync(id);
            if (powerSupply is null)
                return NotFound();
            return Ok(_mapper.Map<PowerSupplyDto>(powerSupply));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var powerSupplies = await _powerSupplyRepository.GetAllAsync();
                if (powerSupplies is null || !powerSupplies.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.PowerSupplyFilter(configuration, ref powerSupplies);

                return Ok(powerSupplies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpPost]
        public async Task<IActionResult> CreatePowerSupply([FromBody] PowerSupplyDto powerSupply)
        {
            var newPowerSupply = _mapper.Map<PowerSupply>(powerSupply);
            try
            {
                await _powerSupplyRepository.Create(newPowerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize("IsAdminOrScraperJwt")]
        [HttpPut]
        public async Task<IActionResult> UpdatePowerSupply([FromBody] PowerSupplyDto powerSupply)
        {
            var newPowerSupply = _mapper.Map<PowerSupply>(powerSupply);
            try
            {
                await _powerSupplyRepository.Update(newPowerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
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

                PowerSupply powerSupply = await _powerSupplyRepository.GetByIdAsync(newPrices.Id);

                if (powerSupply is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = powerSupply.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        powerSupply.Prices.Add(price);
                    }
                }

                await _powerSupplyRepository.Update(powerSupply);

                return Ok(powerSupply);
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
        public async Task<IActionResult> DeletePowerSupply(int id)
        {
            var powerSupply = await _powerSupplyRepository.GetByIdAsync(id);
            if (powerSupply is null)
                return NotFound();
            try
            {
                await _powerSupplyRepository.Delete(powerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

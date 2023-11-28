using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

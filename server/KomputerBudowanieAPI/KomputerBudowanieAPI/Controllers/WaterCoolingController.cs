using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

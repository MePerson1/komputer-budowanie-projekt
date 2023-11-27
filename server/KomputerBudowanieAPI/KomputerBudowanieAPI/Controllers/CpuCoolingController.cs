using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/cpu-cooling")]
    public class CpuCoolingController : Controller
    {
        private readonly IGenericRepository<CpuCooling> _cpuCoolingRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public CpuCoolingController(IGenericRepository<CpuCooling> cpuCoolingRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
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

    }
}

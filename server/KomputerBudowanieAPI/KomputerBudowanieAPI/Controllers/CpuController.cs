using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

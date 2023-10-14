using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CpuController : Controller
    {
        public readonly IGenericRepository<Cpu> _cpuRepository;
        public readonly IMapper _mapper;

        public CpuController(IGenericRepository<Cpu> cpuRepository, IMapper mapper)
        {
            _cpuRepository = cpuRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpus()
        {
            var cpus = await _cpuRepository.GetAllAsync();
            if (cpus is null)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<CpuDto>>(cpus));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCpuById(int id)
        {
            var cpu = await _cpuRepository.GetByIdAsync(id);
            if (cpu is null)
                return NotFound();
            return Ok(_mapper.Map<CpuDto>(cpu));
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

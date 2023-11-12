using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterCoolingController : Controller
    {
        public readonly IGenericRepository<WaterCooling> _fanRepository;
        public readonly IMapper _mapper;

        public WaterCoolingController(IGenericRepository<WaterCooling> fanRepository, IMapper mapper)
        {
            _fanRepository = fanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFans()
        {
            var fans = await _fanRepository.GetAllAsync();
            if (fans is null || !fans.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<WaterCoolingDto>>(fans));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFanById(int id)
        {
            var fan = await _fanRepository.GetByIdAsync(id);
            if (fan is null)
                return NotFound();
            return Ok(_mapper.Map<WaterCoolingDto>(fan));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFan([FromBody] WaterCoolingDto fan)
        {
            var newFan = _mapper.Map<WaterCooling>(fan);
            try
            {
                await _fanRepository.Create(newFan);
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
                await _fanRepository.Update(newFan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFan(int id)
        {
            var fan = await _fanRepository.GetByIdAsync(id);
            if (fan is null)
                return NotFound();
            try
            {
                await _fanRepository.Delete(fan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

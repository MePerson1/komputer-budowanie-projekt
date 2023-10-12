using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FanController : Controller
    {
        public readonly IFanRepository _fanRepository;
        public readonly IMapper _mapper;

        public FanController(IFanRepository fanRepository, IMapper mapper)
        {
            _fanRepository = fanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFans()
        {
            var fans = await _fanRepository.GetAllAsync();
            if (fans is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<FanDto>>(fans));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFanById(int id)
        {
            var fan = await _fanRepository.GetById(id);
            if (fan is null)
                return NotFound();
            return Ok(_mapper.Map<FanDto>(fan));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFan([FromBody] FanDto fan)
        {
            var newFan = _mapper.Map<Fan>(fan);
            try
            {
                await _fanRepository.Create(newFan);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFan([FromBody] FanDto fan)
        {
            var newFan = _mapper.Map<Fan>(fan);
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
            var fan = await _fanRepository.GetById(id);
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

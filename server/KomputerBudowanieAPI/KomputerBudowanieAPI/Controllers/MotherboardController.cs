using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotherboardController : Controller
    {
        public readonly IGenericRepository<Motherboard> _motherboardRepository;
        public readonly IMapper _mapper;

        public MotherboardController(IGenericRepository<Motherboard> motherboardRepository, IMapper mapper)
        {
            _motherboardRepository = motherboardRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMotherboards()
        {
            var motherboards = await _motherboardRepository.GetAllAsync();
            if (motherboards is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<MotherboardDto>>(motherboards));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMotherboardById(int id)
        {
            var motherboard = await _motherboardRepository.GetByIdAsync(id);
            if (motherboard is null)
                return NotFound();
            return Ok(_mapper.Map<MotherboardDto>(motherboard));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotherboard([FromBody] MotherboardDto motherboard)
        {
            var newMotherboard = _mapper.Map<Motherboard>(motherboard);
            try
            {
                await _motherboardRepository.Create(newMotherboard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMotherboard([FromBody] MotherboardDto motherboard)
        {
            var newMotherboard = _mapper.Map<Motherboard>(motherboard);
            try
            {
                await _motherboardRepository.Update(newMotherboard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMotherboard(int id)
        {
            var motherboard = await _motherboardRepository.GetByIdAsync(id);
            if (motherboard is null)
                return NotFound();
            try
            {
                await _motherboardRepository.Delete(motherboard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : Controller
    {
        public readonly IGenericRepository<Storage> _memoryRepository;
        public readonly IMapper _mapper;

        public StorageController(IGenericRepository<Storage> memoryRepository, IMapper mapper)
        {
            _memoryRepository = memoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMemories()
        {
            var memories = await _memoryRepository.GetAllAsync();
            if (memories is null || !memories.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StorageDto>>(memories));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMemoryById(int id)
        {
            var memory = await _memoryRepository.GetByIdAsync(id);
            if (memory is null)
                return NotFound();
            return Ok(_mapper.Map<StorageDto>(memory));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMemory([FromBody] StorageDto memory)
        {
            var newMemory = _mapper.Map<Storage>(memory);
            try
            {
                await _memoryRepository.Create(newMemory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMemory([FromBody] StorageDto memory)
        {
            var newMemory = _mapper.Map<Storage>(memory);
            try
            {
                await _memoryRepository.Update(newMemory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMemory(int id)
        {
            var memory = await _memoryRepository.GetByIdAsync(id);
            if (memory is null)
                return NotFound();
            try
            {
                await _memoryRepository.Delete(memory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

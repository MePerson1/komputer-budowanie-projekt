using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class StorageController : Controller
    {
        private readonly IGenericRepository<Storage> _memoryRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public StorageController(IGenericRepository<Storage> memoryRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _memoryRepository = memoryRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
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

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var storages = await _memoryRepository.GetAllAsync();
                if (storages is null || !storages.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.StorageFilter(configuration, ref storages);

                return Ok(storages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

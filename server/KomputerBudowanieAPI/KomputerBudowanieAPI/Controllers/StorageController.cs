using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class StorageController : Controller
    {
        private readonly IPcPartsRepository<Storage> _storageRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public StorageController(IPcPartsRepository<Storage> storageRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _storageRepository = storageRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMemories()
        {
            var memories = await _storageRepository.GetAllAsync();
            if (memories is null || !memories.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StorageDto>>(memories));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMemoryById(int id)
        {
            var memory = await _storageRepository.GetByIdAsync(id);
            if (memory is null)
                return NotFound();
            return Ok(_mapper.Map<StorageDto>(memory));
        }
        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllStoragesScraper()
        {
            var cases = await _storageRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var storages = await _storageRepository.GetAllAsync();
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

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateMemory([FromBody] StorageDto memory)
        {
            var newMemory = _mapper.Map<Storage>(memory);
            try
            {
                await _storageRepository.Create(newMemory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut]
        public async Task<IActionResult> UpdateMemory([FromBody] StorageDto memory)
        {
            var newMemory = _mapper.Map<Storage>(memory);
            try
            {
                await _storageRepository.Update(newMemory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices == null || newPrices.Prices == null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                Storage storage = await _storageRepository.GetByIdAsync(newPrices.Id);

                if (storage is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = storage.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        storage.Prices.Add(price);
                    }
                    await _storageRepository.Update(storage);
                }
                return Ok(storage);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Error updating prices. Please try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMemory(int id)
        {
            var memory = await _storageRepository.GetByIdAsync(id);
            if (memory is null)
                return NotFound();
            try
            {
                await _storageRepository.Delete(memory);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

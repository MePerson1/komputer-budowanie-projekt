using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Helpers;
using KomputerBudowanieAPI.Helpers.Request;
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
        public async Task<IActionResult> GetAllStorages()
        {
            var storages = await _storageRepository.GetAllAsync();
            if (storages is null || !storages.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StorageDto>>(storages));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAllStoragesPaginate([FromQuery] PartsParams partsParams)
        {
            var storages = await _storageRepository.GetAllAsyncPagination(partsParams);

            if (storages is null || !storages.Any())
            {
                return NotFound();
            }

            Response.AddPaginationHeader(storages.MetaData);
            return Ok(storages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMemoryById(int id)
        {
            var storages = await _storageRepository.GetByIdAsync(id);
            if (storages is null)
                return NotFound();
            return Ok(_mapper.Map<StorageDto>(storages));
        }
        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllStoragesScraper()
        {
            var storages = await _storageRepository.GetAllAsync();
            if (storages is null || !storages.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(storages));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails, [FromQuery] PartsParams partsParams)
        {
            try
            {
                var storages = await _storageRepository.GetAllAsyncSortSearch(partsParams);
                if (storages is null || !storages.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.StorageFilter(configuration, ref storages);

                var paginationStorage = await PagedList<Storage>.ToPagedList(storages, partsParams.PageNumber, partsParams.PageSize);
                Response.AddPaginationHeader(paginationStorage.MetaData);

                return Ok(paginationStorage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateMemory([FromBody] StorageDto storage)
        {
            var newStorage = _mapper.Map<Storage>(storage);
            try
            {
                await _storageRepository.Create(newStorage);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut]
        public async Task<IActionResult> UpdateMemory([FromBody] StorageDto storages)
        {
            var newMemory = _mapper.Map<Storage>(storages);
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

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
    [Route("api/motherboard")]
    public class MotherboardController : Controller
    {
        private readonly IPcPartsRepository<Motherboard> _motherboardRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public MotherboardController(IPcPartsRepository<Motherboard> motherboardRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _motherboardRepository = motherboardRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMotherboards()
        {
            var motherboards = await _motherboardRepository.GetAllAsync();
            if (motherboards is null || !motherboards.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<MotherboardDto>>(motherboards));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAllMotherboardsPaginate([FromQuery] PartsParams partsParams)
        {
            var motherboards = await _motherboardRepository.GetAllAsyncPagination(partsParams);

            if (motherboards is null || !motherboards.Any())
            {
                return NotFound();
            }

            Response.AddPaginationHeader(motherboards.MetaData);
            return Ok(motherboards);
        }

        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllMotherboardScraper()
        {
            var cases = await _motherboardRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMotherboardById(int id)
        {
            var motherboard = await _motherboardRepository.GetByIdAsync(id);
            if (motherboard is null)
                return NotFound();
            return Ok(_mapper.Map<MotherboardDto>(motherboard));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails, [FromQuery] PartsParams partsParams)
        {
            try
            {
                var motherboards = await _motherboardRepository.GetAllAsync();
                if (motherboards is null || !motherboards.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.MotherboardFilter(configuration, ref motherboards);

                var paginationMotherboards = await PagedList<Motherboard>.ToPagedList(motherboards, partsParams.PageNumber, partsParams.PageSize);
                Response.AddPaginationHeader(paginationMotherboards.MetaData);

                return Ok(paginationMotherboards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

                Motherboard motherboard = await _motherboardRepository.GetByIdAsync(newPrices.Id);

                if (motherboard is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = motherboard.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        motherboard.Prices.Add(price);
                    }

                    await _motherboardRepository.Update(motherboard);
                }
                return Ok(motherboard);
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

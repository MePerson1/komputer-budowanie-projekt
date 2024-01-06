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
    [Route("api/case")]
    public class CaseController : Controller
    {
        private readonly IPcPartsRepository<Case> _caseRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public CaseController(IPcPartsRepository<Case> caseRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCases()
        {
            var cases = await _caseRepository.GetAllAsync();

            if (cases is null || !cases.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ICollection<CaseDto>>(cases));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCasesPaginate(int page = 1, int pageSize = 10, string sortBy = "", string searchKeyword = "")
        {
            var cases = await _caseRepository.GetAllAsyncPagination(page, pageSize, sortBy, searchKeyword);

            if (cases is null || !cases.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ICollection<CaseDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            var pcCase = await _caseRepository.GetByIdAsync(id);
            if (pcCase is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CaseDto>(pcCase));
        }

        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllCasesScraper()
        {
            var cases = await _caseRepository.GetAllAsync();
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
                var cases = await _caseRepository.GetAllAsync();
                if (cases is null || !cases.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.CaseFilter(configuration, ref cases);

                return Ok(cases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] CaseDto pcCase)
        {
            try
            {
                var newPcCase = _mapper.Map<Case>(pcCase);
                await _caseRepository.Create(newPcCase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var pcCase = await _caseRepository.GetByIdAsync(id);
            if (pcCase is null)
            {
                return NotFound();
            }

            try
            {
                await _caseRepository.Delete(pcCase);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCase(int id, [FromBody] CaseDto pcCase)
        {
            try
            {
                var newPcCase = _mapper.Map<Case>(pcCase);
                await _caseRepository.Update(newPcCase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

                Case pcCase = await _caseRepository.GetByIdAsync(newPrices.Id);

                if (pcCase is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = pcCase.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        pcCase.Prices.Add(price);
                    }
                    await _caseRepository.Update(pcCase);
                }
                return Ok(pcCase);
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
    }
}

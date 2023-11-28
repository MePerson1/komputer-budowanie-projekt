using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/case")]
    public class CaseController : Controller
    {
        private readonly IGenericRepository<Case> _caseRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public CaseController(IGenericRepository<Case> caseRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
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
            return Ok(_mapper.Map<IEnumerable<CaseDto>>(cases));
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

        [HttpPut]
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

        //HttpPost do sprawdznia compactybilnosci przy pobieraniu danych

    }
}

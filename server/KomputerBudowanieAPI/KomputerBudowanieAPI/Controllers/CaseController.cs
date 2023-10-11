using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController : Controller
    {
        public readonly ICaseRepository _caseRepository;
        public readonly IMapper _mapper;

        public CaseController(ICaseRepository caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCases()
        {
            var cases = await _caseRepository.GetAllAsync();
            if (cases is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CaseDto>>(cases));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            var pcCase = await _caseRepository.GetById(id);
            if (pcCase is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CaseDto>(pcCase));
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
            var pcCase = await _caseRepository.GetById(id);
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

    }
}

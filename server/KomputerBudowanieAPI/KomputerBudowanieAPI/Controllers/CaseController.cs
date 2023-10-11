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
            return Ok(cases);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] CaseDto pcCase)
        {
            try
            {
                var newPcCase = _mapper.Map<Case>(pcCase);
                _caseRepository.Insert(newPcCase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

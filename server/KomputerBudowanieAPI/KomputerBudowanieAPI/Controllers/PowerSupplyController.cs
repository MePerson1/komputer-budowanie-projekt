using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerSupplyController : Controller
    {
        public readonly IGenericRepository<PowerSupply> _powerSupplyRepository;
        public readonly IMapper _mapper;

        public PowerSupplyController(IGenericRepository<PowerSupply> powerSupplyRepository, IMapper mapper)
        {
            _powerSupplyRepository = powerSupplyRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPowerSupplies()
        {
            var powerSupplies = await _powerSupplyRepository.GetAllAsync();
            if (powerSupplies is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PowerSupplyDto>>(powerSupplies));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPowerSupplyById(int id)
        {
            var powerSupply = await _powerSupplyRepository.GetByIdAsync(id);
            if (powerSupply is null)
                return NotFound();
            return Ok(_mapper.Map<PowerSupplyDto>(powerSupply));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePowerSupply([FromBody] PowerSupplyDto powerSupply)
        {
            var newPowerSupply = _mapper.Map<PowerSupply>(powerSupply);
            try
            {
                await _powerSupplyRepository.Create(newPowerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePowerSupply([FromBody] PowerSupplyDto powerSupply)
        {
            var newPowerSupply = _mapper.Map<PowerSupply>(powerSupply);
            try
            {
                await _powerSupplyRepository.Update(newPowerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePowerSupply(int id)
        {
            var powerSupply = await _powerSupplyRepository.GetByIdAsync(id);
            if (powerSupply is null)
                return NotFound();
            try
            {
                await _powerSupplyRepository.Delete(powerSupply);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

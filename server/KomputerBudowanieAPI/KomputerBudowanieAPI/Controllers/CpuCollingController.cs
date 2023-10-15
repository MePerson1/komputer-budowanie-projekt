﻿using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CpuCollingController : Controller
    {
        public readonly IGenericRepository<CpuCooling> _cpuCoolingRepository;
        public readonly IMapper _mapper;

        public CpuCollingController(IGenericRepository<CpuCooling> cpuCoolingRepository, IMapper mapper)
        {
            _cpuCoolingRepository = cpuCoolingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpusCollings()
        {
            var cpuCoolings = await _cpuCoolingRepository.GetAllAsync();
            if (cpuCoolings is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CpuCoolingDto>>(cpuCoolings));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCpuCoolingById(int id)
        {
            var cpuCooling = await _cpuCoolingRepository.GetByIdAsync(id);
            if (cpuCooling is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CpuCoolingDto>(cpuCooling));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCpuCooling([FromBody] CpuCoolingDto cpuCooling)
        {
            try
            {
                var newCpuCooling = _mapper.Map<CpuCooling>(cpuCooling);
                await _cpuCoolingRepository.Create(newCpuCooling);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCpuCooling(int id)
        {
            var cpuCooling = await _cpuCoolingRepository.GetByIdAsync(id);
            if (cpuCooling is null)
            {
                return NotFound();
            }

            try
            {
                await _cpuCoolingRepository.Delete(cpuCooling);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCase([FromBody] CpuCoolingDto cpuCooling)
        {
            try
            {
                var newCpuCooling = _mapper.Map<CpuCooling>(cpuCooling);
                await _cpuCoolingRepository.Update(newCpuCooling);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
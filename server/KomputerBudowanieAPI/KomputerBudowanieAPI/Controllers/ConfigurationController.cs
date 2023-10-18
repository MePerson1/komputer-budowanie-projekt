﻿using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        public readonly IPcConfigurationRepository _pcConfigurationRepository;
        public readonly IUserRepository _userRepository;

        public readonly IGenericRepository<Case> _caseRepository;
        public readonly IGenericRepository<CpuCooling> _cpuCoolingRepository;
        public readonly IGenericRepository<Cpu> _cpuRepository;
        public readonly IGenericRepository<Fan> _fanRepository;
        public readonly IGenericRepository<GraphicCard> _graphicCardRepository;
        public readonly IGenericRepository<Memory> _memoryRepository;
        public readonly IGenericRepository<Motherboard> _motherboardRepository;
        public readonly IGenericRepository<PowerSupply> _powerSupplyRepository;
        public readonly IGenericRepository<Ram> _ramRepository;

        public ConfigurationController(IPcConfigurationRepository pcConfigurationRepository, IUserRepository userRepository, IGenericRepository<Case> caseRepository, IGenericRepository<CpuCooling> cpuCoolingRepository, IGenericRepository<Cpu> cpuRepository, IGenericRepository<Fan> fanRepository, IGenericRepository<GraphicCard> graphicCardRepository, IGenericRepository<Memory> memoryRepository, IGenericRepository<Motherboard> motherboardRepository, IGenericRepository<PowerSupply> powerSupplyRepository, IGenericRepository<Ram> ramRepository)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            _userRepository = userRepository;
            _caseRepository = caseRepository;
            _cpuCoolingRepository = cpuCoolingRepository;
            _cpuRepository = cpuRepository;
            _fanRepository = fanRepository;
            _graphicCardRepository = graphicCardRepository;
            _memoryRepository = memoryRepository;
            _motherboardRepository = motherboardRepository;
            _powerSupplyRepository = powerSupplyRepository;
            _ramRepository = ramRepository;
        }

        // GET: api/<ConfigurationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var configs = await _pcConfigurationRepository.GetAllAsync();
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            return Ok(configs);
        }

        // GET api/users/5/configurations
        [Route("api/users/{userId}/configurations")]
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var configs = await _pcConfigurationRepository.GetAllAsync(userId);
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            return Ok(configs);
        }

        // GET api/<ConfigurationController>/5
        [Route("api/users/{userId}/configurations/{configurationId}")]
        [HttpGet]
        public async Task<IActionResult> Get(int userId, Guid configurationId)
        {
            var config = await _pcConfigurationRepository.GetByIdAsync(configurationId);
            if (config is null)
            {
                return NotFound();
            }
            return Ok(config);
        }

        // POST api/<ConfigurationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PcConfigurationCreateDTO newConfigurationDetails)
        {
            if (newConfigurationDetails == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            PcConfiguration newPcConfiguration = new PcConfiguration();
            //newPcConfiguration.Id = Guid.NewGuid();
            //newPcConfiguration.User = _userRepository.GetById(newConfigurationDetails.UserId);
            newPcConfiguration.Name = newConfigurationDetails.Name;
            newPcConfiguration.Description = newConfigurationDetails.Description ?? "";
            newPcConfiguration.Motherboard = await _motherboardRepository.GetByIdAsync(newConfigurationDetails.MotherboadId);
            newPcConfiguration.GraphicCard = await _graphicCardRepository.GetByIdAsync(newConfigurationDetails.GraphicCardId);
            newPcConfiguration.Cpu = await _cpuRepository.GetByIdAsync(newConfigurationDetails.CpuId);
            newPcConfiguration.CPU_Cooling = await _cpuCoolingRepository.GetByIdAsync(newConfigurationDetails.CpuCoolingId);
            
            
            await _pcConfigurationRepository.Create(newPcConfiguration);

            return Ok(done);
        }

        // PUT api/<ConfigurationController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] PcConfigurationDto editingConfigurationDetails)
        {

        }

        // DELETE api/<ConfigurationController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {

        }


    }
}

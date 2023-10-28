using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
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

        public ConfigurationController(IPcConfigurationRepository pcConfigurationRepository, IUserRepository userRepository)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            _userRepository = userRepository;
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
        public async Task<IActionResult> Post([FromBody] PcConfigurationDto newConfigurationDetails)
        {
            if (newConfigurationDetails == null)
            {
                return BadRequest();
            }
            /*if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }*/

            //PcConfiguration newPcConfiguration = new PcConfiguration();
            //newPcConfiguration.Id = Guid.NewGuid();
            //newPcConfiguration.User = _userRepository.GetById(newConfigurationDetails.UserId);
            //newPcConfiguration.Name = newConfigurationDetails.Name;
            //newPcConfiguration.Description = newConfigurationDetails.Description ?? "";
            //newPcConfiguration.Motherboard = await _motherboardRepository.GetByIdAsync(newConfigurationDetails.MotherboadId);
            //newPcConfiguration.GraphicCard = await _graphicCardRepository.GetByIdAsync(newConfigurationDetails.GraphicCardId);
            //newPcConfiguration.Cpu = await _cpuRepository.GetByIdAsync(newConfigurationDetails.CpuId);
            //newPcConfiguration.CPU_Cooling = await _cpuCoolingRepository.GetByIdAsync(newConfigurationDetails.CpuCoolingId);


            //await _pcConfigurationRepository.Create(newPcConfiguration);
            var done = await _pcConfigurationRepository.Create(newConfigurationDetails);

            return done == false ? BadRequest(done) : Ok(done);
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

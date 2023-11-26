using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        public readonly IPcConfigurationRepository _pcConfigurationRepository;
        public readonly IUserRepository _userRepository;

        public ConfigurationController(IPcConfigurationRepository pcConfigurationRepository, IUserRepository userRepository, ICompatibilityService compatibilityService)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var configuration = await _pcConfigurationRepository.GetByIdAsync(id);
            if (configuration is null)
                return NotFound();
            return Ok(configuration);
        }


        // GET api/users/5/configurations
        [Route("users/{userId}")]
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
        [Route("users/{userId}/{configurationId}")]
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

            var done = await _pcConfigurationRepository.Create(newConfigurationDetails);

            return done == false ? BadRequest("Something went wrong") : Ok("New configuration created successfuly!");
        }

        // PUT api/<ConfigurationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PcConfigurationDto editingConfigurationDetails)
        {
            var done = await _pcConfigurationRepository.Update(id, editingConfigurationDetails);
            return done == false ? BadRequest("Something went wrong.") : Ok("Configuration updated successfuly!");
        }

        // DELETE api/<ConfigurationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pcConf = await _pcConfigurationRepository.GetByIdAsync(id);
            if (pcConf is null)
            {
                return NotFound();
            }

            await _pcConfigurationRepository.Delete(pcConf);
            return Accepted();
        }
    }
}

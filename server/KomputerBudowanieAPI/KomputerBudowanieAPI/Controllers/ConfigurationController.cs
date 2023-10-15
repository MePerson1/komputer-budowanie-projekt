using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        public readonly IPcConfigurationRepository _pcConfigurationRepository;

        public ConfigurationController(IPcConfigurationRepository pcConfigurationRepository)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            
        }

        // GET: api/<ConfigurationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var configs = await _pcConfigurationRepository.GetAllAsync();
            if (configs is null)
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
            if (configs is null)
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
        public IActionResult Post([FromBody] PcConfigurationDto newConfigurationDetails)
        {
            if(newConfigurationDetails == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Created("", "");
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

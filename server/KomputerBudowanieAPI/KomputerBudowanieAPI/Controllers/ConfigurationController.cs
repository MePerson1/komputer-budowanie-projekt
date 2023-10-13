using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var configs = await _pcConfigurationRepository.GetAll();
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
            var configs = await _pcConfigurationRepository.GetAll(userId);
            if (configs is null)
            {
                return NotFound();
            }
            return Ok(configs);
        }

        // GET api/<ConfigurationController>/5
        //[Route("api/users/{userId}/configurations/{configurationId}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var config = await _pcConfigurationRepository.GetById(id);
            if (config is null)
            {
                return NotFound();
            }
            return Ok(config);
        }

        // GET api/users/5/configurations/786
        /*[Route("api/users/{userId}/configurations/{configurationId}")]
        [HttpGet]
        public IActionResult Get(int userId, Guid configurationId)
        {

            return Ok($"Get request for user with ID: {userId}");
        }*/

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
        public void Put(int id, [FromBody] PcConfigurationDto editingConfigurationDetails)
        {

        }

        // DELETE api/<ConfigurationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        
    }
}

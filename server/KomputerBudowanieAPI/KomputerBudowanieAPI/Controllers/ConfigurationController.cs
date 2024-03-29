﻿using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Helpers;
using KomputerBudowanieAPI.Helpers.Request;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IPcConfigurationRepository _pcConfigurationRepository;
        private readonly IMapper _mapper;

        public ConfigurationController(IPcConfigurationRepository pcConfigurationRepository, IMapper mapper)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            _mapper = mapper;
        }

        // GET: api/<ConfigurationController>
        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

        [HttpGet("public")]
        public async Task<IActionResult> GetPublic()
        {
            var configs = await _pcConfigurationRepository.GetAllAsyncPublic();
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<PcConfigurationViewModel>>(configs));
        }

        [HttpGet("public/pagination")]
        public async Task<IActionResult> GetPublicPagination([FromQuery] PartsParams partsParams)
        {
            var configs = await _pcConfigurationRepository.GetAllAsyncPublicPagination(partsParams);
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            Response.AddPaginationHeader(configs.MetaData);
            return Ok(_mapper.Map<ICollection<PcConfigurationViewModel>>(configs));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var configuration = await _pcConfigurationRepository.GetByIdAsync(id);
            if (configuration is null)
                return NotFound();
            return Ok(_mapper.Map<PcConfigurationViewModel>(configuration));
        }


        // GET api/users/5/configurations
        [Authorize]
        [Route("users/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var configs = await _pcConfigurationRepository.GetAllAsync(userId);
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<PcConfigurationViewModel>>(configs));
        }

        [Authorize]
        [Route("users/{userId}/pagination")]
        [HttpGet]
        public async Task<IActionResult> GetByUserIdPagination(string userId, [FromQuery] PartsParams partsParams)
        {
            var configs = await _pcConfigurationRepository.GetAllAsyncByUserIdPagination(userId, partsParams);
            if (configs is null || !configs.Any())
            {
                return NotFound();
            }
            Response.AddPaginationHeader(configs.MetaData);
            return Ok(_mapper.Map<ICollection<PcConfigurationViewModel>>(configs));
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PcConfigurationCreateModel newConfigurationDetails)
        {
            if (newConfigurationDetails == null)
            {
                return BadRequest();
            }

            try
            {
                var pcConfiguration = await _pcConfigurationRepository.Create(newConfigurationDetails);
                return Ok(pcConfiguration);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ConfigurationController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PcConfigurationCreateModel editedConfigurationDto)
        {
            try
            {
                var pcConfiguration = await _pcConfigurationRepository.Update(id, editedConfigurationDto);
                return Ok(pcConfiguration);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<ConfigurationController>/5
        [Authorize]
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

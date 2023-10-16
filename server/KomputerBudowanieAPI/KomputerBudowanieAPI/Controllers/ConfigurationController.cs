using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        public readonly IPcConfigurationRepository _pcConfigurationRepository;
        public readonly IUserRepository _userRepository;
        public readonly KomBuildDbContext _dbContext;

        public ConfigurationController(KomBuildDbContext dbContext, IPcConfigurationRepository pcConfigurationRepository, IUserRepository userRepository)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            _userRepository = userRepository;
            _dbContext = dbContext;
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
            //if(!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}


            PcConfiguration newPcConfiguration = new PcConfiguration
            {
                Name = newConfigurationDetails.Name,
                Description = newConfigurationDetails.Description,
                Case = _dbContext.Cases.FirstOrDefault(x => x.Id == newConfigurationDetails.CaseId),
                Motherboard = _dbContext.Motherboards.FirstOrDefault(x => x.Id == newConfigurationDetails.MotherboadId),
                GraphicCard = _dbContext.GraphicCards.FirstOrDefault(x => x.Id == newConfigurationDetails.GraphicCardId),
                Cpu = _dbContext.Cpus.FirstOrDefault(x => x.Id == newConfigurationDetails.CpuId),
                CPU_Cooling = _dbContext.CpuCoolings.FirstOrDefault(x => x.Id == newConfigurationDetails.CpuCoolingId),
                PowerSupply = _dbContext.PowerSupplys.FirstOrDefault(x => x.Id == newConfigurationDetails.PowerSupllyId),
                Fan = _dbContext.Fans.FirstOrDefault(x => x.Id == newConfigurationDetails.FanId),
                User = null
            };
            //newPcConfiguration.Id = Guid.NewGuid();
            ////newPcConfiguration.User = _userRepository.GetById(newConfigurationDetails.UserId);
            //newPcConfiguration.Name = newConfigurationDetails.Name;
            //newPcConfiguration.Description = newConfigurationDetails.Description;



            newConfigurationDetails.RamIds.ForEach(ramId =>
            {
                if (_dbContext.Rams.FirstOrDefaultAsync(x => x.Id == ramId) != null)
                {
                    newPcConfiguration.PcConfigurationRams.Add(new PcConfigurationRam()
                    {
                        PcConfiguration = newPcConfiguration,
                        RamId = ramId
                    });
                }
            });
            newConfigurationDetails.MemoryIds.ForEach(memoryId =>
            {
                if (_dbContext.Memories.FirstOrDefaultAsync(x => x.Id == memoryId) != null)
                {
                    newPcConfiguration.PcConfigurationMemories.Add(new PcConfigurationMemory()
                    {
                        PcConfiguration = newPcConfiguration,
                        MemoryId = memoryId
                    });
                }

            });


            _dbContext.SaveChanges();
            await _pcConfigurationRepository.Create(newPcConfiguration);
            _dbContext.SaveChangesAsync();
            return Created(newPcConfiguration.Id.ToString(), newPcConfiguration);
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

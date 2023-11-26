using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/compatibility")]
    public class CompatibilityController : ControllerBase
    {
        public readonly IPcConfigurationRepository _pcConfigurationRepository;
        public readonly ICompatibilityService _compatibilityService;

        public CompatibilityController(IPcConfigurationRepository pcConfigurationRepository, ICompatibilityService compatibilityService)
        {
            _pcConfigurationRepository = pcConfigurationRepository;
            _compatibilityService = compatibilityService;
        }


        [HttpPost]
        public async Task<IActionResult> WholeCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.CompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("cpu")]
        public async Task<IActionResult> CpuCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.CpuCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("motherboard")]
        public async Task<IActionResult> MotherboardCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.MotherboardCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("storage")]
        public async Task<IActionResult> StorageCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.StorageCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("cpu-cooling")]
        public async Task<IActionResult> CpuCoolingCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.CpuCoolingCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("water-cooling")]
        public async Task<IActionResult> WaterCoolingCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.WaterCoolingCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("ram")]
        public async Task<IActionResult> RamCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.RamCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("graphic-card")]
        public async Task<IActionResult> GraphicCardCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.GraphicCardCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("case")]
        public async Task<IActionResult> CaseCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.CaseCompatibilityCheck(pcConfigration);

            return Ok(toast);
        }

        [HttpPost("power-supply")]
        public async Task<IActionResult> PowerSupplyCompatibilityCheck([FromBody] PcConfigurationDto pcConfigurationDto)
        {
            PcConfiguration? pcConfigration = new PcConfiguration();
            pcConfigration = await _pcConfigurationRepository.GetDataFromIds(pcConfigurationDto, pcConfigration);

            if (pcConfigration is null)
            {
                return BadRequest();
            }

            var toast = await _compatibilityService.PowerSupplyCompatibliityCheck(pcConfigration);

            return Ok(toast);
        }
    }
}

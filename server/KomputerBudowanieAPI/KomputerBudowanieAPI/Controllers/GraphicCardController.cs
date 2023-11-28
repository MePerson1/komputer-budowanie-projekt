using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/graphic-card")]
    public class GraphicCardController : Controller
    {
        private readonly IGenericRepository<GraphicCard> _graphicCardRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public GraphicCardController(IGenericRepository<GraphicCard> graphicCardRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
        {
            _graphicCardRepository = graphicCardRepository;
            _mapper = mapper;
            _compatibilityDataFilterService = compatibilityDataFilterService;
            _pcConfigurationRepository = pcConfigurationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGraphicCards()
        {
            var graphicCards = await _graphicCardRepository.GetAllAsync();
            if (graphicCards is null || !graphicCards.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<GraphicCardDto>>(graphicCards));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGraphicCardById(int id)
        {
            var graphicCard = await _graphicCardRepository.GetByIdAsync(id);
            if (graphicCard is null)
                return NotFound();
            return Ok(_mapper.Map<GraphicCardDto>(graphicCard));
        }

        [HttpPost("compatible")]
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails)
        {
            try
            {
                var graphicCards = await _graphicCardRepository.GetAllAsync();
                if (graphicCards is null || !graphicCards.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.GraphicCardFilter(configuration, ref graphicCards);

                return Ok(graphicCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGraphicCard([FromBody] GraphicCardDto graphicCard)
        {
            var newGraphicCard = _mapper.Map<GraphicCard>(graphicCard);
            try
            {
                await _graphicCardRepository.Create(newGraphicCard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGraphicCard([FromBody] GraphicCardDto graphicCard)
        {
            var newGraphicCard = _mapper.Map<GraphicCard>(graphicCard);
            try
            {
                await _graphicCardRepository.Update(newGraphicCard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGraphicCard(int id)
        {
            var graphicCard = await _graphicCardRepository.GetByIdAsync(id);
            if (graphicCard is null)
                return NotFound();
            try
            {
                await _graphicCardRepository.Delete(graphicCard);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

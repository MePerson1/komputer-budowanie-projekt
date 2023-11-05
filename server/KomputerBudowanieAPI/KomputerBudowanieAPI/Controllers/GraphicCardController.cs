using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphicCardController : Controller
    {
        public readonly IGenericRepository<GraphicCard> _graphicCardRepository;
        public readonly IMapper _mapper;

        public GraphicCardController(IGenericRepository<GraphicCard> graphicCardRepository, IMapper mapper)
        {
            _graphicCardRepository = graphicCardRepository;
            _mapper = mapper;
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

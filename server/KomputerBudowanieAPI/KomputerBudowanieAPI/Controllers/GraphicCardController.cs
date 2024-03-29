﻿using AutoMapper;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Helpers;
using KomputerBudowanieAPI.Helpers.Request;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/graphic-card")]
    public class GraphicCardController : Controller
    {
        private readonly IPcPartsRepository<GraphicCard> _graphicCardRepository;
        private readonly IMapper _mapper;

        private readonly ICompatibilityDataFilterService _compatibilityDataFilterService;
        private readonly IPcConfigurationRepository _pcConfigurationRepository;

        public GraphicCardController(IPcPartsRepository<GraphicCard> graphicCardRepository, IMapper mapper, ICompatibilityDataFilterService compatibilityDataFilterService, IPcConfigurationRepository pcConfigurationRepository)
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

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAllGraphicCardsPaginate([FromQuery] PartsParams partsParams)
        {
            var graphicCards = await _graphicCardRepository.GetAllAsyncPagination(partsParams);

            if (graphicCards is null || !graphicCards.Any())
            {
                return NotFound();
            }

            Response.AddPaginationHeader(graphicCards.MetaData);
            return Ok(graphicCards);
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpGet("scraper")]
        public async Task<IActionResult> GetAllGraphicCardsScraper()
        {
            var cases = await _graphicCardRepository.GetAllAsync();
            if (cases is null || !cases.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<ProductDto>>(cases));
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
        public async Task<IActionResult> GetCompatible([FromBody] PcConfigurationDto configurationDetails, [FromQuery] PartsParams partsParams)
        {
            try
            {
                var graphicCards = await _graphicCardRepository.GetAllAsyncSortSearch(partsParams);
                if (graphicCards is null || !graphicCards.Any())
                {
                    return NotFound();
                }

                var configuration = new PcConfiguration();
                await _pcConfigurationRepository.GetDataFromIds(configurationDetails, configuration);
                _compatibilityDataFilterService.GraphicCardFilter(configuration, ref graphicCards);

                var paginationGraphicCards = await PagedList<GraphicCard>.ToPagedList(graphicCards, partsParams.PageNumber, partsParams.PageSize);
                Response.AddPaginationHeader(paginationGraphicCards.MetaData);

                return Ok(paginationGraphicCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductDto newPrices)
        {
            try
            {
                if (newPrices == null || newPrices.Prices == null)
                {
                    return BadRequest("Invalid or empty price data.");
                }

                GraphicCard graphicCard = await _graphicCardRepository.GetByIdAsync(newPrices.Id);

                if (graphicCard is null)
                {
                    return BadRequest("Case with this ID does not exist.");
                }

                foreach (var price in newPrices.Prices)
                {
                    var existingPrice = graphicCard.Prices.FirstOrDefault(p => p.Id == price.Id);

                    if (existingPrice != null)
                    {
                        existingPrice.ShopName = price.ShopName;
                        existingPrice.Link = price.Link;
                        existingPrice.Price = price.Price;
                    }
                    else
                    {
                        graphicCard.Prices.Add(price);
                    }
                    await _graphicCardRepository.Update(graphicCard);
                }
                return Ok(graphicCard);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Error updating prices. Please try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(IdentityData.ScraperOrAdminPolicyName)]
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

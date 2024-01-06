using KomputerBudowanieAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KomputerBudowanieAPI.Controllers
{
    [Route("api/shop-price")]
    [ApiController]
    public class ShopPriceController : ControllerBase
    {
        private readonly IShopPriceRepository _shopPriceRepository;

        public ShopPriceController(IShopPriceRepository shopPriceRepository)
        {
            _shopPriceRepository = shopPriceRepository;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShopPrice(int id)
        {
            var shopPrice = await _shopPriceRepository.GetByIdAsync(id);
            if (shopPrice is null)
                return NotFound();
            try
            {
                await _shopPriceRepository.Delete(shopPrice);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

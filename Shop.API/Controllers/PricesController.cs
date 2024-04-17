using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Price;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/products/{productId}/prices")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IPriceHistoryService _priceHistoryService;
        public PricesController(IPriceHistoryService priceHistoryService)
        {
            _priceHistoryService = priceHistoryService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetProductCurrentPrice(int productId)
        {
            return Ok(await _priceHistoryService.GetProductCurrentPrice(productId));
        }

        [HttpGet("price-history")]
        public async Task<IActionResult> GetProductPriceHistory(int productId)
        {
            return Ok(await _priceHistoryService.GetProductPriceHistory(productId));
        }

        [Authorize(Roles = "root")]
        [HttpPut]
        public async Task<IActionResult> UpdateProductPrice(int productId, PriceUpdateDto priceUpdateDto)
        {
            return Ok(await _priceHistoryService.UpdateProductPrice(productId, priceUpdateDto));
        }
    }
}

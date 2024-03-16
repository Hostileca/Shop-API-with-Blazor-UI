using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.CartElement;
using Shop.BL.Services.Interfaces;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/v1/users/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartElementsService _cartElementsService;
        public CartController(ICartElementsService cartElementsService)
        {
            _cartElementsService = cartElementsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCartElement(CartElementCreateDto cartElementCreateDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var cartElement = await _cartElementsService.CreateCartElement(userName, cartElementCreateDto);
            return Ok(cartElement);
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCartElement(int productId)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var cartElement = await _cartElementsService.DeleteCartElementByProductId(userName, productId);
            return Ok(cartElement);
        }

        [HttpPut("{productId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartElement(int productId, CartElementUpdateDto cartElementUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var cartElement = await _cartElementsService.UpdateCartElement(userName, productId, cartElementUpdateDto);
            return Ok(cartElement);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserCart()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var cart = await _cartElementsService.GetUserCart(userName);
            return Ok(cart);
        }
    }
}

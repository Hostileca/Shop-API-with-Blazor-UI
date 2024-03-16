using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Services.Interfaces;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _ordersService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _ordersService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var order = await _ordersService.CreateOrder(userName);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _ordersService.DeleteOrder(id);
            return Ok(order);
        }
    }
}

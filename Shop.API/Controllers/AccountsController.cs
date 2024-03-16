using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.User;
using Shop.BL.Services.Interfaces;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/v1/users/")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly IOrdersService _ordersService;
        private readonly IBuyerCardsService _buyerCardsService;

        public AccountsController(IAccountService userService, IOrdersService ordersService, IBuyerCardsService buyerCardsService)
        {
            _userService = userService;
            _ordersService = ordersService;
            _buyerCardsService = buyerCardsService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            return Ok(await _userService.RegisterUser(userRegisterDto));
        }

        [HttpPost("authorization")]
        public async Task<IActionResult> Login(UserRegisterDto userRegisterDto)
        {
            return Ok(await _userService.Login(userRegisterDto));
        }

        [HttpGet("orders")]
        [Authorize]
        public async Task<IActionResult> GetUserOrders()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var orders = await _ordersService.GetUserOrders(userName);
            return Ok(orders);
        }

        [HttpPost("bind-card")]
        [Authorize]
        public async Task<IActionResult> BindCard(BindCardDto bindCardDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var buyerCard = await _buyerCardsService.BindBuyerCard(userName, bindCardDto);
            return Ok(buyerCard);
        }

        [HttpGet("buyer-cards")]
        [Authorize]
        public async Task<IActionResult> GetUserCard()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var buyerCard = await _buyerCardsService.GetBuyerCardByUsername(userName);
            return Ok(buyerCard);
        }

        [HttpDelete("unbind-card")]
        [Authorize]
        public async Task<IActionResult> UnbindCard()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var buyerCard = await _buyerCardsService.UnbindCard(userName);
            return Ok(buyerCard);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateSettings(UserUpdateDto userUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userService.UpdateUser(userName, userUpdateDto);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userService.GetUser(userName);
            return Ok(user);
        }
    }
}

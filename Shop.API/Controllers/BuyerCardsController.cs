using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.BuyerCard;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/buyer-cards")]
    [ApiController]
    public class BuyerCardsController : ControllerBase
    {
        private readonly IBuyerCardsService _buyerCardsService;
        public BuyerCardsController(IBuyerCardsService buyerCardsService)
        {
            _buyerCardsService = buyerCardsService;
        }

        [HttpGet]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> GetAllBuyerCards()
        {
            var buyerCards = await _buyerCardsService.GetAllBuyerCards();
            return Ok(buyerCards);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> GetBuyerCardById(int id)
        {
            var order = await _buyerCardsService.GetBuyerCardById(id);
            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> CreateBuyerCard(BuyerCardCreateDto buyerCardCreateDto)
        {
            var buyerCard = await _buyerCardsService.CreateBuyerCard(buyerCardCreateDto);
            return Ok(buyerCard);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "root")]
        public async Task<IActionResult> DeleteBuyerCard(int id)
        {
            var buyerCard = await _buyerCardsService.DeleteBuyerCard(id);
            return Ok(buyerCard);
        }


    }
}

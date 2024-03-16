using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Review;
using Shop.BL.Services.Interfaces;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("{productId}/reviews")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            var reviews = await _reviewsService.GetProductReviews(productId);
            return Ok(reviews);
        }

        [Authorize(Roles = "root")]
        [HttpGet("users/{userName}/reviews")]
        public async Task<IActionResult> GetUserReviews(string userName)
        {
            var reviews = await _reviewsService.GetUserReviews(userName);
            return Ok(reviews);
        }

        [Authorize]
        [HttpPost("products/{productId}/reviews")]
        public async Task<IActionResult> CreateReview(int productId, ReviewCreateDto reviewCreateDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var reviews = await _reviewsService.CreateReview(userName, productId, reviewCreateDto);
            return Ok(reviews);
        }

        [Authorize]
        [HttpPut("reviews/{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, ReviewUpdateDto reviewUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var reviews = await _reviewsService.UpdateReview(reviewId, userName, reviewUpdateDto);
            return Ok(reviews);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var reviews = await _reviewsService.DeleteReview(reviewId);
            return Ok(reviews);
        }
    }
}

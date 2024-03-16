using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.BL.Dtos.Review;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepo _reviewsRepo;
        private readonly UserManager<User> _userManager;
        private readonly IProductsRepo _productsRepo;
        private readonly IMapper _mapper;

        public ReviewsService(IReviewsRepo reviewsRepo, UserManager<User> userManager, IProductsRepo productsRepo, IMapper mapper)
        {
            _reviewsRepo = reviewsRepo;
            _userManager = userManager;
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        public async Task<ReviewReadDto> CreateReview(string userName, int productId, ReviewCreateDto reviewCreateDto)
        {
            var review = _mapper.Map<Review>(reviewCreateDto);
            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser is null)
            {
                throw new KeyNotFoundException($"User not found with user name:{userName}");
            }
            review.User = existingUser;

            var existingProduct = await _productsRepo.GetProductById(productId);
            if (existingProduct is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }
            review.Product = existingProduct;
            review.Date = DateTime.Now;

            await _reviewsRepo.AddReview(review);
            await _reviewsRepo.SaveChanges();
            return _mapper.Map<ReviewReadDto>(review);
        }

        public async Task<ReviewReadDto> DeleteReview(int id)
        {
            var reviewFromRepo = await _reviewsRepo.GetReviewById(id);
            if (reviewFromRepo is null)
            {
                throw new KeyNotFoundException($"Review not found with id:{id}");
            }
            _reviewsRepo.DeleteReview(reviewFromRepo);
            await _reviewsRepo.SaveChanges();
            return _mapper.Map<ReviewReadDto>(reviewFromRepo);
        }

        public async Task<IEnumerable<ReviewReadDto>> GetProductReviews(int productId)
        {
            var reviews = await _reviewsRepo.GetProductReviews(productId);
            return _mapper.Map<IEnumerable<ReviewReadDto>>(reviews);
        }

        public async Task<IEnumerable<ReviewReadDto>> GetUserReviews(string userName)
        {
            var reviews = await _reviewsRepo.GetUserReviews(userName);
            return _mapper.Map<IEnumerable<ReviewReadDto>>(reviews);
        }

        public async Task<ReviewReadDto> UpdateReview(int id, string userName, ReviewUpdateDto reviewUpdateDto)
        {
            var reviewFromRepo = await _reviewsRepo.GetReviewById(id);
            if (reviewFromRepo is null)
            {
                throw new KeyNotFoundException($"Review not found with id:{id}");
            }
            if (reviewFromRepo.User.UserName != userName)
            {
                throw new UnauthorizedAccessException("You cannot change other people's reviews");
            }
            var reviewToRepo = _mapper.Map(reviewUpdateDto, reviewFromRepo);
            await _reviewsRepo.SaveChanges();
            return _mapper.Map<ReviewReadDto>(reviewToRepo);
        }
    }
}

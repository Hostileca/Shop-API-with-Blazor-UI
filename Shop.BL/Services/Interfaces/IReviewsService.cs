using Shop.BL.Dtos.Review;

namespace Shop.BL.Services.Interfaces
{
    public interface IReviewsService
    {
        Task<IEnumerable<ReviewReadDto>> GetProductReviews(int productId);
        Task<IEnumerable<ReviewReadDto>> GetUserReviews(string userName);
        Task<ReviewReadDto> CreateReview(string userName, int productId, ReviewCreateDto reviewCreateDto);
        Task<ReviewReadDto> DeleteReview(int id);
        Task<ReviewReadDto> UpdateReview(int id, string userName, ReviewUpdateDto reviewUpdateDto);
    }
}

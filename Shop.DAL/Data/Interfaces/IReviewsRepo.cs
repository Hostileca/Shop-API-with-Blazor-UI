using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IReviewsRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Review>> GetProductReviews(int productId);
        Task<IEnumerable<Review>> GetUserReviews(string userName);
        Task AddReview(Review review);
        void DeleteReview(Review review);
        Task<Review> GetReviewById(int id);
    }
}

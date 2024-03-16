using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class ReviewsRepo : IReviewsRepo
    {
        private readonly AppDbContext _dbContext;
        public ReviewsRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddReview(Review review)
        {
            await _dbContext.AddAsync(review);
        }

        public void DeleteReview(Review review)
        {
            _dbContext.Remove(review);
        }

        public async Task<IEnumerable<Review>> GetProductReviews(int productId)
        {
            return await _dbContext.Reviews.Where(r => r.Product.Id == productId)
                                           .Include(r => r.User)
                                           .Include(r => r.Product)
                                           .ToListAsync();
        }

        public async Task<Review> GetReviewById(int id)
        {
            return await _dbContext.Reviews.Include(r => r.User)
                                           .Include(r => r.Product)
                                           .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<IEnumerable<Review>> GetUserReviews(string userName)
        {
            return await _dbContext.Reviews.Where(r => r.User.UserName == userName)
                                           .Include(r => r.User)
                                           .Include(r => r.Product)
                                           .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

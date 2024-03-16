using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class PriceHistoryRepo : IPriceHistoryRepo
    {
        private readonly AppDbContext _dbContext;
        public PriceHistoryRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddProductPrice(Price price)
        {
            await _dbContext.PricesHistory.AddAsync(price);
        }

        public Task<Price> GetCurrentPrice(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetProductPriceHistory(int productId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;
using System.Diagnostics;

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

        public async Task<Price> GetCurrentPrice(int productId)
        {
            return await _dbContext.PricesHistory.Include(p => p.Product).FirstOrDefaultAsync(p => p.Product.Id == productId && p.EndDate == null);
        }

        public async Task<IEnumerable<Price>> GetProductPriceHistory(int productId)
        {
            return await _dbContext.PricesHistory.Include(p => p.Product).Where(p => p.Product.Id == productId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

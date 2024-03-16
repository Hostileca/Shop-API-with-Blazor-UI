using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly AppDbContext _dbContext;
        public ProductsRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.Include(p => p.Reviews).ThenInclude(r => r.User)
                                            .Include(p => p.PriceHistory)
                                            .Include(p => p.Category)
                                            .Include(p => p.OrderProduct)
                                            .Include(p => p.Manufacturer).ThenInclude(m => m.Image)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products.Include(p => p.Reviews).ThenInclude(r => r.User)
                                            .Include(p => p.PriceHistory)
                                            .Include(p => p.Category)
                                            .Include(p => p.OrderProduct)
                                            .Include(p => p.Manufacturer).ThenInclude(m => m.Image)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

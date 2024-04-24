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
                                            .Include(p => p.Manufacturer)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products.Include(p => p.Reviews).ThenInclude(r => r.User)
                                            .Include(p => p.PriceHistory)
                                            .Include(p => p.Category).ThenInclude(c => c.Image)
                                            .Include(p => p.OrderProduct)
                                            .Include(p => p.Manufacturer).ThenInclude(m => m.Image)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetTop10Products()
        {
            var topProducts = await _dbContext.Products
                                            .Include(p => p.Reviews).ThenInclude(r => r.User)
                                            .Include(p => p.PriceHistory)
                                            .Include(p => p.Category)
                                            .Include(p => p.OrderProduct)
                                            .Include(p => p.Manufacturer)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .Where(p => p.Reviews.Any()) 
                                            .OrderByDescending(p => p.Reviews.Average(r => r.Rating) * Math.Log(p.Reviews.Count)) 
                                            .Take(10)
                                            .ToListAsync();
            return topProducts;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> SearchByName(string searchName)
        {
            var searchText = searchName.ToLower();
            var products = await _dbContext.Products
            .Where(p => p.Name.ToLower().Contains(searchText))
                .Include(p => p.Reviews).ThenInclude(r => r.User)
                                            .Include(p => p.PriceHistory)
                                            .Include(p => p.Category)
                                            .Include(p => p.OrderProduct)
                                            .Include(p => p.Manufacturer).ThenInclude(m => m.Image)
                                            .Include(p => p.ProductAttributes).ThenInclude(pa => pa.Attribute)
                                            .Include(p => p.Images)
                                            .ToListAsync();
            return products;
        }
    }
}

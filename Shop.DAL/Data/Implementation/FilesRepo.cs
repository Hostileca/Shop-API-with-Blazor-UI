using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models.Files;

namespace Shop.DAL.Data.Implementation
{
    public class FilesRepo : IFilesRepo
    {
        private readonly AppDbContext _dbContext;
        public FilesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCategoryImage(CategoryImage categoryImage)
        {
            await _dbContext.CategoriesImages.AddAsync(categoryImage);
        }

        public async Task AddManufactuerImage(ManufacturerImage manufacturerImage)
        {
            await _dbContext.ManufacturerImages.AddAsync(manufacturerImage);
        }

        public async Task AddProductImage(ProductImage productImage)
        {
            await _dbContext.ProductsImages.AddAsync(productImage);
        }

        public void DeleteCategoryImage(CategoryImage categoryImage)
        {
            _dbContext.CategoriesImages.Remove(categoryImage);
        }

        public void DeleteManufacturerImage(ManufacturerImage manufacturerImage)
        {
            _dbContext.ManufacturerImages.Remove(manufacturerImage);
        }

        public void DeleteProductImage(ProductImage productImage)
        {
            _dbContext.ProductsImages.Remove(productImage);
        }

        public async Task<CategoryImage> GetCategoryImageById(int categoryImageId)
        {
            return await _dbContext.CategoriesImages.FirstOrDefaultAsync(ci => ci.Id == categoryImageId);
        }

        public async Task<ManufacturerImage> GetManufacturerImageById(int manufacturerImageId)
        {
            return await _dbContext.ManufacturerImages.FirstOrDefaultAsync(mi => mi.Id == manufacturerImageId);
        }

        public async Task<ProductImage> GetProductImageById(int productImageId)
        {
            return await _dbContext.ProductsImages.FirstOrDefaultAsync(pi => pi.Id == productImageId);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

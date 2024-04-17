using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly AppDbContext _dbContext;
        public CategoriesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public void DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _dbContext.Categories.Include(c => c.Products)
                                             .Include(c => c.Image)
                                              .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _dbContext.Categories.Include(c => c.Products)
                                              .Include(c => c.Image)
                                              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _dbContext.Categories.Include(c => c.Products)
                                              .Include(c => c.Image)
                                               .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> SearchByName(string searchName)
        {
            var searchText = searchName.ToLower();
            var categories = await _dbContext.Categories
            .Where(p => p.Name.ToLower().Contains(searchText))
                .Include(c => c.Products)
                .Include(c => c.Image)
            .ToListAsync();
            return categories;
        }
    }
}

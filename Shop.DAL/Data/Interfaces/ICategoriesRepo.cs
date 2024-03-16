using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface ICategoriesRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Category>> GetAllCategories();
        Task AddCategory(Category category);
        void DeleteCategory(Category category);
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
    }
}

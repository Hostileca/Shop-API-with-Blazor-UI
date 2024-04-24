using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IProductsRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetTop10Products();
        Task AddProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> SearchByName(string searchName);
    }
}

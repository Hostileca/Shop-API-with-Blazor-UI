using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface ICartElementsRepo
    {
        Task SaveChanges();
        Task<IEnumerable<CartElement>> GetUserCart(string userName);
        Task<CartElement> GetUserCartById(string username, int productId);
        Task AddCartElement(CartElement cartElement);
        void DeleteCartElement(CartElement cartElement);
        Task ClearUserCart(string userName);
    }
}

using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IPriceHistoryRepo
    {
        Task<Price> GetCurrentPrice(int productId);
        Task<IEnumerable<Price>> GetProductPriceHistory(int productId);
        Task AddProductPrice(Price price);
        Task SaveChanges();
    }
}

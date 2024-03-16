using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IOrdersRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetUserOrders(string userName);
        Task AddOrder(Order order);
        void DeleteOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}

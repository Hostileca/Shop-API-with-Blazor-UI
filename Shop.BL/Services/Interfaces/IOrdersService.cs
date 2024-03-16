using Shop.BL.Dtos.Order;

namespace Shop.BL.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderReadDto>> GetAllOrders();
        Task<IEnumerable<OrderReadDto>> GetUserOrders(string userName);
        Task<OrderReadDto> GetOrderById(int id);
        Task<OrderReadDto> CreateOrder(string userName);
        Task<OrderReadDto> DeleteOrder(int id);
    }
}

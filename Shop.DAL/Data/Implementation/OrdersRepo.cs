using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly AppDbContext _dbContext;
        public OrdersRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public void DeleteOrder(Order order)
        {
            _dbContext.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.Include(o => o.User)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Manufacturer)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Category)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.PriceHistory)
                                           .ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.Include(o => o.User)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Manufacturer)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Category)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.PriceHistory)
                                           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userName)
        {
            return await _dbContext.Orders.Include(o => o.User)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Manufacturer)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.Category)
                                           .Include(o => o.OrderProduct).ThenInclude(op => op.Product).ThenInclude(p => p.PriceHistory)
                                           .Where(o => o.User.UserName == userName).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

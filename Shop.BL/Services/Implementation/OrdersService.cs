using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.BL.Dtos.Order;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepo _ordersRepo;
        private readonly IProductsRepo _productsRepo;
        private readonly ICartElementsRepo _cartElementsRepo;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public OrdersService(IOrdersRepo ordersRepo, IProductsRepo productsRepo, UserManager<User> userManager, ICartElementsRepo cartElementsRepo, IMapper mapper)
        {
            _ordersRepo = ordersRepo;
            _productsRepo = productsRepo;
            _cartElementsRepo = cartElementsRepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> CreateOrder(string userName)
        {
            var cartElements = await _cartElementsRepo.GetUserCart(userName);

            var order = new Order();
            order.OrderProduct = new List<OrderProduct>();
            foreach (var cartElement in cartElements)
            {
                order.OrderProduct.Add(new OrderProduct()
                {
                    Amount = cartElement.Amount,
                    ProductId = cartElement.ProductId,
                });
            }

            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                order.User = existingUser;
            }
            order.Date = DateTime.Now;
            await _ordersRepo.AddOrder(order);
            await _cartElementsRepo.ClearUserCart(userName);
            await _ordersRepo.SaveChanges();
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> DeleteOrder(int id)
        {
            var order = await _ordersRepo.GetOrderById(id);
            if (order is null)
            {
                throw new KeyNotFoundException($"Order not found with id:{id}");
            }
            _ordersRepo.DeleteOrder(order);
            await _ordersRepo.SaveChanges();
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrders()
        {
            var orders = await _ordersRepo.GetAllOrders();
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderById(int id)
        {
            var order = await _ordersRepo.GetOrderById(id);
            if (order is null)
            {
                throw new KeyNotFoundException($"Order not found with id:{id}");
            }
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetUserOrders(string userName)
        {
            var orders = await _ordersRepo.GetUserOrders(userName);
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }
    }
}

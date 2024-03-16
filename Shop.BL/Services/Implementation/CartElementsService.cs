using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.BL.Dtos.CartElement;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class CartElementsService : ICartElementsService
    {
        private readonly ICartElementsRepo _cartElementsRepo;
        private readonly IProductsRepo _productsRepo;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public CartElementsService(ICartElementsRepo cartElementsRepo, IProductsRepo productsRepo, UserManager<User> userManager, IMapper mapper)
        {
            _cartElementsRepo = cartElementsRepo;
            _productsRepo = productsRepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CartElementReadDto> CreateCartElement(string userName, CartElementCreateDto cartElementCreateDto)
        {
            if (cartElementCreateDto.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            var cartElement = _mapper.Map<CartElement>(cartElementCreateDto);

            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                cartElement.User = existingUser;
            }
            await _cartElementsRepo.AddCartElement(cartElement);
            await _cartElementsRepo.SaveChanges();

            return _mapper.Map<CartElementReadDto>(cartElement);
        }

        public async Task<CartElementReadDto> DeleteCartElementByProductId(string userName, int productId)
        {
            var cartElement = await _cartElementsRepo.GetUserCartById(userName, productId);
            if (cartElement == null)
            {
                throw new KeyNotFoundException($"Cart element not found with product id:{productId}");
            }
            _cartElementsRepo.DeleteCartElement(cartElement);
            await _cartElementsRepo.SaveChanges();
            return _mapper.Map<CartElementReadDto>(cartElement);
        }

        public async Task<IEnumerable<CartElementReadDto>> GetUserCart(string userName)
        {
            var cartElements = await _cartElementsRepo.GetUserCart(userName);
            return _mapper.Map<IEnumerable<CartElementReadDto>>(cartElements);
        }

        public async Task<CartElementReadDto> UpdateCartElement(string userName, int productId, CartElementUpdateDto cartElementUpdateDto)
        {
            if (cartElementUpdateDto.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            var cartElement = await _cartElementsRepo.GetUserCartById(userName, productId);
            if (cartElement == null)
            {
                throw new KeyNotFoundException($"Cart element not found with product id:{productId}");
            }
            cartElement.Amount = cartElementUpdateDto.Amount;
            await _cartElementsRepo.SaveChanges();
            return _mapper.Map<CartElementReadDto>(cartElement);
        }
    }
}

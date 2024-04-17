using AutoMapper;
using Shop.BL.Dtos.Price;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IPriceHistoryRepo _priceHistoryRepo;
        private readonly IMapper _mapper;
        public PriceHistoryService(IPriceHistoryRepo priceHistoryRepo, IMapper mapper)
        {
            _priceHistoryRepo = priceHistoryRepo;
            _mapper = mapper;
        }

        public async Task<PriceReadDto> GetProductCurrentPrice(int productId)
        {
            var price = await _priceHistoryRepo.GetCurrentPrice(productId);
            return _mapper.Map<PriceReadDto>(price);
        }

        public async Task<IEnumerable<PriceReadDto>> GetProductPriceHistory(int productId)
        {
            var priceHistory = await _priceHistoryRepo.GetProductPriceHistory(productId);
            return _mapper.Map<IEnumerable<PriceReadDto>>(priceHistory);
        }

        public async Task<PriceReadDto> UpdateProductPrice(int productId, PriceUpdateDto priceUpdateDto)
        {
            var price = await _priceHistoryRepo.GetCurrentPrice(productId);
            price.EndDate = DateTime.UtcNow;
            var newPrice = _mapper.Map<Price>(priceUpdateDto);
            newPrice.StartDate = DateTime.Now;
            newPrice.Product = price.Product;
            await _priceHistoryRepo.AddProductPrice(newPrice);
            await _priceHistoryRepo.SaveChanges();
            return _mapper.Map<PriceReadDto>(newPrice);
        }
    }
}

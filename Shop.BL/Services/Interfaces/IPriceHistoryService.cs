using Shop.BL.Dtos.Price;

namespace Shop.BL.Services.Interfaces
{
    public interface IPriceHistoryService
    {
        Task<PriceReadDto> GetProductCurrentPrice(int productId);
        Task<IEnumerable<PriceReadDto>> GetProductPriceHistory(int productId);
        Task<PriceReadDto> UpdateProductPrice(int productId, PriceUpdateDto priceUpdateDto);
    }
}

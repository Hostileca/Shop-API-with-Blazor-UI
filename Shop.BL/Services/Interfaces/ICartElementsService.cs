using Shop.BL.Dtos.CartElement;

namespace Shop.BL.Services.Interfaces
{
    public interface ICartElementsService
    {
        Task<IEnumerable<CartElementReadDto>> GetUserCart(string userName);
        Task<CartElementReadDto> CreateCartElement(string userName, CartElementCreateDto cartElementCreateDto);
        Task<CartElementReadDto> UpdateCartElement(string userName, int productId, CartElementUpdateDto cartElementUpdateDto);
        Task<CartElementReadDto> DeleteCartElementByProductId(string userName, int productId);
    }
}

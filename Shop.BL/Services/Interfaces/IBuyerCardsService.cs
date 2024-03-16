using Shop.BL.Dtos.BuyerCard;
using Shop.BL.Dtos.User;

namespace Shop.BL.Services.Interfaces
{
    public interface IBuyerCardsService
    {
        Task<IEnumerable<BuyerCardReadDto>> GetAllBuyerCards();
        Task<BuyerCardReadDto> GetBuyerCardById(int id);
        Task<BuyerCardReadDto> GetBuyerCardByUsername(string userName);
        Task<BuyerCardReadDto> CreateBuyerCard(BuyerCardCreateDto buyerCardCreateDto);
        Task<BuyerCardReadDto> BindBuyerCard(string userName, BindCardDto bindCardDto);
        Task<BuyerCardReadDto> UnbindCard(string userName);
        Task<BuyerCardReadDto> DeleteBuyerCard(int id);
    }
}

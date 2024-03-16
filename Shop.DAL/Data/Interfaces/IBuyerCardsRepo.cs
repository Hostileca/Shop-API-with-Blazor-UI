using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IBuyerCardsRepo
    {
        Task SaveChanges();
        Task<IEnumerable<BuyerCard>> GetAllBuyerCards();
        Task AddBuyerCard(BuyerCard buyerCard);
        void DeleteBuyerCard(BuyerCard buyerCard);
        Task<BuyerCard> GetBuyerCardById(int id);
        Task<BuyerCard> GetBuyerCardByPhoneNumber(string phoneNumber);
        Task<BuyerCard> GetBuyerCardByUsername(string username);
    }
}

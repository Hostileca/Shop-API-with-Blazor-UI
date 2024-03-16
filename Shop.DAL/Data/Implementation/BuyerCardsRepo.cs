using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class BuyerCardsRepo : IBuyerCardsRepo
    {
        private readonly AppDbContext _dbContext;
        public BuyerCardsRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBuyerCard(BuyerCard buyerCard)
        {
            await _dbContext.BuyersCards.AddAsync(buyerCard);
        }

        public void DeleteBuyerCard(BuyerCard buyerCard)
        {
            _dbContext.BuyersCards.Remove(buyerCard);
        }

        public async Task<IEnumerable<BuyerCard>> GetAllBuyerCards()
        {
            return await _dbContext.BuyersCards.Include(bc => bc.User)
                                               .ToListAsync();
        }

        public async Task<BuyerCard> GetBuyerCardById(int id)
        {
            return await _dbContext.BuyersCards.Include(bc => bc.User)
                                               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BuyerCard> GetBuyerCardByPhoneNumber(string phoneNumber)
        {
            return await _dbContext.BuyersCards.Include(bc => bc.User)
                                               .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<BuyerCard> GetBuyerCardByUsername(string username)
        {
            return await _dbContext.BuyersCards.Include(bc => bc.User)
                                               .FirstOrDefaultAsync(p => p.User.UserName == username);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

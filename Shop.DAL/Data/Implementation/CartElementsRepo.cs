using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class CartElementsRepo : ICartElementsRepo
    {
        private readonly AppDbContext _dbContext;
        public CartElementsRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCartElement(CartElement cartElement)
        {
            await _dbContext.CartElements.AddAsync(cartElement);
        }

        public async Task ClearUserCart(string userName)
        {
            var cart = await _dbContext.CartElements
                                                .Where(ce => ce.User.UserName == userName)
                                                .ToListAsync();
            foreach (var cartElement in cart)
            {
                _dbContext.CartElements.Remove(cartElement);
            }
        }

        public void DeleteCartElement(CartElement cartElement)
        {
            _dbContext.CartElements.Remove(cartElement);
        }

        public async Task<IEnumerable<CartElement>> GetUserCart(string userName)
        {
            return await _dbContext.CartElements.Include(ce => ce.Product).ThenInclude(p => p.Images)
                                                .Include(ce => ce.Product).ThenInclude(p => p.Manufacturer)
                                                .Include(ce => ce.Product).ThenInclude(p => p.Category)
                                                .Include(ce => ce.Product).ThenInclude(p => p.PriceHistory)
                                                .Include(ce => ce.User)
                                                .Where(ce => ce.User.UserName == userName)
                                                .ToListAsync();
        }

        public async Task<CartElement> GetUserCartById(string userName, int productId)
        {
            return await _dbContext.CartElements.Include(ce => ce.Product).ThenInclude(p => p.Images)
                                                .Include(ce => ce.Product).ThenInclude(p => p.Manufacturer)
                                                .Include(ce => ce.Product).ThenInclude(p => p.Category)
                                                .Include(ce => ce.Product).ThenInclude(p => p.PriceHistory)
                                                .Include(ce => ce.User)
                                                .FirstOrDefaultAsync(ce => ce.User.UserName == userName && ce.Product.Id == productId);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

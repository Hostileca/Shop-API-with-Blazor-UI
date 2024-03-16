using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;

namespace Shop.DAL.Data.Implementation
{
    public class AttributesRepo : IAttributesRepo
    {
        private readonly AppDbContext _dbContext;
        public AttributesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAttribute(Models.Attribute attribute)
        {
            await _dbContext.Attributes.AddAsync(attribute);
        }

        public void DeleteAttribute(Models.Attribute attribute)
        {
            _dbContext.Attributes.Remove(attribute);
        }

        public async Task<IEnumerable<Models.Attribute>> GetAllAttributes()
        {
            return await _dbContext.Attributes.Include(a => a.ProductAttributes).ToListAsync();
        }

        public async Task<Models.Attribute> GetAttributeById(int id)
        {
            return await _dbContext.Attributes.Include(a => a.ProductAttributes)
                                              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Models.Attribute> GetAttributeByName(string name)
        {
            return await _dbContext.Attributes.Include(a => a.ProductAttributes)
                                              .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

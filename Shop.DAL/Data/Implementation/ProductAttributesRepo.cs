using Microsoft.EntityFrameworkCore;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.DAL.Data.Implementation
{
    public class ProductAttributesRepo : IProductAttributesRepo
    {
        private readonly AppDbContext _dbContext;
        public ProductAttributesRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAttributeToProduct(ProductAttribute productAttribute)
        {
            var existingAttribute = await _dbContext.Attributes.SingleOrDefaultAsync(a => a.Name == productAttribute.Attribute.Name);

            productAttribute.Attribute = existingAttribute ?? productAttribute.Attribute;
            await _dbContext.ProductsAttrubutes.AddAsync(productAttribute);
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllProductAttributes()
        {
            return await _dbContext.ProductsAttrubutes.Include(a => a.Product)
                                                      .Include(a => a.Attribute)
                                                      .ToListAsync();
        }

        public void DeleteProductAttribute(ProductAttribute productAttribute)
        {
            _dbContext.ProductsAttrubutes.Remove(productAttribute);

            if (productAttribute.Attribute.ProductAttributes.Count <= 1)
            {
                _dbContext.Attributes.Remove(productAttribute.Attribute);
            }
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllProductAttributesByProductId(int productId)
        {
            return await _dbContext.ProductsAttrubutes.Include(a => a.Product)
                                                      .Include(a => a.Attribute)
                                                      .Where(pa => pa.ProductId == productId)
                                                      .ToListAsync();
        }

        public async Task<ProductAttribute> GetProductAttributeByIds(int productId, int attributeId)
        {
            return await _dbContext.ProductsAttrubutes.FirstOrDefaultAsync(pa => pa.ProductId == productId
                                                                                    && pa.AttributeId == attributeId);
        }
    }
}

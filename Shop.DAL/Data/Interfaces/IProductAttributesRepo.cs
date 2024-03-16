using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IProductAttributesRepo
    {
        Task SaveChanges();
        Task<IEnumerable<ProductAttribute>> GetAllProductAttributes();
        Task<IEnumerable<ProductAttribute>> GetAllProductAttributesByProductId(int productId);
        Task<ProductAttribute> GetProductAttributeByIds(int productId, int attributeId);
        Task AddAttributeToProduct(ProductAttribute productAttribute);
        void DeleteProductAttribute(ProductAttribute productAttribute);
    }
}

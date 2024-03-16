using Shop.BL.Dtos.Attribute;

namespace Shop.BL.Services.Interfaces
{
    public interface IProductAttributesService
    {
        Task<IEnumerable<ProductAttributeReadDto>> GetAllProductAttributes(int productId);
        Task<ProductAttributeReadDto> CreateProductAttribute(int productId, AttributeCreateDto attributeCreateDto);
        Task<ProductAttributeReadDto> DeleteProductAttribute(int productId, int id);
        Task<ProductAttributeReadDto> UpdateProductAttribute(int productId, int id, ProductAttributeUpdateDto attributeUpdateDto);
    }
}

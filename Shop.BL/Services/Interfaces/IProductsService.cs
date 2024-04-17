using Shop.BL.Dtos.Product;

namespace Shop.BL.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProducts();
        Task<ProductDetailedReadDto> GetProductById(int id);
        Task<ProductDetailedReadDto> CreateProduct(ProductCreateDto productCreateDto);
        Task<ProductDetailedReadDto> DeleteProduct(int id);
        Task<ProductDetailedReadDto> UpdateProduct(int productId, ProductUpdateDto productUpdateDto);
        Task<IEnumerable<ProductReadDto>> SearchProducts(string searchText);
    }
}

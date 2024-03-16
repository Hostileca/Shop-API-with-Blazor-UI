using Shop.DAL.Models.Files;

namespace Shop.DAL.Data.Interfaces
{
    public interface IFilesRepo
    {
        Task<ProductImage> GetProductImageById(int productImageId);
        Task<CategoryImage> GetCategoryImageById(int categoryImageId);
        Task<ManufacturerImage> GetManufacturerImageById(int manufacturerImageId);
        Task AddProductImage(ProductImage image);
        Task AddCategoryImage(CategoryImage categoryImage);
        Task AddManufactuerImage(ManufacturerImage manufacturerImage);

        void DeleteProductImage(ProductImage productImage);
        void DeleteCategoryImage(CategoryImage categoryImage);
        void DeleteManufacturerImage(ManufacturerImage manufacturerImage);
        Task SaveChanges();

    }
}

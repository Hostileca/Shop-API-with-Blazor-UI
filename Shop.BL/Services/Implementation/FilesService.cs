using Microsoft.AspNetCore.Http;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models.Files;

namespace Shop.BL.Services.Implementation
{
    public class FilesService : IFilesService
    {
        private readonly IFilesRepo _filesRepo;
        private readonly IProductsRepo _productsRepo;
        private readonly ICategoriesRepo _categoryRepo;
        private readonly IManufacturersRepo _manufacturersRepo;

        private const string _productsDirectoryPath = "files/images/products";
        private const string _categoriesDirectoryPath = "files/images/categories";
        private const string _manufacturersDirectoryPath = "files/images/manufacturers";
        public FilesService(IFilesRepo filesRepo, IProductsRepo productsRepo, ICategoriesRepo categoryRepo,
            IManufacturersRepo manufacturersRepo)
        {
            _filesRepo = filesRepo;
            _productsRepo = productsRepo;
            _categoryRepo = categoryRepo;
            _manufacturersRepo = manufacturersRepo;

            Directory.CreateDirectory(_productsDirectoryPath);
            Directory.CreateDirectory(_categoriesDirectoryPath);
            Directory.CreateDirectory(_manufacturersDirectoryPath);
        }

        private async Task UploadFile(string filePath, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new FileLoadException("Invalid file");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task<byte[]> GetProductImageById(int imageId)
        {
            var productImage = await _filesRepo.GetProductImageById(imageId);
            if (productImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            byte[] fileBytes = await File.ReadAllBytesAsync(productImage.FilePath);
            return fileBytes;
        }

        public async Task<string> UploadProductImage(int productId, IFormFile file)
        {
            var product = await _productsRepo.GetProductById(productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_productsDirectoryPath, fileName);
            await UploadFile(filePath, file);
            await _filesRepo.AddProductImage(new ProductImage
            {
                Product = product,
                FilePath = filePath
            });
            await _filesRepo.SaveChanges();
            return filePath;
        }

        public async Task<string> UploadCategoryImage(int categoryId, IFormFile file)
        {
            var category = await _categoryRepo.GetCategoryById(categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category not found with id:{categoryId}");
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_categoriesDirectoryPath, fileName);
            await UploadFile(filePath, file);
            await _filesRepo.AddCategoryImage(new CategoryImage
            {
                Category = category,
                FilePath = filePath
            });
            await _filesRepo.SaveChanges();
            return filePath;
        }

        public async Task<string> UploadManufacturerImage(int manufacturerId, IFormFile file)
        {
            var manufacturer = await _manufacturersRepo.GetManufacturerById(manufacturerId);

            if (manufacturer == null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{manufacturerId}");
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_manufacturersDirectoryPath, fileName);
            await UploadFile(filePath, file);
            await _filesRepo.AddManufactuerImage(new ManufacturerImage
            {
                Manufacturer = manufacturer,
                FilePath = filePath
            });
            await _filesRepo.SaveChanges();
            return filePath;
        }

        public async Task<byte[]> GetCategoryImageById(int imageId)
        {
            var categoryImage = await _filesRepo.GetCategoryImageById(imageId);
            if (categoryImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            byte[] fileBytes = await File.ReadAllBytesAsync(categoryImage.FilePath);
            return fileBytes;
        }

        public async Task<byte[]> GetManufactuerImageById(int imageId)
        {
            var manufacturerImage = await _filesRepo.GetManufacturerImageById(imageId);
            if (manufacturerImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            byte[] fileBytes = await File.ReadAllBytesAsync(manufacturerImage.FilePath);
            return fileBytes;
        }

        public async Task<int> DeleteProductImage(int imageId)
        {
            var productImage = await _filesRepo.GetProductImageById(imageId);
            if (productImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            _filesRepo.DeleteProductImage(productImage);
            await _filesRepo.SaveChanges();
            File.Delete(productImage.FilePath);
            return imageId;
        }

        public async Task<int> DeleteCategoryImage(int imageId)
        {
            var categoryImage = await _filesRepo.GetCategoryImageById(imageId);
            if (categoryImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            _filesRepo.DeleteCategoryImage(categoryImage);
            await _filesRepo.SaveChanges();
            File.Delete(categoryImage.FilePath);
            return imageId;
        }

        public async Task<int> DeleteManufacturerImage(int imageId)
        {
            var manufacturerImage = await _filesRepo.GetManufacturerImageById(imageId);
            if (manufacturerImage == null)
            {
                throw new KeyNotFoundException($"Image not found with id:{imageId}");
            }
            _filesRepo.DeleteManufacturerImage(manufacturerImage);
            await _filesRepo.SaveChanges();
            File.Delete(manufacturerImage.FilePath);
            return imageId;
        }
    }
}

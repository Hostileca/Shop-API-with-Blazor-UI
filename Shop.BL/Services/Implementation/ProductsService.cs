using AutoMapper;
using Shop.BL.Dtos.Product;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;

namespace Shop.BL.Services.Implementation
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepo _productRepo;
        private readonly ICategoriesRepo _categoriesRepo;
        private readonly IManufacturersRepo _manufacturerRepo;
        private readonly IPriceHistoryRepo _priceHistoryRepo;
        private readonly IMapper _mapper;


        public ProductsService(IProductsRepo productRepo, ICategoriesRepo categoriesRepo, IManufacturersRepo manufacturerRepo,
            IPriceHistoryRepo priceHistoryRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoriesRepo = categoriesRepo;
            _manufacturerRepo = manufacturerRepo;
            _priceHistoryRepo = priceHistoryRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var products = await _productRepo.GetAllProducts();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductDetailedReadDto> GetProductById(int id)
        {
            var product = await _productRepo.GetProductById(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{id}");
            }
            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<ProductDetailedReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            var price = _mapper.Map<Price>(productCreateDto.Price);
            product.PriceHistory = new List<Price>
            {
                price
            };
            var existingCategory = await _categoriesRepo.GetCategoryById(productCreateDto.CategoryId);
            if (existingCategory is null)
            {
                throw new KeyNotFoundException($"Category not found with id:{productCreateDto.CategoryId}");
            }
            product.Category = existingCategory;

            var existingManufacturer = await _manufacturerRepo.GetManufacturerById(productCreateDto.ManufacturerId);
            if (existingManufacturer is null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{productCreateDto.ManufacturerId}");
            }
            product.Manufacturer = existingManufacturer;

            product.PriceHistory[0].StartDate = DateTime.Now;
            await _productRepo.AddProduct(product);
            await _productRepo.SaveChanges();
            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<ProductDetailedReadDto> DeleteProduct(int id)
        {
            var product = await _productRepo.GetProductById(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{id}");
            }
            _productRepo.DeleteProduct(product);
            await _productRepo.SaveChanges();
            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<ProductDetailedReadDto> UpdateProduct(int productId, ProductUpdateDto productUpdateDto)
        {
            var product = await _productRepo.GetProductById(productId);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }

            var existingCategory = await _categoriesRepo.GetCategoryById(productUpdateDto.CategoryId);
            if (existingCategory is null)
            {
                throw new KeyNotFoundException($"Category not found with id:{productUpdateDto.CategoryId}");
            }
            product.Category = existingCategory;

            var existingManufacturer = await _manufacturerRepo.GetManufacturerById(productUpdateDto.ManufacturerId);
            if (existingManufacturer is null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{productUpdateDto.ManufacturerId}");
            }
            product.Manufacturer = existingManufacturer;
            var newProduct = _mapper.Map(productUpdateDto, product);
            await _productRepo.SaveChanges();
            return _mapper.Map<ProductDetailedReadDto>(newProduct);
        }

        public async Task<IEnumerable<ProductReadDto>> SearchProducts(string searchText)
        {
            var products = await _productRepo.SearchByName(searchText);
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<IEnumerable<ProductReadDto>> GetTop10Products()
        {
            var products = await _productRepo.GetTop10Products();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}

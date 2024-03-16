using AutoMapper;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;
using Attribute = Shop.DAL.Models.Attribute;

namespace Shop.BL.Services.Implementation
{
    public class ProductAttributesService : IProductAttributesService
    {
        private readonly IProductAttributesRepo _productAttributesRepo;
        private readonly IProductsRepo _productsRepo;
        private readonly IMapper _mapper;

        public ProductAttributesService(IProductAttributesRepo productAttributesRepo, IProductsRepo productsRepo, IMapper mapper)
        {
            _productAttributesRepo = productAttributesRepo;
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductAttributeReadDto>> GetAllProductAttributes(int productId)
        {
            var existingProduct = await _productsRepo.GetProductById(productId);
            if (existingProduct is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }
            var productAttributeFromRepo = await _productAttributesRepo.GetAllProductAttributesByProductId(productId);
            return _mapper.Map<IEnumerable<ProductAttributeReadDto>>(productAttributeFromRepo);
        }

        public async Task<ProductAttributeReadDto> DeleteProductAttribute(int productId, int attributeId)
        {
            var existingProduct = await _productsRepo.GetProductById(productId);
            if (existingProduct is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }
            var productAttributeFromRepo = await _productAttributesRepo.GetProductAttributeByIds(productId, attributeId);
            if (productAttributeFromRepo is null)
            {
                throw new KeyNotFoundException($"Attribute not found with id:{attributeId}");
            }
            _productAttributesRepo.DeleteProductAttribute(productAttributeFromRepo);
            await _productAttributesRepo.SaveChanges();
            return _mapper.Map<ProductAttributeReadDto>(productAttributeFromRepo);
        }

        public async Task<ProductAttributeReadDto> CreateProductAttribute(int productId, AttributeCreateDto attributeCreateDto)
        {
            var existingProduct = await _productsRepo.GetProductById(productId);
            if (existingProduct is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }
            Attribute attribute = _mapper.Map<Attribute>(attributeCreateDto); ;

            var productAttribute = new ProductAttribute();
            productAttribute.ProductId = productId;
            productAttribute.Value = attributeCreateDto.Value;
            productAttribute.Attribute = attribute;
            await _productAttributesRepo.AddAttributeToProduct(productAttribute);
            await _productAttributesRepo.SaveChanges();
            return _mapper.Map<ProductAttributeReadDto>(productAttribute);
        }

        public async Task<ProductAttributeReadDto> UpdateProductAttribute(int productId, int attributeId, ProductAttributeUpdateDto attributeUpdateDto)
        {
            var existingProduct = await _productsRepo.GetProductById(productId);
            if (existingProduct is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{productId}");
            }
            var productAttributeFromRepo = await _productAttributesRepo.GetProductAttributeByIds(productId, attributeId);
            if (productAttributeFromRepo is null)
            {
                throw new KeyNotFoundException($"Attribute not found with id:{attributeId}");
            }
            var productAttributeToRepo = _mapper.Map(attributeUpdateDto, productAttributeFromRepo);
            return _mapper.Map<ProductAttributeReadDto>(productAttributeToRepo);
        }
    }
}

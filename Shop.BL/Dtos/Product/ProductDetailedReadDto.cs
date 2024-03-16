using Shop.BL.Dtos.Attribute;
using Shop.BL.Dtos.Category;
using Shop.BL.Dtos.Manufacturer;
using Shop.BL.Dtos.Price;
using Shop.BL.Dtos.Review;

namespace Shop.BL.Dtos.Product
{
    public class ProductDetailedReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryReadDto Category { get; set; }
        public ManufacturerReadDto Manufacturer { get; set; }
        public List<PriceReadDto>? PriceHistory { get; set; }
        public List<ProductAttributeReadDto> Attributes { get; set; }
        public List<ReviewReadDto>? Reviews { get; set; }
        public List<int> ProductImageIds { get; set; }
    }
}

using WebBlazor.Models.Dtos.Attribute;
using WebBlazor.Models.Dtos.Category;
using WebBlazor.Models.Dtos.Manufacturer;
using WebBlazor.Models.Dtos.Price;
using WebBlazor.Models.Dtos.Review;

namespace WebBlazor.Models.Dtos.Product
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
        public List<ReviewReadDto> Reviews { get; set; }
        public List<int> ProductImageIds { get; set; }
    }
}

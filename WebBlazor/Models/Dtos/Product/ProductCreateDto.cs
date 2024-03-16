using WebBlazor.Models.Dtos.Price;

namespace WebBlazor.Models.Dtos.Product
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string CategoryId { get; set; }
        public string ManufacturerId { get; set; }
        public PriceCreateDto Price { get; set; } = new PriceCreateDto();
    }
}

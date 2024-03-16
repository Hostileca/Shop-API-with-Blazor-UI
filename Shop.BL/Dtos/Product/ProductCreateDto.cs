using Shop.BL.Dtos.Price;

namespace Shop.BL.Dtos.Product
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public PriceCreateDto Price { get; set; }
    }
}

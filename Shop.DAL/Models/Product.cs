using Shop.DAL.Models.Files;

namespace Shop.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<Price>? PriceHistory { get; set; }
        public List<OrderProduct>? OrderProduct { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<ProductAttribute>? ProductAttributes { get; set; }
        public List<ProductImage>? Images { get; set; }
        public List<CartElement> CartElements { get; set; }
    }
}

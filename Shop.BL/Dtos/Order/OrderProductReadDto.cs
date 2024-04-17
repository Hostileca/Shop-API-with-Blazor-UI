using Shop.BL.Dtos.Product;

namespace Shop.BL.Dtos.Order
{
    public class OrderProductReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Amount { get; set; }
    }
}

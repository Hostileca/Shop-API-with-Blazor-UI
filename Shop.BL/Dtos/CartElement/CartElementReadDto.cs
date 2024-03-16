using Shop.BL.Dtos.Product;

namespace Shop.BL.Dtos.CartElement
{
    public class CartElementReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Amount { get; set; }
    }
}

using WebBlazor.Models.Dtos.Product;

namespace WebBlazor.Models.Dtos.Cart
{
    public class CartElementReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Amount { get; set; }
    }
}

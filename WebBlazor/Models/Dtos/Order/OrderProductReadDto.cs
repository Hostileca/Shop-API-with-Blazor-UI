using WebBlazor.Models.Dtos.Product;

namespace WebBlazor.Models.Dtos.Order
{
    public class OrderProductReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Amount { get; set; } 
    }
}

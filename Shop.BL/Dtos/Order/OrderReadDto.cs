using Shop.BL.Dtos.Product;

namespace Shop.BL.Dtos.Order
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}

using Shop.BL.Dtos.Product;

namespace Shop.BL.Dtos.Manufacturer
{
    public class ManufacturerDetailedReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}

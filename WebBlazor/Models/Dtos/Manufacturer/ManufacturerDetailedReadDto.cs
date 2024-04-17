using WebBlazor.Models.Dtos.Product;

namespace WebBlazor.Models.Dtos.Manufacturer
{
    public class ManufacturerDetailedReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerImageId { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}

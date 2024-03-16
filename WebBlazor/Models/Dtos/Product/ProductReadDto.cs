using WebBlazor.Models.Dtos.Category;
using WebBlazor.Models.Dtos.Manufacturer;

namespace WebBlazor.Models.Dtos.Product
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ManufacturerReadDto Manufacturer { get; set; }
        public CategoryReadDto Category { get; set; }
        public List<int> ProductImageIds { get; set; }
        public float Price { get; set; }
    }
}

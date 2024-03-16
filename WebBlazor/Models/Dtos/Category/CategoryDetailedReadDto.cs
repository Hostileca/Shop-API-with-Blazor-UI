using WebBlazor.Models.Dtos.Product;

namespace WebBlazor.Models.Dtos.Category
{
    public class CategoryDetailedReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryImageId { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}

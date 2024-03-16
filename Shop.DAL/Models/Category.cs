using Shop.DAL.Models.Files;

namespace Shop.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public CategoryImage Image { get; set; }
    }
}

namespace Shop.DAL.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}

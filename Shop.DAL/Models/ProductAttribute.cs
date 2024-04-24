namespace Shop.DAL.Models
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
    }
}

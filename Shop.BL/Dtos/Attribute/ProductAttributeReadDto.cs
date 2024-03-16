namespace Shop.BL.Dtos.Attribute
{
    public class ProductAttributeReadDto
    {
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }
    }
}

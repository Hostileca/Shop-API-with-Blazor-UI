namespace Shop.BL.Dtos.Product
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
    }
}

namespace Shop.DAL.Models
{
    public class Price
    {
        public int Id { get; set; }
        public float PriceValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Product Product { get; set; }
    }
}

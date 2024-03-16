namespace Shop.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public List<OrderProduct> OrderProduct { get; set; }
    }
}

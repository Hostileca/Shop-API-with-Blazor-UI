namespace Shop.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}

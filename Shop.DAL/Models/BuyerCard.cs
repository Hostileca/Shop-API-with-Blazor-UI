namespace Shop.DAL.Models
{
    public class BuyerCard
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public int Bonuses { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}

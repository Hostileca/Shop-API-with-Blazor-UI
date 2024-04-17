namespace Shop.BL.Dtos.BuyerCard
{
    public class BuyerCardReadDto
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public int Bonuses { get; set; }
    }
}

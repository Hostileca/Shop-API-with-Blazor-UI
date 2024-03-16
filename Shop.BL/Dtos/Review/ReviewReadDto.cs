using Shop.BL.Dtos.User;

namespace Shop.BL.Dtos.Review
{
    public class ReviewReadDto
    {
        public int Id { get; set; }
        public UserReadDto User { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}

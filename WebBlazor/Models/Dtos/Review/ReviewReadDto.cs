using WebBlazor.Models.Dtos.User;

namespace WebBlazor.Models.Dtos.Review
{
    public class ReviewReadDto
    {
        public int Id { get; set; }
        public UserReadDto User { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}

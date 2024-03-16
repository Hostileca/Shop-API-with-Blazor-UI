namespace Shop.DAL.Models.Files
{
    public class CategoryImage : FileBase
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}

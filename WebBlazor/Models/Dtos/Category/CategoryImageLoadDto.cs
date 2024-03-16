using Microsoft.AspNetCore.Components.Forms;

namespace WebBlazor.Models.Dtos.Category
{
    public class CategoryImageLoadDto
    {
        public string Id { get; set; }
        public IBrowserFile File { get; set; }
    }
}

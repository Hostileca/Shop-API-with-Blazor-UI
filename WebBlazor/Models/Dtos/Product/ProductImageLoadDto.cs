using Microsoft.AspNetCore.Components.Forms;

namespace WebBlazor.Models.Dtos.Product
{
    public class ProductImageLoadDto
    {
        public string Id { get; set; }
        public IBrowserFile File { get; set; }
    }
}

using Microsoft.AspNetCore.Components.Forms;

namespace WebBlazor.Models.Dtos.Manufacturer
{
    public class ManufacturerImageLoadDto
    {
        public string Id { get; set; }
        public IBrowserFile File { get; set; }
    }
}

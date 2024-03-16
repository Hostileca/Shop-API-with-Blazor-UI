namespace Shop.DAL.Models.Files
{
    public class ManufacturerImage : FileBase
    {
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
    }
}

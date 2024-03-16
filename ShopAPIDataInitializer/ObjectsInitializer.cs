
namespace ShopAPIDataInitializer
{
    internal class ObjectsInitializer
    {
        public void Categories()
        {
            var categories = new List<CategoryCreateDto>
                {
                    new CategoryCreateDto { Name = "Smartphones" },
                    new CategoryCreateDto { Name = "Laptops" },
                    new CategoryCreateDto { Name = "Headphones" },
                    new CategoryCreateDto { Name = "Monitors" },
                    new CategoryCreateDto { Name = "Tablets" },
                    new CategoryCreateDto { Name = "Cameras" }
                };
        }

        public void Manufacturers()
        {
            var manufacturers = new List<ManufacturerCreateDto>
                {
                    new ManufacturerCreateDto { Name = "Samsung" },
                    new ManufacturerCreateDto { Name = "Apple" },
                    new ManufacturerCreateDto { Name = "Sony" },
                    new ManufacturerCreateDto { Name = "Dell" },
                    new ManufacturerCreateDto { Name = "LG" },
                    new ManufacturerCreateDto { Name = "Lenovo" }
                };
        }

        public void Products()
        {

        } 
    }
}

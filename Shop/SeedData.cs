using Microsoft.AspNetCore.Identity;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Dtos.Category;
using Shop.BL.Dtos.Manufacturer;
using Shop.BL.Dtos.Price;
using Shop.BL.Dtos.Product;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Models;
using System;

namespace Shop
{
    public class SeedData
    {
        public static async Task InitializeRootUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminRole = "root";
            string userRole = "user";
            string adminUserName = "root";
            string adminPassword = "123";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (!await roleManager.RoleExistsAsync(userRole))
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }

            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new User { UserName = adminUserName, Email = adminUserName };
                await userManager.CreateAsync(adminUser, adminPassword);

                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }

        public static async Task InitializeCategories(ICategoriesService categoriesService)
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

            foreach (var category in categories)
            {
                await categoriesService.CreateCategory(category);
            }
        }

        public static async Task InitializeManufacturers(IManufacturersService manufacturersService)
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

            foreach (var manufacturer in manufacturers)
            {
                await manufacturersService.CreateManufacturer(manufacturer);
            }
        }

        public static async Task InitializeProducts(IProductsService productsService)
        {
            var products = new List<ProductCreateDto>
                {
                    new ProductCreateDto
                    {
                        Name = "Galaxy S21",
                        Description = "Flagship smartphone from Samsung",
                        CategoryId = 1,
                        ManufacturerId = 1,
                        Price = new PriceCreateDto { PriceValue = 999.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "MacBook Air",
                        Description = "Thin and light laptop from Apple",
                        CategoryId = 2,
                        ManufacturerId = 2,
                        Price = new PriceCreateDto { PriceValue = 1299.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "WH-1000XM4",
                        Description = "Noise-canceling headphones from Sony",
                        CategoryId = 3,
                        ManufacturerId = 3,
                        Price = new PriceCreateDto { PriceValue = 349.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "XPS 13",
                        Description = "Ultra-thin laptop from Dell",
                        CategoryId = 4,
                        ManufacturerId = 4,
                        Price = new PriceCreateDto { PriceValue = 1499.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "OLED C1",
                        Description = "4K OLED TV from LG",
                        CategoryId = 5,
                        ManufacturerId = 5,
                        Price = new PriceCreateDto { PriceValue = 1999.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "ThinkPad X1 Carbon",
                        Description = "Business ultrabook from Lenovo",
                        CategoryId = 6,
                        ManufacturerId = 6,
                        Price = new PriceCreateDto { PriceValue = 1599.99F }
                    },
                    new ProductCreateDto
                    {
                        Name = "EOS 5D Mark IV",
                        Description = "Full-frame DSLR camera from Canon",
                        CategoryId = 6,
                        ManufacturerId = 6,
                        Price = new PriceCreateDto { PriceValue = 2499.99F }
                    }
                };

            foreach (var product in products)
            {
                await productsService.CreateProduct(product);
            }
        }

        public static async Task InitializeAttributes(IProductAttributesService productAttributesService)
        {
            var galaxyS21Attributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Display Size", Value = "6.2 inches" },
                    new AttributeCreateDto { Name = "Resolution", Value = "2400 x 1080 pixels" },
                    new AttributeCreateDto { Name = "Processor", Value = "Exynos 2100 / Snapdragon 888" },
                    new AttributeCreateDto { Name = "RAM", Value = "8 GB" },
                    new AttributeCreateDto { Name = "Storage", Value = "128 GB or 256 GB" },
                    new AttributeCreateDto { Name = "Main Camera", Value = "Triple - 12 MP (wide), 12 MP (ultrawide), 64 MP (telephoto)" },
                    new AttributeCreateDto { Name = "Front Camera", Value = "10 MP" },
                    new AttributeCreateDto { Name = "Battery", Value = "4000 mAh" },
                    new AttributeCreateDto { Name = "Operating System", Value = "Android 11, One UI 3.1" },
                    new AttributeCreateDto { Name = "Weight", Value = "171 grams" },
                    new AttributeCreateDto { Name = "Dimensions", Value = "151.7 x 71.2 x 7.9 mm" },
                    new AttributeCreateDto { Name = "Wireless Charging", Value = "Yes" },
                    new AttributeCreateDto { Name = "5G Support", Value = "Yes" }
                };
            foreach (var attribute in galaxyS21Attributes)
            {
                await productAttributesService.CreateProductAttribute(1, attribute);
            }

            var macBookAirAttributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Display Size", Value = "13.3 inches" },
                    new AttributeCreateDto { Name = "Resolution", Value = "2560 x 1600 pixels" },
                    new AttributeCreateDto { Name = "Processor", Value = "Apple M1 Chip" },
                    new AttributeCreateDto { Name = "RAM", Value = "8 GB or 16 GB" },
                    new AttributeCreateDto { Name = "Storage", Value = "256 GB or 512 GB or 1 TB or 2 TB" },
                    new AttributeCreateDto { Name = "Weight", Value = "2.8 pounds (1.29 kg)" },
                    new AttributeCreateDto { Name = "Operating System", Value = "macOS Big Sur" },
                    new AttributeCreateDto { Name = "Battery", Value = "Up to 15 hours of web browsing" },
                    new AttributeCreateDto { Name = "Thunderbolt Ports", Value = "Two Thunderbolt 3 ports" },
                    new AttributeCreateDto { Name = "Backlit Keyboard", Value = "Yes" },
                    new AttributeCreateDto { Name = "Touch ID", Value = "Yes" },
                    new AttributeCreateDto { Name = "Retina Display", Value = "Yes" }
                };
            foreach (var attribute in macBookAirAttributes)
            {
                await productAttributesService.CreateProductAttribute(2, attribute); // Assuming 2 is the ID for MacBook Air in your system
            }

            var wh1000xm4Attributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Type", Value = "Over-ear, Noise-canceling" },
                    new AttributeCreateDto { Name = "Driver Unit", Value = "40mm, dome type" },
                    new AttributeCreateDto { Name = "Frequency Response", Value = "4Hz-40,000Hz" },
                    new AttributeCreateDto { Name = "Battery Life", Value = "Up to 30 hours" },
                    new AttributeCreateDto { Name = "Noise Cancellation", Value = "Yes, Industry-leading" },
                    new AttributeCreateDto { Name = "Touch Controls", Value = "Yes" },
                    new AttributeCreateDto { Name = "Quick Charge", Value = "Yes, 10 minutes for 5 hours playback" },
                    new AttributeCreateDto { Name = "Weight", Value = "254 grams" },
                    new AttributeCreateDto { Name = "Connectivity", Value = "Bluetooth 5.0, NFC" },
                    new AttributeCreateDto { Name = "Foldable Design", Value = "Yes" },
                    new AttributeCreateDto { Name = "Microphone", Value = "Built-in with Speak-to-Chat feature" },
                    new AttributeCreateDto { Name = "Compatibility", Value = "iOS and Android devices" },
                    new AttributeCreateDto { Name = "Color Options", Value = "Black, Silver" }
                };
            foreach (var attribute in wh1000xm4Attributes)
            {
                await productAttributesService.CreateProductAttribute(3, attribute); // Assuming 3 is the ID for WH-1000XM4 in your system
            }

            var xps13Attributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Display Size", Value = "13.4 inches" },
                    new AttributeCreateDto { Name = "Resolution", Value = "1920 x 1200 pixels or 3840 x 2400 pixels" },
                    new AttributeCreateDto { Name = "Processor", Value = "Intel Core i5 or i7 (11th Gen)" },
                    new AttributeCreateDto { Name = "RAM", Value = "8 GB or 16 GB or 32 GB" },
                    new AttributeCreateDto { Name = "Storage", Value = "256 GB or 512 GB or 1 TB or 2 TB SSD" },
                    new AttributeCreateDto { Name = "Graphics", Value = "Intel Iris Xe Graphics" },
                    new AttributeCreateDto { Name = "Battery Life", Value = "Up to 14 hours" },
                    new AttributeCreateDto { Name = "Operating System", Value = "Windows 10 Home or Pro" },
                    new AttributeCreateDto { Name = "Weight", Value = "2.8 pounds (1.27 kg)" },
                    new AttributeCreateDto { Name = "Dimensions", Value = "296 x 199 x 14.8 mm" },
                    new AttributeCreateDto { Name = "Ports", Value = "2 x Thunderbolt 4, 1 x USB-C 3.1, 3.5mm headphone jack" },
                    new AttributeCreateDto { Name = "Backlit Keyboard", Value = "Yes" },
                    new AttributeCreateDto { Name = "Fingerprint Reader", Value = "Yes" }
                };
            foreach (var attribute in xps13Attributes)
            {
                await productAttributesService.CreateProductAttribute(4, attribute); // Assuming 4 is the ID for XPS 13 in your system
            }

            var oledC1Attributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Display Size", Value = "Available in various sizes (e.g., 55 inches, 65 inches, 77 inches)" },
                    new AttributeCreateDto { Name = "Resolution", Value = "4K Ultra HD (3840 x 2160 pixels)" },
                    new AttributeCreateDto { Name = "Panel Type", Value = "OLED" },
                    new AttributeCreateDto { Name = "HDR Support", Value = "Dolby Vision, HDR10, HLG" },
                    new AttributeCreateDto { Name = "Processor", Value = "α9 Gen 4 AI Processor" },
                    new AttributeCreateDto { Name = "Audio", Value = "Dolby Atmos, OLED Surround, AI Sound Pro" },
                    new AttributeCreateDto { Name = "Smart TV Platform", Value = "webOS with ThinQ AI" },
                    new AttributeCreateDto { Name = "HDMI Ports", Value = "4 HDMI ports (eARC support)" },
                    new AttributeCreateDto { Name = "USB Ports", Value = "3 USB ports" },
                    new AttributeCreateDto { Name = "Built-in Wi-Fi", Value = "Yes (802.11ac)" },
                    new AttributeCreateDto { Name = "Dimensions", Value = "Varies by model" },
                    new AttributeCreateDto { Name = "Weight", Value = "Varies by model" },
                    new AttributeCreateDto { Name = "Remote Control", Value = "Magic Remote with voice control" },
                    new AttributeCreateDto { Name = "Bluetooth", Value = "Yes" }
                };
            foreach (var attribute in oledC1Attributes)
            {
                await productAttributesService.CreateProductAttribute(5, attribute); // Assuming 5 is the ID for OLED C1 in your system
            }

            var thinkPadX1CarbonAttributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Display Size", Value = "14 inches" },
                    new AttributeCreateDto { Name = "Resolution", Value = "1920 x 1080 pixels or 2560 x 1440 pixels or 3840 x 2160 pixels" },
                    new AttributeCreateDto { Name = "Processor", Value = "Intel Core i5 or i7 (11th Gen)" },
                    new AttributeCreateDto { Name = "RAM", Value = "8 GB or 16 GB or 32 GB" },
                    new AttributeCreateDto { Name = "Storage", Value = "256 GB or 512 GB or 1 TB SSD" },
                    new AttributeCreateDto { Name = "Graphics", Value = "Intel UHD Graphics or Intel Iris Xe Graphics" },
                    new AttributeCreateDto { Name = "Battery Life", Value = "Up to 15 hours" },
                    new AttributeCreateDto { Name = "Operating System", Value = "Windows 10 Pro" },
                    new AttributeCreateDto { Name = "Weight", Value = "Starting at 2.49 pounds (1.13 kg)" },
                    new AttributeCreateDto { Name = "Dimensions", Value = "323 x 217 x 15.95 mm" },
                    new AttributeCreateDto { Name = "Ports", Value = "2 x Thunderbolt 4, 2 x USB 3.2 Gen 1, HDMI 2.0, 3.5mm headphone jack" },
                    new AttributeCreateDto { Name = "Backlit Keyboard", Value = "Yes" },
                    new AttributeCreateDto { Name = "Fingerprint Reader", Value = "Yes" },
                    new AttributeCreateDto { Name = "Dolby Atmos Speaker System", Value = "Yes" }
                };
            foreach (var attribute in thinkPadX1CarbonAttributes)
            {
                await productAttributesService.CreateProductAttribute(6, attribute); // Assuming 6 is the ID for ThinkPad X1 Carbon in your system
            }

            var eos5DMarkIVAttributes = new List<AttributeCreateDto>
                {
                    new AttributeCreateDto { Name = "Sensor Type", Value = "Full-frame CMOS" },
                    new AttributeCreateDto { Name = "Resolution", Value = "30.4 megapixels" },
                    new AttributeCreateDto { Name = "ISO Range", Value = "100-32000 (expandable to 50-102400)" },
                    new AttributeCreateDto { Name = "Autofocus System", Value = "61-point AF system with 41 cross-type points" },
                    new AttributeCreateDto { Name = "Viewfinder", Value = "Optical pentaprism with 100% coverage" },
                    new AttributeCreateDto { Name = "LCD Screen", Value = "3.2-inch, 1620K-dot Clear View II LCD" },
                    new AttributeCreateDto { Name = "Continuous Shooting", Value = "7 fps" },
                    new AttributeCreateDto { Name = "Video Recording", Value = "4K at 30 fps, Full HD at 60 fps" },
                    new AttributeCreateDto { Name = "Built-in Wi-Fi", Value = "Yes" },
                    new AttributeCreateDto { Name = "GPS", Value = "Yes" },
                    new AttributeCreateDto { Name = "Weight", Value = "Approximately 800 grams" },
                    new AttributeCreateDto { Name = "Dimensions", Value = "150.7 x 116.4 x 75.9 mm" },
                    new AttributeCreateDto { Name = "Battery Life", Value = "Approximately 900 shots (CIPA)" },
                    new AttributeCreateDto { Name = "Dual Card Slots", Value = "Yes (CompactFlash and SD)" }
                };
            foreach (var attribute in eos5DMarkIVAttributes)
            {
                await productAttributesService.CreateProductAttribute(7, attribute); // Assuming 7 is the ID for EOS 5D Mark IV in your system
            }

        }
    }
}

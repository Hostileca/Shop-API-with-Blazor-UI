using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;
using Shop.DAL.Models.Files;
using Attribute = Shop.DAL.Models.Attribute;

namespace Shop.DAL.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BuyerCard> BuyersCards => Set<BuyerCard>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Price> PricesHistory => Set<Price>();
        public DbSet<ProductAttribute> ProductsAttrubutes => Set<ProductAttribute>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<ProductImage> ProductsImages => Set<ProductImage>();
        public DbSet<CategoryImage> CategoriesImages => Set<CategoryImage>();
        public DbSet<ManufacturerImage> ManufacturerImages => Set<ManufacturerImage>();
        public DbSet<CartElement> CartElements => Set<CartElement>();
        public DbSet<OrderProduct> OrderProduct => Set<OrderProduct>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(op => new { op.ProductId, op.OrderId });
                entity.Property(op => op.Amount);

                entity.HasOne(op => op.Product)
                      .WithMany(p => p.OrderProduct)
                      .HasForeignKey(pa => pa.ProductId)
                      .IsRequired(true);

                entity.HasOne(op => op.Order)
                      .WithMany(a => a.OrderProduct)
                      .HasForeignKey(pa => pa.OrderId)
                      .IsRequired(true);
            });

            modelBuilder.Entity<CartElement>(entity =>
            {
                entity.HasKey(op => new { op.ProductId, op.UserId });

                entity.HasOne(ce => ce.Product)
                      .WithMany(p => p.CartElements)
                      .HasForeignKey(ce => ce.ProductId)
                      .IsRequired(true);

                entity.HasOne(ce => ce.User)
                      .WithMany(user => user.CartElements)
                      .HasForeignKey(pa => pa.UserId)
                      .IsRequired(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(user => user.FirstName).IsRequired(false);
                entity.Property(user => user.LastName).IsRequired(false);
                entity.HasOne(user => user.BuyerCard)
                    .WithOne(card => card.User)
                    .HasForeignKey<BuyerCard>(card => card.UserId)
                    .IsRequired(false);

                entity.HasMany(user => user.Orders).WithOne(order => order.User).IsRequired();

                entity.HasMany(user => user.Reviews).WithOne(review => review.User);
            });

            modelBuilder.Entity<BuyerCard>(entity =>
            {
                entity.HasKey(card => card.Id);

                entity.Property(card => card.RegistrationDate).IsRequired(true);

                entity.Property(card => card.Bonuses);

                entity.HasOne(card => card.User)
                        .WithOne(user => user.BuyerCard)
                        .IsRequired(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(order => order.Id);
                entity.Property(order => order.Date);
                entity.HasOne(order => order.User).WithMany(buyer => buyer.Orders).IsRequired();
                entity.HasMany(order => order.OrderProduct).WithOne(op => op.Order);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.Id);

                entity.HasIndex(product => product.Name)
                      .IsUnique();

                entity.Property(product => product.Description);

                entity.HasOne(product => product.Category)
                      .WithMany(category => category.Products)
                      .IsRequired(true);

                entity.HasOne(product => product.Manufacturer)
                      .WithMany(manufacturer => manufacturer.Products)
                      .IsRequired(true);

                entity.HasMany(product => product.PriceHistory)
                      .WithOne(price => price.Product)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(product => product.OrderProduct)
                      .WithOne(op => op.Product);

                entity.HasMany(product => product.ProductAttributes)
                      .WithOne(pa => pa.Product)
                      .HasForeignKey(pa => pa.ProductId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(product => product.Reviews).WithOne(review => review.Product);

                entity.HasMany(product => product.Images).WithOne(image => image.Product);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(manufacturer => manufacturer.Id);

                entity.HasIndex(manufacturer => manufacturer.Name)
                      .IsUnique();

                entity.HasMany(manufacturer => manufacturer.Products)
                      .WithOne(product => product.Manufacturer)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(category => category.Id);

                entity.HasIndex(category => category.Name)
                      .IsUnique();

                entity.HasMany(category => category.Products)
                      .WithOne(product => product.Category)
                      .IsRequired(true)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(category => category.Image).WithOne(image => image.Category);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(price => price.Id);
                entity.Property(price => price.PriceValue).IsRequired(true);
                entity.Property(price => price.StartDate).IsRequired(true);
                entity.Property(price => price.EndDate);
                entity.HasOne(price => price.Product).WithMany(product => product.PriceHistory);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(pa => new { pa.ProductId, pa.AttributeId });
                entity.Property(pa => pa.Value);
                entity.HasOne(pa => pa.Product)
                      .WithMany(p => p.ProductAttributes)
                      .HasForeignKey(pa => pa.ProductId)
                      .IsRequired(true);

                entity.HasOne(pa => pa.Attribute)
                      .WithMany(a => a.ProductAttributes)
                      .HasForeignKey(pa => pa.AttributeId)
                      .IsRequired(true);
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasKey(attribute => attribute.Id);

                entity.HasIndex(attribute => attribute.Name)
                      .IsUnique();

                entity.HasMany(attribute => attribute.ProductAttributes)
                      .WithOne(pa => pa.Attribute)
                      .HasForeignKey(pa => pa.AttributeId)
                      .IsRequired(true)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(review => review.Id);

                entity.HasOne(review => review.User).WithMany(user => user.Reviews).IsRequired(true);
                entity.HasOne(review => review.Product).WithMany(product => product.Reviews);

                entity.Property(review => review.Rating).IsRequired();

                entity.Property(review => review.Content);

                entity.Property(review => review.Date).IsRequired();
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(Image => Image.Id);

                entity.HasOne(image => image.Product).WithMany(product => product.Images).IsRequired(true);

                entity.Property(image => image.FilePath).IsRequired(true);
            });

            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.HasKey(Image => Image.Id);

                entity.HasOne(image => image.Category).WithOne(category => category.Image).HasForeignKey<CategoryImage>(ci => ci.CategoryId);

                entity.Property(image => image.FilePath).IsRequired(true);
            });

            modelBuilder.Entity<ManufacturerImage>(entity =>
            {
                entity.HasKey(Image => Image.Id);

                entity.HasOne(image => image.Manufacturer).WithOne(manufacturer => manufacturer.Image)
                .HasForeignKey<ManufacturerImage>(mi => mi.ManufacturerId);

                entity.Property(image => image.FilePath).IsRequired(true);
            });
        }
    }
}

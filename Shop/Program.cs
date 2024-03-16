using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.BL.Profiles;
using Shop.BL.Services.Implementation;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data;
using Shop.DAL.Data.Implementation;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;
using System.Text;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var sqlConnectionBuilder = new SqlConnectionStringBuilder();
            sqlConnectionBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(sqlConnectionBuilder.ConnectionString));

            builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
            builder.Services.AddScoped<IAttributesRepo, AttributesRepo>();
            builder.Services.AddScoped<IProductAttributesRepo, ProductAttributesRepo>();
            builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
            builder.Services.AddScoped<IManufacturersRepo, ManufacturersRepo>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IReviewsService, ReviewsService>();
            builder.Services.AddScoped<IFilesService, FilesService>();
            builder.Services.AddScoped<IPriceHistoryService, PriceHistoryService>();
            builder.Services.AddScoped<IBuyerCardsService, BuyerCardsService>();
            builder.Services.AddScoped<ICartElementsService, CartElementsService>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IAttributesService, AttributesService>();
            builder.Services.AddScoped<IProductAttributesService, ProductAttributesService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IManufacturersService, ManufacturersService>();
            builder.Services.AddScoped<IOrdersRepo, OrdersRepo>();
            builder.Services.AddScoped<IReviewsRepo, ReviewsRepo>();
            builder.Services.AddScoped<IFilesRepo, FilesRepo>();
            builder.Services.AddScoped<IPriceHistoryRepo, PriceHistoryRepo>();
            builder.Services.AddScoped<IBuyerCardsRepo, BuyerCardsRepo>();
            builder.Services.AddScoped<ICartElementsRepo, CartElementsRepo>();

            builder.Services.AddScoped<ExceptionHandlingMiddleware>();

            builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins()
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            }));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                SeedData.InitializeRootUser(userManager, roleManager).Wait();

                var categoriesService = scope.ServiceProvider.GetRequiredService<ICategoriesService>();
                var manufacturersService = scope.ServiceProvider.GetRequiredService<IManufacturersService>();
                var productsService = scope.ServiceProvider.GetRequiredService<IProductsService>();
                var attributesService = scope.ServiceProvider.GetRequiredService<IProductAttributesService>();

                try
                {
                    SeedData.InitializeCategories(categoriesService).Wait();
                    SeedData.InitializeManufacturers(manufacturersService).Wait();
                    SeedData.InitializeProducts(productsService).Wait();
                    SeedData.InitializeAttributes(attributesService).Wait();
                }
                catch
                {
                    Console.WriteLine("Data already exist");
                }
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}
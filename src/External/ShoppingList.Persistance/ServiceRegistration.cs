using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Application.Common.Abstractions.Repositories.Brands;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;
using ShoppingList.Application.Common.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Domain.Entities.Identity;
using ShoppingList.Persistance.Context;
using ShoppingList.Persistance.Repositories.BasketLines;
using ShoppingList.Persistance.Repositories.Baskets;
using ShoppingList.Persistance.Repositories.Brands;
using ShoppingList.Persistance.Repositories.Categories;
using ShoppingList.Persistance.Repositories.ProductImage;   
using ShoppingList.Persistance.Repositories.Products;
using ShoppingList.Persistance.Services;

namespace ShoppingList.Persistance;

public static class ServiceRegistration
{
   public static void PersistanceContextConfiguration(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<ShoppingListDbContext>(opt =>
         opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

      services.AddIdentityCore<AppUser>(opt =>
      {
         opt.Password.RequireNonAlphanumeric = false;
         opt.Password.RequiredLength = 4;
         opt.Password.RequireLowercase = false;
         opt.Password.RequireUppercase = false;
         opt.Password.RequireDigit = false;
         opt.SignIn.RequireConfirmedEmail = false;
      })
         .AddRoles<AppRole>()
         .AddEntityFrameworkStores<ShoppingListDbContext>()
         .AddDefaultTokenProviders();
   }

   public static void ConfigureRepositoryManager(this IServiceCollection services)
   {
      services.AddScoped<IProductReadRepository, ProductReadRepository>();
      services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

      services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
      services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

      services.AddScoped<IBrandReadRepository, BrandReadRepository>();
      services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();

      services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
      services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();

      services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
      services.AddScoped<IBasketReadRepository, BasketReadRepository>();

      services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
      services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
   }

   public static void ConfigureServiceManager(this IServiceCollection services)
   {
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IBasketService, BasketService>();
   }
}
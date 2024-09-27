using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Repositories.Brands;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities.Identity;
using ShoppingList.Persistance.Context;
using ShoppingList.Persistance.Repositories.Brands;
using ShoppingList.Persistance.Repositories.Categories;
using ShoppingList.Persistance.Repositories.Products;

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
         .AddEntityFrameworkStores<ShoppingListDbContext>();
   }

   public static void ConfigureRepositoryManager(this IServiceCollection services)
   {
      services.AddScoped<IProductReadRepository, ProductReadRepository>();
      services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

      services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
      services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

      services.AddScoped<IBrandReadRepository, BrandReadRepository>();
      services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();
   }
}
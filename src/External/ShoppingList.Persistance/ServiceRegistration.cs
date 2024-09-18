using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Repositories;
using ShoppingList.Application.Repositories.Brands;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Application.Repositories.Products;
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
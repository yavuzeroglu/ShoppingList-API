using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Repositories;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Persistance.Context;
using ShoppingList.Persistance.Repositories.Categories;
using ShoppingList.Persistance.Repositories.Products;

namespace ShoppingList.Persistance;

public static class ServiceRegistration
{
   public static void PersistanceContextConfiguration(this IServiceCollection services)
   {
      services.AddDbContext<ShoppingListDbContext>(opt => 
         opt.UseNpgsql(ConfigurationHelper.GetConnectionString));
   }

   public static void ConfigureRepositoryManager(this IServiceCollection services)
   {
      services.AddScoped<IProductReadRepository, ProductReadRepository>();
      services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

      services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
      services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();  
   }
}
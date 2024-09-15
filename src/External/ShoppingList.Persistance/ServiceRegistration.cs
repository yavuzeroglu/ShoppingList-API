using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Repositories;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance;

public static class ServiceRegistration
{
   public static void PersistanceContextConfiguration(this IServiceCollection services)
   {
      services.AddDbContext<ShoppingListDbContext>(opt => 
         opt.UseNpgsql(ConfigurationHelper.GetConnectionString));
   }
}
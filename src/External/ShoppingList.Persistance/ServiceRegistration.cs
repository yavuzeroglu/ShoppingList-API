using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance;

public static class ServiceRegistration
{
   public static void AddPersistanceServices(this IServiceCollection services)
   {
      services.AddDbContext<ShoppingListDbContext>(opt => 
         opt.UseNpgsql(ConfigurationHelper.GetConnectionString));
   }
}
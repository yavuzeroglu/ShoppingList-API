using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingList.Application;

public static class ServiceRegistration
{
   public static void ConfigureApplicationRegistration(this IServiceCollection services)
   {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
   }
}
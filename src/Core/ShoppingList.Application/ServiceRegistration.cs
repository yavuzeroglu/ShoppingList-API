using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Exceptions;

namespace ShoppingList.Application;

public static class ServiceRegistration
{
   public static void ConfigureApplicationRegistration(this IServiceCollection services)
   {
      var assembly = Assembly.GetExecutingAssembly();

      services.AddAutoMapper(assembly);

      services.AddTransient<ExceptionMiddleware>();

      services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
   }
}
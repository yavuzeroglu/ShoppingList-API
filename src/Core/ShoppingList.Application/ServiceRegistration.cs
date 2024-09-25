using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Behaviors;
using ShoppingList.Application.Exceptions;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;

namespace ShoppingList.Application;

public static class ServiceRegistration
{
    public static void ConfigureApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddTransient<ExceptionMiddleware>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
    }
}
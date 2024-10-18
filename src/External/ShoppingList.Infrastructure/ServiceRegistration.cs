using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.Abstractions.Tokens;
using ShoppingList.Infrastructure.Services;
using ShoppingList.Infrastructure.Services.Token;
using ShoppingList.Infrastructure.Services.Storages;
using ShoppingList.Application.Abstractions.Storage;
using ShoppingList.Infrastructure.Enums;
using System.Security.Cryptography;

namespace ShoppingList.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ILocalStorage, LocalStorage>();
        services.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
    {
        services.AddScoped<IStorage, T>();
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, StorageType storageType)
        => storageType switch
        {
            StorageType.Local => services.AddScoped<IStorage, LocalStorage>(),
            StorageType.Azure => services.AddScoped<IStorage, AzureStorage>(),
            _ => services.AddScoped<IStorage, LocalStorage>()  
        };

    public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Admin", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        expires != null ? expires > DateTime.UtcNow : false,


                    NameClaimType = ClaimTypes.Name
                };
            });
    }
}

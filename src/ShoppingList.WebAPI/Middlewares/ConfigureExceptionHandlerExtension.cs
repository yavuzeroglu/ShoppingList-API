using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ShoppingList.WebAPI.Middlewares;

public static class ConfigureExceptionHandlerExtension
{
    public static void ConfigureExceptionHandle<T>(this WebApplication app, ILogger<T> logger)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var cotnextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if(cotnextFeature is not null)
                {
                    logger.LogError(cotnextFeature.Error.Message);


                    await context.Response.WriteAsync(JsonSerializer.Serialize(new 
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = cotnextFeature.Error.Message,
                        Title = "Hata Alındı"
                    }));
                }
            });
        });
    }
}

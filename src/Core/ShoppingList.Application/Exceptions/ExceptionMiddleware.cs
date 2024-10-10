using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;


namespace ShoppingList.Application.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception, ILogger<ExceptionMiddleware> _logger)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = statusCode;

        if (exception is ValidationException validationException)
        {
            _logger.LogWarning("Validation error: {errors}", string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage)));

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest
            }.ToString());
        }


        List<string> errors = new()
        {
            exception.Message
        };
        _logger.LogError(exception.Message);
        return httpContext.Response.WriteAsync(new ExceptionModel
        {
            Errors = errors,
            StatusCode = statusCode
        }.ToString());

    }

    private static int GetStatusCode(Exception exception)
      => exception switch
      {
          BadRequestException => StatusCodes.Status400BadRequest,
          ValidationException => StatusCodes.Status422UnprocessableEntity,
          NotFoundException => StatusCodes.Status404NotFound,
          _ => StatusCodes.Status500InternalServerError
      };
}
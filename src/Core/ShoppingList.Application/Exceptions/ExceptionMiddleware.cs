using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;


namespace ShoppingList.Application.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = statusCode;

        if (exception.GetType() == typeof(ValidationException))
        {
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
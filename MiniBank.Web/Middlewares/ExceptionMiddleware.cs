using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MiniBank.Core.Exception;

namespace MiniBank.Web.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseValidationException(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException ex)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new {Message = ex.Message});
                }
                catch (FluentValidation.ValidationException exception)
                {
                    var errors = exception.Errors.Select(x => $"{x.ErrorMessage}");
                    var errorMessage = string.Join(Environment.NewLine, errors);
                    
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new {Message = errorMessage});
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { Message = "Внутренняя ошибка сервера" });
                }
            });
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MiniBank.Core.Exception;

namespace MiniBank.Web.Middlewares
{
    public static class ValidationExceptionMiddleware
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
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
                }
            });
        }
    }
}
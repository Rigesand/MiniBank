using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiniBank.Web.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseException(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
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
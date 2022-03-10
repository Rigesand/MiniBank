using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiniBank.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new { Message = "Внутренняя ошибка сервера" });
            }
        }
    }
}
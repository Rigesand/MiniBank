using Microsoft.AspNetCore.Builder;
using MiniBank.Web.Middlewares;

namespace MiniBank.Web.ExstensionsMiddlewares
{
    public static class ExceptionExstensions
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
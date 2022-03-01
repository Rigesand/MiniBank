using Microsoft.AspNetCore.Builder;
using MiniBank.Core.Exception;
using MiniBank.Web.Middlewares;

namespace MiniBank.Web.ExstensionsMiddlewares
{
    public static class UserFriendlyExceptionExstensions
    {
        public static IApplicationBuilder UserFriendlyException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserFriendlyExceptionMiddleware>();
        }
    }
}
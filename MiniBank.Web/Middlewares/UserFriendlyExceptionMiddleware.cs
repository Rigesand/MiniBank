using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MiniBank.Core.Exception;

namespace MiniBank.Web.Middlewares
{
    public class UserFriendlyExceptionMiddleware
    {
        public readonly RequestDelegate next;

        public UserFriendlyExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (UserFriendlyException ex)
            {
                var errorMessage =$"Exception Error: {ex.Message}"+
                                   $"StackTrace: {ex.StackTrace}"; 
                await httpContext.Response.WriteAsJsonAsync(new { Message=errorMessage});
            }
        }
    }
}
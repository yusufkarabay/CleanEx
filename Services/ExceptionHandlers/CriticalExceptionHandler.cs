using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanEx.Services.ExceptionHandlers
{
    public class CriticalExceptionHandler() : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                //burada telegram mesajı gibi yapıları yapabilirsin.(konsola yazabilirsin)
                Console.WriteLine($"Critical Exception");
            }
            return ValueTask.FromResult(false);
        }
    }
}

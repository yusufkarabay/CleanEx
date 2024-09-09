using CleanEx.Services.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanEx.Services.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorDto = ServiceResult<ErrorDto>.Fail(exception.Message, true, System.Net.HttpStatusCode.InternalServerError);
            httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorDto);
            return true;
        }
    }
}

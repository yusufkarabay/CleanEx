using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CleanEx.API
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleDbUpdateConcurrencyExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleDbUpdateConcurrencyExceptionAsync(HttpContext context, DbUpdateConcurrencyException ex)
        {

            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            return context.Response.WriteAsJsonAsync(new
            {
                message = "Bir hata meydana geldi",
                detail = ex.Message
            });
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsJsonAsync(new
            {
                message = "An unexpected error occurred.",
                detail = ex.Message
            });
        }
    }

}

using CleanEx.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanEx.Services
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToList();

                var resultModel = ServiceResult<ErrorDto>.Fail(new ErrorDto(errors, true));
                context.Result = new BadRequestObjectResult(resultModel);
                return;
            }
            await next();
        }
    }
}

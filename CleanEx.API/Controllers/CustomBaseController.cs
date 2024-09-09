using CleanEx.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanEx.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> CreateActionResult<T>(ServiceResult<T> result) where T : class
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result);
            }
            if (!result.IsSuccessful)
            {
                return new ObjectResult(result.Error) { StatusCode = (int)result.StatusCode };
            }

            return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };
        }

        [NonAction]
        public async Task<IActionResult> CreateActionResult(ServiceResult<NoContentDto> result)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
            }
            return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };
        }
    }
}

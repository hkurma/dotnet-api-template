using Microsoft.AspNetCore.Mvc;

namespace DotNet.Template.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected ActionResult HandleResult<T>(Application.Common.Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Error?.Contains("not found") == true)
        {
            return NotFound(new { message = result.Error });
        }

        return BadRequest(new { message = result.Error, errors = result.Errors });
    }
}

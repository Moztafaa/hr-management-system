namespace HRManagementSystem.WebApi.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult HandleResponse<T>(Response<T> response)
    {
        if (response is null)
            return StatusCode(500, new { hasError = true, errorMessage = "Unexpected null response." });

        if (response.HasError)
        {
            // If the service says the resource is missing → NotFound
            if (response.ErrorMessage?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { response.HasError, response.ErrorMessage });

            return BadRequest(new { response.HasError, response.ErrorMessage });
        }

        return Ok(response);
    }
}

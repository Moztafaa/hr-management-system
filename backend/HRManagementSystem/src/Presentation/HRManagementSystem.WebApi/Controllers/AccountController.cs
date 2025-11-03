namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService _accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        (bool Succeeded, AuthResponseDto? Response, IEnumerable<string> Errors) result = await _accountService.LoginAsync(loginDto);
        if (!result.Succeeded)
        {
            return Unauthorized(result);
        }
        return Ok(new { result.Succeeded, result.Response });
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
    {
        (bool Succeeded, IEnumerable<string> Errors) result = await _accountService.AssignRoleAsync(assignRoleDto);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    // Register
   [HttpPost("register-employee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDto registerEmployeeDto)
    {
        (bool Succeeded, RegisterEmployeeResponseDto? Response, IEnumerable<string> Errors) result = await _accountService.RegisterEmployeeAsync(registerEmployeeDto);
        if (!result.Succeeded)
        {
            return BadRequest(new { succeeded = result.Succeeded, errors = result.Errors });
        }
        return Ok(new { succeeded = result.Succeeded, response = result.Response });
    }

}

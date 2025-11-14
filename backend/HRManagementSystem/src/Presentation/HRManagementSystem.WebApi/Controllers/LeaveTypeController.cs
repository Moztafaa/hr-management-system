namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LeaveTypeController(ILeaveTypeService _leaveTypeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLeaveTypes(CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveTypeDto>> response = await _leaveTypeService.GetAllLeaveTypesAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveTypeById(int id, CancellationToken cancellationToken)
    {
        Response<LeaveTypeDto> response = await _leaveTypeService.GetLeaveTypeByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }
}

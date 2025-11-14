namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController(ILeaveRequestService _leaveRequestService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLeaveRequests(CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveRequestDto>> response = await _leaveRequestService.GetAllLeaveRequestsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveRequestById(int id, CancellationToken cancellationToken)
    {
        Response<LeaveRequestDto> response = await _leaveRequestService.GetLeaveRequestByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestDto createLeaveRequestDto, CancellationToken cancellationToken)
    {
        Response<LeaveRequestDto> response = await _leaveRequestService.CreateLeaveRequestAsync(createLeaveRequestDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetLeaveRequestById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLeaveRequest(int id, UpdateLeaveRequestDto updateLeaveRequestDto, CancellationToken cancellationToken)
    {
        Response<bool> response = await _leaveRequestService.UpdateLeaveRequestAsync(id, updateLeaveRequestDto, 0,cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaveRequest(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _leaveRequestService.DeleteLeaveRequestAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    [HttpGet("employee/{id}")]
    public async Task<IActionResult> GetLeaveRequestByEmployeeId(int id, CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveRequestDto>> response = await _leaveRequestService.GetLeaveRequestByEmployeeIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }
    [HttpGet("manager/{id}")]
    public async Task<IActionResult> GetLeaveRequestByReviewerId(int id, CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveRequestDto>> response = await _leaveRequestService.GetLeaveRequestByReviewerIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }
}

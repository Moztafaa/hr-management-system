
using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;

namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LeaveBalanceController(ILeaveBalanceService _leaveBalanceService) : ControllerBase
{
    // ✅ Get balances for specific employee
    [HttpGet("employee-leavebalance/{employeeId:int}")]
    public async Task<IActionResult> GetByEmployeeId(int employeeId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveBalanceDto>> response = await _leaveBalanceService.GetByEmployeeIdAsync(employeeId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }
    [HttpGet("employee-currentyear-leavebalance/{employeeId:int}")]
    public async Task<IActionResult> GetByEmployeeIdAndYear(int employeeId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<LeaveBalanceDto>> response = await _leaveBalanceService.GetByEmployeeIdAndYearAsync(employeeId, DateTime.UtcNow.Year, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // ✅ Allocate balances for specific employee (e.g., when new employee joins)
    [HttpPost("allocate/{employeeId:int}")]
    public async Task<IActionResult> AllocateForEmployee(int employeeId, CancellationToken cancellationToken)
    {
        Response<bool> response = await _leaveBalanceService.AllocateInitialBalancesAsync(employeeId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // ✅ Deduct leave days after approval
    [HttpPost("deduct")]
    public async Task<IActionResult> DeductLeave([FromBody] DeductLeaveRequestDto request, CancellationToken cancellationToken)
    {
        Response<bool> response = await _leaveBalanceService.DeductLeaveDaysAsync(request.EmployeeId, request.LeaveTypeId, request.DaysUsed, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // ✅ Reset balances for all employees (New Year process)
    [HttpPost("reset-annual")]
    public async Task<IActionResult> ResetAnnualBalances(CancellationToken cancellationToken)
    {
        Response<bool> response = await _leaveBalanceService.AllocateBalancesForNewYearAsync(cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }
}

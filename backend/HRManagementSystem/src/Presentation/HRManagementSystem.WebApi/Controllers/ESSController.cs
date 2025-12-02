using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.ESS;
using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;
using HRManagementSystem.Application.DTOs.Training;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ESSController(IESSService essService) : ControllerBase
{
    private int GetCurrentEmployeeId()
    {
        string? employeeIdClaim = User.FindFirst("EmployeeId")?.Value;

        if (string.IsNullOrEmpty(employeeIdClaim) || !int.TryParse(employeeIdClaim, out int employeeId))
        {
            throw new UnauthorizedAccessException("Employee ID not found in token claims");
        }

        return employeeId;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<ESSProfileDto> response = await essService.GetProfileAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateESSProfileDto dto, CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<bool> response = await essService.UpdateProfileAsync(employeeId, dto, cancellationToken);
        return Ok(response);
    }

    [HttpGet("leave-requests")]
    public async Task<IActionResult> GetLeaveHistory(CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<List<LeaveRequestDto>> response = await essService.GetLeaveHistoryAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpPost("leave-requests")]
    public async Task<IActionResult> SubmitLeaveRequest([FromBody] CreateLeaveRequestDto dto, CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();

        // Ensure the employee can only submit leave requests for themselves
        dto.EmployeeId = employeeId;

        Response<int> response = await essService.SubmitLeaveRequestAsync(employeeId, dto, cancellationToken);
        return Ok(response);
    }

    [HttpGet("leave-balance")]
    public async Task<IActionResult> GetLeaveBalance([FromQuery] int? year, CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        int targetYear = year ?? DateTime.UtcNow.Year;
        Response<List<LeaveBalanceDto>> response = await essService.GetLeaveBalancesAsync(employeeId, targetYear, cancellationToken);
        return Ok(response);
    }

    [HttpGet("payslips")]
    public async Task<IActionResult> GetPayslips(CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<List<PayslipDto>> response = await essService.GetPayslipsAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("payslips/{payslipId}")]
    public async Task<IActionResult> GetPayslipById(int payslipId, CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<PayslipDto> response = await essService.GetPayslipByIdAsync(employeeId, payslipId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        int employeeId = GetCurrentEmployeeId();
        Response<ESSDashboardDto> response = await essService.GetDashboardAsync(employeeId, cancellationToken);
        return Ok(response);
    }


    // Training endpoints
    [HttpGet("training/my-courses")]
    public async Task<IActionResult> GetMyCourses(CancellationToken cancellationToken)
    {
        (bool success, int employeeId, IActionResult? errorResult) = TryGetCurrentEmployeeId();
        if (!success) return errorResult!;

        Response<List<EmployeeTrainingDto>> response = await essService.GetMyCoursesAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("training/my-requests")]
    public async Task<IActionResult> GetMyTrainingRequests(CancellationToken cancellationToken)
    {
        (bool success, int employeeId, IActionResult? errorResult) = TryGetCurrentEmployeeId();
        if (!success) return errorResult!;

        Response<List<TrainingRequestDto>> response = await essService.GetMyTrainingRequestsAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("training/available-courses")]
    public async Task<IActionResult> GetAvailableCourses(CancellationToken cancellationToken)
    {
        (bool success, int employeeId, IActionResult? errorResult) = TryGetCurrentEmployeeId();
        if (!success) return errorResult!;

        Response<List<TrainingCourseDto>> response = await essService.GetAvailableCoursesAsync(employeeId, cancellationToken);
        return Ok(response);
    }

    [HttpPost("training/request")]
    public async Task<IActionResult> SubmitTrainingRequest([FromBody] ESSTrainingRequestCreateDto dto, CancellationToken cancellationToken)
    {
        (bool success, int employeeId, IActionResult? errorResult) = TryGetCurrentEmployeeId();
        if (!success) return errorResult!;

        Response<TrainingRequestDto> response = await essService.SubmitTrainingRequestAsync(employeeId, dto, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("training/request/{requestId}")]
    public async Task<IActionResult> CancelTrainingRequest(int requestId, CancellationToken cancellationToken)
    {
        (bool success, int employeeId, IActionResult? errorResult) = TryGetCurrentEmployeeId();
        if (!success) return errorResult!;

        Response<bool> response = await essService.CancelTrainingRequestAsync(employeeId, requestId, cancellationToken);
        return Ok(response);
    }
    private (bool success, int employeeId, IActionResult? errorResult) TryGetCurrentEmployeeId()
    {
        try
        {
            int employeeId = GetCurrentEmployeeId();
            return (true, employeeId, null);
        }
        catch (UnauthorizedAccessException ex)
        {
            return (false, 0, Unauthorized(new { Message = ex.Message }));
        }
    }

}

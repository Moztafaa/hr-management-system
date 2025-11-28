using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.ESS;
using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        if (dto.EmployeeId != employeeId)
        {
            return BadRequest(new Response<int>(0, "You can only submit leave requests for yourself", true));
        }

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
}

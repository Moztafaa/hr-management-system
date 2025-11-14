using HRManagementSystem.Application.DTOs.Leaves.LeaveApprovalDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LeaveApprovalController(ILeaveApprovalService _leaveApprovalService) : ControllerBase
{
    // ✅ Approve Leave Request
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] LeaveApprovalActionDto dto)
    {
        Response<bool> response = await _leaveApprovalService.ApproveAsync(dto);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // ❌ Reject Leave Request
    [HttpPost("reject")]
    public async Task<IActionResult> Reject([FromBody] LeaveApprovalActionDto dto)
    {
        Response<bool> response = await _leaveApprovalService.RejectAsync(dto);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // 🔍 Get all approvals assigned to an approver
    [HttpGet("approver/{approverId}")]
    public async Task<IActionResult> GetAllByApproverId(int approverId)
    {
        Response<IEnumerable<LeaveApprovalDto>> response = await _leaveApprovalService.GetAllApprovalsByApproverIdAsync(approverId);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    // 🔍 Get all approval steps for a specific leave request
    [HttpGet("request/{leaveRequestId}")]
    public async Task<IActionResult> GetAllByRequestId(int leaveRequestId)
    {
        Response<IEnumerable<LeaveApprovalDto>> response = await _leaveApprovalService.GetAllApprovalsByRequestIdAsync(leaveRequestId);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }
}

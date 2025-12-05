using HRManagementSystem.Application.DTOs.Resignation;
using HRManagementSystem.Application.Interfaces.IResignationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResignationApprovalController(IResignationApprovalService _resignationApprovalService) : ControllerBase
{
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ResignationApprovalActionDto dto, CancellationToken cancellationToken)
    {
        Response<bool> response = await _resignationApprovalService.ApproveAsync(dto, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost("reject")]
    public async Task<IActionResult> Reject([FromBody] ResignationApprovalActionDto dto, [FromQuery] string rejectionReason, CancellationToken cancellationToken)
    {
        Response<bool> response = await _resignationApprovalService.RejectAsync(dto, rejectionReason, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("approver/{approverId}")]
    public async Task<IActionResult> GetAllByApproverId(int approverId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<ResignationApprovalDto>> response = await _resignationApprovalService.GetAllApprovalsByApproverIdAsync(approverId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("resignation/{resignationId}")]
    public async Task<IActionResult> GetAllByResignationId(int resignationId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<ResignationApprovalDto>> response = await _resignationApprovalService.GetAllApprovalsByResignationIdAsync(resignationId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }
}

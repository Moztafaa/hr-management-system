using HRManagementSystem.Application.DTOs.Resignation;
using HRManagementSystem.Application.Interfaces.IResignationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResignationController(IResignationService _resignationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllResignations(CancellationToken cancellationToken)
    {
        Response<IEnumerable<ResignationDto>> response = await _resignationService.GetAllResignationsAsync(cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResignationById(int id, CancellationToken cancellationToken)
    {
        Response<ResignationDto> response = await _resignationService.GetResignationByIdAsync(id, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResignation([FromBody] CreateResignationDto dto, CancellationToken cancellationToken)
    {
        Response<ResignationDto> response = await _resignationService.CreateResignationAsync(dto, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return CreatedAtAction(nameof(GetResignationById), new { id = response.Data.Id }, response);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawResignation([FromBody] WithdrawResignationDto dto, CancellationToken cancellationToken)
    {
        Response<bool> response = await _resignationService.WithdrawResignationAsync(dto, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResignation(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _resignationService.DeleteResignationAsync(id, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetResignationsByEmployeeId(int employeeId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<ResignationDto>> response = await _resignationService.GetResignationsByEmployeeIdAsync(employeeId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("pending/approver/{approverId}")]
    public async Task<IActionResult> GetPendingResignationsForApprover(int approverId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<ResignationDto>> response = await _resignationService.GetPendingResignationsForApproverAsync(approverId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("active/employee/{employeeId}")]
    public async Task<IActionResult> GetActiveResignationByEmployeeId(int employeeId, CancellationToken cancellationToken)
    {
        Response<ResignationDto?> response = await _resignationService.GetActiveResignationByEmployeeIdAsync(employeeId, cancellationToken);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }
}

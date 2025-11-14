using HRManagementSystem.Application.DTOs.Payroll.Payslip;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
//[Authorize(Roles = "Admin,HR")]

public class PayslipController(IPayslipService _payslipService) : ControllerBase
{

    [HttpPost("{employeeId}/generate")]
    public async Task<IActionResult> GeneratePayslip(int employeeId, int month, int year)
    {
        var response = await _payslipService.GeneratePayslipAsync(employeeId, month, year);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost("generate/month")]
    public async Task<IActionResult> GeneratePayslipsForMonth(int month, int year)
    {
        var response = await _payslipService.GeneratePayslipsForMonthAsync(month, year);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayslipById(int id)
    {
        var response = await _payslipService.GetByIdAsync(id);
        if (response.HasError)
            return NotFound(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetPayslipsByEmployee(int employeeId)
    {
        var response = await _payslipService.GetByEmployeeAsync(employeeId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("month")]
    public async Task<IActionResult> GetPayslipsByMonth(int month, int year)
    {
        var response = await _payslipService.GetByMonthAsync(month, year);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayslip(int id)
    {
        var response = await _payslipService.DeleteAsync(id);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return NoContent();
    }
}

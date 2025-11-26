using HRManagementSystem.Application.Services;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
// [Authorize(Roles = "Admin,HR")]
public class PayslipController(IPayslipService payslipService, IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IPayslipService _payslipService = payslipService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private async Task<IEnumerable<Deduction>> GetFixedDeductionsAsync() => await _unitOfWork.Deductions.GetFixedDeductionsAsync();

    [HttpPost("{employeeId}/generate")]
    public async Task<IActionResult> GeneratePayslip(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<PayslipDto> response = await _payslipService.GeneratePayslipAsync(employeeId, month, year, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpPost("generate/month")]
    public async Task<IActionResult> GeneratePayslipsForMonth(int month, int year, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GeneratePayslipsForMonthAsync(month, year, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayslipById(int id, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<PayslipDto> response = await _payslipService.GetByIdAsync(id, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetPayslipsByEmployee(int employeeId, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GetByEmployeeAsync(employeeId, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("employee/{employeeId}/month")]
    public async Task<IActionResult> GetEmployeePayslipForMonth(int employeeId, [FromQuery] int month, [FromQuery] int year, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<PayslipDto> response = await _payslipService.GetEmployeePayslipForMonthAsync(employeeId, month, year, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("month")]
    public async Task<IActionResult> GetPayslipsByMonth(int month, int year, CancellationToken cancellationToken)
    {
        IEnumerable<Deduction> fixedDeductions = await GetFixedDeductionsAsync();
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GetByMonthAsync(month, year, fixedDeductions, cancellationToken);
        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayslip(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _payslipService.DeleteAsync(id, cancellationToken);
        return HandleResponse(response);
    }
}

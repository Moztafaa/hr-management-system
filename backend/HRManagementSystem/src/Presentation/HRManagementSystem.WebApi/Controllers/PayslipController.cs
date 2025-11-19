using System.Threading;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
//[Authorize(Roles = "Admin,HR")]

public class PayslipController(IPayslipService _payslipService) : BaseApiController
{

    [HttpPost("{employeeId}/generate")]
    public async Task<IActionResult> GeneratePayslip(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        Response<PayslipDto> response = await _payslipService.GeneratePayslipAsync(employeeId, month, year,cancellationToken);
        return HandleResponse(response);
    }

    [HttpPost("generate/month")]
    public async Task<IActionResult> GeneratePayslipsForMonth(int month, int year, CancellationToken cancellationToken)
    {
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GeneratePayslipsForMonthAsync(month, year,cancellationToken);
        return HandleResponse(response);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetPayslipById(int id, CancellationToken cancellationToken)
    //{
    //    Response<PayslipDto> response = await _payslipService.GetByIdAsync(id,cancellationToken);
    //    return HandleResponse(response);
    //}

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetPayslipsByEmployee(int employeeId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GetByEmployeeAsync(employeeId, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("employee/{employeeId}/month")]
    public async Task<IActionResult> GetEmployeePayslipForMonth(int employeeId, [FromQuery] int month, [FromQuery] int year, CancellationToken cancellationToken)
    {
        Response<PayslipDto> response = await _payslipService.GetEmployeePayslipForMonthAsync(employeeId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    [HttpGet("month")]
    public async Task<IActionResult> GetPayslipsByMonth(int month, int year, CancellationToken cancellationToken)
    {
        Response<IEnumerable<PayslipDto>> response = await _payslipService.GetByMonthAsync(month, year,cancellationToken);
        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayslip(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _payslipService.DeleteAsync(id, cancellationToken);
        return HandleResponse(response);
    }
}

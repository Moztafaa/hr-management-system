namespace HRManagementSystem.WebApi.Controllers;

[ApiController]
[Route("api/payslips")]
public class PayslipController : BaseApiController
{
    private readonly IPayslipService _payslipService;

    public PayslipController(IPayslipService payslipService)
    {
        _payslipService = payslipService;
    }

    // --------------------------------------------------------------------------------------------
    // 1) Generate Payslip for One Employee
    // --------------------------------------------------------------------------------------------
    [HttpPost("{employeeId}/generate")]
    public async Task<IActionResult> GeneratePayslip(
        int employeeId,
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var response = await _payslipService.GeneratePayslipAsync(employeeId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 2) Generate All Payslips for a Month
    // --------------------------------------------------------------------------------------------
    [HttpPost("generate-month")]
    public async Task<IActionResult> GeneratePayslipsForMonth(
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var response = await _payslipService.GeneratePayslipsForMonthAsync(month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 3) Regenerate Payslip (HR Feature)
    // --------------------------------------------------------------------------------------------
    [HttpPost("{employeeId}/regenerate")]
    public async Task<IActionResult> RegeneratePayslip(
        int employeeId,
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        // Same logic as generate � service will handle update vs create
        var response = await _payslipService.GeneratePayslipAsync(employeeId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 4) Get Payslip by ID
    // --------------------------------------------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayslipById(int id, CancellationToken cancellationToken)
    {
        var response = await _payslipService.GetByIdAsync(id, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 5) Get All Payslips for an Employee
    // --------------------------------------------------------------------------------------------
    [HttpGet("employee/{employeeId:int}")]
    public async Task<IActionResult> GetPayslipsByEmployee(int employeeId, CancellationToken cancellationToken)
    {
        var response = await _payslipService.GetByEmployeeAsync(employeeId, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 6) Get Monthly Payslip for Employee
    // --------------------------------------------------------------------------------------------
    [HttpGet("employee/{employeeId:int}/month")]
    public async Task<IActionResult> GetEmployeePayslipForMonth(
        int employeeId,
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var response = await _payslipService.GetEmployeePayslipForMonthAsync(employeeId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 7) Get All Payslips in a Month (Payroll Report)
    // --------------------------------------------------------------------------------------------
    [HttpGet("month")]
    public async Task<IActionResult> GetPayslipsByMonth(
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var response = await _payslipService.GetByMonthAsync(month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 8) Check if Payslip Exists (HR / UI validation)
    // --------------------------------------------------------------------------------------------
    [HttpGet("exists")]
    public async Task<IActionResult> PayslipExists(
        [FromQuery] int employeeId,
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var response = await _payslipService.ExistsAsync(employeeId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 9) Employee Self-Service: View Own Payslip
    // --------------------------------------------------------------------------------------------
    [HttpGet("self")]
    // [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetSelfPayslip(
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        int userId = GetCurrentUserId(); // BaseApiController helper
        var response = await _payslipService.GetEmployeePayslipForMonthAsync(userId, month, year, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 10) Delete Payslip
    // --------------------------------------------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePayslip(int id, CancellationToken cancellationToken)
    {
        var response = await _payslipService.DeleteAsync(id, cancellationToken);
        return HandleResponse(response);
    }

    // --------------------------------------------------------------------------------------------
    // 11) Export Payslip (PDF)
    // --------------------------------------------------------------------------------------------
    [HttpGet("{id:int}/export/pdf")]
    public async Task<IActionResult> ExportToPdf(int id, CancellationToken cancellationToken)
    {
        var fileResponse = await _payslipService.ExportToPdfAsync(id, cancellationToken);
        if (fileResponse.HasError)
            return HandleResponse(fileResponse);

        return File(fileResponse.Data.FileBytes, "application/pdf", fileResponse.Data.FileName);
    }

    // --------------------------------------------------------------------------------------------
    // 12) Export Monthly Payslips to Excel
    // --------------------------------------------------------------------------------------------
    [HttpGet("export/month/excel")]
    public async Task<IActionResult> ExportMonthToExcel(
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var fileResponse = await _payslipService.ExportMonthToExcelAsync(month, year, cancellationToken);
        if (fileResponse.HasError)
            return HandleResponse(fileResponse);

        return File(fileResponse.Data.FileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileResponse.Data.FileName);
    }
}

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
    {
        Response<IEnumerable<EmployeeDto>> employees = await employeeService.GetAllEmployeesAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
    {
        Response<EmployeeDto> employee = await employeeService.CreateEmployeeAsync(createEmployeeDto, cancellationToken);
        return Ok(employee);
    }
    [HttpPost("{employeeId}/update-image")]
    public async Task<IActionResult> UpdateImage(int employeeId, [FromForm] UpdateImageRequestDto request)
    //[FromForm] UpdateImageRequestDto request
    {
        using var ms = new MemoryStream();
        await request.Image.CopyToAsync(ms);
        var result = await employeeService.UpdateEmployeeImageAsync(employeeId, ms.ToArray());

        return Ok(new { errorMassage = result.ErrorMessage, employee = result.Data });
    }

}

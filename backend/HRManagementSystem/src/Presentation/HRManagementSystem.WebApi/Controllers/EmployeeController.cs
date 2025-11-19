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
    public async Task<IActionResult> UpdateImage(int employeeId, [FromBody] UpdateImageRequestDto request)
    {
        var result = await employeeService.UpdateEmployeeImageAsync(employeeId, request.Base64Image);
        if (result.HasError)
            return Ok(new { errorMassage = result.ErrorMessage, employee = result.Data });

        return Ok(new { errorMassage = result.ErrorMessage, employee = result.Data });
    }

}

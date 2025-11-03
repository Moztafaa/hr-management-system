namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
    {
        var employees = await employeeService.GetAllEmployeesAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
    {
        var employee = await employeeService.CreateEmployeeAsync(createEmployeeDto, cancellationToken);
        return Ok(employee);
    }


}

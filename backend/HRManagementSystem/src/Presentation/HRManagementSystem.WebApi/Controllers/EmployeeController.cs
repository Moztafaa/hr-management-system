using HRManagementSystem.Application.Common;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    [Authorize]
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


}

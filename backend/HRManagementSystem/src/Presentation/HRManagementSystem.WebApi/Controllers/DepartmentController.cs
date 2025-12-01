namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController(IDepartmentService _departmentService) : ControllerBase
{
    /*

Task<Response<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default);
Task<Response<DepartmentDto>> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default);
Task<Response<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken = default);
Task<Response<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto, CancellationToken cancellationToken = default);
Task<Response<bool>> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default);
Task<Response<DepartmentWithEmployeesDto>> GetDepartmentWithEmployeesAsync(int id, CancellationToken cancellationToken = default);
    */
    [HttpGet]
    public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
    {
        Response<IEnumerable<DepartmentDto>> response = await _departmentService.GetAllDepartmentsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id, CancellationToken cancellationToken)
    {
        Response<DepartmentDto> response = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken)
    {
        Response<DepartmentDto> response = await _departmentService.CreateDepartmentAsync(createDepartmentDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetDepartmentById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto, CancellationToken cancellationToken)
    {
        Response<bool> response = await _departmentService.UpdateDepartmentAsync(id, updateDepartmentDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _departmentService.DeleteDepartmentAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}/employees")]
    public async Task<IActionResult> GetDepartmentWithEmployees(int id, CancellationToken cancellationToken)
    {
        Response<DepartmentWithEmployeesDto> response = await _departmentService.GetDepartmentWithEmployeesAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }
}

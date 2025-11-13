namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
//[Authorize(Roles = "Admin,HR")] 
public class EmployeeAllowanceController(IEmployeeAllowanceService _employeeAllowanceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _employeeAllowanceService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _employeeAllowanceService.GetByIdAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeAllowanceDto request)
    {
        var response = await _employeeAllowanceService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeAllowanceDto request)
    {
        var response = await _employeeAllowanceService.UpdateAsync(id, request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _employeeAllowanceService.DeleteAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }
}

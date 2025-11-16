
namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
//[Authorize(Roles = "Admin,HR")]
public class EmployeeDeductionController(IEmployeeDeductionService _employeeDeductionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<IEnumerable<EmployeeDeductionDto>> response = await _employeeDeductionService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Response<EmployeeDeductionDto> response = await _employeeDeductionService.GetByIdAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDeductionDto request)
    {
        Response<EmployeeDeductionDto> response = await _employeeDeductionService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeDeductionDto request)
    {
        Response<EmployeeDeductionDto> response = await _employeeDeductionService.UpdateAsync(id, request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Response<bool> response = await _employeeDeductionService.DeleteAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }
}

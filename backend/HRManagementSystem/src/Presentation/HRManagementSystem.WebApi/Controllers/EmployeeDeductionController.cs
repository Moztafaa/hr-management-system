namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeDeductionController(IEmployeeDeductionService _employeeDeductionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _employeeDeductionService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{employeeId}/{deductionId}")]
    public async Task<IActionResult> GetByCompositeKey(int employeeId, int deductionId)
    {
        var response = await _employeeDeductionService.GetByCompositeKeyAsync(employeeId, deductionId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDeductionDto request)
    {
        var response = await _employeeDeductionService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return CreatedAtAction(nameof(GetByCompositeKey), new { employeeId = response.Data.EmployeeId, deductionId = response.Data.DeductionId }, response);
    }

    [HttpPut("{employeeId}/{deductionId}")]
    public async Task<IActionResult> Update(int employeeId, int deductionId, UpdateEmployeeDeductionDto request)
    {
        var response = await _employeeDeductionService.UpdateAsync(employeeId, deductionId, request);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{employeeId}/{deductionId}")]
    public async Task<IActionResult> Delete(int employeeId, int deductionId)
    {
        var response = await _employeeDeductionService.DeleteAsync(employeeId, deductionId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return NoContent();
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var response = await _employeeDeductionService.GetByEmployeeIdAsync(employeeId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

}

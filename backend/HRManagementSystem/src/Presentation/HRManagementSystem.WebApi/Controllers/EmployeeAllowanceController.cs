namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeAllowanceController(IEmployeeAllowanceService _employeeAllowanceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _employeeAllowanceService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{employeeId}/{allowanceId}")]
    public async Task<IActionResult> GetByCompositeKey(int employeeId, int allowanceId)
    {
        var response = await _employeeAllowanceService.GetByCompositeKeyAsync(employeeId, allowanceId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeAllowanceDto request)
    {
        var response = await _employeeAllowanceService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return CreatedAtAction(nameof(GetByCompositeKey), new { employeeId = response.Data.EmployeeId, allowanceId = response.Data.AllowanceId }, response);
    }

    [HttpPut("{employeeId}/{allowanceId}")]
    public async Task<IActionResult> Update(int employeeId, int allowanceId, UpdateEmployeeAllowanceDto request)
    {
        var response = await _employeeAllowanceService.UpdateAsync(employeeId, allowanceId, request);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{employeeId}/{allowanceId}")]
    public async Task<IActionResult> Delete(int employeeId, int allowanceId)
    {
        var response = await _employeeAllowanceService.DeleteAsync(employeeId, allowanceId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return NoContent();
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var response = await _employeeAllowanceService.GetByEmployeeIdAsync(employeeId);
        if (response.HasError)
            return BadRequest(new { response.HasError, response.ErrorMessage });

        return Ok(response);
    }

}

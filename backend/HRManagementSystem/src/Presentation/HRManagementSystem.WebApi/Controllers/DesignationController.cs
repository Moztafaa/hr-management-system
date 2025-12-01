namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignationController(IDesignationService _designationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllDesignations(CancellationToken cancellationToken)
    {
        Response<IEnumerable<DesignationDto>> response = await _designationService.GetAllDesignationsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDesignationById(int id, CancellationToken cancellationToken)
    {
        Response<DesignationDto> response = await _designationService.GetDesignationByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDesignation(CreateDesignationDto createDesignationDto, CancellationToken cancellationToken)
    {
        Response<DesignationDto> response = await _designationService.CreateDesignationAsync(createDesignationDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetDesignationById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDesignation(int id, UpdateDesignationDto updateDesignationDto, CancellationToken cancellationToken)
    {
        Response<bool> response = await _designationService.UpdateDesignationAsync(id, updateDesignationDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDesignation(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await _designationService.DeleteDesignationAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}/employees")]
    public async Task<IActionResult> GetDesignationWithEmployees(int id, CancellationToken cancellationToken)
    {
        Response<DesignationWithEmployeesDto> response = await _designationService.GetDesignationWithEmployeesAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }
}

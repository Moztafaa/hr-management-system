namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
//[Authorize(Roles = "Admin,HR")]
public class DeductionController(IDeductionService _deductionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<IEnumerable<DeductionDto>> response = await _deductionService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Response<DeductionDto> response = await _deductionService.GetByIdAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeductionDto request)
    {
        Response<DeductionDto> response = await _deductionService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDeductionDto request)
    {
        Response<DeductionDto> response = await _deductionService.UpdateAsync(id, request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Response<bool> response = await _deductionService.DeleteAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }
}

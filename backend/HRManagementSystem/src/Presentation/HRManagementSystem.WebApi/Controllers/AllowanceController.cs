namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
//[Authorize(Roles = "Admin,HR")]
public class AllowanceController(IAllowanceService _allowanceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<IEnumerable<AllowanceDto>> response = await _allowanceService.GetAllAsync();
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Response<AllowanceDto> response = await _allowanceService.GetByIdAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAllowanceDto request)
    {
        Response<AllowanceDto> response = await _allowanceService.CreateAsync(request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAllowanceDto request)
    {
        Response<AllowanceDto> response = await _allowanceService.UpdateAsync(id, request);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Response<bool> response = await _allowanceService.DeleteAsync(id);
        if (response.HasError)
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });

        return NoContent();
    }
}

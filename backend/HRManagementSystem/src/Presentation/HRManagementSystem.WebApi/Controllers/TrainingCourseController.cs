using HRManagementSystem.Application.DTOs.Training;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
public class TrainingCourseController(ITrainingCourseService trainingCourseService) : BaseApiController
{
    private readonly ITrainingCourseService _trainingCourseService = trainingCourseService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<IEnumerable<TrainingCourseDto>> response =await _trainingCourseService.GetAllAsync();
        return HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Response<TrainingCourseDto> response = await _trainingCourseService.GetByIdAsync(id);
        return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TrainingCourseCreateDto dto)
    {
        Response<TrainingCourseDto> response = await _trainingCourseService.CreateAsync(dto);
        return HandleResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TrainingCourseUpdateDto dto)
    {
        Response<TrainingCourseDto> response = await _trainingCourseService.UpdateAsync(id, dto);
        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _trainingCourseService.DeleteAsync(id);
        var response = new Response<bool>(true, string.Empty, false);
        return HandleResponse(response);
    }
}

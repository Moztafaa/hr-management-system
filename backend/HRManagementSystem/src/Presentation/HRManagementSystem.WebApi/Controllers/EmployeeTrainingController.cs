using HRManagementSystem.Application.DTOs.Training;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
public class EmployeeTrainingController(IEmployeeTrainingService employeeTrainingService) : BaseApiController
{
    private readonly IEmployeeTrainingService _employeeTrainingService = employeeTrainingService;

    [HttpPost("enroll")]
    public async Task<IActionResult> Enroll([FromBody] EmployeeEnrollDto dto)
    {
        Response<EmployeeTrainingDto> response = await _employeeTrainingService.EnrollAsync(dto);
        return HandleResponse(response);
    }

    [HttpPost("{employeeId}/{courseId}/complete")]
    public async Task<IActionResult> Complete(int employeeId, int courseId)
    {
        Response<bool> response = await _employeeTrainingService.CompleteAsync(employeeId, courseId);
        return HandleResponse(response);
    }

    [HttpPost("{employeeId}/{courseId}/cancel")]
    public async Task<IActionResult> Cancel(int employeeId, int courseId)
    {
        Response<bool> response = await _employeeTrainingService.CancelAsync(employeeId, courseId);
        return HandleResponse(response);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        IEnumerable<EmployeeTrainingDto> data = await _employeeTrainingService.GetEnrollmentsByEmployeeAsync(employeeId);
        var response = new Response<IEnumerable<EmployeeTrainingDto>>(data, "", false);

        return HandleResponse(response);
    }

    [HttpGet("by-course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        IEnumerable<EmployeeTrainingDto> data = await _employeeTrainingService.GetEnrollmentsByCourseAsync(courseId);
        var response = new Response<IEnumerable<EmployeeTrainingDto>>(data, "", false);

        return HandleResponse(response);
    }

}

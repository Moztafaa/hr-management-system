using HRManagementSystem.Application.DTOs.Training;
using HRManagementSystem.Domain.Enums;


namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainingRequestController(ITrainingRequestService trainingRequestService) : BaseApiController
{
    private readonly ITrainingRequestService _trainingRequestService = trainingRequestService;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] TrainingRequestCreateDto dto)
    {
        Response<TrainingRequestDto> response = await _trainingRequestService.CreateRequestAsync(dto);
        return HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Response<TrainingRequestDto> response =
            await _trainingRequestService.GetRequestByIdAsync(id);

        return HandleResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<IEnumerable<TrainingRequestDto>> response =
            await _trainingRequestService.GetAllRequestsAsync();

        return HandleResponse(response);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        Response<IEnumerable<TrainingRequestDto>> response =
            await _trainingRequestService.GetRequestsByEmployeeAsync(employeeId);

        return HandleResponse(response);
    }

    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetByStatus(TrainingRequestStatus status)
    {
        Response<IEnumerable<TrainingRequestDto>> response =
            await _trainingRequestService.GetRequestsByStatusAsync(status);

        return HandleResponse(response);
    }

    [HttpPost("{requestId}/review")]
    public async Task<IActionResult> Review(
        int requestId,
        [FromQuery] int managerId,
        [FromQuery] bool approve,
        [FromBody] string? managerNote = null)
    {
        Response<TrainingRequestDto> response =
            await _trainingRequestService.ReviewRequestAsync(requestId, managerId, approve, managerNote);

        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Response<bool> response = await _trainingRequestService.DeleteRequestAsync(id);
        return HandleResponse(response);
    }

}

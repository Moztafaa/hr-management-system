namespace HRManagementSystem.Application.Interfaces;

public interface ITrainingRequestService
{
    Task<Response<TrainingRequestDto>> CreateRequestAsync(TrainingRequestCreateDto dto, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<TrainingRequestDto>>> GetRequestsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<TrainingRequestDto>>> GetRequestsByStatusAsync(TrainingRequestStatus status, CancellationToken cancellationToken = default);
    Task<Response<TrainingRequestDto>> ReviewRequestAsync(int requestId, int managerId, bool approve, string? managerNote = null, CancellationToken cancellationToken = default);
    Task<Response<TrainingRequestDto>> GetRequestByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<TrainingRequestDto>>> GetAllRequestsAsync(CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteRequestAsync(int id, CancellationToken cancellationToken = default);

}


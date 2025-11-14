namespace HRManagementSystem.Application.Interfaces.ILeaveServices;
public interface ILeaveRequestService
{
    Task<Response<IEnumerable<LeaveRequestDto>>> GetAllLeaveRequestsAsync(CancellationToken cancellationToken = default);
    Task<Response<LeaveRequestDto>> GetLeaveRequestByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<LeaveRequestDto>> CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateLeaveRequestAsync(int id, UpdateLeaveRequestDto updateLeaveRequestDto,int flag = 0, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteLeaveRequestAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<LeaveRequestDto>>> GetLeaveRequestByEmployeeIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<LeaveRequestDto>>> GetLeaveRequestByReviewerIdAsync(int id, CancellationToken cancellationToken = default);

}

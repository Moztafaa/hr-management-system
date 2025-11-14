using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces.LeaveRepository;
public interface ILeaveRequestRepository : IRepository<LeaveRequest>
{
    Task<IEnumerable<LeaveRequest>> GetAllByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LeaveRequest>> GetAllByReviewerIdAsync(int reviewerId, CancellationToken cancellationToken = default);

}

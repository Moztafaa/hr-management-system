using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Interfaces;
public interface ILeaveApprovalRepository : IRepository<LeaveApproval>
{
    Task<LeaveApproval?> GetByRequestAndApproverAsync(int leaveRequestId, int approverId, LevelApproval level, CancellationToken cancellationToken = default);
    Task<LeaveApproval> GetByLeaveRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LeaveApproval>> GetAllByApproverIdAsync(int approverId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LeaveApproval>> GetAllByRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken = default);
}

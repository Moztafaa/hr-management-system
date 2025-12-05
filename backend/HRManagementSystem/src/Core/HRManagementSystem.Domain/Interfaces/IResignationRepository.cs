using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IResignationRepository : IRepository<Resignation>
{
    Task<IEnumerable<Resignation>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Resignation>> GetPendingByApproverIdAsync(int approverId, CancellationToken cancellationToken = default);
    Task<Resignation?> GetActiveByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Resignation?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Resignation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}

using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Interfaces;

public interface IResignationApprovalRepository : IRepository<ResignationApproval>
{
    Task<ResignationApproval?> GetByResignationAndApproverAsync(int resignationId, int approverId, LevelApproval level, CancellationToken cancellationToken = default);
    Task<IEnumerable<ResignationApproval>> GetAllByResignationIdAsync(int resignationId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ResignationApproval>> GetAllByApproverIdAsync(int approverId, CancellationToken cancellationToken = default);
}

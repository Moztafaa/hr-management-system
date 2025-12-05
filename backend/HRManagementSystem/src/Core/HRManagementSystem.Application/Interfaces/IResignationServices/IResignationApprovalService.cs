using HRManagementSystem.Application.DTOs.Resignation;

namespace HRManagementSystem.Application.Interfaces.IResignationServices;

public interface IResignationApprovalService
{
    Task<Response<bool>> ApproveAsync(ResignationApprovalActionDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> RejectAsync(ResignationApprovalActionDto dto, string rejectionReason, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ResignationApprovalDto>>> GetAllApprovalsByResignationIdAsync(int resignationId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ResignationApprovalDto>>> GetAllApprovalsByApproverIdAsync(int approverId, CancellationToken cancellationToken = default);
}

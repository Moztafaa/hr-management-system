namespace HRManagementSystem.Application.Interfaces.ILeaveServices;
public interface ILeaveApprovalService
{
    Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto);
    Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto);
    Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByRequestIdAsync(int leaveRequestId);
    Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByApproverIdAsync(int approverId);

}

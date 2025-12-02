using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Domain.Enums.Notification;

namespace HRManagementSystem.Application.Services.LeaveServices;

public class LeaveApprovalService(
        IUnitOfWork _unitOfWork,
        ILeaveRequestService _leaveRequestService,
        ILeaveBalanceService _leaveBalanceService,
        IMapper _mapper,
        IBackgroundJobService _backgroundJobService,
        IEmployeeService _employeeService,
        INotificationService _notificationService
    ) : ILeaveApprovalService
{

    public async Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto)
    {
        try
        {
            // Get the approval step
            LeaveApproval? approval = await _unitOfWork.LeaveApprovals.GetByRequestAndApproverAsync(dto.LeaveRequestId, dto.ApproverId, dto.Level);
            if (approval == null || approval.ActionDate != null)
                return new Response<bool>(false, "Approval step not found or already processed", true);

            approval.ActionDate = DateTime.UtcNow;
            approval.Status = LeaveStatus.Approved;
            await _unitOfWork.LeaveApprovals.UpdateAsync(approval);
            await _unitOfWork.SaveChangesAsync();

            // Check if there are still pending approval steps
            IEnumerable<LeaveApproval> allSteps = await _unitOfWork.LeaveApprovals.GetAllByRequestIdAsync(dto.LeaveRequestId);
            bool stillPending = allSteps.Any(a => a.ActionDate == null);
            if (stillPending)
                return new Response<bool>(true, "Approval recorded. Waiting for higher-level approvals.", false);

            // Update LeaveRequest and balance
            Response<LeaveRequestDto> respose = await _leaveRequestService.GetLeaveRequestByIdAsync(dto.LeaveRequestId);
            if (respose.HasError)
                return new Response<bool>(false, "Leave request not found", true);

            LeaveRequestDto leaveRequest = respose.Data;

            Response<bool> deductResult = await _leaveBalanceService.DeductLeaveDaysAsync(
                leaveRequest.EmployeeId,
                leaveRequest.LeaveTypeId,
                leaveRequest.TotalDays
            );

            if (deductResult.HasError)
                return new Response<bool>(false, $"Failed to deduct leave balance: {deductResult.ErrorMessage}", true);

            UpdateLeaveRequestDto updateLeaveReqdto = _mapper.Map<UpdateLeaveRequestDto>(leaveRequest);
            await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest.Id, updateLeaveReqdto, 1);

            // Send email notification to employee via background job
            Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(leaveRequest.EmployeeId);
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.Email))
            {
                // Queue email to be sent asynchronously
                _backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendLeaveApprovalEmailAsync(
                        employee.Data.Email,
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        "Leave Request",
                        "Approved"));
            }

            // Create notification for employee
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.ApplicationUserId))
            {
                await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.Data.ApplicationUserId,
                    Title = "Leave Request Approved",
                    Message = $"Your leave request has been approved. Enjoy your time off!",
                    Type = NotificationType.Success,
                    Category = NotificationCategory.LeaveApproval,
                    Priority = NotificationPriority.High,
                    RelatedEntityId = leaveRequest.Id,
                    RelatedEntityType = "LeaveRequest"
                }, CancellationToken.None);
            }

            return new Response<bool>(true, "Leave request fully approved.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to approve leave: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByApproverIdAsync(int approverId)
    {
        try
        {
            IEnumerable<LeaveApproval> approvals = await _unitOfWork.LeaveApprovals.GetAllByApproverIdAsync(approverId);
            IEnumerable<LeaveApprovalDto>? approverApprovals = _mapper.Map<IEnumerable<LeaveApprovalDto>>(approvals);


            return new Response<IEnumerable<LeaveApprovalDto>>(approverApprovals, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveApprovalDto>>(null, $"Failed to get approvals for approver: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByRequestIdAsync(int leaveRequestId)
    {
        try
        {
            IEnumerable<LeaveApproval> approvals = await _unitOfWork.LeaveApprovals.GetAllByRequestIdAsync(leaveRequestId);
            IEnumerable<LeaveApprovalDto>? requestApprovals = _mapper.Map<IEnumerable<LeaveApprovalDto>>(approvals);

            return new Response<IEnumerable<LeaveApprovalDto>>(requestApprovals, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveApprovalDto>>(null, $"Failed to get approvals for leave request: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto)
    {
        try
        {

            LeaveApproval? approval = await _unitOfWork.LeaveApprovals.GetByRequestAndApproverAsync(dto.LeaveRequestId, dto.ApproverId, dto.Level);
            if (approval == null || approval.ActionDate != null)
                return new Response<bool>(false, "Approval step not found or already processed", true);

            approval.ActionDate = DateTime.UtcNow;
            approval.Status = LeaveStatus.Rejected;
            await _unitOfWork.LeaveApprovals.UpdateAsync(approval);
            await _unitOfWork.SaveChangesAsync();

            IEnumerable<LeaveApproval> allSteps = await _unitOfWork.LeaveApprovals.GetAllByRequestIdAsync(dto.LeaveRequestId);

            foreach (LeaveApproval step in allSteps.Where(a => a.ActionDate == null))
            {
                step.ActionDate = DateTime.UtcNow;
                step.Status = LeaveStatus.Rejected;
                await _unitOfWork.LeaveApprovals.UpdateAsync(step);
            }
            await _unitOfWork.SaveChangesAsync();

            Response<LeaveRequestDto> leaveRequestResponse = await _leaveRequestService.GetLeaveRequestByIdAsync(dto.LeaveRequestId);
            if (leaveRequestResponse.HasError)
                return new Response<bool>(false, "Leave request not found", true);

            LeaveRequestDto leaveRequest = leaveRequestResponse.Data;

            leaveRequest.Status = (int)LeaveStatus.Rejected;
            leaveRequest.ReviewedAt = null;
            leaveRequest.ReviewedById = dto.ApproverId;

            UpdateLeaveRequestDto updateLeaveReqdto = _mapper.Map<UpdateLeaveRequestDto>(leaveRequest);
            await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest.Id, updateLeaveReqdto, 2);

            // Send email notification to employee via background job
            Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(leaveRequest.EmployeeId);
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.Email))
            {
                // Queue email to be sent asynchronously
                _backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendLeaveApprovalEmailAsync(
                        employee.Data.Email,
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        "Leave Request",
                        "Rejected"));
            }

            // Create notification for employee
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.ApplicationUserId))
            {
                await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.Data.ApplicationUserId,
                    Title = "Leave Request Rejected",
                    Message = $"Your leave request has been rejected. Please contact your manager for more details.",
                    Type = NotificationType.Error,
                    Category = NotificationCategory.LeaveApproval,
                    Priority = NotificationPriority.High,
                    RelatedEntityId = leaveRequest.Id,
                    RelatedEntityType = "LeaveRequest"
                }, CancellationToken.None);
            }

            return new Response<bool>(true, "Leave request rejected successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to reject leave: {ex.Message}", true);
        }
    }
}

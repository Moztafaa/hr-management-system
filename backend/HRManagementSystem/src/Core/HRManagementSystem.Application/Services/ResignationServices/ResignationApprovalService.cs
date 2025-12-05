using AutoMapper;
using HRManagementSystem.Application.DTOs.Employee;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Application.DTOs.Resignation;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Application.Interfaces.IResignationServices;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Enums.Notification;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services.ResignationServices;

public class ResignationApprovalService(
    IUnitOfWork _unitOfWork,
    IResignationService _resignationService,
    IMapper _mapper,
    IBackgroundJobService _backgroundJobService,
    IEmployeeService _employeeService,
    INotificationService _notificationService) : IResignationApprovalService
{
    public async Task<Response<bool>> ApproveAsync(ResignationApprovalActionDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Get the approval step
            ResignationApproval? approval = await _unitOfWork.ResignationApprovals
                .GetByResignationAndApproverAsync(dto.ResignationId, dto.ApproverId, dto.Level, cancellationToken);

            if (approval == null || approval.ActionDate != null)
                return new Response<bool>(false, "Approval step not found or already processed.", true);

            approval.ActionDate = DateTime.UtcNow;
            approval.Status = ResignationStatus.Approved;
            approval.Comments = dto.Comments;

            await _unitOfWork.ResignationApprovals.UpdateAsync(approval, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Check if this is manager-level approval and we need HR approval next
            if (dto.Level == LevelApproval.Manager)
            {
                // Get HR manager/admin to add as next approver (you may need to customize this based on your HR structure)
                // For now, we'll mark the resignation as fully approved after manager approval
                // If you need HR approval, you would create another approval step here
            }

            // Check if there are still pending approval steps
            IEnumerable<ResignationApproval> allSteps = await _unitOfWork.ResignationApprovals
                .GetAllByResignationIdAsync(dto.ResignationId, cancellationToken);

            bool stillPending = allSteps.Any(a => a.ActionDate == null);
            if (stillPending)
                return new Response<bool>(true, "Approval recorded. Waiting for higher-level approvals.", false);

            // Update Resignation status to Approved
            Resignation? resignation = await _unitOfWork.Resignations.GetByIdAsync(dto.ResignationId, cancellationToken);
            if (resignation == null)
                return new Response<bool>(false, "Resignation not found.", true);

            resignation.Status = ResignationStatus.Approved;
            resignation.ReviewedAt = DateTime.UtcNow;
            resignation.ReviewedById = dto.ApproverId;
            resignation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Resignations.UpdateAsync(resignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification to employee via background job
            Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(resignation.EmployeeId, cancellationToken);
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.Email))
            {
                _backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendResignationNotificationEmailAsync(
                        employee.Data.Email,
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        "Approved",
                        resignation.LastWorkingDate));
            }

            // Create notification for employee
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.ApplicationUserId))
            {
                await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.Data.ApplicationUserId,
                    Title = "Resignation Approved",
                    Message = $"Your resignation request has been approved. Your last working date is {resignation.LastWorkingDate:yyyy-MM-dd}.",
                    Type = NotificationType.Success,
                    Category = NotificationCategory.ResignationApproval,
                    Priority = NotificationPriority.High,
                    RelatedEntityId = resignation.Id,
                    RelatedEntityType = "Resignation"
                }, cancellationToken);
            }

            return new Response<bool>(true, "Resignation approved successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to approve resignation: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> RejectAsync(ResignationApprovalActionDto dto, string rejectionReason, CancellationToken cancellationToken = default)
    {
        try
        {
            ResignationApproval? approval = await _unitOfWork.ResignationApprovals
                .GetByResignationAndApproverAsync(dto.ResignationId, dto.ApproverId, dto.Level, cancellationToken);

            if (approval == null || approval.ActionDate != null)
                return new Response<bool>(false, "Approval step not found or already processed.", true);

            approval.ActionDate = DateTime.UtcNow;
            approval.Status = ResignationStatus.Rejected;
            approval.Comments = dto.Comments ?? rejectionReason;

            await _unitOfWork.ResignationApprovals.UpdateAsync(approval, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Mark all remaining approval steps as rejected
            IEnumerable<ResignationApproval> allSteps = await _unitOfWork.ResignationApprovals
                .GetAllByResignationIdAsync(dto.ResignationId, cancellationToken);

            foreach (ResignationApproval step in allSteps.Where(a => a.ActionDate == null))
            {
                step.ActionDate = DateTime.UtcNow;
                step.Status = ResignationStatus.Rejected;
                await _unitOfWork.ResignationApprovals.UpdateAsync(step, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Update Resignation status to Rejected
            Resignation? resignation = await _unitOfWork.Resignations.GetByIdAsync(dto.ResignationId, cancellationToken);
            if (resignation == null)
                return new Response<bool>(false, "Resignation not found.", true);

            resignation.Status = ResignationStatus.Rejected;
            resignation.ReviewedAt = DateTime.UtcNow;
            resignation.ReviewedById = dto.ApproverId;
            resignation.RejectionReason = rejectionReason;
            resignation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Resignations.UpdateAsync(resignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification to employee via background job
            Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(resignation.EmployeeId, cancellationToken);
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.Email))
            {
                _backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendResignationNotificationEmailAsync(
                        employee.Data.Email,
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        $"{employee.Data.FirstName} {employee.Data.LastName}",
                        "Rejected",
                        resignation.LastWorkingDate));
            }

            // Create notification for employee
            if (!employee.HasError && !string.IsNullOrEmpty(employee.Data.ApplicationUserId))
            {
                await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.Data.ApplicationUserId,
                    Title = "Resignation Rejected",
                    Message = $"Your resignation request has been rejected. Reason: {rejectionReason}",
                    Type = NotificationType.Error,
                    Category = NotificationCategory.ResignationApproval,
                    Priority = NotificationPriority.High,
                    RelatedEntityId = resignation.Id,
                    RelatedEntityType = "Resignation"
                }, cancellationToken);
            }

            return new Response<bool>(true, "Resignation rejected successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to reject resignation: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<ResignationApprovalDto>>> GetAllApprovalsByResignationIdAsync(int resignationId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<ResignationApproval> approvals = await _unitOfWork.ResignationApprovals
                .GetAllByResignationIdAsync(resignationId, cancellationToken);
            IEnumerable<ResignationApprovalDto> dtos = _mapper.Map<IEnumerable<ResignationApprovalDto>>(approvals);
            return new Response<IEnumerable<ResignationApprovalDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ResignationApprovalDto>>(null!, $"Failed to get approvals: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<ResignationApprovalDto>>> GetAllApprovalsByApproverIdAsync(int approverId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<ResignationApproval> approvals = await _unitOfWork.ResignationApprovals
                .GetAllByApproverIdAsync(approverId, cancellationToken);
            IEnumerable<ResignationApprovalDto> dtos = _mapper.Map<IEnumerable<ResignationApprovalDto>>(approvals);
            return new Response<IEnumerable<ResignationApprovalDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ResignationApprovalDto>>(null!, $"Failed to get approvals for approver: {ex.Message}", true);
        }
    }
}

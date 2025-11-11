namespace HRManagementSystem.Application.Services.LeaveServices;
public class LeaveApprovalService(
        ILeaveApprovalRepository _leaveApprovalRepo,
        ILeaveRequestService _leaveRequestService,
        ILeaveRequestRepository _leaveRequestRepository,
        ILeaveBalanceService _leaveBalanceService,
        IMapper _mapper
    ) : ILeaveApprovalService
{

    public async Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto)
    {
        try
        {
            // Get the approval step
            LeaveApproval? approval = await _leaveApprovalRepo.GetByRequestAndApproverAsync(dto.LeaveRequestId, dto.ApproverId, dto.Level);
            if (approval == null || approval.ActionDate != null)
                 return new Response<bool>(false, "Approval step not found or already processed", true);

            approval.ActionDate = DateTime.UtcNow;
            approval.Status = LeaveStatus.Approved;
            await _leaveApprovalRepo.UpdateAsync(approval);

            // Check if all steps are done
            IEnumerable<LeaveApproval> allSteps = await _leaveApprovalRepo.GetAllByRequestIdAsync(dto.LeaveRequestId);
            bool stillPending = allSteps.Any(a => a.ActionDate == null);
            if (stillPending)
                return new Response<bool>(true, null, false);

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
                return new Response<bool>(false, deductResult.ErrorMessage, true);

            UpdateLeaveRequestDto updateLeaveReqdto = _mapper.Map<UpdateLeaveRequestDto>(leaveRequest);
            await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest.Id, updateLeaveReqdto, 1);

            return new Response<bool>(true, null, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to approve: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByApproverIdAsync(int approverId)
    {
        try
        {
            IEnumerable<LeaveApproval> approvals = await _leaveApprovalRepo.GetAllByApproverIdAsync(approverId);
            IEnumerable<LeaveApprovalDto>? approverApprovals = _mapper.Map<IEnumerable<LeaveApprovalDto>>(approvals);


            return new Response<IEnumerable<LeaveApprovalDto>>(approverApprovals, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveApprovalDto>>(null, $"Failed to get approvals: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveApprovalDto>>> GetAllApprovalsByRequestIdAsync(int leaveRequestId)
    {
        try
        {
            IEnumerable<LeaveApproval> approvals = await _leaveApprovalRepo.GetAllByRequestIdAsync(leaveRequestId);
            IEnumerable<LeaveApprovalDto>? requestApprovals = _mapper.Map<IEnumerable<LeaveApprovalDto>>(approvals);

            return new Response<IEnumerable<LeaveApprovalDto>>(requestApprovals, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveApprovalDto>>(null, $"Failed to get approvals: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto)
    {
        try
        {
           
            LeaveApproval? approval = await _leaveApprovalRepo.GetByRequestAndApproverAsync(dto.LeaveRequestId, dto.ApproverId, dto.Level);
            if (approval == null || approval.ActionDate != null)
                return new Response<bool>(false, "Approval step not found or already processed", true);

            approval.ActionDate = DateTime.UtcNow;
            await _leaveApprovalRepo.UpdateAsync(approval);

            IEnumerable<LeaveApproval> allSteps = await _leaveApprovalRepo.GetAllByRequestIdAsync(dto.LeaveRequestId);

            foreach (LeaveApproval step in allSteps.Where(a => a.ActionDate == null))
            {
                step.ActionDate = DateTime.UtcNow;
                await _leaveApprovalRepo.UpdateAsync(step);
            }

            Response<LeaveRequestDto> leaveRequestResponse = await _leaveRequestService.GetLeaveRequestByIdAsync(dto.LeaveRequestId);
            if (leaveRequestResponse.HasError)
                return new Response<bool>(false, "Leave request not found", true);

            LeaveRequestDto leaveRequest = leaveRequestResponse.Data;

            leaveRequest.Status = (int)LeaveStatus.Rejected;
            leaveRequest.ReviewedAt = null;
            leaveRequest.ReviewedById = dto.ApproverId;

            UpdateLeaveRequestDto updateLeaveReqdto = _mapper.Map<UpdateLeaveRequestDto>(leaveRequest);
            await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest.Id, updateLeaveReqdto, 2);


            return new Response<bool>(true, null, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to reject: {ex.Message}", true);
        }
    }
}

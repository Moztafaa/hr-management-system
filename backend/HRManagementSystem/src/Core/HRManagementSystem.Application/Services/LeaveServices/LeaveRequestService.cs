namespace HRManagementSystem.Application.Services.LeaveServices;

public class LeaveRequestService(
    ILeaveRequestRepository _LeaveRequestRepository,
    IEmployeeService _employeeService,
    IDepartmentService _departmentService,
    ILeaveApprovalRepository _leaveApprovalRepository,
    IMapper _mapper) : ILeaveRequestService
{
    public async Task<Response<LeaveRequestDto>> CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            DateTime start = createLeaveRequestDto.StartDate.Date;
            DateTime end = createLeaveRequestDto.EndDate.Date;

            Response<LeaveRequestDto> validationResult =
                await CanCreateOrUpdate(createLeaveRequestDto.EmployeeId, -1, start, end, cancellationToken);

            if (validationResult.HasError)
                return validationResult;

            int totalDays = (int)(end - start).TotalDays + 1;

            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(createLeaveRequestDto);
            leaveRequest.TotalDays = totalDays;
            leaveRequest.CreatedAt = DateTime.UtcNow;
            Response<EmployeeDto> emp =
                await _employeeService.GetEmployeeByIdAsync(createLeaveRequestDto.EmployeeId, cancellationToken);
            if (emp.HasError)
                return new Response<LeaveRequestDto>(default!, "Employee not found.", true);

            Response<DepartmentDto> dept = await _departmentService.GetDepartmentByIdAsync(emp.Data.DeptId);
            if (dept.HasError)
                return new Response<LeaveRequestDto>(default!, "Department not found for this employee.", true);
            if (dept.Data.ManagerId==null)
                return new Response<LeaveRequestDto>(default!, "No manager assigned to this department.", true);

            leaveRequest.ReviewedById = dept.Data.ManagerId;

            await _LeaveRequestRepository.AddAsync(leaveRequest, cancellationToken);

            LeaveApproval approval = new LeaveApproval
            {
                LeaveRequestId = leaveRequest.Id,
                ApproverId = (int)leaveRequest.ReviewedById,
                Level = LevelApproval.Manager,
                Status = LeaveStatus.Pending,
                ActionDate = null
            };
            await _leaveApprovalRepository.AddAsync(approval);
            LeaveRequestDto result = _mapper.Map<LeaveRequestDto>(leaveRequest);

            return new Response<LeaveRequestDto>(result, null, false);
        }
        catch (Exception ex)
        {
            return new Response<LeaveRequestDto>(default!, $"Failed to create leave request: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteLeaveRequestAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            LeaveRequest? LeaveRequest = await _LeaveRequestRepository.GetByIdAsync(id, cancellationToken);
            if (LeaveRequest == null)
            {
                return new Response<bool>(false, "LeaveRequest not found.", true);
            }

            if (LeaveRequest.Status != Domain.Enums.LeaveStatus.Pending)
            {
                return new Response<bool>(false, "Cannot delete this leave request because it has already been reviewed.", true);
            }

            await _LeaveRequestRepository.DeleteAsync(LeaveRequest.Id, cancellationToken);
            return new Response<bool>(true, "Leave request deleted successfully.", false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Failed to delete leave request: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveRequestDto>>> GetAllLeaveRequestsAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<LeaveRequest> list = await _LeaveRequestRepository.GetAllAsync(cancellationToken);
            IEnumerable<LeaveRequestDto> dtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(list);
            return new Response<IEnumerable<LeaveRequestDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveRequestDto>>(null, $"Failed to load leave requests: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveRequestDto>>> GetLeaveRequestByEmployeeIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<LeaveRequest> list =
                await _LeaveRequestRepository.GetAllByEmployeeIdAsync(id, cancellationToken);
            IEnumerable<LeaveRequestDto> dtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(list);
            return new Response<IEnumerable<LeaveRequestDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveRequestDto>>(null, $"Failed to load employee leave requests: {ex.Message}", true);
        }
    }

    public async Task<Response<LeaveRequestDto>> GetLeaveRequestByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            LeaveRequest leaveRequest = await _LeaveRequestRepository.GetByIdAsync(id, cancellationToken);
            if (leaveRequest == null)
                return new Response<LeaveRequestDto>(null, "Leave request not found.", true);

            LeaveRequestDto leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);
            return new Response<LeaveRequestDto>(leaveRequestDto, null, false);
        }
        catch (Exception ex)
        {
            return new Response<LeaveRequestDto>(null, $"Failed to retrieve leave request: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<LeaveRequestDto>>> GetLeaveRequestByReviewerIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<LeaveRequest> list =
                await _LeaveRequestRepository.GetAllByReviewerIdAsync(id, cancellationToken);
            IEnumerable<LeaveRequestDto> dtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(list);
            return new Response<IEnumerable<LeaveRequestDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveRequestDto>>(null, $"Failed to load requests: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> UpdateLeaveRequestAsync(int id, UpdateLeaveRequestDto updateLeaveRequestDto,
        int flag = 0, CancellationToken cancellationToken = default)
    {
        try
        {
            LeaveRequest? LeaveRequest = await _LeaveRequestRepository.GetByIdAsync(id, cancellationToken);
            if (LeaveRequest == null)
            {
                return new Response<bool>(false, "LeaveRequest not found.", true);
            }

            if (LeaveRequest.Status != LeaveStatus.Pending)
            {
                return new Response<bool>(false, "Cannot update this leave request because it has already been reviewed.", true);
            }

            DateTime start = updateLeaveRequestDto.StartDate.Date;
            DateTime end = updateLeaveRequestDto.EndDate.Date;

            Response<LeaveRequestDto> validation =
                await CanCreateOrUpdate(updateLeaveRequestDto.EmployeeId, id, start, end, cancellationToken);

            if (validation.HasError)
                return new Response<bool>(false, validation.ErrorMessage, true);
            int totalDays = (int)(end - start).TotalDays + 1;
            LeaveRequest.TotalDays = totalDays;
            LeaveRequest.UpdatedAt = DateTime.UtcNow;
            Response<EmployeeDto> emp =
                await _employeeService.GetEmployeeByIdAsync(updateLeaveRequestDto.EmployeeId, cancellationToken);
            Response<DepartmentDto> dept = await _departmentService.GetDepartmentByIdAsync(emp.Data.DeptId);
            LeaveRequest.ReviewedById = dept.Data.ManagerId;
            if (flag > 0)
            {
                LeaveRequest.Status = (LeaveStatus)flag;
                LeaveRequest.ReviewedAt = DateTime.UtcNow;
            }

            await _LeaveRequestRepository.UpdateAsync(LeaveRequest, cancellationToken);

            LeaveApproval approval =
                await _leaveApprovalRepository.GetByLeaveRequestIdAsync(LeaveRequest.Id, cancellationToken);
            approval.ApproverId = (int)LeaveRequest.ReviewedById;
            await _leaveApprovalRepository.UpdateAsync(approval);

            return new Response<bool>(true, null, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Failed to update leave request: {ex.Message}", true);
        }
    }


    // ========================= Utilities =========================

    public async Task<Response<bool>> HasOverlapAsync(int employeeId, DateTime start, DateTime end, int requestId)
    {
        try
        {
            IEnumerable<LeaveRequest> all = await _LeaveRequestRepository.GetAllByEmployeeIdAsync(employeeId);
            DateTime s = start.Date;
            DateTime e = end.Date;

            bool overlapped = all.Any(r =>
                r.Status != LeaveStatus.Approved &&
                r.StartDate <= e && r.EndDate >= s && r.Id != requestId);

            return new Response<bool>(overlapped, null, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to check leave overlap: {ex.Message}", true);
        }
    }

    private async Task<Response<LeaveRequestDto>> CanCreateOrUpdate(int employeeId, int requestId, DateTime start,
        DateTime end, CancellationToken cancellationToken = default)
    {
        Response<EmployeeDto> emp = await _employeeService.GetEmployeeByIdAsync(employeeId, cancellationToken);
        if (emp.HasError)
            return new Response<LeaveRequestDto>(default!, "Invalid employee ID.", true);

        if (start > end)
            return new Response<LeaveRequestDto>(default!, "Start date cannot be after end date.", true);

        Response<bool> overlap = await HasOverlapAsync(employeeId, start, end, requestId);
        if (overlap.HasError)
            return new Response<LeaveRequestDto>(default!, overlap.ErrorMessage, true);
        if (overlap.Data == true)
            return new Response<LeaveRequestDto>(default!, "Overlapping leave request already exists.", true);
        
        return new Response<LeaveRequestDto>(default!, null, false);
    }
}

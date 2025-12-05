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

public class ResignationService(
    IUnitOfWork _unitOfWork,
    IEmployeeService _employeeService,
    IDepartmentService _departmentService,
    IMapper _mapper,
    IBackgroundJobService _backgroundJobService,
    INotificationService _notificationService) : IResignationService
{
    public async Task<Response<ResignationDto>> CreateResignationAsync(CreateResignationDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if employee exists
            Response<EmployeeDto> emp = await _employeeService.GetEmployeeByIdAsync(dto.EmployeeId, cancellationToken);
            if (emp.HasError)
                return new Response<ResignationDto>(default!, "Employee not found.", true);

            // Check if employee already has an active resignation
            Response<ResignationDto?> existingResignation = await GetActiveResignationByEmployeeIdAsync(dto.EmployeeId, cancellationToken);
            if (!existingResignation.HasError && existingResignation.Data != null)
                return new Response<ResignationDto>(default!, "You already have an active resignation request.", true);

            // Validate last working date
            if (dto.LastWorkingDate.Date < DateTime.UtcNow.Date)
                return new Response<ResignationDto>(default!, "Last working date cannot be in the past.", true);

            // Get department manager
            Response<DepartmentDto> dept = await _departmentService.GetDepartmentByIdAsync(emp.Data.DeptId);
            if (dept.HasError)
                return new Response<ResignationDto>(default!, "Department not found for this employee.", true);
            if (dept.Data.ManagerId == null)
                return new Response<ResignationDto>(default!, "No manager assigned to this department.", true);

            // Create resignation
            Resignation resignation = _mapper.Map<Resignation>(dto);
            resignation.CreatedAt = DateTime.UtcNow;
            resignation.ReviewedById = dept.Data.ManagerId;

            await _unitOfWork.Resignations.AddAsync(resignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Create initial approval step for manager
            ResignationApproval approval = new ResignationApproval
            {
                ResignationId = resignation.Id,
                ApproverId = (int)resignation.ReviewedById,
                Level = LevelApproval.Manager,
                Status = ResignationStatus.Pending,
                ActionDate = null
            };
            await _unitOfWork.ResignationApprovals.AddAsync(approval, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification to manager via background job
            Response<EmployeeDto> manager = await _employeeService.GetEmployeeByIdAsync((int)resignation.ReviewedById, cancellationToken);
            if (!manager.HasError && !string.IsNullOrEmpty(manager.Data.Email))
            {
                _backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendResignationNotificationEmailAsync(
                        manager.Data.Email,
                        $"{manager.Data.FirstName} {manager.Data.LastName}",
                        $"{emp.Data.FirstName} {emp.Data.LastName}",
                        "Pending Review",
                        resignation.LastWorkingDate));
            }

            // Create notification for manager
            if (!manager.HasError && !string.IsNullOrEmpty(manager.Data.ApplicationUserId))
            {
                await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = manager.Data.ApplicationUserId,
                    Title = "New Resignation Request",
                    Message = $"{emp.Data.FirstName} {emp.Data.LastName} has submitted a resignation request with last working date: {resignation.LastWorkingDate:yyyy-MM-dd}.",
                    Type = NotificationType.Warning,
                    Category = NotificationCategory.Resignation,
                    Priority = NotificationPriority.High,
                    RelatedEntityId = resignation.Id,
                    RelatedEntityType = "Resignation"
                }, cancellationToken);
            }

            ResignationDto result = _mapper.Map<ResignationDto>(resignation);
            result.EmployeeName = $"{emp.Data.FirstName} {emp.Data.LastName}";

            return new Response<ResignationDto>(result, null, false);
        }
        catch (Exception ex)
        {
            return new Response<ResignationDto>(default!, $"Failed to create resignation request: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> WithdrawResignationAsync(WithdrawResignationDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            Resignation? resignation = await _unitOfWork.Resignations.GetByIdAsync(dto.ResignationId, cancellationToken);
            if (resignation == null)
                return new Response<bool>(false, "Resignation not found.", true);

            if (resignation.EmployeeId != dto.EmployeeId)
                return new Response<bool>(false, "You can only withdraw your own resignation.", true);

            if (resignation.Status != ResignationStatus.Pending)
                return new Response<bool>(false, "Only pending resignations can be withdrawn.", true);

            resignation.Status = ResignationStatus.Withdrawn;
            resignation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Resignations.UpdateAsync(resignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Notify the approver about the withdrawal
            if (resignation.ReviewedById.HasValue)
            {
                Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(resignation.EmployeeId, cancellationToken);
                Response<EmployeeDto> approver = await _employeeService.GetEmployeeByIdAsync(resignation.ReviewedById.Value, cancellationToken);

                if (!approver.HasError && !string.IsNullOrEmpty(approver.Data.ApplicationUserId))
                {
                    string employeeName = !employee.HasError ? $"{employee.Data.FirstName} {employee.Data.LastName}" : "An employee";

                    await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                    {
                        RecipientUserId = approver.Data.ApplicationUserId,
                        Title = "Resignation Withdrawn",
                        Message = $"{employeeName} has withdrawn their resignation request.",
                        Type = NotificationType.Info,
                        Category = NotificationCategory.Resignation,
                        Priority = NotificationPriority.Normal,
                        RelatedEntityId = resignation.Id,
                        RelatedEntityType = "Resignation"
                    }, cancellationToken);
                }
            }

            return new Response<bool>(true, "Resignation withdrawn successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to withdraw resignation: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteResignationAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Resignation? resignation = await _unitOfWork.Resignations.GetByIdAsync(id, cancellationToken);
            if (resignation == null)
                return new Response<bool>(false, "Resignation not found.", true);

            if (resignation.Status != ResignationStatus.Pending && resignation.Status != ResignationStatus.Withdrawn)
                return new Response<bool>(false, "Cannot delete a processed resignation.", true);

            await _unitOfWork.Resignations.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true, "Resignation deleted successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Failed to delete resignation: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<ResignationDto>>> GetAllResignationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Resignation> resignations = await _unitOfWork.Resignations.GetAllWithDetailsAsync(cancellationToken);
            IEnumerable<ResignationDto> dtos = _mapper.Map<IEnumerable<ResignationDto>>(resignations);
            return new Response<IEnumerable<ResignationDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ResignationDto>>(null!, $"Failed to load resignations: {ex.Message}", true);
        }
    }

    public async Task<Response<ResignationDto>> GetResignationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Resignation? resignation = await _unitOfWork.Resignations.GetByIdWithDetailsAsync(id, cancellationToken);
            if (resignation == null)
                return new Response<ResignationDto>(default!, "Resignation not found.", true);

            ResignationDto dto = _mapper.Map<ResignationDto>(resignation);
            return new Response<ResignationDto>(dto, null, false);
        }
        catch (Exception ex)
        {
            return new Response<ResignationDto>(default!, $"Failed to retrieve resignation: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<ResignationDto>>> GetResignationsByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Resignation> resignations = await _unitOfWork.Resignations.GetByEmployeeIdAsync(employeeId, cancellationToken);
            IEnumerable<ResignationDto> dtos = _mapper.Map<IEnumerable<ResignationDto>>(resignations);
            return new Response<IEnumerable<ResignationDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ResignationDto>>(null!, $"Failed to load employee resignations: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<ResignationDto>>> GetPendingResignationsForApproverAsync(int approverId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Resignation> resignations = await _unitOfWork.Resignations.GetPendingByApproverIdAsync(approverId, cancellationToken);
            IEnumerable<ResignationDto> dtos = _mapper.Map<IEnumerable<ResignationDto>>(resignations);
            return new Response<IEnumerable<ResignationDto>>(dtos, null, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ResignationDto>>(null!, $"Failed to load pending resignations: {ex.Message}", true);
        }
    }

    public async Task<Response<ResignationDto?>> GetActiveResignationByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            Resignation? resignation = await _unitOfWork.Resignations.GetActiveByEmployeeIdAsync(employeeId, cancellationToken);
            if (resignation == null)
                return new Response<ResignationDto?>(null, null, false);

            ResignationDto dto = _mapper.Map<ResignationDto>(resignation);
            return new Response<ResignationDto?>(dto, null, false);
        }
        catch (Exception ex)
        {
            return new Response<ResignationDto?>(null, $"Failed to retrieve active resignation: {ex.Message}", true);
        }
    }
}

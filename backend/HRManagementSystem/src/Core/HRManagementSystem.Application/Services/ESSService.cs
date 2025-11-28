using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.ESS;
using HRManagementSystem.Application.DTOs.Leaves;
using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Application.Interfaces.ILeaveServices;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.Services;

public class ESSService(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    ILeaveRequestService _leaveRequestService,
    ILeaveBalanceService _leaveBalanceService,
    IPayslipService _payslipService,
    IAccountService _accountService) : IESSService
{
    public async Task<Response<ESSProfileDto>> GetProfileAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(employeeId, cancellationToken);
            if (employee is null)
                return new Response<ESSProfileDto>(null!, "Employee not found", true);

            // Load related entities
            Department? department = employee.DepartmentId.HasValue
                ? await _unitOfWork.Repository<Department>().GetByIdAsync(employee.DepartmentId.Value, cancellationToken)
                : null;

            Designation? designation = employee.DesignationId.HasValue
                ? await _unitOfWork.Repository<Designation>().GetByIdAsync(employee.DesignationId.Value, cancellationToken)
                : null;

            var profileDto = new ESSProfileDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                Email = employee.Email,
                ContactNumber = employee.ContactNumber,
                Address = employee.Address,
                DeptId = employee.DepartmentId ?? 0,
                DesignationId = employee.DesignationId ?? 0,
                DepartmentName = department?.Name,
                DesignationName = designation?.Title
            };

            return new Response<ESSProfileDto>(profileDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<ESSProfileDto>(null!, ex.Message, true);
        }
    }

    public async Task<Response<bool>> UpdateProfileAsync(int employeeId, UpdateESSProfileDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            Employee? employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(employeeId, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            // Only allow updating specific fields for ESS
            employee.Email = dto.Email;
            employee.ContactNumber = dto.ContactNumber;
            employee.Address = dto.Address;

            // Synchronize email with ApplicationUser to maintain consistency
            if (!string.IsNullOrEmpty(employee.ApplicationUserId) && Guid.TryParse(employee.ApplicationUserId, out Guid userId))
            {
                Response<bool> emailUpdateResult = await _accountService.UpdateUserEmailAsync(userId, dto.Email);
                if (emailUpdateResult.HasError)
                {
                    return new Response<bool>(false, $"Failed to update user account email: {emailUpdateResult.ErrorMessage}", true);
                }
            }

            await _unitOfWork.Repository<Employee>().UpdateAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }

    public async Task<Response<List<LeaveRequestDto>>> GetLeaveHistoryAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            Response<IEnumerable<LeaveRequestDto>> response = await _leaveRequestService.GetLeaveRequestByEmployeeIdAsync(employeeId, cancellationToken);

            if (response.HasError)
                return new Response<List<LeaveRequestDto>>(null!, response.ErrorMessage, true);

            return new Response<List<LeaveRequestDto>>(response.Data?.ToList() ?? new List<LeaveRequestDto>(), string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<List<LeaveRequestDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<int>> SubmitLeaveRequestAsync(int employeeId, CreateLeaveRequestDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate that the employee ID in the DTO matches the authenticated employee
            if (dto.EmployeeId != employeeId)
                return new Response<int>(0, "You can only submit leave requests for yourself", true);

            Response<LeaveRequestDto> response = await _leaveRequestService.CreateLeaveRequestAsync(dto, cancellationToken);

            if (response.HasError)
                return new Response<int>(0, response.ErrorMessage, true);

            return new Response<int>(response.Data?.Id ?? 0, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, ex.Message, true);
        }
    }

    public async Task<Response<List<LeaveBalanceDto>>> GetLeaveBalancesAsync(int employeeId, int year, CancellationToken cancellationToken = default)
    {
        try
        {
            Response<IEnumerable<LeaveBalanceDto>> response = await _leaveBalanceService.GetByEmployeeIdAndYearAsync(employeeId, year, cancellationToken);

            if (response.HasError)
                return new Response<List<LeaveBalanceDto>>(null!, response.ErrorMessage, true);

            return new Response<List<LeaveBalanceDto>>(response.Data?.ToList() ?? new List<LeaveBalanceDto>(), string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<List<LeaveBalanceDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<List<PayslipDto>>> GetPayslipsAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            Response<IEnumerable<PayslipDto>> response = await _payslipService.GetByEmployeeAsync(employeeId, cancellationToken);

            if (response.HasError)
                return new Response<List<PayslipDto>>(null!, response.ErrorMessage, true);

            return new Response<List<PayslipDto>>(response.Data?.ToList() ?? new List<PayslipDto>(), string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<List<PayslipDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<PayslipDto>> GetPayslipByIdAsync(int employeeId, int payslipId, CancellationToken cancellationToken = default)
    {
        try
        {
            Payslip? payslip = await _unitOfWork.Repository<Payslip>().GetByIdAsync(payslipId, cancellationToken);

            if (payslip is null)
                return new Response<PayslipDto>(null!, "Payslip not found", true);

            // Verify the payslip belongs to the employee
            if (payslip.EmployeeId != employeeId)
                return new Response<PayslipDto>(null!, "You can only view your own payslips", true);

            PayslipDto payslipDto = _mapper.Map<PayslipDto>(payslip);
            return new Response<PayslipDto>(payslipDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, ex.Message, true);
        }
    }

    public async Task<Response<ESSDashboardDto>> GetDashboardAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dashboard = new ESSDashboardDto
            {
                Profile = null,
                LeaveBalances = new List<LeaveBalanceDto>(),
                RecentPayslips = new List<PayslipDto>(),
                PendingLeaveRequestsCount = 0,
                ApprovedLeaveRequestsCount = 0
            };

            // Get profile
            Response<ESSProfileDto> profileResponse = await GetProfileAsync(employeeId, cancellationToken);
            if (!profileResponse.HasError && profileResponse.Data != null)
            {
                dashboard.Profile = profileResponse.Data;
            }

            // Get leave balances for current year
            int currentYear = DateTime.UtcNow.Year;
            Response<List<LeaveBalanceDto>> balancesResponse = await GetLeaveBalancesAsync(employeeId, currentYear, cancellationToken);
            if (!balancesResponse.HasError && balancesResponse.Data != null)
            {
                dashboard.LeaveBalances = balancesResponse.Data;
            }

            // Get recent payslips (last 3 months)
            Response<List<PayslipDto>> payslipsResponse = await GetPayslipsAsync(employeeId, cancellationToken);
            if (!payslipsResponse.HasError && payslipsResponse.Data != null)
            {
                dashboard.RecentPayslips = payslipsResponse.Data
                    .OrderByDescending(p => p.Year)
                    .ThenByDescending(p => p.Month)
                    .Take(3)
                    .ToList();
            }

            // Get leave request counts
            Response<List<LeaveRequestDto>> leaveHistoryResponse = await GetLeaveHistoryAsync(employeeId, cancellationToken);
            if (!leaveHistoryResponse.HasError && leaveHistoryResponse.Data != null)
            {
                dashboard.PendingLeaveRequestsCount = leaveHistoryResponse.Data.Count(lr => lr.Status == (int)LeaveStatus.Pending);
                dashboard.ApprovedLeaveRequestsCount = leaveHistoryResponse.Data.Count(lr => lr.Status == (int)LeaveStatus.Approved);
            }

            return new Response<ESSDashboardDto>(dashboard, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<ESSDashboardDto>(null!, ex.Message, true);
        }
    }
}

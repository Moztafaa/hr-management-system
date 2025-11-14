using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Leaves;
using HRManagementSystem.Application.DTOs.Leaves.LeaveApprovalDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveTypeDtos;

namespace HRManagementSystem.Application.Interfaces;
public interface ILeaveService
{
    // ---------- Leave Types ----------
    Task<Response<int>> CreateLeaveTypeAsync(CreateLeaveTypeDto dto);
    Task<Response<bool>> UpdateLeaveTypeAsync(UpdateLeaveTypeDto dto);
    Task<Response<List<LeaveTypeDto>>> GetLeaveTypesAsync();
    Task<Response<LeaveTypeDto>> GetLeaveTypeAsync(int id);
    Task<Response<bool>> DeleteLeaveTypeAsync(int id);

    // ---------- Leave Balances ----------
    Task<Response<EmployeeLeaveBalanceDto>> GetEmployeeLeaveBalanceAsync(int employeeId, int leaveTypeId, int year);
    Task<Response<int>> AllocateAnnualBalancesAsync(int year);

    // ---------- Leave Requests ----------
    Task<Response<int>> RequestLeaveAsync(CreateLeaveRequestDto dto, int currentYear);
    Task<Response<List<LeaveRequestDto>>> GetEmployeeRequestsAsync(int employeeId);
    Task<Response<List<LeaveRequestDto>>> GetPendingApprovalsAsync(int approverId);

    // ---------- Approvals ----------
    Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto);
    Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto);

    // ---------- Utilities ----------
    Task<Response<bool>> HasOverlapAsync(int employeeId, DateTime start, DateTime end);
}

namespace HRManagementSystem.Application.Interfaces.ILeaveServices;
public interface ILeaveBalanceService
{
    Task<Response<IEnumerable<LeaveBalanceDto>>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<LeaveBalanceDto>>> GetByEmployeeIdAndYearAsync(int employeeId, int year, CancellationToken cancellationToken = default);
    Task<Response<bool>> AllocateInitialBalancesAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeductLeaveDaysAsync(int employeeId, int leaveTypeId, int leaveDays, CancellationToken cancellationToken = default);
    Task<Response<bool>> AllocateBalancesForNewYearAsync(CancellationToken cancellationToken = default);
}

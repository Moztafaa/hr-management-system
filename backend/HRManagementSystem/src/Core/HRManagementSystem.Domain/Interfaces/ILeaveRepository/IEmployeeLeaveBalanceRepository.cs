using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces.LeaveRepository;
public interface IEmployeeLeaveBalanceRepository:IRepository<EmployeeLeaveBalance>
{
    Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAndYearAsync(int employeeId, int year, CancellationToken cancellationToken);
    Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken);
    Task<EmployeeLeaveBalance> GetByEmployeeAndTypeAndYearAsync(int employeeId, int leaveTypeId, int year, CancellationToken cancellationToken);
}

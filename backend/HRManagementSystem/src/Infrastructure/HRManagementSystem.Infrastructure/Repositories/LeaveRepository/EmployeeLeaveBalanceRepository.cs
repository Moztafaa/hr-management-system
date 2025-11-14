using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Domain.Interfaces.LeaveRepository;

namespace HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
public class EmployeeLeaveBalanceRepository(ApplicationDbContext _context) : Repository<EmployeeLeaveBalance>(_context), IEmployeeLeaveBalanceRepository
{
    public async Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAndYearAsync(int employeeId, int year, CancellationToken cancellationToken)
    {
        return await _context.EmployeeLeaveBalances
            .Where(lb => lb.EmployeeId == employeeId && lb.Year==year)
            .Include(lb => lb.LeaveType)
            .ToListAsync();
    }

    public async Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken)
    {
        return await _context.EmployeeLeaveBalances
            .Where(lb => lb.EmployeeId == employeeId)
            .Include(lb => lb.LeaveType)
            .OrderByDescending(lb=>lb.Year)
            .ToListAsync();
    }
    public async Task<EmployeeLeaveBalance> GetByEmployeeAndTypeAndYearAsync(int employeeId, int leaveTypeId, int year, CancellationToken cancellationToken)
    {
        return await _context.EmployeeLeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == employeeId && lb.Year == year && lb.LeaveTypeId == leaveTypeId);
    }

}

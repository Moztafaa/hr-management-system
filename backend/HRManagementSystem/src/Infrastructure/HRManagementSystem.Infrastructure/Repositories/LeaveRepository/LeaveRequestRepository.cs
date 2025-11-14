using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces.LeaveRepository;

namespace HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
public class LeaveRequestRepository(ApplicationDbContext _context) : Repository<LeaveRequest>(_context), ILeaveRequestRepository
{
    public async Task<IEnumerable<LeaveRequest>> GetAllByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default){
        return await _context.LeaveRequests
        .Where(lr => lr.EmployeeId == employeeId)
        .Include(lr => lr.LeaveType)
        .OrderBy(lr =>lr.Status)
        .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<LeaveRequest>> GetAllByReviewerIdAsync(int reviewerId, CancellationToken cancellationToken = default) {
        return await _context.LeaveRequests
        .Where(lr => lr.ReviewedById == reviewerId)
        .Include(lr => lr.LeaveType)
        .OrderBy(lr => lr.Status)
        .ToListAsync(cancellationToken);
    }
}

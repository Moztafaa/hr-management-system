namespace HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
public class LeaveApprovalRepository(ApplicationDbContext _context) : Repository<LeaveApproval>(_context), ILeaveApprovalRepository
{
    public async Task<IEnumerable<LeaveApproval>> GetAllByApproverIdAsync(int approverId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<LeaveApproval>()
            .Where(a => a.ApproverId == approverId)
            .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<LeaveApproval>> GetAllByRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<LeaveApproval>()
            .Where(a => a.LeaveRequestId == leaveRequestId)
            .ToListAsync(cancellationToken);
    }
    public async Task<LeaveApproval> GetByLeaveRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken)
    {
        return await _context.LeaveApprovals
            .FirstOrDefaultAsync(la => la.LeaveRequestId == leaveRequestId, cancellationToken);
    }
    public async Task<LeaveApproval?> GetByRequestAndApproverAsync(int leaveRequestId, int approverId, LevelApproval level, CancellationToken cancellationToken = default)
    {
        return await _context.Set<LeaveApproval>()
            .FirstOrDefaultAsync(a =>
                a.LeaveRequestId == leaveRequestId &&
                a.ApproverId == approverId &&
                a.Level == level,
                cancellationToken);
    }
}

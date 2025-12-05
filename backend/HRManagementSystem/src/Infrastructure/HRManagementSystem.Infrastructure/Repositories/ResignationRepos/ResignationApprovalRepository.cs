using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repositories.ResignationRepos;

public class ResignationApprovalRepository(ApplicationDbContext _context) : Repository<ResignationApproval>(_context), IResignationApprovalRepository
{
    public async Task<ResignationApproval?> GetByResignationAndApproverAsync(int resignationId, int approverId, LevelApproval level, CancellationToken cancellationToken = default)
    {
        return await _context.ResignationApprovals
            .FirstOrDefaultAsync(a =>
                a.ResignationId == resignationId &&
                a.ApproverId == approverId &&
                a.Level == level,
                cancellationToken);
    }

    public async Task<IEnumerable<ResignationApproval>> GetAllByResignationIdAsync(int resignationId, CancellationToken cancellationToken = default)
    {
        return await _context.ResignationApprovals
            .Where(a => a.ResignationId == resignationId)
            .Include(a => a.Approver)
            .OrderBy(a => a.Level)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ResignationApproval>> GetAllByApproverIdAsync(int approverId, CancellationToken cancellationToken = default)
    {
        return await _context.ResignationApprovals
            .Where(a => a.ApproverId == approverId)
            .Include(a => a.Resignation)
                .ThenInclude(r => r.Employee)
            .OrderByDescending(a => a.Resignation.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

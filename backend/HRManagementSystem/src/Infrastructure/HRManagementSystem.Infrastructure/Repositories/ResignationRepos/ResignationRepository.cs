using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repositories.ResignationRepos;

public class ResignationRepository(ApplicationDbContext _context) : Repository<Resignation>(_context), IResignationRepository
{
    public async Task<IEnumerable<Resignation>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Resignations
            .Where(r => r.EmployeeId == employeeId)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Department)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Designation)
            .Include(r => r.Reviewer)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resignation>> GetPendingByApproverIdAsync(int approverId, CancellationToken cancellationToken = default)
    {
        return await _context.Resignations
            .Where(r => r.ReviewedById == approverId && r.Status == ResignationStatus.Pending)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Department)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Designation)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Resignation?> GetActiveByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Resignations
            .Where(r => r.EmployeeId == employeeId && r.Status == ResignationStatus.Pending)
            .Include(r => r.Employee)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Resignation?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Resignations
            .Where(r => r.Id == id)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Department)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Designation)
            .Include(r => r.Reviewer)
            .Include(r => r.ResignationApprovals)
                .ThenInclude(a => a.Approver)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resignation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Resignations
            .Include(r => r.Employee)
                .ThenInclude(e => e.Department)
            .Include(r => r.Employee)
                .ThenInclude(e => e.Designation)
            .Include(r => r.Reviewer)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

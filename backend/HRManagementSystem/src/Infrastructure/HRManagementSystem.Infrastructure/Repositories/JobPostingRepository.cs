using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repositories;

public class JobPostingRepository(ApplicationDbContext context) : Repository<JobPosting>(context), IJobPostingRepository
{
    public async Task<IEnumerable<JobPosting>> GetActiveJobPostingsAsync()
    {
        return await _context.JobPostings
            .Where(jp => jp.IsActive.HasValue && jp.IsActive.Value)
            .Include(jp => jp.Department)
            .Include(jp => jp.Designation)
            .OrderByDescending(jp => jp.PostedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobPosting>> GetJobPostingsByDepartmentAsync(int departmentId)
    {
        return await _context.JobPostings
            .Where(jp => jp.DepartmentId == departmentId)
            .Include(jp => jp.Department)
            .Include(jp => jp.Designation)
            .OrderByDescending(jp => jp.PostedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobPosting>> GetJobPostingsByDesignationAsync(int designationId)
    {
        return await _context.JobPostings
            .Where(jp => jp.DesignationId == designationId)
            .Include(jp => jp.Department)
            .Include(jp => jp.Designation)
            .OrderByDescending(jp => jp.PostedDate)
            .ToListAsync();
    }

    public override async Task<JobPosting?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.JobPostings
            .Include(jp => jp.Department)
            .Include(jp => jp.Designation)
            .FirstOrDefaultAsync(jp => jp.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<JobPosting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JobPostings
            .Include(jp => jp.Department)
            .Include(jp => jp.Designation)
            .OrderByDescending(jp => jp.PostedDate)
            .ToListAsync(cancellationToken);
    }
}

using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Interfaces;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repositories;

public class JobApplicationRepository(ApplicationDbContext context) : Repository<JobApplication>(context), IJobApplicationRepository
{
    public async Task<IEnumerable<JobApplication>> GetApplicationsByCandidateAsync(int candidateId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Where(ja => ja.CandidateId == candidateId)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Department)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Designation)
            .Include(ja => ja.Candidate)
            .OrderByDescending(ja => ja.ApplicationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<JobApplication>> GetApplicationsByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Where(ja => ja.JobPostingId == jobPostingId)
            .Include(ja => ja.Candidate)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Department)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Designation)
            .Include(ja => ja.Reviewer)
            .Include(ja => ja.AssignedRecruiter)
            .OrderByDescending(ja => ja.ApplicationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(ApplicationStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Where(ja => ja.Status == status)
            .Include(ja => ja.Candidate)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Department)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Designation)
            .Include(ja => ja.Reviewer)
            .Include(ja => ja.AssignedRecruiter)
            .OrderByDescending(ja => ja.ApplicationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<JobApplication?> GetApplicationWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Include(ja => ja.Candidate)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Department)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Designation)
            .Include(ja => ja.Reviewer)
            .Include(ja => ja.AssignedRecruiter)
            .FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
    }

    public async Task<int> GetApplicationCountByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .CountAsync(ja => ja.JobPostingId == jobPostingId, cancellationToken);
    }

    public async Task<bool> HasCandidateAppliedAsync(int candidateId, int jobPostingId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .AnyAsync(ja => ja.CandidateId == candidateId && ja.JobPostingId == jobPostingId, cancellationToken);
    }

    public override async Task<JobApplication?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Include(ja => ja.Candidate)
            .Include(ja => ja.JobPosting)
            .FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<JobApplication>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Include(ja => ja.Candidate)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Department)
            .Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Designation)
            .OrderByDescending(ja => ja.ApplicationDate)
            .ToListAsync(cancellationToken);
    }
}

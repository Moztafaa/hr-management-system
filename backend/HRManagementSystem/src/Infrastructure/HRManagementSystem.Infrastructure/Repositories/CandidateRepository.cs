using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repositories;

public class CandidateRepository(ApplicationDbContext context) : Repository<Candidate>(context), ICandidateRepository
{
    public async Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Candidates
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    public async Task<Candidate?> GetCandidatewithApplicationsAsync(int candidateId, CancellationToken cancellationToken = default)
    {
        return await _context.Candidates
            .Include(c => c.JobApplications)
                .ThenInclude(ja => ja.JobPosting)
                    .ThenInclude(jp => jp.Department)
            .Include(c => c.JobApplications)
                .ThenInclude(ja => ja.JobPosting)
                    .ThenInclude(jp => jp.Designation)
            .FirstOrDefaultAsync(c => c.Id == candidateId, cancellationToken);
    }

    public async Task<IEnumerable<Candidate>> SearchCandidatesAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        string lowerSearchTerm = searchTerm.ToLower();

        return await _context.Candidates
            .Where(c =>
                c.FirstName.ToLower().Contains(lowerSearchTerm) ||
                c.LastName.ToLower().Contains(lowerSearchTerm) ||
                c.Email.ToLower().Contains(lowerSearchTerm) ||
                (c.Skills != null && c.Skills.ToLower().Contains(lowerSearchTerm)) ||
                (c.CurrentCompany != null && c.CurrentCompany.ToLower().Contains(lowerSearchTerm)) ||
                (c.CurrentJobTitle != null && c.CurrentJobTitle.ToLower().Contains(lowerSearchTerm)))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Candidates
            .AnyAsync(c => c.Email == email, cancellationToken);
    }

    public override async Task<Candidate?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Candidates
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Candidate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Candidates
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync(cancellationToken);
    }
}

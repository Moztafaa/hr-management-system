using System;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface ICandidateRepository : IRepository<Candidate>
{
    Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Candidate?> GetCandidatewithApplicationsAsync(int candidateId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Candidate>> SearchCandidatesAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default);
}

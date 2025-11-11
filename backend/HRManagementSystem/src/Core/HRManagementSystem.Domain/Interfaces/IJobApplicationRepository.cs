using System;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Interfaces;

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    Task<IEnumerable<JobApplication>> GetApplicationsByCandidateAsync(int candidateId, CancellationToken cancellationToken = default);
    Task<IEnumerable<JobApplication>> GetApplicationsByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(ApplicationStatus status, CancellationToken cancellationToken = default);
    Task<JobApplication?> GetApplicationWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetApplicationCountByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default);
    Task<bool> HasCandidateAppliedAsync(int candidateId, int jobPostingId, CancellationToken cancellationToken = default);
}

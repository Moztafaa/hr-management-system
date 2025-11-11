using System;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Interfaces;

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    /*
   Task<IEnumerable<JobApplication>> GetApplicationsByCandidateAsync(int candidateId)
Task<IEnumerable<JobApplication>> GetApplicationsByJobPostingAsync(int jobPostingId)
Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(ApplicationStatus status)
Task<JobApplication?> GetApplicationWithDetailsAsync(int id) - Include Candidate + JobPosting
Task<int> GetApplicationCountByJobPostingAsync(int jobPostingId)
Task<bool> HasCandidateAppliedAsync(int candidateId, int jobPostingId) - Prevent duplicate */
    Task<IEnumerable<JobApplication>> GetApplicationsByCandidateAsync(int candidateId);
    Task<IEnumerable<JobApplication>> GetApplicationsByJobPostingAsync(int jobPostingId);
    Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(ApplicationStatus status);
    Task<JobApplication?> GetApplicationWithDetailsAsync(int id);
    Task<int> GetApplicationCountByJobPostingAsync(int jobPostingId);
    Task<bool> HasCandidateAppliedAsync(int candidateId, int jobPostingId);

}

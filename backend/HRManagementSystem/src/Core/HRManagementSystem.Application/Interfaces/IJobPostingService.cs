using System;
using HRManagementSystem.Application.DTOs.JobPosting;

namespace HRManagementSystem.Application.Interfaces;

public interface IJobPostingService
{
    Task<JobPostingDto> GetJobPostingByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<JobPostingDto>> GetAllJobPostingsAsync(CancellationToken cancellationToken);
    Task<JobPostingDto> CreateJobPostingAsync(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken);
    Task<JobPostingDto> UpdateJobPostingAsync(int id, UpdateJobPostingDto updateJobPostingDto, CancellationToken cancellationToken);
    Task<bool> DeleteJobPostingAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<JobPostingDto>> GetActiveJobPostingsAsync(CancellationToken cancellationToken);


}

using System;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobPosting;

namespace HRManagementSystem.Application.Interfaces;

public interface IJobPostingService
{
    Task<Response<JobPostingDto>> GetJobPostingByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobPostingDto>>> GetAllJobPostingsAsync(CancellationToken cancellationToken = default);
    Task<Response<JobPostingDto>> CreateJobPostingAsync(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateJobPostingAsync(int id, UpdateJobPostingDto updateJobPostingDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteJobPostingAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobPostingDto>>> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobPostingDto>>> GetJobPostingsByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobPostingDto>>> GetJobPostingsByDesignationAsync(int designationId, CancellationToken cancellationToken = default);
}

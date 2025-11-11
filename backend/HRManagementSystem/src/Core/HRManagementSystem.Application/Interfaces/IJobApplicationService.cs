using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobApplication;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.Interfaces;

public interface IJobApplicationService
{
    Task<Response<JobApplicationDto>> GetJobApplicationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobApplicationDto>>> GetAllJobApplicationsAsync(CancellationToken cancellationToken = default);
    Task<Response<JobApplicationDetailDto>> GetJobApplicationDetailAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByCandidateAsync(int candidateId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByStatusAsync(ApplicationStatus status, CancellationToken cancellationToken = default);
    Task<Response<JobApplicationDto>> CreateJobApplicationAsync(CreateJobApplicationDto createJobApplicationDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateJobApplicationStatusAsync(int id, UpdateJobApplicationStatusDto updateDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteJobApplicationAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<int>> GetApplicationCountByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default);
    Task<Response<bool>> HasCandidateAppliedAsync(int candidateId, int jobPostingId, CancellationToken cancellationToken = default);
}

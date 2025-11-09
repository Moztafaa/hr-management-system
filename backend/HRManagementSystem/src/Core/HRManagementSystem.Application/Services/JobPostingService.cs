using System;
using HRManagementSystem.Application.DTOs.JobPosting;

namespace HRManagementSystem.Application.Services;

public class JobPostingService(IJobPostingRepository jobPostingRepository) : IJobPostingService
{
    public async Task<JobPostingDto> CreateJobPostingAsync(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken)
    {
        try
        {
            var jobPosting = new JobPosting
            {
                Title = createJobPostingDto.Title,
                Description = createJobPostingDto.Description,
                Requirements = createJobPostingDto.Requirements,
                PostedDate = createJobPostingDto.PostedDate,
                ClosingDate = createJobPostingDto.ClosingDate,
                IsActive = createJobPostingDto.IsActive ?? true,
                DepartmentId = createJobPostingDto.DepartmentId,
                DesignationId = createJobPostingDto.DesignationId
            };

            await jobPostingRepository.AddAsync(jobPosting, cancellationToken);

            var jobPostingDto = new JobPostingDto
            {
                Title = jobPosting.Title,
                Description = jobPosting.Description,
                Requirements = jobPosting.Requirements,
                PostedDate = jobPosting.PostedDate,
                ClosingDate = jobPosting.ClosingDate,
                IsActive = jobPosting.IsActive
            };

            return jobPostingDto;
        }
        catch
        {
            throw;                                                                                                                                                                                                                           
        }


    }
    public Task<bool> DeleteJobPostingAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<JobPostingDto>> GetActiveJobPostingsAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<JobPostingDto>> GetAllJobPostingsAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<JobPostingDto> GetJobPostingByIdAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<JobPostingDto> UpdateJobPostingAsync(int id, UpdateJobPostingDto updateJobPostingDto, CancellationToken cancellationToken) => throw new NotImplementedException();
}

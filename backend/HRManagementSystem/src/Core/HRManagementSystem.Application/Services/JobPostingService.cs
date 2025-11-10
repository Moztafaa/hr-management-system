using System;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobPosting;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class JobPostingService(IJobPostingRepository jobPostingRepository) : IJobPostingService
{
    public async Task<Response<JobPostingDto>> CreateJobPostingAsync(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken = default)
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
                DesignationId = createJobPostingDto.DesignationId,
                CreatedAt = DateTime.UtcNow
            };

            await jobPostingRepository.AddAsync(jobPosting, cancellationToken);

            var jobPostingDto = new JobPostingDto
            {
                Title = jobPosting.Title,
                Description = jobPosting.Description,
                Requirements = jobPosting.Requirements,
                PostedDate = jobPosting.PostedDate,
                ClosingDate = jobPosting.ClosingDate,
                IsActive = jobPosting.IsActive,
                DepartmentName = jobPosting.Department?.Name,
                DesignationName = jobPosting.Designation?.Title
            };

            return new Response<JobPostingDto>(jobPostingDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<JobPostingDto>(default!, $"Error occurred while creating the job posting: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteJobPostingAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobPosting? jobPosting = await jobPostingRepository.GetByIdAsync(id, cancellationToken);
            if (jobPosting == null)
            {
                return new Response<bool>(false, "Job posting not found.", true);
            }

            await jobPostingRepository.DeleteAsync(jobPosting.Id, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the job posting: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobPostingDto>>> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobPosting> jobPostings = await jobPostingRepository.GetActiveJobPostingsAsync();
            IEnumerable<JobPostingDto> jobPostingDtos = jobPostings.Select(jp => new JobPostingDto
            {
                Title = jp.Title,
                Description = jp.Description,
                Requirements = jp.Requirements,
                PostedDate = jp.PostedDate,
                ClosingDate = jp.ClosingDate,
                IsActive = jp.IsActive,
                DepartmentName = jp.Department?.Name,
                DesignationName = jp.Designation?.Title
            });

            return new Response<IEnumerable<JobPostingDto>>(jobPostingDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobPostingDto>>(null!, $"Error occurred while retrieving active job postings: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobPostingDto>>> GetAllJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobPosting> jobPostings = await jobPostingRepository.GetAllAsync(cancellationToken);
            IEnumerable<JobPostingDto> jobPostingDtos = jobPostings.Select(jp => new JobPostingDto
            {
                Title = jp.Title,
                Description = jp.Description,
                Requirements = jp.Requirements,
                PostedDate = jp.PostedDate,
                ClosingDate = jp.ClosingDate,
                IsActive = jp.IsActive,
                DepartmentName = jp.Department?.Name,
                DesignationName = jp.Designation?.Title
            });

            return new Response<IEnumerable<JobPostingDto>>(jobPostingDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobPostingDto>>(null!, $"Error occurred while retrieving job postings: {ex.Message}", true);
        }
    }

    public async Task<Response<JobPostingDto>> GetJobPostingByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobPosting? jobPosting = await jobPostingRepository.GetByIdAsync(id, cancellationToken);
            if (jobPosting == null)
            {
                return new Response<JobPostingDto>(null!, "Job posting not found.", true);
            }

            var jobPostingDto = new JobPostingDto
            {
                Title = jobPosting.Title,
                Description = jobPosting.Description,
                Requirements = jobPosting.Requirements,
                PostedDate = jobPosting.PostedDate,
                ClosingDate = jobPosting.ClosingDate,
                IsActive = jobPosting.IsActive,
                DepartmentName = jobPosting.Department?.Name,
                DesignationName = jobPosting.Designation?.Title
            };

            return new Response<JobPostingDto>(jobPostingDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<JobPostingDto>(null!, $"Error occurred while retrieving the job posting: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> UpdateJobPostingAsync(int id, UpdateJobPostingDto updateJobPostingDto, CancellationToken cancellationToken = default)
    {
        try
        {
            JobPosting? jobPosting = await jobPostingRepository.GetByIdAsync(id, cancellationToken);
            if (jobPosting == null)
            {
                return new Response<bool>(false, "Job posting not found.", true);
            }

            jobPosting.Title = updateJobPostingDto.Title;
            jobPosting.Description = updateJobPostingDto.Description;
            jobPosting.Requirements = updateJobPostingDto.Requirements;
            jobPosting.PostedDate = updateJobPostingDto.PostedDate;
            jobPosting.ClosingDate = updateJobPostingDto.ClosingDate;
            jobPosting.IsActive = updateJobPostingDto.IsActive;
            jobPosting.DepartmentId = updateJobPostingDto.DepartmentId;
            jobPosting.DesignationId = updateJobPostingDto.DesignationId;
            jobPosting.UpdatedAt = DateTime.UtcNow;

            await jobPostingRepository.UpdateAsync(jobPosting, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while updating the job posting: {ex.Message}", true);
        }
    }
}

using System;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobPosting;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class JobPostingService(IJobPostingRepository jobPostingRepository, IMapper _mapper) : IJobPostingService
{
    public async Task<Response<JobPostingDto>> CreateJobPostingAsync(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken = default)
    {
        try
        {
            JobPosting? jobPosting = _mapper.Map<JobPosting>(createJobPostingDto);
            if (jobPosting.IsActive == null)
                jobPosting.IsActive = true;
            jobPosting.CreatedAt = DateTime.UtcNow;

            await jobPostingRepository.AddAsync(jobPosting, cancellationToken);

            JobPostingDto? jobPostingDto = _mapper.Map<JobPostingDto>(jobPosting);

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
            IEnumerable<JobPostingDto> jobPostingDtos = _mapper.Map<IEnumerable<JobPostingDto>>(jobPostings);

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
            IEnumerable<JobPostingDto> jobPostingDtos = _mapper.Map<IEnumerable<JobPostingDto>>(jobPostings);

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

            JobPostingDto? jobPostingDto = _mapper.Map<JobPostingDto>(jobPosting);

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

            _mapper.Map(updateJobPostingDto, jobPosting);
            jobPosting.UpdatedAt = DateTime.UtcNow;

            await jobPostingRepository.UpdateAsync(jobPosting, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while updating the job posting: {ex.Message}", true);
        }
    }
    
    
    // Get JobPosting by department
    public async Task<Response<IEnumerable<JobPostingDto>>> GetJobPostingsByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobPosting> jobPostings = await jobPostingRepository.GetJobPostingsByDepartmentAsync(departmentId);
            IEnumerable<JobPostingDto> jobPostingDtos = _mapper.Map<IEnumerable<JobPostingDto>>(jobPostings);

            return new Response<IEnumerable<JobPostingDto>>(jobPostingDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobPostingDto>>(null!, $"Error occurred while retrieving job postings by department: {ex.Message}", true);
        }
    }
    
    // Get JobPosting by designation
    public async Task<Response<IEnumerable<JobPostingDto>>> GetJobPostingsByDesignationAsync(int designationId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobPosting> jobPostings = await jobPostingRepository.GetJobPostingsByDesignationAsync(designationId);
            IEnumerable<JobPostingDto> jobPostingDtos = _mapper.Map<IEnumerable<JobPostingDto>>(jobPostings);

            return new Response<IEnumerable<JobPostingDto>>(jobPostingDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobPostingDto>>(null!, $"Error occurred while retrieving job postings by designation: {ex.Message}", true);
        }
    }
}

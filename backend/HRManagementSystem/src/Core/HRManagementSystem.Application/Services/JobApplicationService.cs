using AutoMapper;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobApplication;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class JobApplicationService(
    IJobApplicationRepository jobApplicationRepository,
    ICandidateRepository candidateRepository,
    IMapper mapper) : IJobApplicationService
{
    public async Task<Response<JobApplicationDto>> GetJobApplicationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobApplication? jobApplication = await jobApplicationRepository.GetByIdAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<JobApplicationDto>(null!, "Job application not found.", true);
            }

            JobApplicationDto jobApplicationDto = mapper.Map<JobApplicationDto>(jobApplication);
            return new Response<JobApplicationDto>(jobApplicationDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<JobApplicationDto>(null!, $"Error occurred while retrieving the job application: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobApplicationDto>>> GetAllJobApplicationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobApplication> jobApplications = await jobApplicationRepository.GetAllAsync(cancellationToken);
            IEnumerable<JobApplicationDto> jobApplicationDtos = mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);

            return new Response<IEnumerable<JobApplicationDto>>(jobApplicationDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobApplicationDto>>(null!, $"Error occurred while retrieving job applications: {ex.Message}", true);
        }
    }

    public async Task<Response<JobApplicationDetailDto>> GetJobApplicationDetailAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobApplication? jobApplication = await jobApplicationRepository.GetApplicationWithDetailsAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<JobApplicationDetailDto>(null!, "Job application not found.", true);
            }

            JobApplicationDetailDto jobApplicationDetailDto = mapper.Map<JobApplicationDetailDto>(jobApplication);
            return new Response<JobApplicationDetailDto>(jobApplicationDetailDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<JobApplicationDetailDto>(null!, $"Error occurred while retrieving the job application details: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByCandidateAsync(int candidateId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobApplication> jobApplications = await jobApplicationRepository.GetApplicationsByCandidateAsync(candidateId, cancellationToken);
            IEnumerable<JobApplicationDto> jobApplicationDtos = mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);

            return new Response<IEnumerable<JobApplicationDto>>(jobApplicationDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobApplicationDto>>(null!, $"Error occurred while retrieving applications by candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobApplication> jobApplications = await jobApplicationRepository.GetApplicationsByJobPostingAsync(jobPostingId, cancellationToken);
            IEnumerable<JobApplicationDto> jobApplicationDtos = mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);

            return new Response<IEnumerable<JobApplicationDto>>(jobApplicationDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobApplicationDto>>(null!, $"Error occurred while retrieving applications by job posting: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<JobApplicationDto>>> GetApplicationsByStatusAsync(ApplicationStatus status, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<JobApplication> jobApplications = await jobApplicationRepository.GetApplicationsByStatusAsync(status, cancellationToken);
            IEnumerable<JobApplicationDto> jobApplicationDtos = mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);

            return new Response<IEnumerable<JobApplicationDto>>(jobApplicationDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<JobApplicationDto>>(null!, $"Error occurred while retrieving applications by status: {ex.Message}", true);
        }
    }

    public async Task<Response<JobApplicationDto>> CreateJobApplicationAsync(CreateJobApplicationDto createJobApplicationDto, CancellationToken cancellationToken = default)
    {
        try
        {
            int candidateId;

            // Check if using existing candidate or creating new one
            if (createJobApplicationDto.CandidateId.HasValue)
            {
                candidateId = createJobApplicationDto.CandidateId.Value;

                // Verify candidate exists
                Candidate? existingCandidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
                if (existingCandidate == null)
                {
                    return new Response<JobApplicationDto>(null!, "Candidate not found.", true);
                }
            }
            else if (createJobApplicationDto.CandidateInfo != null)
            {
                // Create new candidate
                // Check if email already exists
                bool emailExists = await candidateRepository.IsEmailInUseAsync(createJobApplicationDto.CandidateInfo.Email, cancellationToken);
                if (emailExists)
                {
                    return new Response<JobApplicationDto>(null!, "A candidate with this email already exists. Please use the existing candidate ID.", true);
                }

                Candidate newCandidate = mapper.Map<Candidate>(createJobApplicationDto.CandidateInfo);
                newCandidate.CreatedAt = DateTime.UtcNow;
                await candidateRepository.AddAsync(newCandidate, cancellationToken);

                candidateId = newCandidate.Id;
            }
            else
            {
                return new Response<JobApplicationDto>(null!, "Either CandidateId or CandidateInfo must be provided.", true);
            }

            // Check if candidate has already applied to this job
            bool hasApplied = await jobApplicationRepository.HasCandidateAppliedAsync(candidateId, createJobApplicationDto.JobPostingId, cancellationToken);
            if (hasApplied)
            {
                return new Response<JobApplicationDto>(null!, "This candidate has already applied to this job posting.", true);
            }

            // Create job application
            JobApplication jobApplication = mapper.Map<JobApplication>(createJobApplicationDto);
            jobApplication.CandidateId = candidateId;
            jobApplication.ApplicationDate = DateTime.UtcNow;
            jobApplication.CreatedAt = DateTime.UtcNow;

            await jobApplicationRepository.AddAsync(jobApplication, cancellationToken);

            JobApplicationDto jobApplicationDto = mapper.Map<JobApplicationDto>(jobApplication);
            return new Response<JobApplicationDto>(jobApplicationDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<JobApplicationDto>(null!, $"Error occurred while creating the job application: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> UpdateJobApplicationStatusAsync(int id, UpdateJobApplicationStatusDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            JobApplication? jobApplication = await jobApplicationRepository.GetByIdAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<bool>(false, "Job application not found.", true);
            }

            mapper.Map(updateDto, jobApplication);
            jobApplication.UpdatedAt = DateTime.UtcNow;

            await jobApplicationRepository.UpdateAsync(jobApplication, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while updating the job application: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteJobApplicationAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobApplication? jobApplication = await jobApplicationRepository.GetByIdAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<bool>(false, "Job application not found.", true);
            }

            await jobApplicationRepository.DeleteAsync(id, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the job application: {ex.Message}", true);
        }
    }

    public async Task<Response<int>> GetApplicationCountByJobPostingAsync(int jobPostingId, CancellationToken cancellationToken = default)
    {
        try
        {
            int count = await jobApplicationRepository.GetApplicationCountByJobPostingAsync(jobPostingId, cancellationToken);
            return new Response<int>(count, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error occurred while getting application count: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> HasCandidateAppliedAsync(int candidateId, int jobPostingId, CancellationToken cancellationToken = default)
    {
        try
        {
            bool hasApplied = await jobApplicationRepository.HasCandidateAppliedAsync(candidateId, jobPostingId, cancellationToken);
            return new Response<bool>(hasApplied, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while checking application status: {ex.Message}", true);
        }
    }
}

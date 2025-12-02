using AutoMapper;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobApplication;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class JobApplicationService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IBackgroundJobService backgroundJobService,
    INotificationService notificationService) : IJobApplicationService
{
    public async Task<Response<JobApplicationDto>> GetJobApplicationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            JobApplication? jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id, cancellationToken);
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
            IEnumerable<JobApplication> jobApplications = await unitOfWork.JobApplications.GetAllAsync(cancellationToken);
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
            JobApplication? jobApplication = await unitOfWork.JobApplications.GetApplicationWithDetailsAsync(id, cancellationToken);
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
            IEnumerable<JobApplication> jobApplications = await unitOfWork.JobApplications.GetApplicationsByCandidateAsync(candidateId, cancellationToken);
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
            IEnumerable<JobApplication> jobApplications = await unitOfWork.JobApplications.GetApplicationsByJobPostingAsync(jobPostingId, cancellationToken);
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
            IEnumerable<JobApplication> jobApplications = await unitOfWork.JobApplications.GetApplicationsByStatusAsync(status, cancellationToken);
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
                Candidate? existingCandidate = await unitOfWork.Candidates.GetByIdAsync(candidateId, cancellationToken);
                if (existingCandidate == null)
                {
                    return new Response<JobApplicationDto>(null!, "Candidate not found.", true);
                }
            }
            else if (createJobApplicationDto.CandidateInfo != null)
            {
                // Create new candidate
                // Check if email already exists
                bool emailExists = await unitOfWork.Candidates.IsEmailInUseAsync(createJobApplicationDto.CandidateInfo.Email, cancellationToken);
                if (emailExists)
                {
                    return new Response<JobApplicationDto>(null!, "A candidate with this email already exists. Please use the existing candidate ID.", true);
                }

                Candidate newCandidate = mapper.Map<Candidate>(createJobApplicationDto.CandidateInfo);
                newCandidate.CreatedAt = DateTime.UtcNow;
                await unitOfWork.Candidates.AddAsync(newCandidate, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                candidateId = newCandidate.Id;
            }
            else
            {
                return new Response<JobApplicationDto>(null!, "Either CandidateId or CandidateInfo must be provided.", true);
            }

            // Check if candidate has already applied to this job
            bool hasApplied = await unitOfWork.JobApplications.HasCandidateAppliedAsync(candidateId, createJobApplicationDto.JobPostingId, cancellationToken);
            if (hasApplied)
            {
                return new Response<JobApplicationDto>(null!, "This candidate has already applied to this job posting.", true);
            }

            // Create job application
            JobApplication jobApplication = mapper.Map<JobApplication>(createJobApplicationDto);
            jobApplication.CandidateId = candidateId;
            jobApplication.ApplicationDate = DateTime.UtcNow;
            jobApplication.CreatedAt = DateTime.UtcNow;

            await unitOfWork.JobApplications.AddAsync(jobApplication, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification to candidate via background job
            Candidate? candidate = await unitOfWork.Candidates.GetByIdAsync(candidateId, cancellationToken);
            JobPosting? jobPosting = await unitOfWork.JobPostings.GetByIdAsync(createJobApplicationDto.JobPostingId, cancellationToken);

            if (candidate != null && jobPosting != null && !string.IsNullOrEmpty(candidate.Email))
            {
                // Queue email to be sent asynchronously
                backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendJobApplicationStatusEmailAsync(
                        candidate.Email,
                        $"{candidate.FirstName} {candidate.LastName}",
                        jobPosting.Title,
                        "Applied"));
            }

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
            JobApplication? jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<bool>(false, "Job application not found.", true);
            }

            mapper.Map(updateDto, jobApplication);
            jobApplication.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.JobApplications.UpdateAsync(jobApplication, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification to candidate about status change via background job
            Candidate? candidate = await unitOfWork.Candidates.GetByIdAsync(jobApplication.CandidateId, cancellationToken);
            JobPosting? jobPosting = await unitOfWork.JobPostings.GetByIdAsync(jobApplication.JobPostingId, cancellationToken);

            if (candidate != null && jobPosting != null && !string.IsNullOrEmpty(candidate.Email))
            {
                // Queue email to be sent asynchronously
                backgroundJobService.Enqueue<IEmailQueueJob>(
                    job => job.SendJobApplicationStatusEmailAsync(
                        candidate.Email,
                        $"{candidate.FirstName} {candidate.LastName}",
                        jobPosting.Title,
                        updateDto.Status.ToString()));
            }

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
            JobApplication? jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id, cancellationToken);
            if (jobApplication == null)
            {
                return new Response<bool>(false, "Job application not found.", true);
            }

            await unitOfWork.JobApplications.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
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
            int count = await unitOfWork.JobApplications.GetApplicationCountByJobPostingAsync(jobPostingId, cancellationToken);
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
            bool hasApplied = await unitOfWork.JobApplications.HasCandidateAppliedAsync(candidateId, jobPostingId, cancellationToken);
            return new Response<bool>(hasApplied, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while checking application status: {ex.Message}", true);
        }
    }
}

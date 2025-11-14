using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobApplication;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

/*
End Points:
POST   /api/jobapplication                              - Submit application (handles new/existing candidate)
GET    /api/jobapplication                              - Get all applications
GET    /api/jobapplication/{id}                         - Get specific application
GET    /api/jobapplication/{id}/detail                  - Get application with full details
GET    /api/jobapplication/jobposting/{jobPostingId}    - Get applications by job posting
GET    /api/jobapplication/candidate/{candidateId}      - Get applications by candidate
GET    /api/jobapplication/status/{status}              - Get applications by status
GET    /api/jobapplication/count/{jobPostingId}         - Get application count for job posting
GET    /api/jobapplication/check-applied                - Check if candidate applied to job
PUT    /api/jobapplication/{id}/status                  - Update application status
DELETE /api/jobapplication/{id}                         - Delete application
*/

[Route("api/[controller]")]
[ApiController]
public class JobApplicationController(IJobApplicationService jobApplicationService) : ControllerBase
{
    /// <summary>
    /// Get all job applications
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllJobApplications(CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobApplicationDto>> response = await jobApplicationService.GetAllJobApplicationsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get a job application by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobApplicationById(int id, CancellationToken cancellationToken)
    {
        Response<JobApplicationDto> response = await jobApplicationService.GetJobApplicationByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get job application with full details (includes candidate and job posting info)
    /// </summary>
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetJobApplicationDetail(int id, CancellationToken cancellationToken)
    {
        Response<JobApplicationDetailDto> response = await jobApplicationService.GetJobApplicationDetailAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get applications by job posting
    /// </summary>
    [HttpGet("jobposting/{jobPostingId}")]
    public async Task<IActionResult> GetApplicationsByJobPosting(int jobPostingId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobApplicationDto>> response = await jobApplicationService.GetApplicationsByJobPostingAsync(jobPostingId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get applications by candidate
    /// </summary>
    [HttpGet("candidate/{candidateId}")]
    public async Task<IActionResult> GetApplicationsByCandidate(int candidateId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobApplicationDto>> response = await jobApplicationService.GetApplicationsByCandidateAsync(candidateId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get applications by status
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetApplicationsByStatus(ApplicationStatus status, CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobApplicationDto>> response = await jobApplicationService.GetApplicationsByStatusAsync(status, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get count of applications for a job posting
    /// </summary>
    [HttpGet("count/{jobPostingId}")]
    public async Task<IActionResult> GetApplicationCount(int jobPostingId, CancellationToken cancellationToken)
    {
        Response<int> response = await jobApplicationService.GetApplicationCountByJobPostingAsync(jobPostingId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Check if a candidate has already applied to a job posting
    /// </summary>
    [HttpGet("check-applied")]
    public async Task<IActionResult> CheckIfCandidateApplied([FromQuery] int candidateId, [FromQuery] int jobPostingId, CancellationToken cancellationToken)
    {
        Response<bool> response = await jobApplicationService.HasCandidateAppliedAsync(candidateId, jobPostingId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Submit a new job application (can create new candidate or use existing one)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateJobApplication(CreateJobApplicationDto createJobApplicationDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Response<JobApplicationDto> response = await jobApplicationService.CreateJobApplicationAsync(createJobApplicationDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetJobApplicationById), new { id = response.Data.Id }, response);
    }

    /// <summary>
    /// Update job application status and details
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateJobApplicationStatus(int id, UpdateJobApplicationStatusDto updateDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Ensure the ID in the route matches the ID in the DTO
        if (id != updateDto.JobApplicationId)
        {
            return BadRequest(new { hasError = true, errorMessage = "Job application ID mismatch." });
        }

        Response<bool> response = await jobApplicationService.UpdateJobApplicationStatusAsync(id, updateDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    /// <summary>
    /// Delete a job application
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobApplication(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await jobApplicationService.DeleteJobApplicationAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }
}

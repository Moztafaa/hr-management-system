using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.JobPosting;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;


/*
End Points:
GET    /api/jobposting                     - List all job postings
GET    /api/jobposting/active              - List active job postings
GET    /api/jobposting/{id}                - Get specific job posting
GET    /api/jobposting/department/{departmentId} - Get job postings by department
GET    /api/jobposting/designation/{designationId} - Get job postings by designation
POST   /api/jobposting                     - Create new job posting
PUT    /api/jobposting/{id}                - Update job posting
DELETE /api/jobposting/{id}                - Delete job posting
*/


[Route("api/[controller]")]
[ApiController]
public class JobPostingController(IJobPostingService jobPostingService) : ControllerBase
{
    /// <summary>
    /// Get all job postings
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllJobPostings(CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobPostingDto>> response = await jobPostingService.GetAllJobPostingsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get all active job postings
    /// </summary>
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveJobPostings(CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobPostingDto>> response = await jobPostingService.GetActiveJobPostingsAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get a job posting by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobPostingById(int id, CancellationToken cancellationToken)
    {
        Response<JobPostingDto> response = await jobPostingService.GetJobPostingByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get job postings by department
    /// </summary>
    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetJobPostingsByDepartment(int departmentId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobPostingDto>> response = await jobPostingService.GetJobPostingsByDepartmentAsync(departmentId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get job postings by designation
    /// </summary>
    [HttpGet("designation/{designationId}")]
    public async Task<IActionResult> GetJobPostingsByDesignation(int designationId, CancellationToken cancellationToken)
    {
        Response<IEnumerable<JobPostingDto>> response = await jobPostingService.GetJobPostingsByDesignationAsync(designationId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Create a new job posting
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateJobPosting(CreateJobPostingDto createJobPostingDto, CancellationToken cancellationToken)
    {
        Response<JobPostingDto> response = await jobPostingService.CreateJobPostingAsync(createJobPostingDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetJobPostingById), new { id = response.Data }, response);
    }

    /// <summary>
    /// Update an existing job posting
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobPosting(int id, UpdateJobPostingDto updateJobPostingDto, CancellationToken cancellationToken)
    {
        Response<bool> response = await jobPostingService.UpdateJobPostingAsync(id, updateJobPostingDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    /// <summary>
    /// Delete a job posting
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobPosting(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await jobPostingService.DeleteJobPostingAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }
}

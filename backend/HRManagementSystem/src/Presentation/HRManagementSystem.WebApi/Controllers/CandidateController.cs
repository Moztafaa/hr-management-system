using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Candidate;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

/*
End Points:
GET    /api/candidate                     - List all candidates
GET    /api/candidate/{id}                - Get candidate details
GET    /api/candidate/{id}/applications   - Get candidate's application history
GET    /api/candidate/email/{email}       - Find by email
GET    /api/candidate/search              - Search candidates
GET    /api/candidate/check-email/{email} - Check if email exists
POST   /api/candidate                     - Create candidate
PUT    /api/candidate/{id}                - Update candidate
DELETE /api/candidate/{id}                - Delete candidate
*/

[Route("api/[controller]")]
[ApiController]
public class CandidateController(ICandidateService candidateService) : ControllerBase
{
    /// <summary>
    /// Get all candidates
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllCandidates(CancellationToken cancellationToken)
    {
        Response<IEnumerable<CandidateDto>> response = await candidateService.GetAllCandidatesAsync(cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get a candidate by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCandidateById(int id, CancellationToken cancellationToken)
    {
        Response<CandidateDto> response = await candidateService.GetCandidateByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Get candidate with their application history
    /// </summary>
    [HttpGet("{id}/applications")]
    public async Task<IActionResult> GetCandidateWithApplications(int id, CancellationToken cancellationToken)
    {
        Response<CandidateDto> response = await candidateService.GetCandidateWithApplicationsAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Find candidate by email
    /// </summary>
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetCandidateByEmail(string email, CancellationToken cancellationToken)
    {
        Response<CandidateDto> response = await candidateService.GetCandidateByEmailAsync(email, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Search candidates by name, email, or skills
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchCandidates([FromQuery] string searchTerm, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { hasError = true, errorMessage = "Search term is required." });
        }

        Response<IEnumerable<CandidateDto>> response = await candidateService.SearchCandidatesAsync(searchTerm, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Check if email already exists
    /// </summary>
    [HttpGet("check-email/{email}")]
    public async Task<IActionResult> CheckEmailExists(string email, CancellationToken cancellationToken)
    {
        Response<bool> response = await candidateService.CheckEmailExistsAsync(email, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    /// <summary>
    /// Create a new candidate
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCandidate(CreateCandidateDto createCandidateDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Response<CandidateDto> response = await candidateService.CreateCandidateAsync(createCandidateDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetCandidateById), new { id = response.Data.Id }, response);
    }

    /// <summary>
    /// Update an existing candidate
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCandidate(int id, UpdateCandidateDto updateCandidateDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Response<bool> response = await candidateService.UpdateCandidateAsync(id, updateCandidateDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    /// <summary>
    /// Delete a candidate
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidate(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await candidateService.DeleteCandidateAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }
}

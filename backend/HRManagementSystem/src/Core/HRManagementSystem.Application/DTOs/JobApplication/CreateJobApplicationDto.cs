using System;
using System.ComponentModel.DataAnnotations;
using HRManagementSystem.Application.DTOs.Candidate;

namespace HRManagementSystem.Application.DTOs.JobApplication;

public class CreateJobApplicationDto
{
    // Either provide CandidateId for existing candidate or CandidateInfo for new candidate
    public int? CandidateId { get; set; }

    public CreateCandidateDto? CandidateInfo { get; set; } // For new candidates

    [Required(ErrorMessage = "Job posting ID is required")]
    public int JobPostingId { get; set; }


    [Required(ErrorMessage = "Application source is required")]
    [StringLength(50)]
    public string Source { get; set; } = string.Empty;

    [StringLength(5000)]
    public string? CoverLetter { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Expected salary must be positive")]
    public decimal? ExpectedSalary { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public int? AssignedRecruiterId { get; set; }
}

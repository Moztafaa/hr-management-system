using System;
using HRManagementSystem.Application.DTOs.Candidate;
using HRManagementSystem.Application.DTOs.JobPosting;

namespace HRManagementSystem.Application.DTOs.JobApplication;

public class JobApplicationDetailDto
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public int JobPostingId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string? CoverLetter { get; set; }
    public string? Notes { get; set; }
    public int? ReviewedBy { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public DateTime? InterviewDate { get; set; }
    public string? InterviewFeedback { get; set; }
    public int? InterviewRating { get; set; }
    public decimal? ExpectedSalary { get; set; }
    public decimal? OfferedSalary { get; set; }
    public string? RejectionReason { get; set; }
    public int? AssignedRecruiterId { get; set; }
    public string CurrentStage { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation DTOs
    public CandidateDto? Candidate { get; set; }
    public JobPostingDto? JobPosting { get; set; }
}

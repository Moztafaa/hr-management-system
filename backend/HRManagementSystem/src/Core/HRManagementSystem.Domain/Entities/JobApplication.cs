using System;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;

public class JobApplication : BaseEntity
{
    public int CandidateId { get; set; }
    public int JobPostingId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public ApplicationSource Source { get; set; }
    public string? CoverLetter { get; set; }
    public string? Notes { get; set; }
    public int? ReviewedBy { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public DateTime? InterviewDate { get; set; }
    public string? InterviewFeedback { get; set; }
    public int? InterviewRating { get; set; } // 1-5 or 1-10 scale
    public decimal? ExpectedSalary { get; set; }
    public decimal? OfferedSalary { get; set; }
    public string? RejectionReason { get; set; }
    public int? AssignedRecruiterId { get; set; }
    public RecruitmentStage CurrentStage { get; set; }

    // Navigation properties
    public Candidate? Candidate { get; set; }
    public JobPosting? JobPosting { get; set; }
    public Employee? Reviewer { get; set; }
    public Employee? AssignedRecruiter { get; set; }
}

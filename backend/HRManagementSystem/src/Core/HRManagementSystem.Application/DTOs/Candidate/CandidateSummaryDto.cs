using System;

namespace HRManagementSystem.Application.DTOs.Candidate;

public class CandidateSummaryDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? CurrentJobTitle { get; set; }
    public string? Skills { get; set; }
    public int TotalApplications { get; set; }
    public DateTime CreatedAt { get; set; }
}

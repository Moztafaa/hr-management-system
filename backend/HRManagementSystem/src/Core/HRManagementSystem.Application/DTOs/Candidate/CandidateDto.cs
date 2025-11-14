using System;

namespace HRManagementSystem.Application.DTOs.Candidate;

public class CandidateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ResumeUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? PortfolioUrl { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? CurrentCompany { get; set; }
    public string? CurrentJobTitle { get; set; }
    public decimal? CurrentSalary { get; set; }
    public decimal? ExpectedSalary { get; set; }
    public string? Skills { get; set; }
    public string? Education { get; set; }
    public string? Certifications { get; set; }
    public int? NoticePeriodDays { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public string? PreferredWorkLocation { get; set; }
    public bool WillingToRelocate { get; set; }
    public string? Notes { get; set; }
    public int? ConvertedToEmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

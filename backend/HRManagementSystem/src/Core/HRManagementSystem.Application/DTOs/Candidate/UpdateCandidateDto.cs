using System;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Application.DTOs.Candidate;

public class UpdateCandidateDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Url(ErrorMessage = "Invalid resume URL")]
    [StringLength(500)]
    public string? ResumeUrl { get; set; }

    [Url(ErrorMessage = "Invalid LinkedIn URL")]
    [StringLength(500)]
    public string? LinkedInUrl { get; set; }

    [Url(ErrorMessage = "Invalid portfolio URL")]
    [StringLength(500)]
    public string? PortfolioUrl { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(20)]
    public string? Gender { get; set; }

    [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50")]
    public int? YearsOfExperience { get; set; }

    [StringLength(200)]
    public string? CurrentCompany { get; set; }

    [StringLength(200)]
    public string? CurrentJobTitle { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Current salary must be positive")]
    public decimal? CurrentSalary { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Expected salary must be positive")]
    public decimal? ExpectedSalary { get; set; }

    [StringLength(2000)]
    public string? Skills { get; set; }

    [StringLength(1000)]
    public string? Education { get; set; }

    [StringLength(1000)]
    public string? Certifications { get; set; }

    [Range(0, 365, ErrorMessage = "Notice period must be between 0 and 365 days")]
    public int? NoticePeriodDays { get; set; }

    public DateTime? AvailableFrom { get; set; }

    [StringLength(200)]
    public string? PreferredWorkLocation { get; set; }

    public bool WillingToRelocate { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}

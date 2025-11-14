using System;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Application.DTOs.JobApplication;

public class UpdateJobApplicationStatusDto
{
    [Required(ErrorMessage = "Job application ID is required")]
    public int JobApplicationId { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [StringLength(50)]
    public string Status { get; set; } = string.Empty;

    [StringLength(50)]
    public string? CurrentStage { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }

    public int? ReviewedBy { get; set; }

    public DateTime? ReviewedDate { get; set; }

    public DateTime? InterviewDate { get; set; }

    [StringLength(2000)]
    public string? InterviewFeedback { get; set; }

    [Range(1, 10, ErrorMessage = "Interview rating must be between 1 and 10")]
    public int? InterviewRating { get; set; }

    [StringLength(1000)]
    public string? RejectionReason { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Offered salary must be positive")]
    public decimal? OfferedSalary { get; set; }
}

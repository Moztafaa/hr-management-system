using System;

namespace HRManagementSystem.Application.DTOs.JobPosting;

public class JobPostingDto
{
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public required string Title { get; set; }
    [StringLength(2000)]
    public string? Description { get; set; }
    public string? Requirements { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public bool? IsActive { get; set; }
    public string? DepartmentName { get; set; }
    public string? DesignationName { get; set; }
}

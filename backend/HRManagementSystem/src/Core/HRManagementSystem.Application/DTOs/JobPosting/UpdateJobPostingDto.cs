using System;

namespace HRManagementSystem.Application.DTOs.JobPosting;

public class UpdateJobPostingDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Requirements { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public bool? IsActive { get; set; }
    public int DepartmentId { get; set; }
    public int DesignationId { get; set; }

}

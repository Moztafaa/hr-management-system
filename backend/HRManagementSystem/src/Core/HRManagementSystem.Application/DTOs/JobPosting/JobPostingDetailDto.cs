using System;

namespace HRManagementSystem.Application.DTOs.JobPosting;

public class JobPostingDetailDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Requirements { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public bool? IsActive { get; set; }
    public string? DepartmentName { get; set; }
    public string? DesignationName { get; set; }

}

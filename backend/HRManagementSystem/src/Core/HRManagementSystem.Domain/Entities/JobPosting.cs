using System;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Domain.Entities;

public class JobPosting : BaseEntity
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

    // Navigation properties
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public int DesignationId { get; set; }
    public Designation? Designation { get; set; }

    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}

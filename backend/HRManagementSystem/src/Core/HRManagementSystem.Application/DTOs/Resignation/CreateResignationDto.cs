using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Application.DTOs.Resignation;

public class CreateResignationDto
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 10)]
    public required string Reason { get; set; }

    [Required]
    public DateTime LastWorkingDate { get; set; }

    public bool IsImmediateResignation { get; set; }

    [StringLength(2000)]
    public string? HandoverNotes { get; set; }
}

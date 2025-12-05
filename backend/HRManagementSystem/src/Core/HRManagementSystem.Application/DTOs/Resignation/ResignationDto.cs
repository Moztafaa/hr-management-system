using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.DTOs.Resignation;

public class ResignationDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? DepartmentName { get; set; }
    public string? DesignationName { get; set; }

    public required string Reason { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime LastWorkingDate { get; set; }
    public int NoticePeriodDays { get; set; }
    public bool IsImmediateResignation { get; set; }
    public string? HandoverNotes { get; set; }

    public ResignationStatus Status { get; set; }

    public int? ReviewedById { get; set; }
    public string? ReviewerName { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? RejectionReason { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

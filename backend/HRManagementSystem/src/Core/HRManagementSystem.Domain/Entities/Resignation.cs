using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;

public class Resignation : BaseEntity
{
    public int EmployeeId { get; set; }

    public required string Reason { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime LastWorkingDate { get; set; }
    public int NoticePeriodDays { get; set; }
    public bool IsImmediateResignation { get; set; }
    public string? HandoverNotes { get; set; }

    public ResignationStatus Status { get; set; } = ResignationStatus.Pending;

    public int? ReviewedById { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? RejectionReason { get; set; }

    // 🔗 Navigation
    public Employee Employee { get; set; } = null!;
    public Employee? Reviewer { get; set; }
    public ICollection<ResignationApproval>? ResignationApprovals { get; set; }
}

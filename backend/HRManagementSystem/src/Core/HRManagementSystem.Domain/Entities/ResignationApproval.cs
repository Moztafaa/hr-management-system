using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;

public class ResignationApproval
{
    public int Id { get; set; }

    public int ResignationId { get; set; }
    public int ApproverId { get; set; }

    public LevelApproval Level { get; set; }
    public ResignationStatus Status { get; set; } = ResignationStatus.Pending;
    public DateTime? ActionDate { get; set; }
    public string? Comments { get; set; }

    // 🔗 Navigation
    public Resignation Resignation { get; set; } = null!;
    public Employee Approver { get; set; } = null!;
}

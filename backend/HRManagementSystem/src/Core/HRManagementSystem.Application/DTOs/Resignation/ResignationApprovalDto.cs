using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.DTOs.Resignation;

public class ResignationApprovalDto
{
    public int Id { get; set; }
    public int ResignationId { get; set; }
    public int ApproverId { get; set; }
    public string? ApproverName { get; set; }

    public LevelApproval Level { get; set; }
    public ResignationStatus Status { get; set; }
    public DateTime? ActionDate { get; set; }
    public string? Comments { get; set; }
}

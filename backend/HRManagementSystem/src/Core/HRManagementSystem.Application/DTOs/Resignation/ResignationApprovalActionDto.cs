using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.DTOs.Resignation;

public class ResignationApprovalActionDto
{
    public int ResignationId { get; set; }
    public int ApproverId { get; set; }
    public LevelApproval Level { get; set; }
    public string? Comments { get; set; }
}

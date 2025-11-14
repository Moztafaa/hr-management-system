namespace HRManagementSystem.Application.DTOs.Leaves.LeaveApprovalDtos;
public class LeaveApprovalActionDto
{
    public int LeaveRequestId { get; set; }
    public int ApproverId { get; set; }
    public LevelApproval Level { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Leaves.LeaveApprovalDtos;
public class LeaveApprovalDto
{
    public int Id { get; set; }

    public int LeaveRequestId { get; set; }
    public int ApproverId { get; set; }   

    public LevelApproval Level { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public DateTime? ActionDate { get; set; }
}

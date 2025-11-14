namespace HRManagementSystem.Domain.Entities;
public class LeaveType:BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int MaxDays { get; set; }
    public bool CanCarryForward { get; set; } = false;
    public int? CarryForwardLimit { get; set; }
    public bool IsPaid { get; set; } = true;
    public string? GenderRestriction { get; set; }  

    // 🔗 Navigation
    public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    public ICollection<EmployeeLeaveBalance>? EmployeeLeaveBalances { get; set; }
}

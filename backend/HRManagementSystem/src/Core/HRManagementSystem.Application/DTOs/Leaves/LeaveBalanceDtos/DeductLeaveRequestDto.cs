namespace HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
public class DeductLeaveRequestDto
{
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public int DaysUsed { get; set; }
}

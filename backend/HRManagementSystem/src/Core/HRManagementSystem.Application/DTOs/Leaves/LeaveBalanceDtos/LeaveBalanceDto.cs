namespace HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
public class LeaveBalanceDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public int TotalAllocated { get; set; }
    public int UsedDays { get; set; }
    public int RemainingDays { get; set; }
    public int Year { get; set; }
}

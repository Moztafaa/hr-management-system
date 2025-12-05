namespace HRManagementSystem.Application.DTOs.Resignation;

public class WithdrawResignationDto
{
    public int ResignationId { get; set; }
    public int EmployeeId { get; set; }
    public string? WithdrawalReason { get; set; }
}

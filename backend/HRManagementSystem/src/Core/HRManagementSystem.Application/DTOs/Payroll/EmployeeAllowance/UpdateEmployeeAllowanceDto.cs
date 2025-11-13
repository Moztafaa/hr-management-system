namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeAllowance;

public class UpdateEmployeeAllowanceDto
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public decimal Amount { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeAllowance;

public class EmployeeAllowanceDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public decimal Amount { get; set; }
}

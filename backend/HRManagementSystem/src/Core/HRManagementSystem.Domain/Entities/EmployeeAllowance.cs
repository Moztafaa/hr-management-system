namespace HRManagementSystem.Domain.Entities;

public class EmployeeAllowance:BaseEntity
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public decimal Amount { get; set; }
}

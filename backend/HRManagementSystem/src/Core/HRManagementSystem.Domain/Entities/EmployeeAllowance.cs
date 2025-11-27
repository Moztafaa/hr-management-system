namespace HRManagementSystem.Domain.Entities;

public class EmployeeAllowance:BaseEntity
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public decimal? Amount { get; set; }
    public int Month { get; set; }   
    public int Year { get; set; }
    public Employee Employee { get; set; } = default!;
    public Allowance Allowance { get; set; } = default!;
}

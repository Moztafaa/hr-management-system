namespace HRManagementSystem.Domain.Entities;

public class EmployeeDeduction:BaseEntity
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Employee Employee { get; set; } = default!;
    public Deduction Deduction { get; set; } = default!;
}

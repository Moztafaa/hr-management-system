namespace HRManagementSystem.Domain.Entities;

public class EmployeeDeduction:BaseEntity
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
}

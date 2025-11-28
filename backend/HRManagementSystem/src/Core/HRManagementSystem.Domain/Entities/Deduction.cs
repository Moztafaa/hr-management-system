namespace HRManagementSystem.Domain.Entities;

public class Deduction: BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
     public bool IsPercentage { get; set; } = false; 
    public ICollection<EmployeeDeduction> EmployeeDeductions { get; set; } = new List<EmployeeDeduction>();

}

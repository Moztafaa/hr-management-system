namespace HRManagementSystem.Domain.Entities;

public class Deduction: BaseEntity
{
    public string Name { get; set; } = default!;
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
    public ICollection<EmployeeDeduction> EmployeeDeductions { get; set; } = new List<EmployeeDeduction>();
    
}

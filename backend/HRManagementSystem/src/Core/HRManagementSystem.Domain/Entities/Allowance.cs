namespace HRManagementSystem.Domain.Entities;

public class Allowance:BaseEntity
{
    public string Name { get; set; } = default!;

    // If true → Value is percentage (e.g., 50 = 50%)
    // If false → Value is fixed amount (e.g., 50 = 50 EGP)
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
    public ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();
  
}

namespace HRManagementSystem.Domain.Entities;

public class Allowance:BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; } = false;
    public ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();


}

namespace HRManagementSystem.Domain.Entities;

public class Allowance:BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();
    //public ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
}

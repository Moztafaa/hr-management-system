namespace HRManagementSystem.Domain.Entities;

public class Deduction: BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public ICollection<EmployeeDeduction> EmployeeDeductions { get; set; } = new List<EmployeeDeduction>();
    //public ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
}

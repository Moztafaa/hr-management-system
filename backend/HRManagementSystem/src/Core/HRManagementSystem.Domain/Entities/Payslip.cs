namespace HRManagementSystem.Domain.Entities;

public class Payslip : BaseEntity
{
    public int EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetSalary { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public DateTime GeneratedAt { get; set; }

    public Employee Employee { get; set; } = null!;

    //public ICollection<Allowance> Allowances { get; set; } = new List<Allowance>();
    //public ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();

}

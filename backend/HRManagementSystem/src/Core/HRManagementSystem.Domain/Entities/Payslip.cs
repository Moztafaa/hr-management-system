namespace HRManagementSystem.Domain.Entities;
public class Payslip : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetSalary { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

}

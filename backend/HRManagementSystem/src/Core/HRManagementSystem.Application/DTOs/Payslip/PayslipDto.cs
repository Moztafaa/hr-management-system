namespace HRManagementSystem.Application.DTOs.Payroll.Payslip;

public class PayslipDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }


    public required string EmployeeName { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }

    public decimal NetSalary { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }

    public DateTime GeneratedAt { get; set; }

    public List<AllowanceDto>? Allowances { get; set; }
    public List<DeductionDto>? Deductions { get; set; }

}

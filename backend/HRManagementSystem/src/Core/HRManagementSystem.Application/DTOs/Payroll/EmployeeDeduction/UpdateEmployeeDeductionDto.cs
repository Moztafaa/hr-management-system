using System.ComponentModel;

namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class UpdateEmployeeDeductionDto
{
    [DefaultValue(null)]
    public decimal? Amount { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
}

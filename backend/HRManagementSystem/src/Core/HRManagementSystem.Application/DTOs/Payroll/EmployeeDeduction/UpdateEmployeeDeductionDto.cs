namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class UpdateEmployeeDeductionDto
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
}

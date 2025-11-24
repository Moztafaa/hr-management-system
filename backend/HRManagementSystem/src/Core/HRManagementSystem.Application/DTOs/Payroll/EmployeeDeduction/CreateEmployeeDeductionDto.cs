namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class CreateEmployeeDeductionDto
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class CreateEmployeeDeductionDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
}

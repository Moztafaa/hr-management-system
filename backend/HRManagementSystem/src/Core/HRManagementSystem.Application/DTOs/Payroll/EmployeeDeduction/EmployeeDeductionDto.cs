namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class EmployeeDeductionDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public string DeductionName { get; set; } = default!;
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class CreateDeductionDto
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; }
}

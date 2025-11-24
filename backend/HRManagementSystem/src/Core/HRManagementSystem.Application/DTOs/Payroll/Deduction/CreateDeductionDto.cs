namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class CreateDeductionDto
{
    public string Name { get; set; } = default!;
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
}

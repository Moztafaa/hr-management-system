namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class UpdateDeductionDto
{
    public string? Name { get; set; } = default!;
    public decimal? Amount { get; set; }
    public bool? IsPercentage { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class DeductionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; }
}

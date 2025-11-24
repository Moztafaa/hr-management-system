namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class DeductionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
}

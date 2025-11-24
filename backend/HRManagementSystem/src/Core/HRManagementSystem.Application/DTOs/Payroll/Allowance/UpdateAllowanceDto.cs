namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class UpdateAllowanceDto
{
    public string Name { get; set; } = default!;
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
}

namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class CreateAllowanceDto
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; }
}

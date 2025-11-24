namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class AllowanceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsPercentage { get; set; }
    public decimal Value { get; set; }
}

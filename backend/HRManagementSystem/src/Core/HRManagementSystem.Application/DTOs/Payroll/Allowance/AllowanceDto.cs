namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class AllowanceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
}

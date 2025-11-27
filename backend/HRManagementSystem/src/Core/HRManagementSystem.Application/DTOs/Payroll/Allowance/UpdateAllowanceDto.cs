

namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class UpdateAllowanceDto
{
    public string? Name { get; set; } = default!;
    public decimal? Amount { get; set; }
    public bool? IsPercentage { get; set; }
}

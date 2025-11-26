using System.ComponentModel;

namespace HRManagementSystem.Application.DTOs.Payroll.Allowance;

public class UpdateAllowanceDto
{
    public string? Name { get; set; } = default!;

    [DefaultValue(null)]
    public bool? IsPercentage { get; set; }

    [DefaultValue(null)]
    public decimal? Value { get; set; }
}

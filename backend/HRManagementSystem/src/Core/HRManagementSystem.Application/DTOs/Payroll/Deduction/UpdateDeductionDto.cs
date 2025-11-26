using System.ComponentModel;

namespace HRManagementSystem.Application.DTOs.Payroll.Deduction;

public class UpdateDeductionDto
{
    public string? Name { get; set; } = default!;

    [DefaultValue(null)]
    public bool? IsPercentage { get; set; }

    [DefaultValue(null)]
    public decimal? Value { get; set; }
}

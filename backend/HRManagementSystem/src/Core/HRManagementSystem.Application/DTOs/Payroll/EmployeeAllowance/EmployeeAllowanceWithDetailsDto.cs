namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeAllowance;

public class EmployeeAllowanceWithDetailsDto
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }

    public string AllowanceName { get; set; }
    public decimal AllowanceDefaultAmount { get; set; }
    public bool AllowanceDefaultIsPercentage { get; set; }

    public decimal? OverrideAmount { get; set; }
    public bool? OverrideIsPercentage { get; set; }

    public decimal EffectiveAmount { get; set; }
    public bool EffectiveIsPercentage { get; set; }

    public RecurrenceType Recurrence { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


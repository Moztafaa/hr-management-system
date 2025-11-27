using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;

public class EmployeeDeduction
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsPercentage { get; set; }
    public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneTime;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Employee Employee { get; set; }
    public Deduction Deduction { get; set; }
}

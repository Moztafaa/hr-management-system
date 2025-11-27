using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;

public class EmployeeAllowance
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public decimal? Amount { get; set; }       
    public bool? IsPercentage { get; set; }
    public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneTime;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Employee Employee { get; set; }
    public Allowance Allowance { get; set; }
}

using System.Text.Json.Serialization;

namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

public class EmployeeDeductionDto
{
    public int EmployeeId { get; set; }
    public int DeductionId { get; set; }
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RecurrenceType Recurrence { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

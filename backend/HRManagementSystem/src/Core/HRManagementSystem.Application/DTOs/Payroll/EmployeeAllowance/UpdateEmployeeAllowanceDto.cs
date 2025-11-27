using System.Text.Json.Serialization;

namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeAllowance;

public class UpdateEmployeeAllowanceDto
{
    public int EmployeeId { get; set; }
    public int? AllowanceId { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsPercentage { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RecurrenceType? Recurrence { get; set; } = RecurrenceType.OneTime;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

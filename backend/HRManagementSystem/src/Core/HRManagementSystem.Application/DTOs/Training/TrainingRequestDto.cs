using System.Text.Json.Serialization;

namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingRequestDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;

    public int TrainingCourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;

    public TrainingRequestStatus Status { get; set; }
    public DateTime RequestDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ReviewedByManagerId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ManagerName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ReviewedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ManagerNote { get; set; }

    public string? EmployeeNote { get; set; }
}

using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;
public class TrainingRequest : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int TrainingCourseId { get; set; }
    public TrainingCourse? TrainingCourse { get; set; }

    public TrainingRequestStatus Status { get; set; } = TrainingRequestStatus.Pending;

    public DateTime RequestDate { get; set; } = DateTime.UtcNow;

    public int? ReviewedByManagerId { get; set; }
    public Employee? Reviewer { get; set; }

    public DateTime? ReviewedAt { get; set; }
    public string? ManagerNote { get; set; }
    public string? EmployeeNote { get; set; }

}

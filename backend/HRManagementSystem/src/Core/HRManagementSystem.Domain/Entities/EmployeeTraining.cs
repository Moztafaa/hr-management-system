using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;
public class EmployeeTraining : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int TrainingCourseId { get; set; }
    public TrainingCourse? TrainingCourse { get; set; }

    public TrainingStatus Status { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletionDate { get; set; }
    public bool RewardGiven { get; set; } = false;     // prevents double bonus
}

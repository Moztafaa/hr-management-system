namespace HRManagementSystem.Domain.Entities;
public class TrainingCourse : BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? DurationHours { get; set; }

    public ICollection<EmployeeTraining>? EmployeeTrainings { get; set; }
}


namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int? DurationHours { get; set; }
}


namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingCourseUpdateDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? DurationHours { get; set; }
}


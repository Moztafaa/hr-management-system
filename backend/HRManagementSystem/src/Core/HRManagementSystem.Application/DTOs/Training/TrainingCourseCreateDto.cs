namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingCourseCreateDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? DurationHours { get; set; }  
}

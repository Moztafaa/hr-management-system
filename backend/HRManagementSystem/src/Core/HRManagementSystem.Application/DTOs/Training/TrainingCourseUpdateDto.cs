using System.ComponentModel;

namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingCourseUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    [DefaultValue(null)]
    public int? DurationHours { get; set; }
}


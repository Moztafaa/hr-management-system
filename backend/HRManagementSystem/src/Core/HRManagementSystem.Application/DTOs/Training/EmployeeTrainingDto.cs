namespace HRManagementSystem.Application.DTOs.Training;
public class EmployeeTrainingDto
{
    public int EmployeeId { get; set; }
    public int TrainingCourseId { get; set; }
    public string Status { get; set; } = null!; // Enrolled, Completed, Cancelled
    public DateTime EnrollmentDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public bool RewardGiven { get; set; }

    public string? EmployeeName { get; set; }       // Optional for convenience
    public string? TrainingCourseTitle { get; set; } // Optional for convenience
}


namespace HRManagementSystem.Application.DTOs.Training;
public class TrainingRequestCreateDto
{
    public int EmployeeId { get; set; }            
    public int TrainingCourseId { get; set; }
    public string? EmployeeNote { get; set; }         
}

namespace HRManagementSystem.Application.DTOs.ESS;

public class ESSProfileDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public DateTime HireDate { get; set; }
    public required string Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
    public string? DepartmentName { get; set; }
    public string? DesignationName { get; set; }
    public int DeptId { get; set; }
    public int DesignationId { get; set; }
}

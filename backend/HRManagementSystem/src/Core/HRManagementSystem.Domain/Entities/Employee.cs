namespace HRManagementSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? EFF_Start { get; set; }
    public DateTime? EFF_End { get; set; }
    public required string Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
    public decimal BasicSalary { get; set; }
    public string? ApplicationUserId { get; set; }

    // TODO: Navigation property (e.g., Department, Designation, etc.) can be added here in the future
}

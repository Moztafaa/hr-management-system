namespace HRManagementSystem.Application.DTOs.ESS;

public class UpdateESSProfileDto
{
    public required string Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
}

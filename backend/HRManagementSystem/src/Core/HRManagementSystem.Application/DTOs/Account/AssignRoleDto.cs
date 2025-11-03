namespace HRManagementSystem.Application.DTOs.Account;

public class AssignRoleDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public required string RoleName { get; set; }
}

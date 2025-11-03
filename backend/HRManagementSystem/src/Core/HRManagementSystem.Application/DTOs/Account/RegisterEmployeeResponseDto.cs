namespace HRManagementSystem.Application.DTOs.Account;

public class RegisterEmployeeResponseDto
{
    public Guid UserId { get; set; }
    public int EmployeeId { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public List<string> Roles { get; set; } = new();
    public string? Token { get; set; }
    public DateTime? TokenExpiration { get; set; }
}

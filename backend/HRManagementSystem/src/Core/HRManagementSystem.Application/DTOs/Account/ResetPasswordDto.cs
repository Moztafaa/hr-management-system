namespace HRManagementSystem.Application.DTOs.Account;

public class ResetPasswordDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Token { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword")]
    public required string ConfirmNewPassword { get; set; }
}

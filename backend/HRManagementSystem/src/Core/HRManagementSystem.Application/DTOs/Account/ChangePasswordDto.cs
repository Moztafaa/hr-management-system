namespace HRManagementSystem.Application.DTOs.Account;

public class ChangePasswordDto
{
    [Required]
    public required string CurrentPassword { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword")]
    public required string ConfirmNewPassword { get; set; }
}

namespace HRManagementSystem.Application.Interfaces;

public interface IAccountService
{
    // Authentication
    Task<Response<RegisterEmployeeResponseDto>> RegisterEmployeeAsync(RegisterEmployeeDto registerEmployeeDto);
    Task<Response<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    Task<Response<bool>> LogoutAsync();

    // Password Management
    Task<Response<bool>> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);
    Task<Response<bool>> ForgotPasswordAsync(string email);
    Task<Response<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    // User Management
    Task<Response<UserDto>> GetUserByIdAsync(Guid userId);
    Task<Response<UserDto>> GetUserByEmailAsync(string email);
    Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<Response<bool>> UpdateUserAsync(Guid userId, UserDto userDto);
    Task<Response<bool>> UpdateUserEmailAsync(Guid userId, string newEmail);
    Task<Response<bool>> DeleteUserAsync(Guid userId);
    Task<Response<bool>> UserExistsAsync(string email);

    // Role Management
    Task<Response<bool>> AssignRoleAsync(AssignRoleDto assignRoleDto);
    Task<Response<bool>> RemoveRoleAsync(Guid userId, string roleName);
    Task<Response<IEnumerable<string>>> GetUserRolesAsync(Guid userId);
    Task<Response<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName);


}

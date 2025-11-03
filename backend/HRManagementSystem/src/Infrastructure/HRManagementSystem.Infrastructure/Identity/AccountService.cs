using HRManagementSystem.Application.Common;

namespace HRManagementSystem.Infrastructure.Identity;


public class AccountService(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    RoleManager<ApplicationRole> _roleManager,
    ApplicationDbContext _context,
    JwtTokenGenerator _jwtGenerator
    ) : IAccountService
{

    #region Authentication

    public async Task<Response<RegisterEmployeeResponseDto>> RegisterEmployeeAsync(RegisterEmployeeDto registerEmployeeDto)
    {
        // TODO: Will be replaced with UnitOfWork pattern ...
        // Start a transaction to ensure both user and employee are created together
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. Check if user already exists
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(registerEmployeeDto.Email);
            if (existingUser != null)
            {
                return new Response<RegisterEmployeeResponseDto>(default!, "User with this email already exists.", true);
            }

            // 2. Create the ApplicationUser
            var user = new ApplicationUser
            {
                UserName = registerEmployeeDto.Email,
                Email = registerEmployeeDto.Email,
                PhoneNumber = registerEmployeeDto.PhoneNumber ?? registerEmployeeDto.ContactNumber
            };

            IdentityResult userResult = await _userManager.CreateAsync(user, registerEmployeeDto.Password);

            if (!userResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return new Response<RegisterEmployeeResponseDto>(default!, string.Join(", ", userResult.Errors.Select(e => e.Description)), true);
            }

            // 3. Assign roles if provided
            if (registerEmployeeDto.Roles != null && registerEmployeeDto.Roles.Any())
            {
                foreach (string roleName in registerEmployeeDto.Roles)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(roleName);
                    if (roleExists)
                    {
                        await _userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }

            // 4. Create the Employee entity
            var employee = new Employee
            {
                FirstName = registerEmployeeDto.FirstName,
                LastName = registerEmployeeDto.LastName,
                Email = registerEmployeeDto.Email,
                DateOfBirth = registerEmployeeDto.DateOfBirth,
                Gender = registerEmployeeDto.Gender,
                HireDate = registerEmployeeDto.HireDate,
                EFF_Start = registerEmployeeDto.EFF_Start,
                EFF_End = registerEmployeeDto.EFF_End,
                ContactNumber = registerEmployeeDto.ContactNumber,
                Address = registerEmployeeDto.Address,
                BasicSalary = registerEmployeeDto.BasicSalary,
                ApplicationUserId = user.Id.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // 5. Link the employee to the user
            user.EmployeeId = employee.Id;
            await _userManager.UpdateAsync(user);

            // Commit the transaction
            await transaction.CommitAsync();

            // 6. Get user roles for response
            IList<string> roles = await _userManager.GetRolesAsync(user);

            var response = new RegisterEmployeeResponseDto
            {
                UserId = user.Id,
                EmployeeId = employee.Id,
                Email = user.Email!,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Roles = roles.ToList()
            };

            return new Response<RegisterEmployeeResponseDto>(response, string.Empty, false);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new Response<RegisterEmployeeResponseDto>(default!, $"An error occurred while registering the employee: {ex.Message}", true);
        }
    }

    public async Task<Response<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return new Response<AuthResponseDto>(default!, "Invalid email or password.", true);
        }

        SignInResult result = await _signInManager.PasswordSignInAsync(
            user,
            loginDto.Password,
            loginDto.RememberMe,
            lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return new Response<AuthResponseDto>(default!, "Account is locked out.", true);
            }
            if (result.IsNotAllowed)
            {
                return new Response<AuthResponseDto>(default!, "Login is not allowed. Please confirm your email.", true);
            }
            if (result.RequiresTwoFactor)
            {
                return new Response<AuthResponseDto>(default!, "Two-factor authentication is required.", true);
            }

            return new Response<AuthResponseDto>(default!, "Invalid email or password.", true);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        (string? token, DateTime expires) = _jwtGenerator.GenerateToken(user, roles);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email!,
            FullName = user.UserName!,
            EmployeeId = user.EmployeeId,
            Roles = roles.ToList(),
            Token = token,
            TokenExpiration = expires
        };

        return new Response<AuthResponseDto>(response, string.Empty, false);
    }

    public async Task<Response<bool>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new Response<bool>(true, string.Empty, false);
    }

    #endregion

    #region Password Management

    public async Task<Response<bool>> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        IdentityResult result = await _userManager.ChangePasswordAsync(
            user,
            changePasswordDto.CurrentPassword,
            changePasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> ForgotPasswordAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return new Response<bool>(true, string.Empty, false);
        }

        // Generate password reset token
        _ = await _userManager.GeneratePasswordResetTokenAsync(user);

        // TODO: Send email with token

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        IdentityResult result = await _userManager.ResetPasswordAsync(
            user,
            resetPasswordDto.Token,
            resetPasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    #endregion

    #region User Management

    public async Task<Response<UserDto>> GetUserByIdAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<UserDto>(default!, "User not found.", true);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            AccessFailedCount = user.AccessFailedCount,
            EmployeeId = user.EmployeeId,
            Roles = roles.ToList()
        };

        return new Response<UserDto>(userDto, string.Empty, false);
    }

    public async Task<Response<UserDto>> GetUserByEmailAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new Response<UserDto>(default!, "User not found.", true);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            AccessFailedCount = user.AccessFailedCount,
            EmployeeId = user.EmployeeId,
            Roles = roles.ToList()
        };

        return new Response<UserDto>(userDto, string.Empty, false);
    }

    public async Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        List<ApplicationUser> users = await _userManager.Users.ToListAsync();
        var userDtos = new List<UserDto>();

        foreach (ApplicationUser? user in users)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                AccessFailedCount = user.AccessFailedCount,
                EmployeeId = user.EmployeeId,
                Roles = roles.ToList()
            });
        }

        return new Response<IEnumerable<UserDto>>(userDtos, string.Empty, false);
    }

    public async Task<Response<bool>> UpdateUserAsync(Guid userId, UserDto userDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        user.Email = userDto.Email;
        user.UserName = userDto.UserName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.EmployeeId = userDto.EmployeeId;

        IdentityResult result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> DeleteUserAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        IdentityResult result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> UserExistsAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        bool exists = user != null;
        return new Response<bool>(exists, string.Empty, false);
    }

    #endregion

    #region Role Management

    public async Task<Response<bool>> AssignRoleAsync(AssignRoleDto assignRoleDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(assignRoleDto.UserId.ToString());
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        bool roleExists = await _roleManager.RoleExistsAsync(assignRoleDto.RoleName);
        if (!roleExists)
        {
            return new Response<bool>(false, $"Role '{assignRoleDto.RoleName}' does not exist.", true);
        }

        bool isInRole = await _userManager.IsInRoleAsync(user, assignRoleDto.RoleName);
        if (isInRole)
        {
            return new Response<bool>(false, $"User is already in role '{assignRoleDto.RoleName}'.", true);
        }

        IdentityResult result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<bool>> RemoveRoleAsync(Guid userId, string roleName)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<bool>(false, "User not found.", true);
        }

        bool isInRole = await _userManager.IsInRoleAsync(user, roleName);
        if (!isInRole)
        {
            return new Response<bool>(false, $"User is not in role '{roleName}'.", true);
        }

        IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            return new Response<bool>(false, string.Join(", ", result.Errors.Select(e => e.Description)), true);
        }

        return new Response<bool>(true, string.Empty, false);
    }

    public async Task<Response<IEnumerable<string>>> GetUserRolesAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new Response<IEnumerable<string>>(Array.Empty<string>(), "User not found.", true);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);
        return new Response<IEnumerable<string>>(roles, string.Empty, false);
    }

    public async Task<Response<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName)
    {
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            return new Response<IEnumerable<UserDto>>(Array.Empty<UserDto>(), $"Role '{roleName}' does not exist.", true);
        }

        IList<ApplicationUser> users = await _userManager.GetUsersInRoleAsync(roleName);
        var userDtos = new List<UserDto>();

        foreach (ApplicationUser user in users)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                AccessFailedCount = user.AccessFailedCount,
                EmployeeId = user.EmployeeId,
                Roles = roles.ToList()
            });
        }

        return new Response<IEnumerable<UserDto>>(userDtos, string.Empty, false);
    }

    #endregion

}


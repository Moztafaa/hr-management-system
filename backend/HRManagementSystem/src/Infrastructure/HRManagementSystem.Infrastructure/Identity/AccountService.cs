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

    public async Task<(bool Succeeded, RegisterEmployeeResponseDto? Response, IEnumerable<string> Errors)> RegisterEmployeeAsync(RegisterEmployeeDto registerEmployeeDto)
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
                return (false, null, new[] { "User with this email already exists." });
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
                return (false, null, userResult.Errors.Select(e => e.Description));
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

            return (true, response, Array.Empty<string>());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, null, new[] { $"An error occurred while registering the employee: {ex.Message}" });
        }
    }

    public async Task<(bool Succeeded, AuthResponseDto? Response, IEnumerable<string> Errors)> LoginAsync(LoginDto loginDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return (false, null, new[] { "Invalid email or password." });
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
                return (false, null, new[] { "Account is locked out." });
            }
            if (result.IsNotAllowed)
            {
                return (false, null, new[] { "Login is not allowed. Please confirm your email." });
            }
            if (result.RequiresTwoFactor)
            {
                return (false, null, new[] { "Two-factor authentication is required." });
            }

            return (false, null, new[] { "Invalid email or password." });
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        (var token, var expires) = _jwtGenerator.GenerateToken(user, roles);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email!,
            FullName = user.UserName!,
            EmployeeId = user.EmployeeId,
            Roles = roles.ToList(),
            Token=token,
            TokenExpiration= expires
        };

        return (true, response, Array.Empty<string>());
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    #endregion

    #region Password Management

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        IdentityResult result = await _userManager.ChangePasswordAsync(
            user,
            changePasswordDto.CurrentPassword,
            changePasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> ForgotPasswordAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return (true, Array.Empty<string>());
        }

        // Generate password reset token
        _ = await _userManager.GeneratePasswordResetTokenAsync(user);

        // TODO: Send email with token

        return (true, Array.Empty<string>());
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        IdentityResult result = await _userManager.ResetPasswordAsync(
            user,
            resetPasswordDto.Token,
            resetPasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    #endregion

    #region User Management

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return null;
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        return new UserDto
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
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        return new UserDto
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
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
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

        return userDtos;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(Guid userId, UserDto userDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        user.Email = userDto.Email;
        user.UserName = userDto.UserName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.EmployeeId = userDto.EmployeeId;

        IdentityResult result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        IdentityResult result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    #endregion

    #region Role Management

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(AssignRoleDto assignRoleDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(assignRoleDto.UserId.ToString());
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        bool roleExists = await _roleManager.RoleExistsAsync(assignRoleDto.RoleName);
        if (!roleExists)
        {
            return (false, new[] { $"Role '{assignRoleDto.RoleName}' does not exist." });
        }

        bool isInRole = await _userManager.IsInRoleAsync(user, assignRoleDto.RoleName);
        if (isInRole)
        {
            return (false, new[] { $"User is already in role '{assignRoleDto.RoleName}'." });
        }

        IdentityResult result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> RemoveRoleAsync(Guid userId, string roleName)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return (false, new[] { "User not found." });
        }

        bool isInRole = await _userManager.IsInRoleAsync(user, roleName);
        if (!isInRole)
        {
            return (false, new[] { $"User is not in role '{roleName}'." });
        }

        IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        return (true, Array.Empty<string>());
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return Array.Empty<string>();
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

    public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName)
    {
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            return Array.Empty<UserDto>();
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

        return userDtos;
    }

    #endregion

}

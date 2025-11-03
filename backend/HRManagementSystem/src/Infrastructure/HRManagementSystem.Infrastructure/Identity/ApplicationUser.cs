namespace HRManagementSystem.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public int? EmployeeId { get; set; }
}

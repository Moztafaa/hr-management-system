using System.Reflection.Emit;
using HRManagementSystem.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply configurations from current assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Apply configurations
        builder.ApplyConfiguration(new EmployeeConfiguration());
        builder.ApplyConfiguration(new DepartmentConfiguration());
        builder.ApplyConfiguration(new DesignationConfiguration());
        builder.ApplyConfiguration(new LeaveTypeConfiguration());
        builder.ApplyConfiguration(new LeaveRequestConfiguration());
        builder.ApplyConfiguration(new LeaveApprovalConfiguration());
        builder.ApplyConfiguration(new EmployeeLeaveBalanceConfiguration());

        //Add Leave types in database
        builder.SeedLeaveTypes();
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveApproval> LeaveApprovals { get; set; }
    public DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
    public DbSet<JobPosting> JobPostings { get; set; }
}

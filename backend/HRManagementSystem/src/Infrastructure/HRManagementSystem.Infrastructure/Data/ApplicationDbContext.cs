using System.Reflection.Emit;
using HRManagementSystem.Infrastructure.Configurations;
using HRManagementSystem.Infrastructure.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        builder.ApplyConfiguration(new JobPostingConfiguration());
        builder.ApplyConfiguration(new EmployeeAllowanceConfiguration());
        builder.ApplyConfiguration(new EmployeeDeductionConfiguration());
        builder.ApplyConfiguration(new PayslipConfiguration());

        // Seed initial data
        builder.SeedDepartments();
        builder.SeedDesignations();
        builder.SeedEmployees();
        builder.SeedLeaveTypes();
        builder.SeedCandidates();
        builder.SeedJobPostings();
        builder.SeedJobApplications();

        // Seed Payroll data
        builder.SeedAllowances();
        builder.SeedDeductions();
        builder.SeedEmployeeAllowances();
        builder.SeedEmployeeDeductions();
        builder.SeedPayslips();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry<BaseEntity>> entries = ChangeTracker.Entries<BaseEntity>();

        foreach (EntityEntry<BaseEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }

    public DbSet<Payslip> Payslips { get; set; }
    public DbSet<Allowance> Allowances { get; set; }
    public DbSet<Deduction> Deductions { get; set; }
    public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }
    public DbSet<EmployeeDeduction> EmployeeDeductions { get; set; }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveApproval> LeaveApprovals { get; set; }
    public DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
    public DbSet<JobPosting> JobPostings { get; set; }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
}

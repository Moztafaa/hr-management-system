using System;
using HRManagementSystem.Domain.Entities;
namespace HRManagementSystem.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.HireDate).IsRequired();
        builder.Property(e => e.BasicSalary).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(e => e.Gender).HasMaxLength(20);
        builder.Property(e => e.ContactNumber).HasMaxLength(50);
        builder.Property(e => e.Address).HasMaxLength(500);

        // Relationships
        builder.HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Designation)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DesignationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LeaveRequests)
               .WithOne(lr => lr.Employee)
               .HasForeignKey(lr => lr.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LeaveApprovals)
               .WithOne(la => la.Approver)
               .HasForeignKey(la => la.ApproverId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LeaveBalances)
               .WithOne(lb => lb.Employee)
               .HasForeignKey(lb => lb.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

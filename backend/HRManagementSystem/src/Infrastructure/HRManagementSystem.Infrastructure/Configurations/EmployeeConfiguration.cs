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
    }
}

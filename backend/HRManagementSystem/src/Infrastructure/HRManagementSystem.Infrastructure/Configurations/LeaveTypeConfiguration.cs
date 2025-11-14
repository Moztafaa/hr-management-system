using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder) {
        builder.ToTable("LeaveTypes");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
        builder.Property(l => l.Description).HasMaxLength(500);
        builder.Property(l => l.GenderRestriction).HasMaxLength(100);
        builder.Property(l => l.MaxDays).IsRequired();
        builder.Property(l => l.CanCarryForward).IsRequired();
        builder.Property(l => l.CarryForwardLimit);
        builder.Property(l => l.CreatedAt).IsRequired();

        builder.HasMany(l => l.LeaveRequests)
               .WithOne(lr => lr.LeaveType)
               .HasForeignKey(lr => lr.LeaveTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.EmployeeLeaveBalances)
               .WithOne(lb => lb.LeaveType)
               .HasForeignKey(lb => lb.LeaveTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

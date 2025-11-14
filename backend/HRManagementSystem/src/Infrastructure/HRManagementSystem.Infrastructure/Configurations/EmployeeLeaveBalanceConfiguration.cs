using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class EmployeeLeaveBalanceConfiguration : IEntityTypeConfiguration<EmployeeLeaveBalance>
{
    public void Configure(EntityTypeBuilder<EmployeeLeaveBalance> builder){
        builder.ToTable("EmployeeLeaveBalances");

        builder.HasKey(lb => lb.Id);

        builder.Property(lb => lb.Year).IsRequired();
        builder.Property(lb => lb.TotalAllocated).IsRequired();
        builder.Property(lb => lb.UsedDays).IsRequired();
        builder.Property(lb => lb.RemainingDays).IsRequired();
        builder.Property(lb => lb.LastUpdated).IsRequired();

        builder.HasOne(lb => lb.Employee)
               .WithMany(e => e.LeaveBalances)
               .HasForeignKey(lb => lb.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lb => lb.LeaveType)
               .WithMany(lt => lt.EmployeeLeaveBalances)
               .HasForeignKey(lb => lb.LeaveTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

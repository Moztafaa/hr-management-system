using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder){
        builder.ToTable("LeaveRequests");

        builder.HasKey(lr => lr.Id);

        builder.Property(lr => lr.StartDate).IsRequired();
        builder.Property(lr => lr.EndDate).IsRequired();
        builder.Property(lr => lr.TotalDays).IsRequired();
        builder.Property(lr => lr.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

        builder.HasOne(lr => lr.Employee)
               .WithMany(e => e.LeaveRequests)
               .HasForeignKey(lr => lr.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lr => lr.LeaveType)
               .WithMany(lt => lt.LeaveRequests)
               .HasForeignKey(lr => lr.LeaveTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lr => lr.Reviewer)
               .WithMany()
               .HasForeignKey(lr => lr.ReviewedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(lr => lr.LeaveApprovals)
               .WithOne(la => la.LeaveRequest)
               .HasForeignKey(la => la.LeaveRequestId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

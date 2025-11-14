using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class LeaveApprovalConfiguration : IEntityTypeConfiguration<LeaveApproval>
{
    public void Configure(EntityTypeBuilder<LeaveApproval> builder) {
        builder.ToTable("LeaveApprovals");

        builder.HasKey(la => la.Id);

        builder.Property(la => la.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
        builder.Property(la => la.Level)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
        builder.Property(la => la.ActionDate);

        builder.HasOne(la => la.LeaveRequest)
               .WithMany(lr => lr.LeaveApprovals)
               .HasForeignKey(la => la.LeaveRequestId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(la => la.Approver)
               .WithMany(e => e.LeaveApprovals)
               .HasForeignKey(la => la.ApproverId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

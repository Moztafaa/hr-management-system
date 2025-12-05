using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Configurations;

public class ResignationApprovalConfiguration : IEntityTypeConfiguration<ResignationApproval>
{
    public void Configure(EntityTypeBuilder<ResignationApproval> builder)
    {
        builder.ToTable("ResignationApprovals");

        builder.HasKey(ra => ra.Id);

        builder.Property(ra => ra.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(ra => ra.Level)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(ra => ra.ActionDate);

        builder.Property(ra => ra.Comments)
               .HasMaxLength(1000);

        builder.HasOne(ra => ra.Resignation)
               .WithMany(r => r.ResignationApprovals)
               .HasForeignKey(ra => ra.ResignationId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ra => ra.Approver)
               .WithMany(e => e.ResignationApprovals)
               .HasForeignKey(ra => ra.ApproverId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

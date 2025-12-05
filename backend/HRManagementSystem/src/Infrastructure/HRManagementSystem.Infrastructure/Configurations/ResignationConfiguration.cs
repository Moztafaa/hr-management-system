using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Configurations;

public class ResignationConfiguration : IEntityTypeConfiguration<Resignation>
{
    public void Configure(EntityTypeBuilder<Resignation> builder)
    {
        builder.ToTable("Resignations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Reason)
               .IsRequired()
               .HasMaxLength(2000);

        builder.Property(r => r.SubmissionDate)
               .IsRequired();

        builder.Property(r => r.LastWorkingDate)
               .IsRequired();

        builder.Property(r => r.NoticePeriodDays)
               .IsRequired();

        builder.Property(r => r.IsImmediateResignation)
               .IsRequired();

        builder.Property(r => r.HandoverNotes)
               .HasMaxLength(2000);

        builder.Property(r => r.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(r => r.RejectionReason)
               .HasMaxLength(1000);

        builder.HasOne(r => r.Employee)
               .WithMany(e => e.Resignations)
               .HasForeignKey(r => r.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Reviewer)
               .WithMany()
               .HasForeignKey(r => r.ReviewedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.ResignationApprovals)
               .WithOne(ra => ra.Resignation)
               .HasForeignKey(ra => ra.ResignationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

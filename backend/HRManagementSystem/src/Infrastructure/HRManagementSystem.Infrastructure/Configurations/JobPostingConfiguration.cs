using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Configurations;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.ToTable("JobPostings");

        builder.HasKey(jp => jp.Id);

        builder.Property(jp => jp.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(jp => jp.Description)
            .HasMaxLength(2000);

        builder.Property(jp => jp.Requirements);

        builder.Property(jp => jp.PostedDate)
            .IsRequired();

        builder.Property(jp => jp.ClosingDate);

        builder.Property(jp => jp.IsActive)
            .HasDefaultValue(true);

        // Configure relationships
        builder.HasOne(jp => jp.Department)
            .WithMany()
            .HasForeignKey(jp => jp.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(jp => jp.Designation)
            .WithMany()
            .HasForeignKey(jp => jp.DesignationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

namespace HRManagementSystem.Infrastructure.Configurations;

public class TrainingRequestConfiguration : IEntityTypeConfiguration<TrainingRequest>
{
    public void Configure(EntityTypeBuilder<TrainingRequest> builder)
    {
        builder.ToTable("EmployeeTrainingRequests");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Status)
               .HasConversion<string>()
               .HasDefaultValue(TrainingRequestStatus.Pending)
               .IsRequired();

        builder.Property(r => r.RequestDate)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(r => r.ManagerNote)
               .HasMaxLength(1000)
               .IsRequired(false);

        // FK to Employee (Requester)
        builder.HasOne(r => r.Employee)
               .WithMany(e => e.TrainingRequests)
               .HasForeignKey(r => r.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        // FK to Manager (Reviewer)
        builder.HasOne(r => r.Reviewer)
               .WithMany()
               .HasForeignKey(r => r.ReviewedByManagerId)
               .OnDelete(DeleteBehavior.Restrict);

        // FK to Training Course
        builder.HasOne(r => r.TrainingCourse)
               .WithMany()
               .HasForeignKey(r => r.TrainingCourseId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}


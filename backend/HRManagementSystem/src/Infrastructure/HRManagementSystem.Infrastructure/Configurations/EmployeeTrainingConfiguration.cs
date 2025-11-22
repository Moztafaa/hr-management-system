namespace HRManagementSystem.Infrastructure.Configurations;
public class EmployeeTrainingConfiguration : IEntityTypeConfiguration<EmployeeTraining>
{
    public void Configure(EntityTypeBuilder<EmployeeTraining> builder)
    {
        builder.ToTable("EmployeeTrainings");

        builder.HasKey(et => new { et.EmployeeId, et.TrainingCourseId });

        builder.HasOne(et => et.Employee)
               .WithMany(e => e.EmployeeTrainings)
               .HasForeignKey(et => et.EmployeeId);

        builder.HasOne(et => et.TrainingCourse)
               .WithMany(tc => tc.EmployeeTrainings)
               .HasForeignKey(et => et.TrainingCourseId);

        builder.Property(et => et.EnrollmentDate)
                .HasDefaultValueSql("GETDATE()");

        builder.Property(et => et.CompletionDate)
       .IsRequired(false);

        builder.Property(et => et.RewardGiven)
       .HasDefaultValue(false);

        builder.Property(et => et.Status)
                    .HasConversion<string>()
                    .HasDefaultValue(TrainingStatus.Enrolled)
                    .IsRequired();
    }
}


namespace HRManagementSystem.Infrastructure.Configurations;
public class TrainingCourseConfiguration : IEntityTypeConfiguration<TrainingCourse>
{
    public void Configure(EntityTypeBuilder<TrainingCourse> builder)
    {
        builder.ToTable("TrainingCourses");

        builder.HasKey(tc => tc.Id);

        builder.Property(tc => tc.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(tc => tc.Description)
               .HasMaxLength(1000);

        builder.Property(tc => tc.DurationHours)
               .IsRequired(false);
    }
}


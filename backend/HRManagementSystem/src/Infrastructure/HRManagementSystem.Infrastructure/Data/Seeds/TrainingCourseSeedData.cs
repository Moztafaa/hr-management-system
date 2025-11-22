namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class TrainingCourseSeedData
{
    public static void SeedTrainingCourses(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingCourse>().HasData(
            new TrainingCourse
            {
                Id = 1,
                Title = "Effective Communication",
                Description = "Improve interpersonal and workplace communication skills.",
                DurationHours = 8
            },
            new TrainingCourse
            {
                Id = 2,
                Title = "Leadership Essentials",
                Description = "Core leadership principles and team management skills.",
                DurationHours = 16
            },
            new TrainingCourse
            {
                Id = 3,
                Title = "Project Management Fundamentals",
                Description = "Basics of project planning, scope, scheduling, and risk.",
                DurationHours = 40
            },
            new TrainingCourse
            {
                Id = 4,
                Title = "Agile & Scrum Workshop",
                Description = "Workshop covering Agile principles and Scrum ceremonies.",
                DurationHours = 16
            },
            new TrainingCourse
            {
                Id = 5,
                Title = "Time Management",
                Description = "Prioritization and productivity techniques.",
                DurationHours = 4
            },
            new TrainingCourse
            {
                Id = 6,
                Title = "Advanced C# Programming",
                Description = "Deep dive into C# features and best practices.",
                DurationHours = 40
            },
            new TrainingCourse
            {
                Id = 7,
                Title = "Data Protection & Security",
                Description = "Secure coding, compliance, and data protection essentials.",
                DurationHours = 8
            },
            new TrainingCourse
            {
                Id = 8,
                Title = "Customer Service Excellence",
                Description = "Best practices for customer interactions.",
                DurationHours = 8
            }
        );
    }
}

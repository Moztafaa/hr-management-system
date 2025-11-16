namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DesignationSeedData
{
    public static void SeedDesignations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Designation>().HasData(
            // IT Designations
            new Designation
            {
                Id = 1,
                Title = "Software Engineer",
                Description = "Develops and maintains software applications",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 2,
                Title = "Senior Software Engineer",
                Description = "Lead developer responsible for complex technical solutions",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 3,
                Title = "Team Lead",
                Description = "Manages and guides team members to achieve project goals",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 4,
                Title = "DevOps Engineer",
                Description = "Manages deployment pipelines and infrastructure",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 5,
                Title = "QA Engineer",
                Description = "Ensures software quality through testing and automation",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // HR Designations
            new Designation
            {
                Id = 6,
                Title = "HR Manager",
                Description = "Oversees human resources operations and employee management",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 7,
                Title = "HR Specialist",
                Description = "Handles recruitment, onboarding, and employee relations",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 8,
                Title = "Recruiter",
                Description = "Focuses on talent acquisition and candidate screening",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Finance Designations
            new Designation
            {
                Id = 9,
                Title = "Financial Analyst",
                Description = "Analyzes financial data and prepares reports",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 10,
                Title = "Accountant",
                Description = "Manages accounting records and financial transactions",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 11,
                Title = "Finance Manager",
                Description = "Oversees financial planning and budget management",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Sales & Marketing Designations
            new Designation
            {
                Id = 12,
                Title = "Sales Representative",
                Description = "Drives sales and builds customer relationships",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 13,
                Title = "Marketing Specialist",
                Description = "Develops and executes marketing campaigns",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 14,
                Title = "Business Development Manager",
                Description = "Identifies growth opportunities and partnerships",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Operations Designations
            new Designation
            {
                Id = 15,
                Title = "Operations Manager",
                Description = "Manages daily operations and process improvements",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 16,
                Title = "Project Manager",
                Description = "Plans, executes, and closes projects successfully",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Customer Support Designations
            new Designation
            {
                Id = 17,
                Title = "Customer Support Representative",
                Description = "Provides customer assistance and resolves issues",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 18,
                Title = "Support Team Lead",
                Description = "Leads customer support team and ensures service quality",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

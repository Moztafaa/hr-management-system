namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class JobPostingSeedData
{
    public static void SeedJobPostings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobPosting>().HasData(
            new JobPosting
            {
                Id = 1,
                Title = "Senior .NET Developer",
                Description = "We are looking for an experienced .NET developer to join our IT team. The ideal candidate will have strong experience in ASP.NET Core, Entity Framework, and modern web technologies.",
                Requirements = "5+ years of .NET development experience\nProficiency in C#, ASP.NET Core, EF Core\nExperience with SQL Server and Azure\nKnowledge of Angular or React is a plus",
                DepartmentId = 1,
                DesignationId = 2,
                PostedDate = new DateTime(2024, 10, 1),
                ClosingDate = new DateTime(2024, 11, 30),
                IsActive = true,
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobPosting
            {
                Id = 2,
                Title = "HR Specialist",
                Description = "Join our HR team to manage recruitment, onboarding, and employee relations. We're looking for someone with excellent communication skills and HR experience.",
                Requirements = "3+ years of HR experience\nExperience with recruitment and onboarding\nKnowledge of labor laws and HR best practices\nExcellent interpersonal and communication skills",
                DepartmentId = 2,
                DesignationId = 7,
                PostedDate = new DateTime(2024, 10, 5),
                ClosingDate = new DateTime(2024, 12, 5),
                IsActive = true,
                CreatedAt = new DateTime(2024, 10, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobPosting
            {
                Id = 3,
                Title = "Financial Analyst",
                Description = "We need a detail-oriented financial analyst to support our finance team with budgeting, forecasting, and financial reporting.",
                Requirements = "Bachelor's degree in Finance or Accounting\n2+ years of financial analysis experience\nProficiency in Excel and financial software\nStrong analytical and problem-solving skills",
                DepartmentId = 3,
                DesignationId = 9,
                PostedDate = new DateTime(2024, 10, 10),
                ClosingDate = new DateTime(2024, 11, 25),
                IsActive = true,
                CreatedAt = new DateTime(2024, 10, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobPosting
            {
                Id = 4,
                Title = "Marketing Specialist",
                Description = "Looking for a creative marketing specialist to develop and execute marketing campaigns across digital and traditional channels.",
                Requirements = "3+ years of marketing experience\nExperience with digital marketing and SEO\nExcellent content creation skills\nFamiliarity with marketing analytics tools",
                DepartmentId = 4,
                DesignationId = 13,
                PostedDate = new DateTime(2024, 10, 15),
                ClosingDate = new DateTime(2024, 12, 15),
                IsActive = true,
                CreatedAt = new DateTime(2024, 10, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobPosting
            {
                Id = 5,
                Title = "DevOps Engineer",
                Description = "Join our IT team as a DevOps engineer to manage our cloud infrastructure, CI/CD pipelines, and deployment processes.",
                Requirements = "3+ years of DevOps experience\nExperience with Azure/AWS cloud platforms\nProficiency in Docker, Kubernetes\nKnowledge of CI/CD tools (Jenkins, Azure DevOps)",
                DepartmentId = 1,
                DesignationId = 4,
                PostedDate = new DateTime(2024, 10, 20),
                ClosingDate = new DateTime(2024, 12, 1),
                IsActive = true,
                CreatedAt = new DateTime(2024, 10, 20, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class JobApplicationSeedData
{
    public static void SeedJobApplications(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobApplication>().HasData(
            new JobApplication
            {
                Id = 1,
                CandidateId = 1,
                JobPostingId = 1,
                ApplicationDate = new DateTime(2024, 10, 2, 0, 0, 0, DateTimeKind.Utc),
                Status = ApplicationStatus.UnderReview,
                CoverLetter = "I am excited to apply for the Senior .NET Developer position. With 3 years of experience in .NET development, I am confident in my ability to contribute to your team.",
                ExpectedSalary = 18000.00m,
                OfferedSalary = null,
                InterviewDate = new DateTime(2024, 10, 15, 10, 0, 0, DateTimeKind.Utc),
                Notes = "Strong technical background, scheduled for technical interview",
                CreatedAt = new DateTime(2024, 10, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobApplication
            {
                Id = 2,
                CandidateId = 2,
                JobPostingId = 2,
                ApplicationDate = new DateTime(2024, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                Status = ApplicationStatus.InterviewScheduled,
                CoverLetter = "With 5 years of HR experience, I am well-equipped to handle recruitment and employee relations at your organization.",
                ExpectedSalary = 22000.00m,
                OfferedSalary = null,
                InterviewDate = new DateTime(2024, 10, 18, 14, 0, 0, DateTimeKind.Utc),
                Notes = "Excellent HR background, moved to shortlist",
                CreatedAt = new DateTime(2024, 10, 6, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobApplication
            {
                Id = 3,
                CandidateId = 3,
                JobPostingId = 3,
                ApplicationDate = new DateTime(2024, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                Status = ApplicationStatus.Applied,
                CoverLetter = "I am applying for the Financial Analyst position. My background in accounting and financial reporting makes me a great fit.",
                ExpectedSalary = 16000.00m,
                OfferedSalary = null,
                InterviewDate = null,
                Notes = "New application, pending review",
                CreatedAt = new DateTime(2024, 10, 11, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobApplication
            {
                Id = 4,
                CandidateId = 4,
                JobPostingId = 4,
                ApplicationDate = new DateTime(2024, 10, 16, 0, 0, 0, DateTimeKind.Utc),
                Status = ApplicationStatus.UnderReview,
                CoverLetter = "I am passionate about digital marketing and would love to bring my skills to your marketing team.",
                ExpectedSalary = 17000.00m,
                OfferedSalary = null,
                InterviewDate = null,
                Notes = "Good portfolio, under review by marketing manager",
                CreatedAt = new DateTime(2024, 10, 16, 0, 0, 0, DateTimeKind.Utc)
            },
            new JobApplication
            {
                Id = 5,
                CandidateId = 5,
                JobPostingId = 5,
                ApplicationDate = new DateTime(2024, 10, 21, 0, 0, 0, DateTimeKind.Utc),
                Status = ApplicationStatus.Applied,
                CoverLetter = "I am eager to start my DevOps career with your company. I have hands-on experience with Docker and Kubernetes from my academic projects.",
                ExpectedSalary = 13000.00m,
                OfferedSalary = null,
                InterviewDate = null,
                Notes = "Junior candidate, needs experience verification",
                CreatedAt = new DateTime(2024, 10, 21, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

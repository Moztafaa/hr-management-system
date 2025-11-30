using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Performance;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class FeedbackSeedData
{
    public static void SeedFeedbacks(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>().HasData(
            // Feedback for Review 1 (Ahmed Hassan - 2024 Annual)
            new Feedback
            {
                Id = 1,
                PerformanceReviewId = 1,
                FromEmployeeId = 1, // Self-assessment
                Type = FeedbackType.Self,
                Comments = "I believe I have successfully led the team through a challenging year of modernization. I have focused on improving code quality and mentoring junior developers. Areas for improvement include work-life balance and delegation.",
                SubmittedAt = new DateTime(2024, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 12, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Feedback
            {
                Id = 2,
                PerformanceReviewId = 1,
                FromEmployeeId = 4, // Mona Ali - Manager feedback
                Type = FeedbackType.Manager,
                Comments = "Ahmed has been an exceptional team lead. His technical expertise and leadership have been instrumental in the successful delivery of our major projects. He consistently goes above and beyond.",
                SubmittedAt = new DateTime(2024, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 12, 12, 0, 0, 0, DateTimeKind.Utc)
            },
            new Feedback
            {
                Id = 3,
                PerformanceReviewId = 1,
                FromEmployeeId = 2, // Sarah - Peer feedback
                Type = FeedbackType.Peer,
                Comments = "Ahmed is a great mentor and always available to help. His code reviews are thorough and educational. I have learned a lot from working with him.",
                SubmittedAt = new DateTime(2024, 12, 11, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 12, 11, 0, 0, 0, DateTimeKind.Utc)
            },

            // Feedback for Review 2 (Sarah Mohamed - 2024 Annual)
            new Feedback
            {
                Id = 4,
                PerformanceReviewId = 2,
                FromEmployeeId = 2, // Self-assessment
                Type = FeedbackType.Self,
                Comments = "This year I focused on improving our deployment processes and expanding my cloud skills. I completed my Azure certification and implemented CI/CD pipelines. I want to take on more leadership responsibilities.",
                SubmittedAt = new DateTime(2024, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 12, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Feedback
            {
                Id = 5,
                PerformanceReviewId = 2,
                FromEmployeeId = 1, // Ahmed - Manager/Lead feedback
                Type = FeedbackType.Manager,
                Comments = "Sarah has shown tremendous growth this year. Her initiative in setting up our CI/CD infrastructure has significantly improved team productivity. Ready for senior technical responsibilities.",
                SubmittedAt = new DateTime(2024, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 12, 12, 0, 0, 0, DateTimeKind.Utc)
            },

            // Feedback for Review 4 (Ahmed - Q1 2025)
            new Feedback
            {
                Id = 6,
                PerformanceReviewId = 4,
                FromEmployeeId = 1, // Self-assessment
                Type = FeedbackType.Self,
                Comments = "Q1 has been focused on improving team processes and reducing technical debt. While code review improvements are on track, the technical debt reduction is behind schedule due to urgent production issues.",
                SubmittedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },

            // Feedback for Review 6 (Mona - HR Manager - Q1 2025)
            new Feedback
            {
                Id = 7,
                PerformanceReviewId = 6,
                FromEmployeeId = 4, // Self-assessment
                Type = FeedbackType.Self,
                Comments = "I have successfully reduced our time-to-hire by 25%, exceeding our target. The wellness program is progressing well and has received positive feedback from employees. Looking forward to expanding HR digital initiatives.",
                SubmittedAt = new DateTime(2025, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 3, 22, 0, 0, 0, DateTimeKind.Utc)
            },
            new Feedback
            {
                Id = 8,
                PerformanceReviewId = 6,
                FromEmployeeId = 5, // Peer feedback from Tarek
                Type = FeedbackType.Peer,
                Comments = "Mona has been instrumental in improving our hiring process. Her collaboration with the finance team on the wellness budget was excellent. Great cross-departmental communication.",
                SubmittedAt = new DateTime(2025, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 3, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

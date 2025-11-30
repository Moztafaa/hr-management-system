using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Performance;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class GoalSeedData
{
    public static void SeedGoals(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Goal>().HasData(
            // Goals for Review 1 (Ahmed Hassan - 2024 Annual - Closed)
            new Goal
            {
                Id = 1,
                PerformanceReviewId = 1,
                Title = "Complete System Modernization Project",
                Description = "Lead the migration of legacy systems to .NET 8",
                Status = GoalStatus.Completed,
                ProgressPercent = 100,
                DueDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Goal
            {
                Id = 2,
                PerformanceReviewId = 1,
                Title = "Mentor Junior Developers",
                Description = "Provide guidance and code reviews for 3 junior developers",
                Status = GoalStatus.Completed,
                ProgressPercent = 100,
                DueDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Goals for Review 2 (Sarah Mohamed - 2024 Annual)
            new Goal
            {
                Id = 3,
                PerformanceReviewId = 2,
                Title = "Implement CI/CD Pipeline",
                Description = "Set up automated deployment pipelines for all projects",
                Status = GoalStatus.Completed,
                ProgressPercent = 100,
                DueDate = new DateTime(2024, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Goal
            {
                Id = 4,
                PerformanceReviewId = 2,
                Title = "Obtain Azure Certification",
                Description = "Complete Azure Developer Associate certification",
                Status = GoalStatus.Completed,
                ProgressPercent = 100,
                DueDate = new DateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Goals for Review 4 (Ahmed - Q1 2025 - In Progress)
            new Goal
            {
                Id = 5,
                PerformanceReviewId = 4,
                Title = "Improve Team Code Review Process",
                Description = "Establish and document code review standards",
                Status = GoalStatus.OnTrack,
                ProgressPercent = 75,
                DueDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new Goal
            {
                Id = 6,
                PerformanceReviewId = 4,
                Title = "Reduce Technical Debt",
                Description = "Refactor and resolve 50% of identified technical debt items",
                Status = GoalStatus.AtRisk,
                ProgressPercent = 30,
                DueDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },

            // Goals for Review 5 (Sarah - Q1 2025)
            new Goal
            {
                Id = 7,
                PerformanceReviewId = 5,
                Title = "Implement API Performance Monitoring",
                Description = "Set up monitoring and alerting for all critical APIs",
                Status = GoalStatus.OnTrack,
                ProgressPercent = 60,
                DueDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },

            // Goals for Review 6 (Mona - HR Manager - Q1 2025)
            new Goal
            {
                Id = 8,
                PerformanceReviewId = 6,
                Title = "Streamline Recruitment Process",
                Description = "Reduce average time-to-hire by 20%",
                Status = GoalStatus.Completed,
                ProgressPercent = 100,
                DueDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new Goal
            {
                Id = 9,
                PerformanceReviewId = 6,
                Title = "Launch Employee Wellness Program",
                Description = "Design and implement a comprehensive wellness initiative",
                Status = GoalStatus.OnTrack,
                ProgressPercent = 80,
                DueDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },

            // Goals for Review 7 (Tarek - Q2 2025 - Draft)
            new Goal
            {
                Id = 10,
                PerformanceReviewId = 7,
                Title = "Complete Financial Audit Preparation",
                Description = "Prepare all documentation for Q2 financial audit",
                Status = GoalStatus.NotStarted,
                ProgressPercent = 0,
                DueDate = new DateTime(2025, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

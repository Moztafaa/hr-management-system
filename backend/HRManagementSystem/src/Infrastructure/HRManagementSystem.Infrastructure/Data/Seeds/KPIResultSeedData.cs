using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class KPIResultSeedData
{
    public static void SeedKPIResults(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KPIResult>().HasData(
            // KPI Results for Review 1 (Ahmed Hassan - 2024 Annual)
            new KPIResult
            {
                Id = 1,
                PerformanceReviewId = 1,
                KPIId = 1, // Project Completion Rate
                Actual = 97.0m,
                WeightedScore = 4.7m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 2,
                PerformanceReviewId = 1,
                KPIId = 3, // Code Quality Score
                Actual = 92.0m,
                WeightedScore = 4.5m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 3,
                PerformanceReviewId = 1,
                KPIId = 4, // Attendance Rate
                Actual = 99.0m,
                WeightedScore = 4.8m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // KPI Results for Review 2 (Sarah Mohamed - 2024 Annual)
            new KPIResult
            {
                Id = 4,
                PerformanceReviewId = 2,
                KPIId = 1, // Project Completion Rate
                Actual = 94.0m,
                WeightedScore = 4.3m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 5,
                PerformanceReviewId = 2,
                KPIId = 3, // Code Quality Score
                Actual = 88.0m,
                WeightedScore = 4.2m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 6,
                PerformanceReviewId = 2,
                KPIId = 5, // Training Completion
                Actual = 4.0m,
                WeightedScore = 4.5m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // KPI Results for Review 3 (Khaled - 2024 Annual)
            new KPIResult
            {
                Id = 7,
                PerformanceReviewId = 3,
                KPIId = 1, // Project Completion Rate
                Actual = 85.0m,
                WeightedScore = 3.8m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 8,
                PerformanceReviewId = 3,
                KPIId = 4, // Attendance Rate
                Actual = 96.0m,
                WeightedScore = 4.0m,
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // KPI Results for Review 4 (Ahmed - Q1 2025 - In Progress)
            new KPIResult
            {
                Id = 9,
                PerformanceReviewId = 4,
                KPIId = 1, // Project Completion Rate
                Actual = 90.0m,
                WeightedScore = null, // Not yet calculated
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPIResult
            {
                Id = 10,
                PerformanceReviewId = 4,
                KPIId = 7, // Ticket Resolution Time
                Actual = 20.0m,
                WeightedScore = null,
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },

            // KPI Results for Review 6 (Mona - HR Manager - Q1 2025)
            new KPIResult
            {
                Id = 11,
                PerformanceReviewId = 6,
                KPIId = 8, // Time to Hire
                Actual = 25.0m, // Beat the target of 30 days
                WeightedScore = 4.8m,
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

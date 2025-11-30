using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Performance;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class PerformanceReviewSeedData
{
    public static void SeedPerformanceReviews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerformanceReview>().HasData(
            // 2024 Annual Reviews (Completed)
            new PerformanceReview
            {
                Id = 1,
                EmployeeId = 1, // Ahmed Hassan - IT Team Lead
                ReviewCycleId = 1, // 2024 Annual
                Status = ReviewStatus.Closed,
                FinalRating = 4.5m,
                CreatedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PerformanceReview
            {
                Id = 2,
                EmployeeId = 2, // Sarah Mohamed - Senior Developer
                ReviewCycleId = 1,
                Status = ReviewStatus.Closed,
                FinalRating = 4.2m,
                CreatedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PerformanceReview
            {
                Id = 3,
                EmployeeId = 3, // Khaled Ibrahim
                ReviewCycleId = 1,
                Status = ReviewStatus.Closed,
                FinalRating = 3.8m,
                CreatedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Q1 2025 Reviews (In Progress)
            new PerformanceReview
            {
                Id = 4,
                EmployeeId = 1,
                ReviewCycleId = 2, // Q1 2025
                Status = ReviewStatus.Submitted,
                FinalRating = null,
                CreatedAt = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new PerformanceReview
            {
                Id = 5,
                EmployeeId = 2,
                ReviewCycleId = 2,
                Status = ReviewStatus.InProgress,
                FinalRating = null,
                CreatedAt = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new PerformanceReview
            {
                Id = 6,
                EmployeeId = 4, // Mona Ali - HR Manager
                ReviewCycleId = 2,
                Status = ReviewStatus.Submitted,
                FinalRating = null,
                CreatedAt = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Q2 2025 Reviews (Draft)
            new PerformanceReview
            {
                Id = 7,
                EmployeeId = 5, // Tarek Farouk
                ReviewCycleId = 3, // Q2 2025
                Status = ReviewStatus.Draft,
                FinalRating = null,
                CreatedAt = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

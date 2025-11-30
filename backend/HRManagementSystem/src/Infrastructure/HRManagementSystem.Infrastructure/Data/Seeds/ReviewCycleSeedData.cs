using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Performance;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class ReviewCycleSeedData
{
    public static void SeedReviewCycles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReviewCycle>().HasData(
            new ReviewCycle
            {
                Id = 1,
                Name = "2024 Annual Review",
                Frequency = CycleFrequency.Annual,
                StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                RatingScale = RatingScaleType.OneToFive,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ReviewCycle
            {
                Id = 2,
                Name = "Q1 2025 Review",
                Frequency = CycleFrequency.Quarterly,
                StartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2025, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                RatingScale = RatingScaleType.OneToFive,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ReviewCycle
            {
                Id = 3,
                Name = "Q2 2025 Review",
                Frequency = CycleFrequency.Quarterly,
                StartDate = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2025, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                RatingScale = RatingScaleType.OneToFive,
                CreatedAt = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

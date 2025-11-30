using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeCompetencyRatingSeedData
{
    public static void SeedEmployeeCompetencyRatings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeCompetencyRating>().HasData(
            // Ratings for Review 1 (Ahmed Hassan - 2024 Annual)
            new EmployeeCompetencyRating
            {
                Id = 1,
                PerformanceReviewId = 1,
                CompetencyId = 1, // Communication
                Rating = 4.5m,
                Notes = "Excellent communication with team and stakeholders",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 2,
                PerformanceReviewId = 1,
                CompetencyId = 4, // Leadership
                Rating = 4.8m,
                Notes = "Outstanding leadership skills, successfully mentored junior team members",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 3,
                PerformanceReviewId = 1,
                CompetencyId = 5, // Technical Expertise
                Rating = 4.7m,
                Notes = "Deep technical knowledge in .NET and cloud technologies",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Ratings for Review 2 (Sarah Mohamed - 2024 Annual)
            new EmployeeCompetencyRating
            {
                Id = 4,
                PerformanceReviewId = 2,
                CompetencyId = 2, // Problem Solving
                Rating = 4.5m,
                Notes = "Excellent analytical and problem-solving abilities",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 5,
                PerformanceReviewId = 2,
                CompetencyId = 5, // Technical Expertise
                Rating = 4.3m,
                Notes = "Strong technical skills with room for growth in architecture",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 6,
                PerformanceReviewId = 2,
                CompetencyId = 8, // Initiative
                Rating = 4.0m,
                Notes = "Proactively suggests improvements to processes",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Ratings for Review 3 (Khaled - 2024 Annual)
            new EmployeeCompetencyRating
            {
                Id = 7,
                PerformanceReviewId = 3,
                CompetencyId = 3, // Teamwork
                Rating = 4.0m,
                Notes = "Good team player, collaborates well with colleagues",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 8,
                PerformanceReviewId = 3,
                CompetencyId = 7, // Time Management
                Rating = 3.5m,
                Notes = "Needs improvement in meeting deadlines consistently",
                CreatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
            },

            // Ratings for Review 4 (Ahmed - Q1 2025)
            new EmployeeCompetencyRating
            {
                Id = 9,
                PerformanceReviewId = 4,
                CompetencyId = 4, // Leadership
                Rating = 4.6m,
                Notes = "Continues to demonstrate strong leadership",
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 10,
                PerformanceReviewId = 4,
                CompetencyId = 6, // Adaptability
                Rating = 4.3m,
                Notes = "Handled team restructuring effectively",
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },

            // Ratings for Review 6 (Mona - HR Manager - Q1 2025)
            new EmployeeCompetencyRating
            {
                Id = 11,
                PerformanceReviewId = 6,
                CompetencyId = 1, // Communication
                Rating = 4.8m,
                Notes = "Exceptional communication across all departments",
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeCompetencyRating
            {
                Id = 12,
                PerformanceReviewId = 6,
                CompetencyId = 8, // Initiative
                Rating = 4.5m,
                Notes = "Launched multiple HR initiatives successfully",
                CreatedAt = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

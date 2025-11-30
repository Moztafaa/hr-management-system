using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class CompetencySeedData
{
    public static void SeedCompetencies(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competency>().HasData(
            new Competency
            {
                Id = 1,
                Name = "Communication",
                Description = "Ability to clearly convey information and ideas through various mediums",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 2,
                Name = "Problem Solving",
                Description = "Ability to identify, analyze, and resolve problems effectively",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 3,
                Name = "Teamwork",
                Description = "Ability to work collaboratively with others to achieve common goals",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 4,
                Name = "Leadership",
                Description = "Ability to guide, inspire, and influence others towards achieving objectives",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 5,
                Name = "Technical Expertise",
                Description = "Proficiency in job-specific technical skills and knowledge",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 6,
                Name = "Adaptability",
                Description = "Ability to adjust to new conditions and handle change effectively",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 7,
                Name = "Time Management",
                Description = "Ability to prioritize tasks and manage time efficiently",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 8,
                Name = "Initiative",
                Description = "Ability to take proactive action and go beyond basic requirements",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

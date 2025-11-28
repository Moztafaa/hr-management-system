using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeTrainingSeedData
{
    public static void SeedEmployeeTraining(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeTraining>().HasData(

            // 1. Employee 1 → Enrolled, not completed
            new EmployeeTraining
            {
                Id = 1,
                EmployeeId = 1,
                TrainingCourseId = 1,
                Status = TrainingStatus.Enrolled,
                EnrollmentDate = new DateTime(2025, 05, 10, 0, 0, 0, DateTimeKind.Utc),
                CompletionDate = null,
                RewardGiven = false
            },

            // 2. Employee 2 → Completed in May, reward not given yet
            new EmployeeTraining
            {
                Id = 2,
                EmployeeId = 2,
                TrainingCourseId = 3,
                Status = TrainingStatus.Completed,
                EnrollmentDate = new DateTime(2025, 04, 01, 0, 0, 0, DateTimeKind.Utc),
                CompletionDate = new DateTime(2025, 05, 15, 0, 0, 0, DateTimeKind.Utc),
                RewardGiven = true
            },

            // 3. Employee 3 → Cancelled the course
            new EmployeeTraining
            {
                Id = 3,
                EmployeeId = 3,
                TrainingCourseId = 5,
                Status = TrainingStatus.Cancelled,
                EnrollmentDate = new DateTime(2025, 03, 22, 0, 0, 0, DateTimeKind.Utc),
                CompletionDate = null,
                RewardGiven = false
            }
        );
    }
}

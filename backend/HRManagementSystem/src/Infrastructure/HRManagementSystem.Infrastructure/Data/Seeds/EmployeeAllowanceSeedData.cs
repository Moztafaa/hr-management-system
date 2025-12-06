using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds
{
    public static class EmployeeAllowanceSeedData
    {
        public static void SeedEmployeeAllowances(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAllowance>().HasData(
                // Employee 1 (Ahmed Hassan - Team Lead IT)
                new EmployeeAllowance
                {
                    EmployeeId = 1,
                    AllowanceId = 1, // Housing
                    Amount = 2000.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 1,
                    AllowanceId = 2, // Transportation
                    Amount = 800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 1,
                    AllowanceId = 4, // Mobile
                    Amount = 300.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Employee 2 (Sarah Mohamed)
                new EmployeeAllowance
                {
                    EmployeeId = 2,
                    AllowanceId = 1, // Housing
                    Amount = 1800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 2,
                    AllowanceId = 2, // Transportation
                    Amount = 800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 2,
                    AllowanceId = 5, // Internet
                    Amount = 200.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Employee 3 (Khaled Ibrahim)
                new EmployeeAllowance
                {
                    EmployeeId = 3,
                    AllowanceId = 1, // Housing
                    Amount = 1500.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 3,
                    AllowanceId = 2, // Transportation
                    Amount = 700.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Employee 4 (Fatima Ali)
                new EmployeeAllowance
                {
                    EmployeeId = 4,
                    AllowanceId = 1, // Housing
                    Amount = 1800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 4,
                    AllowanceId = 2, // Transportation
                    Amount = 800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 4,
                    AllowanceId = 4, // Mobile
                    Amount = 300.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Employee 5 (Omar Mahmoud)
                new EmployeeAllowance
                {
                    EmployeeId = 5,
                    AllowanceId = 1, // Housing
                    Amount = 1500.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 5,
                    AllowanceId = 2, // Transportation
                    Amount = 700.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Employee 6 (Mariam Youssef)
                new EmployeeAllowance
                {
                    EmployeeId = 6,
                    AllowanceId = 1, // Housing
                    Amount = 2000.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 6,
                    AllowanceId = 2, // Transportation
                    Amount = 800.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 6,
                    AllowanceId = 4, // Mobile
                    Amount = 300.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.Permanent,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Training Completion Bonus (OneTime allowance with start date)
                new EmployeeAllowance
                {
                    EmployeeId = 2,
                    AllowanceId = 8, // Bonus
                    Amount = 2000.00m,
                    IsPercentage = false,
                    Recurrence = RecurrenceType.OneTime,
                    StartDate = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // Annual Performance Bonus (Annual recurrence with start date)
                new EmployeeAllowance
                {
                    EmployeeId = 1,
                    AllowanceId = 9, // Annual Performance Bonus
                    Amount = 15.00m, // 15%
                    IsPercentage = true,
                    Recurrence = RecurrenceType.Annual,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new EmployeeAllowance
                {
                    EmployeeId = 6,
                    AllowanceId = 9, // Annual Performance Bonus
                    Amount = 15.00m, // 15%
                    IsPercentage = true,
                    Recurrence = RecurrenceType.Annual,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}

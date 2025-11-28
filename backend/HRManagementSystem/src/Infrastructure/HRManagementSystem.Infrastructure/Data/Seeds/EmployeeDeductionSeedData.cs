using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeDeductionSeedData
{
    public static void SeedEmployeeDeductions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDeduction>().HasData(
            // Employee 1 (Ahmed Hassan) - Social Insurance & Income Tax
            new EmployeeDeduction
            {
                EmployeeId = 1,
                DeductionId = 1, // Social Insurance
                Amount = 2250.00m, // 9% of basic salary (25000 * 0.09)
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 1, 15),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 1,
                DeductionId = 2, // Income Tax
                Amount = 3750.00m, // 15% of basic salary
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 1, 15),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 2 (Sarah Mohamed)
            new EmployeeDeduction
            {
                EmployeeId = 2,
                DeductionId = 1,
                Amount = 1800.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 3, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 2,
                DeductionId = 2,
                Amount = 2500.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 3, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 3 (Khaled Ibrahim)
            new EmployeeDeduction
            {
                EmployeeId = 3,
                DeductionId = 1,
                Amount = 1350.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 6, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 3,
                DeductionId = 2,
                Amount = 1500.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 6, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 4 (Fatima Ali)
            new EmployeeDeduction
            {
                EmployeeId = 4,
                DeductionId = 1,
                Amount = 1980.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 2, 15),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 4,
                DeductionId = 2,
                Amount = 2750.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 2, 15),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 5 (Omar Mahmoud)
            new EmployeeDeduction
            {
                EmployeeId = 5,
                DeductionId = 1,
                Amount = 1440.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 5, 10),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 5,
                DeductionId = 2,
                Amount = 1600.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2022, 5, 10),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 6 (Mariam Youssef)
            new EmployeeDeduction
            {
                EmployeeId = 6,
                DeductionId = 1,
                Amount = 2160.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 8, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                EmployeeId = 6,
                DeductionId = 2,
                Amount = 3000.00m,
                Recurrence = RecurrenceType.Permanent,
                StartDate = new DateTime(2021, 8, 1),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

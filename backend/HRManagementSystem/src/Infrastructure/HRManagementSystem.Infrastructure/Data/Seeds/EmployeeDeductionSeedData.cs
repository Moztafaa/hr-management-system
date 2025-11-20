namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeDeductionSeedData
{
    public static void SeedEmployeeDeductions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDeduction>().HasData(
            // Employee 1 (Ahmed Hassan) - Social Insurance & Income Tax
            new EmployeeDeduction
            {
                Id = 1,
                EmployeeId = 1,
                DeductionId = 1, // Social Insurance
                Amount = 2250.00m, // 9% of basic salary (25000 * 0.09)
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 2,
                EmployeeId = 1,
                DeductionId = 2, // Income Tax
                Amount = 3750.00m, // 15% of basic salary
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 2 (Sarah Mohamed)
            new EmployeeDeduction
            {
                Id = 3,
                EmployeeId = 2,
                DeductionId = 1, // Social Insurance
                Amount = 1800.00m, // 9% of 20000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 4,
                EmployeeId = 2,
                DeductionId = 2, // Income Tax
                Amount = 2500.00m, // 12.5% of 20000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 3 (Khaled Ibrahim)
            new EmployeeDeduction
            {
                Id = 5,
                EmployeeId = 3,
                DeductionId = 1, // Social Insurance
                Amount = 1350.00m, // 9% of 15000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 6,
                EmployeeId = 3,
                DeductionId = 2, // Income Tax
                Amount = 1500.00m, // 10% of 15000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 4 (Fatima Ali - HR Manager)
            new EmployeeDeduction
            {
                Id = 7,
                EmployeeId = 4,
                DeductionId = 1, // Social Insurance
                Amount = 1980.00m, // 9% of 22000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 8,
                EmployeeId = 4,
                DeductionId = 2, // Income Tax
                Amount = 2750.00m, // 12.5% of 22000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 5 (Omar Mahmoud)
            new EmployeeDeduction
            {
                Id = 9,
                EmployeeId = 5,
                DeductionId = 1, // Social Insurance
                Amount = 1440.00m, // 9% of 16000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 10,
                EmployeeId = 5,
                DeductionId = 2, // Income Tax
                Amount = 1600.00m, // 10% of 16000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 6 (Mariam Youssef - Finance Manager)
            new EmployeeDeduction
            {
                Id = 11,
                EmployeeId = 6,
                DeductionId = 1, // Social Insurance
                Amount = 2160.00m, // 9% of 24000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 12,
                EmployeeId = 6,
                DeductionId = 2, // Income Tax
                Amount = 3000.00m, // 12.5% of 24000
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

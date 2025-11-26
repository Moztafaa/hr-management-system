namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeAllowanceSeedData
{
    public static void SeedEmployeeAllowances(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeAllowance>().HasData(
            // Employee 1 (Ahmed Hassan - Team Lead IT) - Senior level allowances
            new EmployeeAllowance
            {
                Id = 1,
                EmployeeId = 1,
                AllowanceId = 1, // Housing
                Amount = 2000.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 2,
                EmployeeId = 1,
                AllowanceId = 2, // Transportation
                Amount = 8m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 3,
                EmployeeId = 1,
                AllowanceId = 4, // Mobile
                Amount = 300.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 2 (Sarah Mohamed - Senior Software Engineer)
            new EmployeeAllowance
            {
                Id = 4,
                EmployeeId = 2,
                AllowanceId = 1, // Housing
                Amount = 1800.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 5,
                EmployeeId = 2,
                AllowanceId = 2, // Transportation
                Amount = 8m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 6,
                EmployeeId = 2,
                AllowanceId = 5, // Internet
                Amount = 200.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 3 (Khaled Ibrahim - Software Engineer)
            new EmployeeAllowance
            {
                Id = 7,
                EmployeeId = 3,
                AllowanceId = 1, // Housing
                Amount = 1500.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 8,
                EmployeeId = 3,
                AllowanceId = 2, // Transportation
                Amount = 7m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 4 (Fatima Ali - HR Manager)
            new EmployeeAllowance
            {
                Id = 9,
                EmployeeId = 4,
                AllowanceId = 1, // Housing
                Amount = 1800.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 10,
                EmployeeId = 4,
                AllowanceId = 2, // Transportation
                Amount = 8m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 11,
                EmployeeId = 4,
                AllowanceId = 4, // Mobile
                Amount = 300.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 5 (Omar Mahmoud - HR Specialist)
            new EmployeeAllowance
            {
                Id = 12,
                EmployeeId = 5,
                AllowanceId = 1, // Housing
                Amount = 1500.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 13,
                EmployeeId = 5,
                AllowanceId = 2, // Transportation
                Amount = 7m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 6 (Mariam Youssef - Finance Manager)
            new EmployeeAllowance
            {
                Id = 14,
                EmployeeId = 6,
                AllowanceId = 1, // Housing
                Amount = 2000.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 15,
                EmployeeId = 6,
                AllowanceId = 2, // Transportation
                Amount = 8m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 16,
                EmployeeId = 6,
                AllowanceId = 4, // Mobile
                Amount = 300.00m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeAllowance
            {
                Id = 17,
                EmployeeId = 2,
                AllowanceId = 8, // Bonus
                Amount = 2000.00m,
                Month = 5,
                Year = 2025,
                CreatedAt = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc)
            } 
        );
    }
}

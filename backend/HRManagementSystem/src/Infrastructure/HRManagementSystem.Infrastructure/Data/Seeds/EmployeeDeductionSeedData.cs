namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeDeductionSeedData
{
    public static void SeedEmployeeDeductions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDeduction>().HasData(

            // Employee 1 → Has Loan Installment + Late Penalty
            new EmployeeDeduction
            {
                Id = 1,
                EmployeeId = 1,
                DeductionId = 4, // Loan Installment
                Amount = 1000m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 2,
                EmployeeId = 1,
                DeductionId = 5, // Late Penalty
                Amount = 100m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 2 → Has NO extra deductions (only system ones will apply)
            // no records

            // Employee 3 → Has Absence Deduction
            new EmployeeDeduction
            {
                Id = 3,
                EmployeeId = 3,
                DeductionId = 3, // Absence
                Amount = 500m,   // e.g. 500 EGP based on absent days
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 4 → Has Loan Installment only
            new EmployeeDeduction
            {
                Id = 4,
                EmployeeId = 4,
                DeductionId = 4, // Loan Installment
                Amount = 1000m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 5 → Has Late Penalty only
            new EmployeeDeduction
            {
                Id = 5,
                EmployeeId = 5,
                DeductionId = 5,
                Amount = 100m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 6 → Has NO extra deductions
            // no records

            // Employee 7 → Has Absence + Late Penalty
            new EmployeeDeduction
            {
                Id = 6,
                EmployeeId = 7,
                DeductionId = 3,
                Amount = 300m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new EmployeeDeduction
            {
                Id = 7,
                EmployeeId = 7,
                DeductionId = 5,
                Amount = 100m,
                Month = 1,
                Year = 2024,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}


namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DeductionSeedData
{
    public static void SeedDeductions(this ModelBuilder modelBuilder) => modelBuilder.Entity<Deduction>().HasData(
            new Deduction
            {
                Id = 1,
                Name = "Social Insurance",
                Value = 9m,               // 9%
                IsPercentage = true,
                IsFixed = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 2,
                Name = "Income Tax",
                Value = 15m,             // 15%
                IsPercentage = true,
                IsFixed = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 3,
                Name = "Absence Deduction",
                Value = 0.00m, // Calculated based on absent days
                IsPercentage = true,
                IsFixed = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 4,
                Name = "Loan Installment",
                Value = 1000m,           // fixed
                IsPercentage = false,
                IsFixed = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 5,
                Name = "Late Arrival Penalty",
                Value = 100m,
                IsPercentage = false,
                IsFixed = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
}

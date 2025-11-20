namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DeductionSeedData
{
    public static void SeedDeductions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deduction>().HasData(
            new Deduction
            {
                Id = 1,
                Name = "Social Insurance",
                Amount = 0.00m, // Percentage-based, calculated per employee
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 2,
                Name = "Income Tax",
                Amount = 0.00m, // Percentage-based, calculated per employee
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 3,
                Name = "Absence Deduction",
                Amount = 0.00m, // Calculated based on absent days
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 4,
                Name = "Late Arrival Penalty",
                Amount = 100.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 5,
                Name = "Loan Installment",
                Amount = 0.00m, // Varies per employee
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

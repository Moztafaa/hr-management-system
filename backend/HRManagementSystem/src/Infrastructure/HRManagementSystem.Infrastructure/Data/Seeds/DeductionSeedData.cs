namespace HRManagementSystem.Infrastructure.Data.Seeds
{
    public static class DeductionSeedData
    {
        public static void SeedDeductions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deduction>().HasData(
                new Deduction
                {
                    Id = 1,
                    Name = "Social Insurance",
                    Amount = 9.0m, // 9% typical rate
                    IsPercentage = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Deduction
                {
                    Id = 2,
                    Name = "Income Tax",
                    Amount = 12.5m, // 12.5% typical rate
                    IsPercentage = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Deduction
                {
                    Id = 3,
                    Name = "Absence Deduction",
                    Amount = 500.0m, // fixed example amount
                    IsPercentage = false,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Deduction
                {
                    Id = 4,
                    Name = "Late Arrival Penalty",
                    Amount = 100.0m, // fixed
                    IsPercentage = false,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Deduction
                {
                    Id = 5,
                    Name = "Loan Installment",
                    Amount = 1000.0m, // example fixed value
                    IsPercentage = false,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}

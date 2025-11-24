namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class AllowanceSeedData
{
    public static void SeedAllowances(this ModelBuilder modelBuilder) => modelBuilder.Entity<Allowance>().HasData(
            new Allowance
            {
                Id = 1,
                Name = "Housing Allowance",
                Value = 2000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 2,
                Name = "Transportation Allowance",
                Value = 8m,
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 3,
                Name = "Food Allowance",
                Value = 500.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 4,
                Name = "Mobile Allowance",
                Value = 300.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 5,
                Name = "Internet Allowance",
                Value = 200.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 6,
                Name = "Performance Bonus",
                Value = 3000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 7,
                Name = "Health Insurance",
                Value = 15m,
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 8,
                Name = "Training Completion Bonus",
                Value = 1700.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            } 

        );
}

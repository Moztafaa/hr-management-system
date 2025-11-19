namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class AllowanceSeedData
{
    public static void SeedAllowances(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allowance>().HasData(
            new Allowance
            {
                Id = 1,
                Name = "Housing Allowance",
                Amount = 2000.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 2,
                Name = "Transportation Allowance",
                Amount = 800.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 3,
                Name = "Food Allowance",
                Amount = 500.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 4,
                Name = "Mobile Allowance",
                Amount = 300.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 5,
                Name = "Internet Allowance",
                Amount = 200.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 6,
                Name = "Performance Bonus",
                Amount = 3000.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 7,
                Name = "Health Insurance",
                Amount = 1500.00m,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

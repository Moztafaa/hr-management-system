namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DepartmentSeedData
{
    public static void SeedDepartments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                Name = "Information Technology",
                Description = "Manages all IT infrastructure, software development, and technical support",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 2,
                Name = "Human Resources",
                Description = "Handles recruitment, employee relations, benefits, and HR policies",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 3,
                Name = "Finance",
                Description = "Manages financial operations, accounting, budgeting, and reporting",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 4,
                Name = "Sales & Marketing",
                Description = "Drives sales growth, customer acquisition, and marketing campaigns",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 5,
                Name = "Operations",
                Description = "Oversees day-to-day operations and process optimization",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 6,
                Name = "Customer Support",
                Description = "Provides customer service and technical support",
                ManagerId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

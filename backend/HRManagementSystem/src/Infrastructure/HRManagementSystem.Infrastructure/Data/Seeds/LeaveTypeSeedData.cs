namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class LeaveTypeSeedData
{
    public static void SeedLeaveTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveType>().HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Annual Leave",
                Description = "Annual paid leave after completing the first year of work",
                MaxDays = 30,
                CanCarryForward = true,
                CarryForwardLimit = 5,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = null
            },
            new LeaveType
            {
                Id = 2,
                Name = "Sick Leave",
                Description = "Medical leave based on a valid medical certificate",
                MaxDays = 30,
                CanCarryForward = false,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = null
            },
            new LeaveType
            {
                Id = 3,
                Name = "Maternity Leave",
                Description = "Maternity leave for female employees, 90 days paid",
                MaxDays = 90,
                CanCarryForward = false,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = "Female"
            },
            new LeaveType
            {
                Id = 4,
                Name = "Paternity Leave",
                Description = "Short paid leave for new fathers as per company policy",
                MaxDays = 3,
                CanCarryForward = false,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = "Male"
            },
            new LeaveType
            {
                Id = 5,
                Name = "Unpaid Leave",
                Description = "Leave without pay subject to management approval",
                MaxDays = 30,
                CanCarryForward = false,
                IsPaid = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = null
            },
            new LeaveType
            {
                Id = 6,
                Name = "Emergency Leave",
                Description = "Leave for emergencies (death of a relative, special circumstances)",
                MaxDays = 5,
                CanCarryForward = false,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = null
            },
            new LeaveType
            {
                Id = 7,
                Name = "Hajj Leave",
                Description = "Hajj leave for Muslims, 10 days paid, once in a lifetime",
                MaxDays = 10,
                CanCarryForward = false,
                IsPaid = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                GenderRestriction = null
            }
        );
    }
}

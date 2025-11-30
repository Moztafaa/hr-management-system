using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class KPISeedData
{
    public static void SeedKPIs(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KPI>().HasData(
            new KPI
            {
                Id = 1,
                Code = "PROJ-COMP",
                Name = "Project Completion Rate",
                Unit = "%",
                Target = 95.0m,
                Description = "Percentage of projects completed on time",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 2,
                Code = "CUST-SAT",
                Name = "Customer Satisfaction Score",
                Unit = "score",
                Target = 4.5m,
                Description = "Average customer satisfaction rating (1-5 scale)",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 3,
                Code = "CODE-QUAL",
                Name = "Code Quality Score",
                Unit = "%",
                Target = 90.0m,
                Description = "Code quality metrics including test coverage and code review pass rate",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 4,
                Code = "ATTEND",
                Name = "Attendance Rate",
                Unit = "%",
                Target = 98.0m,
                Description = "Employee attendance rate excluding approved leaves",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 5,
                Code = "TRAIN-COMP",
                Name = "Training Completion",
                Unit = "courses",
                Target = 3.0m,
                Description = "Number of training courses completed per review cycle",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 6,
                Code = "SALES-REV",
                Name = "Sales Revenue Target",
                Unit = "EGP",
                Target = 500000.0m,
                Description = "Revenue generated from sales activities",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 7,
                Code = "TICKET-RES",
                Name = "Ticket Resolution Time",
                Unit = "hours",
                Target = 24.0m,
                Description = "Average time to resolve support tickets",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 8,
                Code = "RECRUIT-TIME",
                Name = "Time to Hire",
                Unit = "days",
                Target = 30.0m,
                Description = "Average time from job posting to hire",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

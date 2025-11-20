namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class PayslipSeedData
{
    public static void SeedPayslips(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payslip>().HasData(
            // October 2024 Payslips
            // Employee 1 (Ahmed Hassan) - October 2024
            new Payslip
            {
                Id = 1,
                EmployeeId = 1,
                BasicSalary = 25000.00m,
                TotalAllowances = 3100.00m, // Housing(2000) + Transportation(800) + Mobile(300)
                TotalDeductions = 6000.00m, // Social Insurance(2250) + Income Tax(3750)
                NetSalary = 22100.00m, // 25000 + 3100 - 6000
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 2 (Sarah Mohamed) - October 2024
            new Payslip
            {
                Id = 2,
                EmployeeId = 2,
                BasicSalary = 20000.00m,
                TotalAllowances = 2800.00m, // Housing(1800) + Transportation(800) + Internet(200)
                TotalDeductions = 4300.00m, // Social Insurance(1800) + Income Tax(2500)
                NetSalary = 18500.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 3 (Khaled Ibrahim) - October 2024
            new Payslip
            {
                Id = 3,
                EmployeeId = 3,
                BasicSalary = 15000.00m,
                TotalAllowances = 2200.00m, // Housing(1500) + Transportation(700)
                TotalDeductions = 2850.00m, // Social Insurance(1350) + Income Tax(1500)
                NetSalary = 14350.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 4 (Fatima Ali) - October 2024
            new Payslip
            {
                Id = 4,
                EmployeeId = 4,
                BasicSalary = 22000.00m,
                TotalAllowances = 2900.00m, // Housing(1800) + Transportation(800) + Mobile(300)
                TotalDeductions = 4730.00m, // Social Insurance(1980) + Income Tax(2750)
                NetSalary = 20170.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 5 (Omar Mahmoud) - October 2024
            new Payslip
            {
                Id = 5,
                EmployeeId = 5,
                BasicSalary = 16000.00m,
                TotalAllowances = 2200.00m, // Housing(1500) + Transportation(700)
                TotalDeductions = 3040.00m, // Social Insurance(1440) + Income Tax(1600)
                NetSalary = 15160.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 6 (Mariam Youssef) - October 2024
            new Payslip
            {
                Id = 6,
                EmployeeId = 6,
                BasicSalary = 24000.00m,
                TotalAllowances = 3100.00m, // Housing(2000) + Transportation(800) + Mobile(300)
                TotalDeductions = 5160.00m, // Social Insurance(2160) + Income Tax(3000)
                NetSalary = 21940.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // September 2024 Payslips (Historical data)
            // Employee 1 - September 2024
            new Payslip
            {
                Id = 7,
                EmployeeId = 1,
                BasicSalary = 25000.00m,
                TotalAllowances = 3100.00m,
                TotalDeductions = 6000.00m,
                NetSalary = 22100.00m,
                Month = 9,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 2 - September 2024
            new Payslip
            {
                Id = 8,
                EmployeeId = 2,
                BasicSalary = 20000.00m,
                TotalAllowances = 2800.00m,
                TotalDeductions = 4300.00m,
                NetSalary = 18500.00m,
                Month = 9,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 4 - September 2024
            new Payslip
            {
                Id = 9,
                EmployeeId = 4,
                BasicSalary = 22000.00m,
                TotalAllowances = 2900.00m,
                TotalDeductions = 4730.00m,
                NetSalary = 20170.00m,
                Month = 9,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Employee 6 - September 2024
            new Payslip
            {
                Id = 10,
                EmployeeId = 6,
                BasicSalary = 24000.00m,
                TotalAllowances = 3100.00m,
                TotalDeductions = 5160.00m,
                NetSalary = 21940.00m,
                Month = 9,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

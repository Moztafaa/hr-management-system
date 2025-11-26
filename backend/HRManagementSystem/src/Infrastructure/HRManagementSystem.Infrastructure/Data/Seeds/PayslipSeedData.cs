using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class PayslipSeedData
{
    public static void SeedPayslips(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payslip>().HasData(
            // Employee 1 → Ahmed Hassan (IT) - October 2024
            new Payslip
            {
                Id = 1,
                EmployeeId = 1,
                BasicSalary = 25000.00m,
                TotalAllowances = 3100.00m, // Housing(2000) + Transportation(8% of basic?) + Mobile(300)
                TotalDeductions = 6000.00m, // Social Insurance(9% of basic) + Income Tax(15% of basic)
                NetSalary = 22100.00m,      // 25000 + 3100 - 6000
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 2 → Sarah Mohamed (IT) - October 2024
            new Payslip
            {
                Id = 2,
                EmployeeId = 2,
                BasicSalary = 20000.00m,
                TotalAllowances = 2800.00m, // Housing + Transportation + Internet + Training Completion Bonus if applicable
                TotalDeductions = 4300.00m, // Social Insurance + Income Tax
                NetSalary = 18500.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 3 → Khaled Ibrahim (IT) - October 2024
            new Payslip
            {
                Id = 3,
                EmployeeId = 3,
                BasicSalary = 15000.00m,
                TotalAllowances = 2200.00m,
                TotalDeductions = 2850.00m,
                NetSalary = 14350.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 4 → Fatima Ali (HR) - October 2024
            new Payslip
            {
                Id = 4,
                EmployeeId = 4,
                BasicSalary = 22000.00m,
                TotalAllowances = 2900.00m,
                TotalDeductions = 4730.00m,
                NetSalary = 20170.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 5 → Omar Mahmoud (HR) - October 2024
            new Payslip
            {
                Id = 5,
                EmployeeId = 5,
                BasicSalary = 16000.00m,
                TotalAllowances = 2200.00m,
                TotalDeductions = 3040.00m,
                NetSalary = 15160.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 6 → Mariam Youssef (Finance) - October 2024
            new Payslip
            {
                Id = 6,
                EmployeeId = 6,
                BasicSalary = 24000.00m,
                TotalAllowances = 3100.00m,
                TotalDeductions = 5160.00m,
                NetSalary = 21940.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 7 → Hassan Saleh (Finance) - October 2024
            new Payslip
            {
                Id = 7,
                EmployeeId = 7,
                BasicSalary = 14000.00m,
                TotalAllowances = 2100.00m,
                TotalDeductions = 2500.00m,
                NetSalary = 13600.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 8 → Nour Abdel (Sales) - October 2024
            new Payslip
            {
                Id = 8,
                EmployeeId = 8,
                BasicSalary = 23000.00m,
                TotalAllowances = 3000.00m,
                TotalDeductions = 4500.00m,
                NetSalary = 21500.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 9 → Tarek Farid (Sales) - October 2024
            new Payslip
            {
                Id = 9,
                EmployeeId = 9,
                BasicSalary = 13000.00m,
                TotalAllowances = 2000.00m,
                TotalDeductions = 2300.00m,
                NetSalary = 12700.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            // Employee 10 → Layla Kamal (Operations) - October 2024
            new Payslip
            {
                Id = 10,
                EmployeeId = 10,
                BasicSalary = 26000.00m,
                TotalAllowances = 3200.00m,
                TotalDeductions = 5200.00m,
                NetSalary = 24000.00m,
                Month = 10,
                Year = 2024,
                GeneratedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

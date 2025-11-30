using HRManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class AttendanceSeedData
{
    public static void SeedAttendance(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>().HasData(
            // November 2025 Attendance Records
            // Employee 1 - Ahmed Hassan
            new Attendance
            {
                Id = 1,
                EmployeeId = 1,
                Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 24, 8, 55, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 24, 17, 30, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 2,
                EmployeeId = 1,
                Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 25, 9, 10, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 25, 18, 00, 0, DateTimeKind.Utc),
                IsLate = true,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 3,
                EmployeeId = 1,
                Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 26, 8, 45, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 26, 17, 15, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 4,
                EmployeeId = 1,
                Date = new DateTime(2025, 11, 27, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 27, 8, 50, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 27, 17, 45, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },

            // Employee 2 - Sarah Mohamed
            new Attendance
            {
                Id = 5,
                EmployeeId = 2,
                Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 24, 8, 30, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 24, 17, 00, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 6,
                EmployeeId = 2,
                Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 25, 8, 45, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 25, 17, 30, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 7,
                EmployeeId = 2,
                Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = null,
                CheckOutTime = null,
                IsLate = false,
                IsAbsent = true // Sick leave
            },
            new Attendance
            {
                Id = 8,
                EmployeeId = 2,
                Date = new DateTime(2025, 11, 27, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 27, 8, 55, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 27, 17, 20, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },

            // Employee 3 - Khaled Ibrahim
            new Attendance
            {
                Id = 9,
                EmployeeId = 3,
                Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 24, 9, 15, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 24, 17, 45, 0, DateTimeKind.Utc),
                IsLate = true,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 10,
                EmployeeId = 3,
                Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 25, 9, 05, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 25, 17, 30, 0, DateTimeKind.Utc),
                IsLate = true,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 11,
                EmployeeId = 3,
                Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 26, 8, 50, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 26, 17, 00, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },

            // Employee 4 - Mona Ali (HR Manager)
            new Attendance
            {
                Id = 12,
                EmployeeId = 4,
                Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 24, 8, 30, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 24, 18, 00, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 13,
                EmployeeId = 4,
                Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 25, 8, 25, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 25, 17, 45, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 14,
                EmployeeId = 4,
                Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 26, 8, 40, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 26, 17, 30, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },

            // Employee 5 - Tarek Farouk (Finance)
            new Attendance
            {
                Id = 15,
                EmployeeId = 5,
                Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 24, 8, 55, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 24, 17, 30, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            },
            new Attendance
            {
                Id = 16,
                EmployeeId = 5,
                Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = null,
                CheckOutTime = null,
                IsLate = false,
                IsAbsent = true // Annual leave
            },
            new Attendance
            {
                Id = 17,
                EmployeeId = 5,
                Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                CheckInTime = new DateTime(2025, 11, 26, 8, 45, 0, DateTimeKind.Utc),
                CheckOutTime = new DateTime(2025, 11, 26, 17, 15, 0, DateTimeKind.Utc),
                IsLate = false,
                IsAbsent = false
            }
        );
    }
}

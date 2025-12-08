using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reviewmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 5, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 8, 2 },
                column: "StartDate",
                value: new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 3 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 5 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 5 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "Permanent", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "EmployeeAllowances",
                columns: new[] { "AllowanceId", "EmployeeId", "Amount", "CreatedAt", "EndDate", "IsPercentage", "Recurrence", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, 1, 15.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Annual", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 9, 6, 15.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Annual", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 1 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 5, 2 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 8, 2 },
                column: "StartDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 3 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 4 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 5 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 5 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 1, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 2, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumns: new[] { "AllowanceId", "EmployeeId" },
                keyValues: new object[] { 4, 6 },
                columns: new[] { "Recurrence", "StartDate" },
                values: new object[] { "OneTime", null });
        }
    }
}

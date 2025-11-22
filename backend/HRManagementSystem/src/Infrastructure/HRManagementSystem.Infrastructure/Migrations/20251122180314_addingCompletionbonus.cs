using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingCompletionbonus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "EmployeeTrainings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RewardGiven",
                table: "EmployeeTrainings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmployeeAllowances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "EmployeeAllowances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Allowances",
                columns: new[] { "Id", "Amount", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 8, 1700.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Training Completion Bonus", null });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Month", "Year" },
                values: new object[] { 0, 0 });

            migrationBuilder.InsertData(
                table: "EmployeeTrainings",
                columns: new[] { "EmployeeId", "TrainingCourseId", "CompletionDate", "CreatedAt", "EnrollmentDate", "Id", "RewardGiven", "UpdatedAt" },
                values: new object[] { 1, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, null });

            migrationBuilder.InsertData(
                table: "EmployeeTrainings",
                columns: new[] { "EmployeeId", "TrainingCourseId", "CompletionDate", "CreatedAt", "EnrollmentDate", "Id", "RewardGiven", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, 3, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, false, "Completed", null },
                    { 3, 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), 3, false, "Cancelled", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allowances",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EmployeeTrainings",
                keyColumns: new[] { "EmployeeId", "TrainingCourseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeTrainings",
                keyColumns: new[] { "EmployeeId", "TrainingCourseId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "EmployeeTrainings",
                keyColumns: new[] { "EmployeeId", "TrainingCourseId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "EmployeeTrainings");

            migrationBuilder.DropColumn(
                name: "RewardGiven",
                table: "EmployeeTrainings");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmployeeAllowances");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "EmployeeAllowances");
        }
    }
}

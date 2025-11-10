using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLeaveTypeEntity_SeedLeaveTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
    name: "IsPaid",
    table: "LeaveTypes",
    type: "bit",
    nullable: false,
    defaultValue: true // أو false حسب السياسة عندك
);

            migrationBuilder.InsertData(

                table: "LeaveTypes",
                columns: new[] { "Id", "CanCarryForward", "CarryForwardLimit", "CreatedAt", "Description", "IsPaid", "MaxDays", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, true, 5, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3323), "Annual paid leave after completing the first year of work", true, 15, "Annual Leave", null },
                    { 2, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3389), "Medical leave based on a valid medical certificate", true, 30, "Sick Leave", null },
                    { 3, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3396), "Maternity leave for female employees, 90 days paid", true, 90, "Maternity Leave", null },
                    { 4, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3401), "Short paid leave for new fathers as per company policy", true, 3, "Paternity Leave", null },
                    { 5, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3407), "Leave without pay subject to management approval", false, 30, "Unpaid Leave", null },
                    { 6, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3412), "Leave for emergencies (death of a relative, special circumstances)", true, 5, "Emergency Leave", null },
                    { 7, false, null, new DateTime(2025, 11, 9, 19, 48, 55, 436, DateTimeKind.Local).AddTicks(3417), "Hajj leave for Muslims, 10 days paid, once in a lifetime", true, 10, "Hajj Leave", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
    name: "IsPaid",
    table: "LeaveTypes"
);
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}

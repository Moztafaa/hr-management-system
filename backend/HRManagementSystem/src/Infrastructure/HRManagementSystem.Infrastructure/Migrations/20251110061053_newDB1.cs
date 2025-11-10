using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7208));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7299));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 10, 52, 692, DateTimeKind.Local).AddTicks(7398));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(7938));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 8, 6, 11, 716, DateTimeKind.Local).AddTicks(8130));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreatedDateFromLeaveTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the duplicate CreatedDate column
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LeaveTypes");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 7, 59, 38, 320, DateTimeKind.Local).AddTicks(8463));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add the CreatedDate column back
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LeaveTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(534));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 6, 28, 13, 361, DateTimeKind.Local).AddTicks(547));
        }
    }
}

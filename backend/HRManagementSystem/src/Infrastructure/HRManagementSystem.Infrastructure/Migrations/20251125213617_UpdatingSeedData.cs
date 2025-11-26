using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 17,
                column: "Amount",
                value: 2000.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeAllowances",
                keyColumn: "Id",
                keyValue: 17,
                column: "Amount",
                value: 1700.00m);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingIsFixedDeduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFixed",
                table: "Deductions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Deductions",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsFixed",
                value: true);

            migrationBuilder.UpdateData(
                table: "Deductions",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsFixed",
                value: true);

            migrationBuilder.UpdateData(
                table: "Deductions",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsFixed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Deductions",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsFixed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Deductions",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsFixed",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFixed",
                table: "Deductions");
        }
    }
}

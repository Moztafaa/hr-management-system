using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResignationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resignations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoticePeriodDays = table.Column<int>(type: "int", nullable: false),
                    IsImmediateResignation = table.Column<bool>(type: "bit", nullable: false),
                    HandoverNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReviewedById = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resignations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resignations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resignations_Employees_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResignationApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResignationId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResignationApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResignationApprovals_Employees_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResignationApprovals_Resignations_ResignationId",
                        column: x => x.ResignationId,
                        principalTable: "Resignations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 1 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 1 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 2 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 2 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 3 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 3 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 4 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 4 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 5 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 5 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 6 },
                column: "Amount",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 6 },
                column: "Amount",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ResignationApprovals_ApproverId",
                table: "ResignationApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ResignationApprovals_ResignationId",
                table: "ResignationApprovals",
                column: "ResignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_EmployeeId",
                table: "Resignations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_ReviewedById",
                table: "Resignations",
                column: "ReviewedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResignationApprovals");

            migrationBuilder.DropTable(
                name: "Resignations");

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 1 },
                column: "Amount",
                value: 2250.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 1 },
                column: "Amount",
                value: 3750.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 2 },
                column: "Amount",
                value: 1800.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 2 },
                column: "Amount",
                value: 2500.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 3 },
                column: "Amount",
                value: 1350.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 3 },
                column: "Amount",
                value: 1500.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 4 },
                column: "Amount",
                value: 1980.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 4 },
                column: "Amount",
                value: 2750.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 5 },
                column: "Amount",
                value: 1440.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 5 },
                column: "Amount",
                value: 1600.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 1, 6 },
                column: "Amount",
                value: 2160.00m);

            migrationBuilder.UpdateData(
                table: "EmployeeDeductions",
                keyColumns: new[] { "DeductionId", "EmployeeId" },
                keyValues: new object[] { 2, 6 },
                column: "Amount",
                value: 3000.00m);
        }
    }
}

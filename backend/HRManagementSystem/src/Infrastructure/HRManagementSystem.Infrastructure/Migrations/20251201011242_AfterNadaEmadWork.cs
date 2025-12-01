using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AfterNadaEmadWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    IsAbsent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    EmployeeCount = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmployeeCount = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaxDays = table.Column<int>(type: "int", nullable: false),
                    CanCarryForward = table.Column<bool>(type: "bit", nullable: false),
                    CarryForwardLimit = table.Column<int>(type: "int", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    GenderRestriction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RelatedEntityId = table.Column<int>(type: "int", nullable: true),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewCycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RatingScale = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewCycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DurationHours = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EFF_Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EFF_End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FaceEmbedding = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    CurrentCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticePeriodDays = table.Column<int>(type: "int", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreferredWorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WillingToRelocate = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertedToEmployeeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Employees_ConvertedToEmployeeId",
                        column: x => x.ConvertedToEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAllowances",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AllowanceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: true),
                    Recurrence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAllowances", x => new { x.EmployeeId, x.AllowanceId });
                    table.ForeignKey(
                        name: "FK_EmployeeAllowances_Allowances_AllowanceId",
                        column: x => x.AllowanceId,
                        principalTable: "Allowances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAllowances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDeductions",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DeductionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: true),
                    Recurrence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDeductions", x => new { x.EmployeeId, x.DeductionId });
                    table.ForeignKey(
                        name: "FK_EmployeeDeductions_Deductions_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Deductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDeductions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalAllocated = table.Column<int>(type: "int", nullable: false),
                    UsedDays = table.Column<int>(type: "int", nullable: false),
                    RemainingDays = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTrainingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TrainingCourseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Pending"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ReviewedByManagerId = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EmployeeNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTrainingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTrainingRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTrainingRequests_Employees_ReviewedByManagerId",
                        column: x => x.ReviewedByManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTrainingRequests_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTrainings",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TrainingCourseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Enrolled"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RewardGiven = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTrainings", x => new { x.EmployeeId, x.TrainingCourseId });
                    table.ForeignKey(
                        name: "FK_EmployeeTrainings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTrainings_TrainingCourses_TrainingCourseId",
                        column: x => x.TrainingCourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReviewedById = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payslips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAllowances = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payslips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payslips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReviewCycleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FinalRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_ReviewCycles_ReviewCycleId",
                        column: x => x.ReviewCycleId,
                        principalTable: "ReviewCycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedBy = table.Column<int>(type: "int", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InterviewFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterviewRating = table.Column<int>(type: "int", nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OfferedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedRecruiterId = table.Column<int>(type: "int", nullable: true),
                    CurrentStage = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_Employees_AssignedRecruiterId",
                        column: x => x.AssignedRecruiterId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplications_Employees_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplications_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveRequestId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApprovals_Employees_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveApprovals_LeaveRequests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "LeaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCompetencyRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceReviewId = table.Column<int>(type: "int", nullable: false),
                    CompetencyId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompetencyRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCompetencyRatings_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCompetencyRatings_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceReviewId = table.Column<int>(type: "int", nullable: false),
                    FromEmployeeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceReviewId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProgressPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPIResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceReviewId = table.Column<int>(type: "int", nullable: false),
                    KPIId = table.Column<int>(type: "int", nullable: false),
                    Actual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightedScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIResults_KPIs_KPIId",
                        column: x => x.KPIId,
                        principalTable: "KPIs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPIResults_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Allowances",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsPercentage", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Housing Allowance", null },
                    { 2, 800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Transportation Allowance", null },
                    { 3, 500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Food Allowance", null },
                    { 4, 300.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Mobile Allowance", null },
                    { 5, 200.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Internet Allowance", null },
                    { 6, 3000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Performance Bonus", null },
                    { 7, 1500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Health Insurance", null },
                    { 8, 2000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Training Completion Bonus", null }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "CheckInTime", "CheckOutTime", "Date", "EmployeeId", "IsAbsent", "IsLate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 24, 8, 55, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, false },
                    { 2, new DateTime(2025, 11, 25, 9, 10, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, true },
                    { 3, new DateTime(2025, 11, 26, 8, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 17, 15, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, false },
                    { 4, new DateTime(2025, 11, 27, 8, 50, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 27, 17, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, false },
                    { 5, new DateTime(2025, 11, 24, 8, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), 2, false, false },
                    { 6, new DateTime(2025, 11, 25, 8, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 2, false, false },
                    { 7, null, null, new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 2, true, false },
                    { 8, new DateTime(2025, 11, 27, 8, 55, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 27, 17, 20, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Utc), 2, false, false },
                    { 9, new DateTime(2025, 11, 24, 9, 15, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 17, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), 3, false, true },
                    { 10, new DateTime(2025, 11, 25, 9, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 3, false, true },
                    { 11, new DateTime(2025, 11, 26, 8, 50, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 3, false, false },
                    { 12, new DateTime(2025, 11, 24, 8, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, false },
                    { 13, new DateTime(2025, 11, 25, 8, 25, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 17, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, false },
                    { 14, new DateTime(2025, 11, 26, 8, 40, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, false },
                    { 15, new DateTime(2025, 11, 24, 8, 55, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), 5, false, false },
                    { 16, null, null, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 5, true, false },
                    { 17, new DateTime(2025, 11, 26, 8, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 17, 15, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 5, false, false }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Address", "AvailableFrom", "Certifications", "City", "ConvertedToEmployeeId", "Country", "CreatedAt", "CurrentCompany", "CurrentJobTitle", "CurrentSalary", "DateOfBirth", "Education", "Email", "ExpectedSalary", "FirstName", "Gender", "LastName", "LinkedInUrl", "Notes", "NoticePeriodDays", "Phone", "PortfolioUrl", "PostalCode", "PreferredWorkLocation", "ResumeUrl", "Skills", "UpdatedAt", "WillingToRelocate", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, "123 New Cairo, Egypt", null, null, null, null, null, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, 12000.00m, new DateTime(1995, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Computer Science", "youssef.ahmed@email.com", 18000.00m, "Youssef", "Male", "Ahmed", null, null, null, "+20-100-111-2222", null, null, null, null, "C#, .NET, SQL Server, Angular", null, false, 3 },
                    { 2, "456 Maadi, Cairo, Egypt", null, null, null, null, null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, null, 15000.00m, new DateTime(1993, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master of Business Administration", "amira.mostafa@email.com", 22000.00m, "Amira", "Female", "Mostafa", null, null, null, "+20-105-222-3333", null, null, null, null, "HR Management, Recruitment, Employee Relations, HRIS", null, false, 5 },
                    { 3, "789 Heliopolis, Cairo, Egypt", null, null, null, null, null, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, null, 10000.00m, new DateTime(1996, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Accounting", "karim.nabil@email.com", 16000.00m, "Karim", "Male", "Nabil", null, null, null, "+20-110-333-4444", null, null, null, null, "Financial Reporting, Excel, SAP, Auditing", null, false, 2 },
                    { 4, "321 Zamalek, Cairo, Egypt", null, null, null, null, null, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, null, 11000.00m, new DateTime(1994, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Marketing", "salma.hany@email.com", 17000.00m, "Salma", "Female", "Hany", null, null, null, "+20-122-444-5555", null, null, null, null, "Digital Marketing, SEO, Social Media, Content Strategy", null, false, 3 },
                    { 5, "654 6th October City, Egypt", null, null, null, null, null, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, null, 8000.00m, new DateTime(1997, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Engineering", "ziad.hamdy@email.com", 13000.00m, "Ziad", "Male", "Hamdy", null, null, null, "+20-155-555-6666", null, null, null, null, "Python, Machine Learning, Data Analysis, TensorFlow", null, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Competencies",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to clearly convey information and ideas through various mediums", "Communication", null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to identify, analyze, and resolve problems effectively", "Problem Solving", null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to work collaboratively with others to achieve common goals", "Teamwork", null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to guide, inspire, and influence others towards achieving objectives", "Leadership", null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Proficiency in job-specific technical skills and knowledge", "Technical Expertise", null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to adjust to new conditions and handle change effectively", "Adaptability", null },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to prioritize tasks and manage time efficiently", "Time Management", null },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ability to take proactive action and go beyond basic requirements", "Initiative", null }
                });

            migrationBuilder.InsertData(
                table: "Deductions",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsPercentage", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 9.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Social Insurance", null },
                    { 2, 12.5m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Income Tax", null },
                    { 3, 500.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Absence Deduction", null },
                    { 4, 100.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Late Arrival Penalty", null },
                    { 5, 1000.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Loan Installment", null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "Description", "EmployeeCount", "ManagerId", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages all IT infrastructure, software development, and technical support", null, null, "Information Technology", null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Handles recruitment, employee relations, benefits, and HR policies", null, null, "Human Resources", null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages financial operations, accounting, budgeting, and reporting", null, null, "Finance", null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Drives sales growth, customer acquisition, and marketing campaigns", null, null, "Sales & Marketing", null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Oversees day-to-day operations and process optimization", null, null, "Operations", null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Provides customer service and technical support", null, null, "Customer Support", null }
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "CreatedAt", "Description", "EmployeeCount", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Develops and maintains software applications", null, "Software Engineer", null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lead developer responsible for complex technical solutions", null, "Senior Software Engineer", null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages and guides team members to achieve project goals", null, "Team Lead", null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages deployment pipelines and infrastructure", null, "DevOps Engineer", null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ensures software quality through testing and automation", null, "QA Engineer", null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Oversees human resources operations and employee management", null, "HR Manager", null },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Handles recruitment, onboarding, and employee relations", null, "HR Specialist", null },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Focuses on talent acquisition and candidate screening", null, "Recruiter", null },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Analyzes financial data and prepares reports", null, "Financial Analyst", null },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages accounting records and financial transactions", null, "Accountant", null },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Oversees financial planning and budget management", null, "Finance Manager", null },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Drives sales and builds customer relationships", null, "Sales Representative", null },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Develops and executes marketing campaigns", null, "Marketing Specialist", null },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Identifies growth opportunities and partnerships", null, "Business Development Manager", null },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manages daily operations and process improvements", null, "Operations Manager", null },
                    { 16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Plans, executes, and closes projects successfully", null, "Project Manager", null },
                    { 17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Provides customer assistance and resolves issues", null, "Customer Support Representative", null },
                    { 18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Leads customer support team and ensures service quality", null, "Support Team Lead", null }
                });

            migrationBuilder.InsertData(
                table: "KPIs",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Name", "Target", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "PROJ-COMP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Percentage of projects completed on time", "Project Completion Rate", 95.0m, "%", null },
                    { 2, "CUST-SAT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Average customer satisfaction rating (1-5 scale)", "Customer Satisfaction Score", 4.5m, "score", null },
                    { 3, "CODE-QUAL", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Code quality metrics including test coverage and code review pass rate", "Code Quality Score", 90.0m, "%", null },
                    { 4, "ATTEND", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Employee attendance rate excluding approved leaves", "Attendance Rate", 98.0m, "%", null },
                    { 5, "TRAIN-COMP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Number of training courses completed per review cycle", "Training Completion", 3.0m, "courses", null },
                    { 6, "SALES-REV", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Revenue generated from sales activities", "Sales Revenue Target", 500000.0m, "EGP", null },
                    { 7, "TICKET-RES", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Average time to resolve support tickets", "Ticket Resolution Time", 24.0m, "hours", null },
                    { 8, "RECRUIT-TIME", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Average time from job posting to hire", "Time to Hire", 30.0m, "days", null }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CanCarryForward", "CarryForwardLimit", "CreatedAt", "Description", "GenderRestriction", "IsPaid", "MaxDays", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, true, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Annual paid leave after completing the first year of work", null, true, 30, "Annual Leave", null },
                    { 2, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Medical leave based on a valid medical certificate", null, true, 30, "Sick Leave", null },
                    { 3, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Maternity leave for female employees, 90 days paid", "Female", true, 90, "Maternity Leave", null },
                    { 4, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Short paid leave for new fathers as per company policy", "Male", true, 3, "Paternity Leave", null },
                    { 5, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Leave without pay subject to management approval", null, false, 30, "Unpaid Leave", null },
                    { 6, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Leave for emergencies (death of a relative, special circumstances)", null, true, 5, "Emergency Leave", null },
                    { 7, false, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hajj leave for Muslims, 10 days paid, once in a lifetime", null, true, 10, "Hajj Leave", null }
                });

            migrationBuilder.InsertData(
                table: "ReviewCycles",
                columns: new[] { "Id", "CreatedAt", "EndDate", "Frequency", "Name", "RatingScale", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), 1, "2024 Annual Review", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Q1 2025 Review", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 3, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Q2 2025 Review", 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "TrainingCourses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationHours", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Improve interpersonal and workplace communication skills.", 8, "Effective Communication", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Core leadership principles and team management skills.", 16, "Leadership Essentials", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basics of project planning, scope, scheduling, and risk.", 40, "Project Management Fundamentals", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop covering Agile principles and Scrum ceremonies.", 16, "Agile & Scrum Workshop", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prioritization and productivity techniques.", 4, "Time Management", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deep dive into C# features and best practices.", 40, "Advanced C# Programming", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Secure coding, compliance, and data protection essentials.", 8, "Data Protection & Security", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best practices for customer interactions.", 8, "Customer Service Excellence", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ApplicationUserId", "BasicSalary", "ContactNumber", "CreatedAt", "DateOfBirth", "DepartmentId", "DesignationId", "EFF_End", "EFF_Start", "Email", "FaceEmbedding", "FirstName", "Gender", "HireDate", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Cairo Street, Nasr City, Cairo, Egypt", null, 25000.00m, "+20-123-456-7890", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, null, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.hassan@company.com", null, "Ahmed", "Male", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hassan", null },
                    { 2, "456 Alexandria Road, Maadi, Cairo, Egypt", null, 20000.00m, "+20-100-555-1234", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1992, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, null, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.mohamed@company.com", null, "Sarah", "Female", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohamed", null },
                    { 3, "789 Pyramids Avenue, Giza, Egypt", null, 15000.00m, "+20-111-222-3333", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1988, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khaled.ibrahim@company.com", null, "Khaled", "Male", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ibrahim", null },
                    { 4, "321 Nile Corniche, Zamalek, Cairo, Egypt", null, 22000.00m, "+20-122-333-4444", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1987, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, null, new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "fatima.ali@company.com", null, "Fatima", "Female", new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", null },
                    { 5, "654 Heliopolis Street, Cairo, Egypt", null, 16000.00m, "+20-155-666-7777", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1991, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, null, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar.mahmoud@company.com", null, "Omar", "Male", new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mahmoud", null },
                    { 6, "987 Garden City, Cairo, Egypt", null, 24000.00m, "+20-101-888-9999", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11, null, new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariam.youssef@company.com", null, "Mariam", "Female", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Youssef", null },
                    { 7, "147 Downtown, Cairo, Egypt", null, 14000.00m, "+20-127-111-2222", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1993, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, null, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "hassan.saleh@company.com", null, "Hassan", "Male", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saleh", null },
                    { 8, "258 New Cairo, Egypt", null, 23000.00m, "+20-150-333-4444", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 14, null, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nour.abdel@company.com", null, "Nour", "Female", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abdel", null },
                    { 9, "369 6th October City, Egypt", null, 13000.00m, "+20-106-555-6666", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1994, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12, null, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "tarek.farid@company.com", null, "Tarek", "Male", new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farid", null },
                    { 10, "741 Mohandessin, Giza, Egypt", null, 26000.00m, "+20-128-777-8888", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1986, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 15, null, new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "layla.kamal@company.com", null, "Layla", "Female", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kamal", null }
                });

            migrationBuilder.InsertData(
                table: "JobPostings",
                columns: new[] { "Id", "ClosingDate", "CreatedAt", "DepartmentId", "Description", "DesignationId", "IsActive", "PostedDate", "Requirements", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "We are looking for an experienced .NET developer to join our IT team. The ideal candidate will have strong experience in ASP.NET Core, Entity Framework, and modern web technologies.", 2, true, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5+ years of .NET development experience\nProficiency in C#, ASP.NET Core, EF Core\nExperience with SQL Server and Azure\nKnowledge of Angular or React is a plus", "Senior .NET Developer", null },
                    { 2, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Join our HR team to manage recruitment, onboarding, and employee relations. We're looking for someone with excellent communication skills and HR experience.", 7, true, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "3+ years of HR experience\nExperience with recruitment and onboarding\nKnowledge of labor laws and HR best practices\nExcellent interpersonal and communication skills", "HR Specialist", null },
                    { 3, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, "We need a detail-oriented financial analyst to support our finance team with budgeting, forecasting, and financial reporting.", 9, true, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor's degree in Finance or Accounting\n2+ years of financial analysis experience\nProficiency in Excel and financial software\nStrong analytical and problem-solving skills", "Financial Analyst", null },
                    { 4, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Looking for a creative marketing specialist to develop and execute marketing campaigns across digital and traditional channels.", 13, true, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3+ years of marketing experience\nExperience with digital marketing and SEO\nExcellent content creation skills\nFamiliarity with marketing analytics tools", "Marketing Specialist", null },
                    { 5, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Join our IT team as a DevOps engineer to manage our cloud infrastructure, CI/CD pipelines, and deployment processes.", 4, true, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3+ years of DevOps experience\nExperience with Azure/AWS cloud platforms\nProficiency in Docker, Kubernetes\nKnowledge of CI/CD tools (Jenkins, Azure DevOps)", "DevOps Engineer", null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeAllowances",
                columns: new[] { "AllowanceId", "EmployeeId", "Amount", "CreatedAt", "EndDate", "IsPercentage", "Recurrence", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 2000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 1, 800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 4, 1, 300.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 1, 2, 1800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 2, 800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 5, 2, 200.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 8, 2, 2000.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 1, 3, 1500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 3, 700.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 1, 4, 1800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 4, 800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 4, 4, 300.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 1, 5, 1500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 5, 700.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 1, 6, 2000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 2, 6, 800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null },
                    { 4, 6, 300.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "OneTime", null, null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeDeductions",
                columns: new[] { "DeductionId", "EmployeeId", "Amount", "CreatedAt", "EndDate", "IsPercentage", "Recurrence", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 2250.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 1, 3750.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 2, 1800.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, 2500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 3, 1350.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 3, 1500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 4, 1980.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 4, 2750.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 5, 1440.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 5, 1600.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 6, 2160.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 6, 3000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Permanent", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeTrainings",
                columns: new[] { "EmployeeId", "TrainingCourseId", "CompletionDate", "CreatedAt", "EnrollmentDate", "Id", "UpdatedAt" },
                values: new object[] { 1, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, null });

            migrationBuilder.InsertData(
                table: "EmployeeTrainings",
                columns: new[] { "EmployeeId", "TrainingCourseId", "CompletionDate", "CreatedAt", "EnrollmentDate", "Id", "RewardGiven", "Status", "UpdatedAt" },
                values: new object[] { 2, 3, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, true, "Completed", null });

            migrationBuilder.InsertData(
                table: "EmployeeTrainings",
                columns: new[] { "EmployeeId", "TrainingCourseId", "CompletionDate", "CreatedAt", "EnrollmentDate", "Id", "Status", "UpdatedAt" },
                values: new object[] { 3, 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Cancelled", null });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ApplicationDate", "AssignedRecruiterId", "CandidateId", "CoverLetter", "CreatedAt", "CurrentStage", "ExpectedSalary", "InterviewDate", "InterviewFeedback", "InterviewRating", "JobPostingId", "Notes", "OfferedSalary", "RejectionReason", "ReviewedBy", "ReviewedDate", "ReviewerId", "Source", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, "I am excited to apply for the Senior .NET Developer position. With 3 years of experience in .NET development, I am confident in my ability to contribute to your team.", new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0, 18000.00m, new DateTime(2024, 10, 15, 10, 0, 0, 0, DateTimeKind.Utc), null, null, 1, "Strong technical background, scheduled for technical interview", null, null, null, null, null, 0, 1, null },
                    { 2, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, "With 5 years of HR experience, I am well-equipped to handle recruitment and employee relations at your organization.", new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), 0, 22000.00m, new DateTime(2024, 10, 18, 14, 0, 0, 0, DateTimeKind.Utc), null, null, 2, "Excellent HR background, moved to shortlist", null, null, null, null, null, 0, 2, null },
                    { 3, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, "I am applying for the Financial Analyst position. My background in accounting and financial reporting makes me a great fit.", new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), 0, 16000.00m, null, null, null, 3, "New application, pending review", null, null, null, null, null, 0, 0, null },
                    { 4, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, "I am passionate about digital marketing and would love to bring my skills to your marketing team.", new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc), 0, 17000.00m, null, null, null, 4, "Good portfolio, under review by marketing manager", null, null, null, null, null, 0, 1, null },
                    { 5, new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, "I am eager to start my DevOps career with your company. I have hands-on experience with Docker and Kubernetes from my academic projects.", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Utc), 0, 13000.00m, null, null, null, 5, "Junior candidate, needs experience verification", null, null, null, null, null, 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Payslips",
                columns: new[] { "Id", "BasicSalary", "CreatedAt", "EmployeeId", "GeneratedAt", "Month", "NetSalary", "TotalAllowances", "TotalDeductions", "UpdatedAt", "Year" },
                values: new object[,]
                {
                    { 1, 25000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 22100.00m, 3100.00m, 6000.00m, null, 2024 },
                    { 2, 20000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 18500.00m, 2800.00m, 4300.00m, null, 2024 },
                    { 3, 15000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 14350.00m, 2200.00m, 2850.00m, null, 2024 },
                    { 4, 22000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 20170.00m, 2900.00m, 4730.00m, null, 2024 },
                    { 5, 16000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 15160.00m, 2200.00m, 3040.00m, null, 2024 },
                    { 6, 24000.00m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 21940.00m, 3100.00m, 5160.00m, null, 2024 },
                    { 7, 25000.00m, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 22100.00m, 3100.00m, 6000.00m, null, 2024 },
                    { 8, 20000.00m, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 18500.00m, 2800.00m, 4300.00m, null, 2024 },
                    { 9, 22000.00m, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 20170.00m, 2900.00m, 4730.00m, null, 2024 },
                    { 10, 24000.00m, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 21940.00m, 3100.00m, 5160.00m, null, 2024 }
                });

            migrationBuilder.InsertData(
                table: "PerformanceReviews",
                columns: new[] { "Id", "CreatedAt", "EmployeeId", "FinalRating", "ReviewCycleId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 4.5m, 1, 3, null },
                    { 2, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 4.2m, 1, 3, null },
                    { 3, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3.8m, 1, 3, null },
                    { 4, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 2, 2, null },
                    { 5, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, 2, 1, null },
                    { 6, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, 2, 2, null },
                    { 7, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, null, 3, 0, null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeCompetencyRatings",
                columns: new[] { "Id", "CompetencyId", "CreatedAt", "Notes", "PerformanceReviewId", "Rating", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Excellent communication with team and stakeholders", 1, 4.5m, null },
                    { 2, 4, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Outstanding leadership skills, successfully mentored junior team members", 1, 4.8m, null },
                    { 3, 5, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Deep technical knowledge in .NET and cloud technologies", 1, 4.7m, null },
                    { 4, 2, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Excellent analytical and problem-solving abilities", 2, 4.5m, null },
                    { 5, 5, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Strong technical skills with room for growth in architecture", 2, 4.3m, null },
                    { 6, 8, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Proactively suggests improvements to processes", 2, 4.0m, null },
                    { 7, 3, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Good team player, collaborates well with colleagues", 3, 4.0m, null },
                    { 8, 7, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Needs improvement in meeting deadlines consistently", 3, 3.5m, null },
                    { 9, 4, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Continues to demonstrate strong leadership", 4, 4.6m, null },
                    { 10, 6, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Handled team restructuring effectively", 4, 4.3m, null },
                    { 11, 1, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Exceptional communication across all departments", 6, 4.8m, null },
                    { 12, 8, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Launched multiple HR initiatives successfully", 6, 4.5m, null }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comments", "CreatedAt", "FromEmployeeId", "PerformanceReviewId", "SubmittedAt", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "I believe I have successfully led the team through a challenging year of modernization. I have focused on improving code quality and mentoring junior developers. Areas for improvement include work-life balance and delegation.", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 2, "Ahmed has been an exceptional team lead. His technical expertise and leadership have been instrumental in the successful delivery of our major projects. He consistently goes above and beyond.", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, null },
                    { 3, "Ahmed is a great mentor and always available to help. His code reviews are thorough and educational. I have learned a lot from working with him.", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, null },
                    { 4, "This year I focused on improving our deployment processes and expanding my cloud skills. I completed my Azure certification and implemented CI/CD pipelines. I want to take on more leadership responsibilities.", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 5, "Sarah has shown tremendous growth this year. Her initiative in setting up our CI/CD infrastructure has significantly improved team productivity. Ready for senior technical responsibilities.", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, null },
                    { 6, "Q1 has been focused on improving team processes and reducing technical debt. While code review improvements are on track, the technical debt reduction is behind schedule due to urgent production issues.", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, 4, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 7, "I have successfully reduced our time-to-hire by 25%, exceeding our target. The wellness program is progressing well and has received positive feedback from employees. Looking forward to expanding HR digital initiatives.", new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), 4, 6, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 8, "Mona has been instrumental in improving our hiring process. Her collaboration with the finance team on the wellness budget was excellent. Great cross-departmental communication.", new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Utc), 5, 6, new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Utc), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "PerformanceReviewId", "ProgressPercent", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Lead the migration of legacy systems to .NET 8", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), 1, 100m, 3, "Complete System Modernization Project", null },
                    { 2, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Provide guidance and code reviews for 3 junior developers", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), 1, 100m, 3, "Mentor Junior Developers", null },
                    { 3, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Set up automated deployment pipelines for all projects", new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Utc), 2, 100m, 3, "Implement CI/CD Pipeline", null },
                    { 4, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Complete Azure Developer Associate certification", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), 2, 100m, 3, "Obtain Azure Certification", null },
                    { 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Establish and document code review standards", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 4, 75m, 1, "Improve Team Code Review Process", null },
                    { 6, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Refactor and resolve 50% of identified technical debt items", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 4, 30m, 2, "Reduce Technical Debt", null },
                    { 7, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Set up monitoring and alerting for all critical APIs", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 5, 60m, 1, "Implement API Performance Monitoring", null },
                    { 8, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Reduce average time-to-hire by 20%", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 6, 100m, 3, "Streamline Recruitment Process", null },
                    { 9, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Design and implement a comprehensive wellness initiative", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), 6, 80m, 1, "Launch Employee Wellness Program", null },
                    { 10, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Prepare all documentation for Q2 financial audit", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), 7, 0m, 0, "Complete Financial Audit Preparation", null }
                });

            migrationBuilder.InsertData(
                table: "KPIResults",
                columns: new[] { "Id", "Actual", "CreatedAt", "KPIId", "PerformanceReviewId", "UpdatedAt", "WeightedScore" },
                values: new object[,]
                {
                    { 1, 97.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, null, 4.7m },
                    { 2, 92.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1, null, 4.5m },
                    { 3, 99.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, null, 4.8m },
                    { 4, 94.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, null, 4.3m },
                    { 5, 88.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, null, 4.2m },
                    { 6, 4.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 2, null, 4.5m },
                    { 7, 85.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 3, null, 3.8m },
                    { 8, 96.0m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, 3, null, 4.0m },
                    { 9, 90.0m, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, 4, null, null },
                    { 10, 20.0m, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 7, 4, null, null },
                    { 11, 25.0m, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 8, 6, null, 4.8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ConvertedToEmployeeId",
                table: "Candidates",
                column: "ConvertedToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllowances_AllowanceId",
                table: "EmployeeAllowances",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompetencyRatings_CompetencyId",
                table: "EmployeeCompetencyRatings",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompetencyRatings_PerformanceReviewId",
                table: "EmployeeCompetencyRatings",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDeductions_DeductionId",
                table: "EmployeeDeductions",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_EmployeeId",
                table: "EmployeeLeaveBalances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTrainingRequests_EmployeeId",
                table: "EmployeeTrainingRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTrainingRequests_ReviewedByManagerId",
                table: "EmployeeTrainingRequests",
                column: "ReviewedByManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTrainingRequests_TrainingCourseId",
                table: "EmployeeTrainingRequests",
                column: "TrainingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTrainings_TrainingCourseId",
                table: "EmployeeTrainings",
                column: "TrainingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PerformanceReviewId",
                table: "Feedbacks",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PerformanceReviewId",
                table: "Goals",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AssignedRecruiterId",
                table: "JobApplications",
                column: "AssignedRecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostingId",
                table: "JobApplications",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ReviewerId",
                table: "JobApplications",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_DepartmentId",
                table: "JobPostings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_DesignationId",
                table: "JobPostings",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIResults_KPIId",
                table: "KPIResults",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIResults_PerformanceReviewId",
                table: "KPIResults",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApprovals_ApproverId",
                table: "LeaveApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApprovals_LeaveRequestId",
                table: "LeaveApprovals",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewedById",
                table: "LeaveRequests",
                column: "ReviewedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientUserId_IsRead",
                table: "Notifications",
                columns: new[] { "RecipientUserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewCycleId",
                table: "PerformanceReviews",
                column: "ReviewCycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "EmployeeAllowances");

            migrationBuilder.DropTable(
                name: "EmployeeCompetencyRatings");

            migrationBuilder.DropTable(
                name: "EmployeeDeductions");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveBalances");

            migrationBuilder.DropTable(
                name: "EmployeeTrainingRequests");

            migrationBuilder.DropTable(
                name: "EmployeeTrainings");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "KPIResults");

            migrationBuilder.DropTable(
                name: "LeaveApprovals");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payslips");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "Deductions");

            migrationBuilder.DropTable(
                name: "TrainingCourses");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "ReviewCycles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");
        }
    }
}

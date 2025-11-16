using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3249));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3357));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 57, 15, 253, DateTimeKind.Local).AddTicks(3492));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ApplicationUserId", "BasicSalary", "ContactNumber", "CreatedAt", "DateOfBirth", "DepartmentId", "DesignationId", "EFF_End", "EFF_Start", "Email", "FirstName", "Gender", "HireDate", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Cairo Street, Nasr City, Cairo, Egypt", null, 25000.00m, "+20-123-456-7890", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, null, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.hassan@company.com", "Ahmed", "Male", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hassan", null },
                    { 2, "456 Alexandria Road, Maadi, Cairo, Egypt", null, 20000.00m, "+20-100-555-1234", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1992, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, null, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.mohamed@company.com", "Sarah", "Female", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohamed", null },
                    { 3, "789 Pyramids Avenue, Giza, Egypt", null, 15000.00m, "+20-111-222-3333", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1988, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khaled.ibrahim@company.com", "Khaled", "Male", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ibrahim", null },
                    { 4, "321 Nile Corniche, Zamalek, Cairo, Egypt", null, 22000.00m, "+20-122-333-4444", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1987, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, null, new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "fatima.ali@company.com", "Fatima", "Female", new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", null },
                    { 5, "654 Heliopolis Street, Cairo, Egypt", null, 16000.00m, "+20-155-666-7777", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1991, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, null, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar.mahmoud@company.com", "Omar", "Male", new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mahmoud", null },
                    { 6, "987 Garden City, Cairo, Egypt", null, 24000.00m, "+20-101-888-9999", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11, null, new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariam.youssef@company.com", "Mariam", "Female", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Youssef", null },
                    { 7, "147 Downtown, Cairo, Egypt", null, 14000.00m, "+20-127-111-2222", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1993, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, null, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "hassan.saleh@company.com", "Hassan", "Male", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saleh", null },
                    { 8, "258 New Cairo, Egypt", null, 23000.00m, "+20-150-333-4444", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 14, null, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nour.abdel@company.com", "Nour", "Female", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abdel", null },
                    { 9, "369 6th October City, Egypt", null, 13000.00m, "+20-106-555-6666", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1994, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12, null, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "tarek.farid@company.com", "Tarek", "Male", new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farid", null },
                    { 10, "741 Mohandessin, Giza, Egypt", null, 26000.00m, "+20-128-777-8888", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1986, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 15, null, new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "layla.kamal@company.com", "Layla", "Female", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kamal", null }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "JobPostings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobPostings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobPostings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobPostings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobPostings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6157));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6258));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6286));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6310));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6353));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 11, 13, 36, 846, DateTimeKind.Local).AddTicks(6391));
        }
    }
}

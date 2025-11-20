namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeSeedData
{
    public static void SeedEmployees(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            // IT Department
            new Employee
            {
                Id = 1,
                FirstName = "Ahmed",
                LastName = "Hassan",
                Email = "ahmed.hassan@company.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = "Male",
                ContactNumber = "+20-123-456-7890",
                Address = "123 Cairo Street, Nasr City, Cairo, Egypt",
                HireDate = new DateTime(2022, 1, 15),
                DepartmentId = 1,
                DesignationId = 3,
                BasicSalary = 25000.00m,
                EFF_Start = new DateTime(2022, 1, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Mohamed",
                Email = "sarah.mohamed@company.com",
                DateOfBirth = new DateTime(1992, 8, 22),
                Gender = "Female",
                ContactNumber = "+20-100-555-1234",
                Address = "456 Alexandria Road, Maadi, Cairo, Egypt",
                HireDate = new DateTime(2022, 3, 1),
                DepartmentId = 1,
                DesignationId = 2,
                BasicSalary = 20000.00m,
                EFF_Start = new DateTime(2022, 3, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 3,
                FirstName = "Khaled",
                LastName = "Ibrahim",
                Email = "khaled.ibrahim@company.com",
                DateOfBirth = new DateTime(1988, 11, 10),
                Gender = "Male",
                ContactNumber = "+20-111-222-3333",
                Address = "789 Pyramids Avenue, Giza, Egypt",
                HireDate = new DateTime(2021, 6, 1),
                DepartmentId = 1,
                DesignationId = 1,
                BasicSalary = 15000.00m,
                EFF_Start = new DateTime(2021, 6, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // HR Department
            new Employee
            {
                Id = 4,
                FirstName = "Fatima",
                LastName = "Ali",
                Email = "fatima.ali@company.com",
                DateOfBirth = new DateTime(1987, 4, 18),
                Gender = "Female",
                ContactNumber = "+20-122-333-4444",
                Address = "321 Nile Corniche, Zamalek, Cairo, Egypt",
                HireDate = new DateTime(2021, 2, 15),
                DepartmentId = 2,
                DesignationId = 6,
                BasicSalary = 22000.00m,
                EFF_Start = new DateTime(2021, 2, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 5,
                FirstName = "Omar",
                LastName = "Mahmoud",
                Email = "omar.mahmoud@company.com",
                DateOfBirth = new DateTime(1991, 9, 5),
                Gender = "Male",
                ContactNumber = "+20-155-666-7777",
                Address = "654 Heliopolis Street, Cairo, Egypt",
                HireDate = new DateTime(2022, 5, 10),
                DepartmentId = 2,
                DesignationId = 7,
                BasicSalary = 16000.00m,
                EFF_Start = new DateTime(2022, 5, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Finance Department
            new Employee
            {
                Id = 6,
                FirstName = "Mariam",
                LastName = "Youssef",
                Email = "mariam.youssef@company.com",
                DateOfBirth = new DateTime(1989, 12, 25),
                Gender = "Female",
                ContactNumber = "+20-101-888-9999",
                Address = "987 Garden City, Cairo, Egypt",
                HireDate = new DateTime(2021, 8, 1),
                DepartmentId = 3,
                DesignationId = 11,
                BasicSalary = 24000.00m,
                EFF_Start = new DateTime(2021, 8, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 7,
                FirstName = "Hassan",
                LastName = "Saleh",
                Email = "hassan.saleh@company.com",
                DateOfBirth = new DateTime(1993, 3, 14),
                Gender = "Male",
                ContactNumber = "+20-127-111-2222",
                Address = "147 Downtown, Cairo, Egypt",
                HireDate = new DateTime(2023, 1, 20),
                DepartmentId = 3,
                DesignationId = 10,
                BasicSalary = 14000.00m,
                EFF_Start = new DateTime(2023, 1, 20),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Sales & Marketing Department
            new Employee
            {
                Id = 8,
                FirstName = "Nour",
                LastName = "Abdel",
                Email = "nour.abdel@company.com",
                DateOfBirth = new DateTime(1990, 7, 8),
                Gender = "Female",
                ContactNumber = "+20-150-333-4444",
                Address = "258 New Cairo, Egypt",
                HireDate = new DateTime(2022, 9, 1),
                DepartmentId = 4,
                DesignationId = 14,
                BasicSalary = 23000.00m,
                EFF_Start = new DateTime(2022, 9, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 9,
                FirstName = "Tarek",
                LastName = "Farid",
                Email = "tarek.farid@company.com",
                DateOfBirth = new DateTime(1994, 2, 20),
                Gender = "Male",
                ContactNumber = "+20-106-555-6666",
                Address = "369 6th October City, Egypt",
                HireDate = new DateTime(2023, 4, 15),
                DepartmentId = 4,
                DesignationId = 12,
                BasicSalary = 13000.00m,
                EFF_Start = new DateTime(2023, 4, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Operations Department
            new Employee
            {
                Id = 10,
                FirstName = "Layla",
                LastName = "Kamal",
                Email = "layla.kamal@company.com",
                DateOfBirth = new DateTime(1986, 10, 30),
                Gender = "Female",
                ContactNumber = "+20-128-777-8888",
                Address = "741 Mohandessin, Giza, Egypt",
                HireDate = new DateTime(2021, 4, 10),
                DepartmentId = 5,
                DesignationId = 15,
                BasicSalary = 26000.00m,
                EFF_Start = new DateTime(2021, 4, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

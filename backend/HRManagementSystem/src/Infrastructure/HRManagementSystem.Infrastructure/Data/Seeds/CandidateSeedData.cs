namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class CandidateSeedData
{
    public static void SeedCandidates(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>().HasData(
            new Candidate
            {
                Id = 1,
                FirstName = "Youssef",
                LastName = "Ahmed",
                Email = "youssef.ahmed@email.com",
                Phone = "+20-100-111-2222",
                DateOfBirth = new DateTime(1995, 6, 12),
                Gender = "Male",
                Address = "123 New Cairo, Egypt",
                CurrentSalary = 12000.00m,
                ExpectedSalary = 18000.00m,
                YearsOfExperience = 3,
                Education = "Bachelor of Computer Science",
                Skills = "C#, .NET, SQL Server, Angular",
                ResumeUrl = null,
                CreatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Candidate
            {
                Id = 2,
                FirstName = "Amira",
                LastName = "Mostafa",
                Email = "amira.mostafa@email.com",
                Phone = "+20-105-222-3333",
                DateOfBirth = new DateTime(1993, 3, 25),
                Gender = "Female",
                Address = "456 Maadi, Cairo, Egypt",
                CurrentSalary = 15000.00m,
                ExpectedSalary = 22000.00m,
                YearsOfExperience = 5,
                Education = "Master of Business Administration",
                Skills = "HR Management, Recruitment, Employee Relations, HRIS",
                ResumeUrl = null,
                CreatedAt = new DateTime(2024, 10, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new Candidate
            {
                Id = 3,
                FirstName = "Karim",
                LastName = "Nabil",
                Email = "karim.nabil@email.com",
                Phone = "+20-110-333-4444",
                DateOfBirth = new DateTime(1996, 8, 18),
                Gender = "Male",
                Address = "789 Heliopolis, Cairo, Egypt",
                CurrentSalary = 10000.00m,
                ExpectedSalary = 16000.00m,
                YearsOfExperience = 2,
                Education = "Bachelor of Accounting",
                Skills = "Financial Reporting, Excel, SAP, Auditing",
                ResumeUrl = null,
                CreatedAt = new DateTime(2024, 10, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Candidate
            {
                Id = 4,
                FirstName = "Salma",
                LastName = "Hany",
                Email = "salma.hany@email.com",
                Phone = "+20-122-444-5555",
                DateOfBirth = new DateTime(1994, 11, 5),
                Gender = "Female",
                Address = "321 Zamalek, Cairo, Egypt",
                CurrentSalary = 11000.00m,
                ExpectedSalary = 17000.00m,
                YearsOfExperience = 3,
                Education = "Bachelor of Marketing",
                Skills = "Digital Marketing, SEO, Social Media, Content Strategy",
                ResumeUrl = null,
                CreatedAt = new DateTime(2024, 10, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Candidate
            {
                Id = 5,
                FirstName = "Ziad",
                LastName = "Hamdy",
                Email = "ziad.hamdy@email.com",
                Phone = "+20-155-555-6666",
                DateOfBirth = new DateTime(1997, 1, 30),
                Gender = "Male",
                Address = "654 6th October City, Egypt",
                CurrentSalary = 8000.00m,
                ExpectedSalary = 13000.00m,
                YearsOfExperience = 1,
                Education = "Bachelor of Engineering",
                Skills = "Python, Machine Learning, Data Analysis, TensorFlow",
                ResumeUrl = null,
                CreatedAt = new DateTime(2024, 10, 20, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

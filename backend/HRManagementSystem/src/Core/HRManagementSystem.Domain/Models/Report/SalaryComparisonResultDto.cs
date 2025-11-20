namespace HRManagementSystem.Application.DTOs.Report;

public class SalaryComparisonResultDto
{
    public decimal AverageExpectedSalary { get; set; }
    public decimal AverageOfferedSalary { get; set; }
    public int TotalApplicationsReviewed { get; set; }
}

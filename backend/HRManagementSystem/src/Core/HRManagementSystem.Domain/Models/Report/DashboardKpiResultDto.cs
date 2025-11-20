namespace HRManagementSystem.Application.DTOs.Report;

public class DashboardKpiResultDto
{
    public int TotalEmployees { get; set; }
    public decimal TotalPayroll { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }
    public int ActiveJobPostings { get; set; }
    public int PendingLeaveRequests { get; set; }
}

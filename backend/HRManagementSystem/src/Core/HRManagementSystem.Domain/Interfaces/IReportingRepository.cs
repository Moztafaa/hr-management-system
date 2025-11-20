using HRManagementSystem.Application.DTOs.Report;

namespace HRManagementSystem.Domain.Interfaces;

public interface IReportingRepository
{
    // EMPLOYEE METRICS

    Task<int> GetTotalEmployeesAsync(CancellationToken cancellationToken = default);
    Task<IDictionary<string, int>> GetEmployeesByDepartmentAsync(CancellationToken cancellationToken = default);
    Task<IDictionary<string, int>> GetEmployeesByDesignationAsync(CancellationToken cancellationToken = default);


    // PAYROLL METRICS

    Task<decimal> GetTotalPayrollThisMonthAsync(int year, int month, CancellationToken cancellationToken = default);
    Task<IEnumerable<MonthlyPayrollSummaryDto>> GetPayrollTrendAsync(int year, CancellationToken cancellationToken = default);
    Task<IDictionary<string, decimal>> GetAllowanceCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default);
    Task<IDictionary<string, decimal>> GetDeductionCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default);

    // RECRUITMENT METRICS


    Task<int> GetTotalJobPostingsAsync(CancellationToken cancellationToken = default);
    Task<int> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default);

    // Count of all job applications for a posting
    Task<IDictionary<string, int>> GetApplicationsPerJobPostingAsync(CancellationToken cancellationToken = default);

    // Applications grouped by recruitment stage
    Task<IDictionary<string, int>> GetRecruitmentPipelineAsync(CancellationToken cancellationToken = default);

    // Avg time to review applications
    Task<double> GetAverageApplicationReviewTimeAsync(CancellationToken cancellationToken = default);

    // Expected vs offered salary analysis
    Task<SalaryComparisonResultDto> GetSalaryComparisonAsync(CancellationToken cancellationToken = default);



    // LEAVE MANAGEMENT METRICS


    Task<int> GetTotalLeaveRequestsAsync(CancellationToken cancellationToken = default);
    Task<IDictionary<string, int>> GetLeaveRequestsByStatusAsync(CancellationToken cancellationToken = default);

    // Leave usage by type (Annual, Sick, Maternity...)
    Task<IDictionary<string, int>> GetLeaveUsageByTypeAsync(CancellationToken cancellationToken = default);

    // Leaves taken per month
    Task<IEnumerable<MonthlyLeaveSummaryDto>> GetMonthlyLeaveTrendAsync(int year, CancellationToken cancellationToken = default);

    // Approval speed
    Task<double> GetAverageLeaveApprovalTimeAsync(CancellationToken cancellationToken = default);


    // DASHBOARD KPIs


    Task<DashboardKpiResultDto> GetDashboardKpisAsync(int year, int month, CancellationToken cancellationToken = default);
}

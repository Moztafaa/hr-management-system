using HRManagementSystem.Application.DTOs.Report;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IReportingService
    {
        // EMPLOYEE METRICS
        Task<Response<int>> GetTotalEmployeesAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetEmployeesByDepartmentAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetEmployeesByDesignationAsync(CancellationToken cancellationToken = default);

        // PAYROLL METRICS
        Task<Response<decimal>> GetTotalPayrollThisMonthAsync(int year, int month, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<MonthlyPayrollSummaryDto>>> GetPayrollTrendAsync(int year, CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, decimal>>> GetAllowanceCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, decimal>>> GetDeductionCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default);

        // RECRUITMENT METRICS
        Task<Response<int>> GetTotalJobPostingsAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetApplicationsPerJobPostingAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetRecruitmentPipelineAsync(CancellationToken cancellationToken = default);
        Task<Response<double>> GetAverageApplicationReviewTimeAsync(CancellationToken cancellationToken = default);
        Task<Response<SalaryComparisonResultDto>> GetSalaryComparisonAsync(CancellationToken cancellationToken = default);

        // LEAVE MANAGEMENT METRICS
        Task<Response<int>> GetTotalLeaveRequestsAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetLeaveRequestsByStatusAsync(CancellationToken cancellationToken = default);
        Task<Response<IDictionary<string, int>>> GetLeaveUsageByTypeAsync(CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<MonthlyLeaveSummaryDto>>> GetMonthlyLeaveTrendAsync(int year, CancellationToken cancellationToken = default);
        Task<Response<double>> GetAverageLeaveApprovalTimeAsync(CancellationToken cancellationToken = default);

        // DASHBOARD KPIs
        Task<Response<DashboardKpiResultDto>> GetDashboardKpisAsync(int year, int month, CancellationToken cancellationToken = default);
    }
}

using AutoMapper;
using HRManagementSystem.Application.DTOs.Report;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class ReportingService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReportingService
{


    //  EMPLOYEE METRICS 
    public async Task<Response<int>> GetTotalEmployeesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int total = await _unitOfWork.Reporting.GetTotalEmployeesAsync(cancellationToken);
            return new Response<int>(total, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error fetching total employees: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetEmployeesByDepartmentAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetEmployeesByDepartmentAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching employees by department: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetEmployeesByDesignationAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetEmployeesByDesignationAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching employees by designation: {ex.Message}", true);
        }
    }

    //  PAYROLL METRICS 
    public async Task<Response<decimal>> GetTotalPayrollThisMonthAsync(int year, int month, CancellationToken cancellationToken = default)
    {
        try
        {
            decimal total = await _unitOfWork.Reporting.GetTotalPayrollThisMonthAsync(year, month, cancellationToken);
            return new Response<decimal>(total, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<decimal>(0, $"Error fetching total payroll: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<MonthlyPayrollSummaryDto>>> GetPayrollTrendAsync(int year, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetPayrollTrendAsync(year, cancellationToken);
            return new Response<IEnumerable<MonthlyPayrollSummaryDto>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<MonthlyPayrollSummaryDto>>(null!, $"Error fetching payroll trend: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, decimal>>> GetAllowanceCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetAllowanceCostBreakdownAsync(month, year, cancellationToken);
            return new Response<IDictionary<string, decimal>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, decimal>>(null!, $"Error fetching allowance breakdown: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, decimal>>> GetDeductionCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetDeductionCostBreakdownAsync(month, year, cancellationToken);
            return new Response<IDictionary<string, decimal>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, decimal>>(null!, $"Error fetching deduction breakdown: {ex.Message}", true);
        }
    }

    //  RECRUITMENT METRICS 
    public async Task<Response<int>> GetTotalJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int total = await _unitOfWork.Reporting.GetTotalJobPostingsAsync(cancellationToken);
            return new Response<int>(total, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error fetching total job postings: {ex.Message}", true);
        }
    }

    public async Task<Response<int>> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int total = await _unitOfWork.Reporting.GetActiveJobPostingsAsync(cancellationToken);
            return new Response<int>(total, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error fetching active job postings: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetApplicationsPerJobPostingAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetApplicationsPerJobPostingAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching applications per job posting: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetRecruitmentPipelineAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetRecruitmentPipelineAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching recruitment pipeline: {ex.Message}", true);
        }
    }

    public async Task<Response<double>> GetAverageApplicationReviewTimeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            double avg = await _unitOfWork.Reporting.GetAverageApplicationReviewTimeAsync(cancellationToken);
            return new Response<double>(avg, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<double>(0, $"Error fetching average review time: {ex.Message}", true);
        }
    }

    public async Task<Response<SalaryComparisonResultDto>> GetSalaryComparisonAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetSalaryComparisonAsync(cancellationToken);
            return new Response<SalaryComparisonResultDto>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<SalaryComparisonResultDto>(null!, $"Error fetching salary comparison: {ex.Message}", true);
        }
    }

    //  LEAVE MANAGEMENT METRICS 
    public async Task<Response<int>> GetTotalLeaveRequestsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int total = await _unitOfWork.Reporting.GetTotalLeaveRequestsAsync(cancellationToken);
            return new Response<int>(total, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<int>(0, $"Error fetching total leave requests: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetLeaveRequestsByStatusAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetLeaveRequestsByStatusAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching leave requests by status: {ex.Message}", true);
        }
    }

    public async Task<Response<IDictionary<string, int>>> GetLeaveUsageByTypeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetLeaveUsageByTypeAsync(cancellationToken);
            return new Response<IDictionary<string, int>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IDictionary<string, int>>(null!, $"Error fetching leave usage by type: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<MonthlyLeaveSummaryDto>>> GetMonthlyLeaveTrendAsync(int year, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetMonthlyLeaveTrendAsync(year, cancellationToken);
            return new Response<IEnumerable<MonthlyLeaveSummaryDto>>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<MonthlyLeaveSummaryDto>>(null!, $"Error fetching monthly leave trend: {ex.Message}", true);
        }
    }

    public async Task<Response<double>> GetAverageLeaveApprovalTimeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            double avg = await _unitOfWork.Reporting.GetAverageLeaveApprovalTimeAsync(cancellationToken);
            return new Response<double>(avg, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<double>(0, $"Error fetching average leave approval time: {ex.Message}", true);
        }
    }

    //  DASHBOARD KPIs 
    public async Task<Response<DashboardKpiResultDto>> GetDashboardKpisAsync(int year, int month, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _unitOfWork.Reporting.GetDashboardKpisAsync(year, month, cancellationToken);
            return new Response<DashboardKpiResultDto>(result, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DashboardKpiResultDto>(null!, $"Error fetching dashboard KPIs: {ex.Message}", true);
        }
    }
}

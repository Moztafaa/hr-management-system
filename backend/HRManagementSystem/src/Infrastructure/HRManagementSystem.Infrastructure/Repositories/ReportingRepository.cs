using HRManagementSystem.Application.DTOs.Report;

namespace HRManagementSystem.Infrastructure.Repositories;

public class ReportingRepository(ApplicationDbContext _context) : IReportingRepository
{

    // EMPLOYEE METRICS


    public async Task<int> GetTotalEmployeesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees.CountAsync(cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetEmployeesByDepartmentAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .Select(d => new
            {
                d.Name,
                Count = d.Employees.Count()
            })
            .ToDictionaryAsync(x => x.Name, x => x.Count, cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetEmployeesByDesignationAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Designations
            .Select(des => new
            {
                des.Title,
                Count = des.Employees.Count()
            })
            .ToDictionaryAsync(x => x.Title, x => x.Count, cancellationToken);
    }



    // PAYROLL METRICS


    public async Task<decimal> GetTotalPayrollThisMonthAsync(int year, int month, CancellationToken cancellationToken = default)
    {
        return await _context.Payslips
            .Where(p => p.GeneratedAt.Year == year && p.GeneratedAt.Month == month)
            .SumAsync(p => p.NetSalary + p.TotalAllowances - p.TotalDeductions, cancellationToken);
    }

    public async Task<IEnumerable<MonthlyPayrollSummaryDto>> GetPayrollTrendAsync(int year, CancellationToken cancellationToken = default)
    {
        return await _context.Payslips
            .Where(p => p.GeneratedAt.Year == year)
            .GroupBy(p => p.GeneratedAt.Month)
            .Select(g => new MonthlyPayrollSummaryDto
            {
                Month = g.Key,
                TotalPayroll = g.Sum(p => p.NetSalary + p.TotalAllowances - p.TotalDeductions)
            })
            .OrderBy(x => x.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task<IDictionary<string, decimal>> GetAllowanceCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default)
    {
        return (IDictionary<string, decimal>)await _context.Allowances
            .Select(a => new
            {
                a.Name,
                Total = a.EmployeeAllowances
                            .Where(ea => ea.Employee.Payslips.Any(p => p.Month == month && p.Year == year))
                            .Sum(ea => ea.Amount)
            })
            .ToDictionaryAsync(x => x.Name, x => x.Total, cancellationToken);
    }


    public async Task<IDictionary<string, decimal>> GetDeductionCostBreakdownAsync(int month, int year, CancellationToken cancellationToken = default)
    {
        return (IDictionary<string, decimal>)await _context.Deductions
            .Select(d => new
            {
                d.Name,
                Total = d.EmployeeDeductions
                            .Where(ed => ed.Employee.Payslips.Any(p => p.Month == month && p.Year == year))
                            .Sum(ed => ed.Amount)
            })
            .ToDictionaryAsync(x => x.Name, x => x.Total, cancellationToken);
    }




    // RECRUITMENT METRICS


    public async Task<int> GetTotalJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JobPostings.CountAsync(cancellationToken);
    }

    public async Task<int> GetActiveJobPostingsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JobPostings
            .CountAsync(j => (bool)j.IsActive, cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetApplicationsPerJobPostingAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JobPostings
            .Select(j => new
            {
                j.Title,
                Count = j.JobApplications.Count()
            })
            .ToDictionaryAsync(x => x.Title, x => x.Count, cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetRecruitmentPipelineAsync(CancellationToken cancellationToken = default)
    {
        return (IDictionary<string, int>)await _context.JobApplications
            .GroupBy(a => a.CurrentStage)
            .Select(g => new
            {
                Stage = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.Stage, x => x.Count, cancellationToken);
    }

    public async Task<double> GetAverageApplicationReviewTimeAsync(CancellationToken cancellationToken = default)
    {
        return (double)await _context.JobApplications
            .Where(a => a.ReviewedDate != null)
            .AverageAsync(a => EF.Functions.DateDiffDay(a.ApplicationDate, a.ReviewedDate), cancellationToken);
    }

    public async Task<SalaryComparisonResultDto> GetSalaryComparisonAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.JobApplications
            .Where(a => a.OfferedSalary != null)
            .GroupBy(a => 1)
            .Select(g => new SalaryComparisonResultDto
            {
                AverageExpectedSalary = (decimal)g.Average(a => a.ExpectedSalary),
                AverageOfferedSalary = g.Average(a => a.OfferedSalary ?? 0)
            })
            .FirstOrDefaultAsync(cancellationToken);

        return result ?? new SalaryComparisonResultDto();
    }


    // LEAVE MANAGEMENT METRICS


    public async Task<int> GetTotalLeaveRequestsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.LeaveRequests.CountAsync(cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetLeaveRequestsByStatusAsync(CancellationToken cancellationToken = default)
    {
        return (IDictionary<string, int>)await _context.LeaveRequests
            .GroupBy(l => l.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }

    public async Task<IDictionary<string, int>> GetLeaveUsageByTypeAsync(CancellationToken cancellationToken = default)
    {
        return (IDictionary<string, int>)await _context.LeaveRequests
            .GroupBy(l => l.LeaveType)
            .Select(g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.Type, x => x.Count, cancellationToken);
    }

    public async Task<IEnumerable<MonthlyLeaveSummaryDto>> GetMonthlyLeaveTrendAsync(int year, CancellationToken cancellationToken = default)
    {
        return await _context.LeaveRequests
            .Where(l => l.StartDate.Year == year)
            .GroupBy(l => l.StartDate.Month)
            .Select(g => new MonthlyLeaveSummaryDto
            {
                Month = g.Key,
                TotalLeaves = g.Count()
            })
            .OrderBy(x => x.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task<double> GetAverageLeaveApprovalTimeAsync(CancellationToken cancellationToken = default)
    {
        return (double)await _context.LeaveRequests
            .Where(l => l.ReviewedAt != null)
            .AverageAsync(
                l => EF.Functions.DateDiffDay(l.CreatedAt, l.ReviewedAt),
                cancellationToken
            );
    }




    // DASHBOARD KPIs


    public async Task<DashboardKpiResultDto> GetDashboardKpisAsync(int year, int month, CancellationToken cancellationToken = default)
    {
        var kpi = new DashboardKpiResultDto();

        // Employee metrics
        kpi.TotalEmployees = await _context.Employees.CountAsync(cancellationToken);

        // Payroll metrics
        kpi.TotalPayroll = await GetTotalPayrollThisMonthAsync(year, month, cancellationToken);
        kpi.TotalAllowances = await _context.Payslips
            .Where(p => p.Year == year && p.Month == month)
            .SumAsync(p => p.TotalAllowances, cancellationToken);
        kpi.TotalDeductions = await _context.Payslips
            .Where(p => p.Year == year && p.Month == month)
            .SumAsync(p => p.TotalDeductions, cancellationToken);

        // Optional: add other KPIs in DTO if needed (ActiveJobPostings, PendingLeaveRequests)
        return kpi;
    }

}

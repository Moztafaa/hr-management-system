namespace HRManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController(IReportingService _reportingService) : ControllerBase
    {

        // EMPLOYEE METRICS 

        [HttpGet("TotalEmployees")]
        public async Task<IActionResult> GetTotalEmployees()
        {
            var result = await _reportingService.GetTotalEmployeesAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("EmployeesByDepartment")]
        public async Task<IActionResult> GetEmployeesByDepartment()
        {
            var result = await _reportingService.GetEmployeesByDepartmentAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("EmployeesByDesignation")]
        public async Task<IActionResult> GetEmployeesByDesignation()
        {
            var result = await _reportingService.GetEmployeesByDesignationAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        // PAYROLL METRICS 

        [HttpGet("TotalPayroll/{year:int}/{month:int}")]
        public async Task<IActionResult> GetTotalPayroll(int year, int month)
        {
            var result = await _reportingService.GetTotalPayrollThisMonthAsync(year, month);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("PayrollTrend/{year:int}")]
        public async Task<IActionResult> GetPayrollTrend(int year)
        {
            var result = await _reportingService.GetPayrollTrendAsync(year);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("AllowanceCostBreakdown/{year:int}/{month:int}")]
        public async Task<IActionResult> GetAllowanceCostBreakdown(int year, int month)
        {
            var result = await _reportingService.GetAllowanceCostBreakdownAsync(month, year);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("DeductionCostBreakdown/{year:int}/{month:int}")]
        public async Task<IActionResult> GetDeductionCostBreakdown(int year, int month)
        {
            var result = await _reportingService.GetDeductionCostBreakdownAsync(month, year);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        //  RECRUITMENT METRICS 

        [HttpGet("TotalJobPostings")]
        public async Task<IActionResult> GetTotalJobPostings()
        {
            var result = await _reportingService.GetTotalJobPostingsAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("ActiveJobPostings")]
        public async Task<IActionResult> GetActiveJobPostings()
        {
            var result = await _reportingService.GetActiveJobPostingsAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("ApplicationsPerJobPosting")]
        public async Task<IActionResult> GetApplicationsPerJobPosting()
        {
            var result = await _reportingService.GetApplicationsPerJobPostingAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("RecruitmentPipeline")]
        public async Task<IActionResult> GetRecruitmentPipeline()
        {
            var result = await _reportingService.GetRecruitmentPipelineAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("AverageApplicationReviewTime")]
        public async Task<IActionResult> GetAverageApplicationReviewTime()
        {
            var result = await _reportingService.GetAverageApplicationReviewTimeAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("SalaryComparison")]
        public async Task<IActionResult> GetSalaryComparison()
        {
            var result = await _reportingService.GetSalaryComparisonAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        //  LEAVE MANAGEMENT METRICS 

        [HttpGet("TotalLeaveRequests")]
        public async Task<IActionResult> GetTotalLeaveRequests()
        {
            var result = await _reportingService.GetTotalLeaveRequestsAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("LeaveRequestsByStatus")]
        public async Task<IActionResult> GetLeaveRequestsByStatus()
        {
            var result = await _reportingService.GetLeaveRequestsByStatusAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("LeaveUsageByType")]
        public async Task<IActionResult> GetLeaveUsageByType()
        {
            var result = await _reportingService.GetLeaveUsageByTypeAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("MonthlyLeaveTrend/{year:int}")]
        public async Task<IActionResult> GetMonthlyLeaveTrend(int year)
        {
            var result = await _reportingService.GetMonthlyLeaveTrendAsync(year);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        [HttpGet("AverageLeaveApprovalTime")]
        public async Task<IActionResult> GetAverageLeaveApprovalTime()
        {
            var result = await _reportingService.GetAverageLeaveApprovalTimeAsync();
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }

        //  DASHBOARD KPIs 

        [HttpGet("DashboardKpis/{year:int}/{month:int}")]
        public async Task<IActionResult> GetDashboardKpis(int year, int month)
        {
            var result = await _reportingService.GetDashboardKpisAsync(year, month);
            return result.HasError ? BadRequest(result.ErrorMessage) : Ok(result.Data);
        }
    }
}

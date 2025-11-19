using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;

namespace HRManagementSystem.Application.DTOs.ESS;

public class ESSDashboardDto
{
    public ESSProfileDto? Profile { get; set; }
    public List<LeaveBalanceDto>? LeaveBalances { get; set; }
    public List<PayslipDto>? RecentPayslips { get; set; }
    public int PendingLeaveRequestsCount { get; set; }
    public int ApprovedLeaveRequestsCount { get; set; }
}

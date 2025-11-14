using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Leaves;
public class EmployeeLeaveBalanceDto
{
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public int Year { get; set; }
    public int TotalAllocated { get; set; }
    public int UsedDays { get; set; }
    public int RemainingDays { get; set; }
    public DateTime LastUpdated { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Domain.Entities;
public class EmployeeLeaveBalance
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }

    public int Year { get; set; }
    public int TotalAllocated { get; set; }
    public int UsedDays { get; set; }
    public int RemainingDays { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // 🔗 Navigation
    public Employee Employee { get; set; }
    public LeaveType LeaveType { get; set; }
}

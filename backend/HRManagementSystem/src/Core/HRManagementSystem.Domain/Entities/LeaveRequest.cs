using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;
public class LeaveRequest: BaseEntity
{
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }

    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

    public int? ReviewedById { get; set; }
    public DateTime? ReviewedAt { get; set; }

    // 🔗 Navigation
    public Employee Employee { get; set; }
    public LeaveType LeaveType { get; set; }
    public Employee? Reviewer { get; set; }
    public ICollection<LeaveApproval>? LeaveApprovals { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Entities;
public class LeaveApproval
{
    public int Id { get; set; }

    public int LeaveRequestId { get; set; }
    public int ApproverId { get; set; }   // ✅ int instead of string

    public LevelApproval Level { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public DateTime? ActionDate { get; set; }

    // 🔗 Navigation
    public LeaveRequest LeaveRequest { get; set; }
    public Employee Approver { get; set; }
}

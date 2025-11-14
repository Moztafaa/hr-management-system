using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Leaves.LeaveTypeDtos;
public class LeaveTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int MaxDays { get; set; }
    public bool CanCarryForward { get; set; }
    public int? CarryForwardLimit { get; set; }
    public bool IsPaid { get; set; } = true;
}

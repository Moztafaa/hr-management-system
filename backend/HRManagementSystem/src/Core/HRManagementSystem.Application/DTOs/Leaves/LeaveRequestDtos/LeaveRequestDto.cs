using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
public class LeaveRequestDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
    public int Status { get; set; } = 0;
    public int? ReviewedById { get; set; }
    public DateTime? ReviewedAt { get; set; } = null;
}

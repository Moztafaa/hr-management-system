using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.DTOs.Performance;
public class GoalDto
{
    public int Id { get; set; }
    public int PerformanceReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GoalStatus Status { get; set; }
    public decimal ProgressPercent { get; set; }
    public DateTime? DueDate { get; set; }
}


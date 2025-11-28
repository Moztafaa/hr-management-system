using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class UpdateGoalProgressDto
{
    public int GoalId { get; set; }
    public decimal ProgressPercent { get; set; } // 0..100
    public string? Status { get; set; } // NotStarted/OnTrack/AtRisk/Completed
}


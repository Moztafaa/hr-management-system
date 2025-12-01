using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Domain.Entities;
public class Goal : BaseEntity
{
    public int PerformanceReviewId { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.NotStarted;
    public decimal ProgressPercent { get; set; } 
    public DateTime? DueDate { get; set; }

    public PerformanceReview Review { get; set; } = default!;
}

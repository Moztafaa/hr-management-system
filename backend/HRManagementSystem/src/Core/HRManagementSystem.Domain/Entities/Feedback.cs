using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Domain.Entities;
public class Feedback : BaseEntity
{
    public int PerformanceReviewId { get; set; }
    public int FromEmployeeId { get; set; } 
    public FeedbackType Type { get; set; }
    public string Comments { get; set; } = default!;
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public PerformanceReview Review { get; set; } = default!;
}

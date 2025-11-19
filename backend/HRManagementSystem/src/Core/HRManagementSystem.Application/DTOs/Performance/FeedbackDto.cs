using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.DTOs.Performance;
public class FeedbackDto
{
    public int Id { get; set; }
    public int PerformanceReviewId { get; set; }
    public int FromEmployeeId { get; set; }
    public FeedbackType Type { get; set; }
    public string Comments { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
}

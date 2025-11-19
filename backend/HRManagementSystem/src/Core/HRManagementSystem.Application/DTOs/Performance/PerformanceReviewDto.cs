using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class PerformanceReviewDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ReviewCycleId { get; set; }
    public string Status { get; set; } = string.Empty; // Draft/InProgress/Submitted/Closed
    public decimal? FinalRating { get; set; }
}

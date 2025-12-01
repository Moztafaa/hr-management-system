using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.DTOs.Performance;
public class PerformanceReviewDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ReviewCycleId { get; set; }
    public ReviewStatus Status { get; set; }
    public decimal? FinalRating { get; set; }
}

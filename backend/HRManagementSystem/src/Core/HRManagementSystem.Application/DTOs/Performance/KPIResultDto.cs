using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class KPIResultDto
{
    public int Id { get; set; }
    public int PerformanceReviewId { get; set; }
    public int KpiId { get; set; }
    public decimal Actual { get; set; }
    public decimal? WeightedScore { get; set; }
}



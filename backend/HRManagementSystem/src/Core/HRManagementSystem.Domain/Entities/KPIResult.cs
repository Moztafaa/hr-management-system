using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Domain.Entities;
public class KPIResult : BaseEntity
{
    public int PerformanceReviewId { get; set; }
    public int KPIId { get; set; }
    public decimal Actual { get; set; }
    public decimal? WeightedScore { get; set; } 

    public PerformanceReview Review { get; set; } = default!;
    public KPI KPI { get; set; } = default!;
}

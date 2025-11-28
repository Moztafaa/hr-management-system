using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class CreateKpiResultDto
{
    public int PerformanceReviewId { get; set; }
    public int KpiId { get; set; }
    public decimal Actual { get; set; }
}


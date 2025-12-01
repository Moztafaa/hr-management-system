using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class RateCompetencyDto
{
    public int PerformanceReviewId { get; set; }
    public int CompetencyId { get; set; }
    public decimal Rating { get; set; }
    public string? Notes { get; set; }
}


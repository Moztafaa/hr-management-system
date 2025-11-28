using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class PerformanceReportDto
{
    public int ReviewId { get; set; }
    public decimal? FinalRating { get; set; }

    public IReadOnlyList<GoalDto> Goals { get; set; } = Array.Empty<GoalDto>();
    public IReadOnlyList<KPIResultDto> KpiResults { get; set; } = Array.Empty<KPIResultDto>();
    public IReadOnlyList<EmployeeCompetencyRatingDto> CompetencyRatings { get; set; } = Array.Empty<EmployeeCompetencyRatingDto>();
    public IReadOnlyList<FeedbackDto> Feedbacks { get; set; } = Array.Empty<FeedbackDto>();
}

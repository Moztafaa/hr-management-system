using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Domain.Entities;
public class PerformanceReview : BaseEntity
{
    public int EmployeeId { get; set; }
    public int ReviewCycleId { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.Draft;
    public decimal? FinalRating { get; set; } 

    public Employee Employee { get; set; } = default!;
    public ReviewCycle ReviewCycle { get; set; } = default!;

    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public ICollection<EmployeeCompetencyRating> CompetencyRatings { get; set; } = new List<EmployeeCompetencyRating>();
    public ICollection<KPIResult> KPIResults { get; set; } = new List<KPIResult>();
}

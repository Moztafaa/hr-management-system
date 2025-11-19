using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Domain.Entities;
public class EmployeeCompetencyRating : BaseEntity
{
    public int PerformanceReviewId { get; set; }
    public int CompetencyId { get; set; }
    public decimal Rating { get; set; } 
    public string? Notes { get; set; }

    public PerformanceReview Review { get; set; } = default!;
    public Competency Competency { get; set; } = default!;
}


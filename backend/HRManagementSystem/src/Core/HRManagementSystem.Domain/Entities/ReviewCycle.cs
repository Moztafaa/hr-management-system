using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Domain.Entities;
public class ReviewCycle : BaseEntity
{
    public string Name { get; set; } = default!;
    public CycleFrequency Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public RatingScaleType RatingScale { get; set; } = RatingScaleType.OneToFive;

    public ICollection<PerformanceReview> Reviews { get; set; } = new List<PerformanceReview>();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class EmployeeCompetencyRatingRepository(ApplicationDbContext _ctx) : Repository<EmployeeCompetencyRating>(_ctx), IEmployeeCompetencyRatingRepository
{

    public Task<IEnumerable<EmployeeCompetencyRating>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<EmployeeCompetencyRating>>(_ctx.EmployeeCompetencyRatings.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());
}


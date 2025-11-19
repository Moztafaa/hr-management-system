using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class EmployeeCompetencyRatingRepository : Repository<EmployeeCompetencyRating>, IEmployeeCompetencyRatingRepository
{
    private readonly ApplicationDbContext _ctx;
    public EmployeeCompetencyRatingRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;
    public Task<IEnumerable<EmployeeCompetencyRating>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<EmployeeCompetencyRating>>(_ctx.EmployeeCompetencyRatings.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());
}


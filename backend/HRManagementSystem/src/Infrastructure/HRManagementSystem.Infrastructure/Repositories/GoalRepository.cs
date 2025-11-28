using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class GoalRepository(ApplicationDbContext _ctx) : Repository<Goal>(_ctx), IGoalRepository
{
    public async Task<IEnumerable<Goal>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => await _ctx.Goals.Where(g => g.PerformanceReviewId == reviewId).ToListAsync(ct);
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class GoalRepository : Repository<Goal>, IGoalRepository
{
    private readonly ApplicationDbContext _ctx;
    public GoalRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

    public async Task<IEnumerable<Goal>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => await _ctx.Goals.Where(g => g.PerformanceReviewId == reviewId).ToListAsync(ct);
}


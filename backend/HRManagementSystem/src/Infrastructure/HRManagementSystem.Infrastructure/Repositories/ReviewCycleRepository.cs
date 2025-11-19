using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class ReviewCycleRepository : Repository<ReviewCycle>, IReviewCycleRepository
{
    private readonly ApplicationDbContext _ctx;
    public ReviewCycleRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

    public async Task<IEnumerable<ReviewCycle>> GetActiveAtAsync(DateTime at, CancellationToken ct = default)
    {
        return await _ctx.ReviewCycles
            .Where(c => c.StartDate <= at && c.EndDate >= at)
            .ToListAsync(ct);
    }
}

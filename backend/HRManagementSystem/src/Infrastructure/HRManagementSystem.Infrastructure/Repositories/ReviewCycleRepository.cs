using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class ReviewCycleRepository(ApplicationDbContext _ctx) : Repository<ReviewCycle>(_ctx), IReviewCycleRepository
{
    public async Task<IEnumerable<ReviewCycle>> GetActiveAtAsync(DateTime at, CancellationToken ct = default)
    {
        return await _ctx.ReviewCycles
            .Where(c => c.StartDate <= at && c.EndDate >= at)
            .ToListAsync(ct);
    }
}

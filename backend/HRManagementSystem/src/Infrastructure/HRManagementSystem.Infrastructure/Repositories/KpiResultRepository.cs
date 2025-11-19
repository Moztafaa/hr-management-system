using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class KpiResultRepository : Repository<KPIResult>, IKpiResultRepository
{
    private readonly ApplicationDbContext _ctx;
    public KpiResultRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;
    public Task<IEnumerable<KPIResult>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<KPIResult>>(_ctx.KPIResults.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());
}


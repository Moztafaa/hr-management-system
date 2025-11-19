using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class KpiResultRepository(ApplicationDbContext _ctx) : Repository<KPIResult>(_ctx), IKpiResultRepository
{
    public Task<IEnumerable<KPIResult>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<KPIResult>>(_ctx.KPIResults.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class KpiRepository : Repository<KPI>, IKpiRepository
{
    private readonly ApplicationDbContext _ctx;
    public KpiRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;
    public Task<KPI?> GetByCodeAsync(string code, CancellationToken ct = default)
        => _ctx.KPIs.FirstOrDefaultAsync(x => x.Code == code, ct);
}


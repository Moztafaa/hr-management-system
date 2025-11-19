using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class PerformanceReviewRepository : Repository<PerformanceReview>, IPerformanceReviewRepository
{
    private readonly ApplicationDbContext _ctx;
    public PerformanceReviewRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

    public async Task<PerformanceReview?> GetWithDetailsAsync(int id, CancellationToken ct = default)
    {
        return await _ctx.PerformanceReviews
            .Include(r => r.Goals)
            .Include(r => r.KPIResults).ThenInclude(k => k.KPI)
            .Include(r => r.CompetencyRatings).ThenInclude(c => c.Competency)
            .Include(r => r.Feedbacks)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<IEnumerable<PerformanceReview>> GetByEmployeeAsync(int employeeId, CancellationToken ct = default)
    {
        return await _ctx.PerformanceReviews
            .Where(r => r.EmployeeId == employeeId)
            .ToListAsync(ct);
    }
}

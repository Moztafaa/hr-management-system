using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class PerformanceReviewRepository(ApplicationDbContext _ctx) : Repository<PerformanceReview>(_ctx), IPerformanceReviewRepository
{

    public async Task<IEnumerable<PerformanceReview>> GetByEmployeeAsync(int employeeId, CancellationToken ct = default)
    {
        return await _ctx.PerformanceReviews
            .Where(r => r.EmployeeId == employeeId)
            .ToListAsync(ct);
    }
}

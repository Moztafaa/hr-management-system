using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class CompetencyRepository : Repository<Competency>, ICompetencyRepository
{
    private readonly ApplicationDbContext _ctx;
    public CompetencyRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;
    public Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => _ctx.Competencies.AnyAsync(x => x.Name == name, ct);
}


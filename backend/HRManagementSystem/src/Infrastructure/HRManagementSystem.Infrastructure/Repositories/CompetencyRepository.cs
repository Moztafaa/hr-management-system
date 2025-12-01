namespace HRManagementSystem.Infrastructure.Repositories;
public class CompetencyRepository(ApplicationDbContext _ctx) : Repository<Competency>(_ctx), ICompetencyRepository
{
 
    public Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => _ctx.Competencies.AnyAsync(x => x.Name == name, ct);
}


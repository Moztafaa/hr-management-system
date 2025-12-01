namespace HRManagementSystem.Infrastructure.Repositories;
public class TrainingRequestRepository(ApplicationDbContext context) : Repository<TrainingRequest>(context), ITrainingRequestRepository
{
    public override async Task<IEnumerable<TrainingRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TrainingRequests
            .Include(r => r.Employee)
            .Include(r => r.TrainingCourse)
            .Include(r => r.Reviewer)
            .ToListAsync(cancellationToken);
    }
    public async Task<bool> IsDuplicateRequestAsync(int employeeId, int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.TrainingRequests
            .AnyAsync(r => r.EmployeeId == employeeId
                           && r.TrainingCourseId == courseId
                           && r.Status == TrainingRequestStatus.Pending,
                      cancellationToken);
    }

    public async Task<IEnumerable<TrainingRequest>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.TrainingRequests
            .Where(r => r.EmployeeId == employeeId)
            .Include(r => r.TrainingCourse)
            .Include(r => r.Employee)
            .Include(r => r.Reviewer)
            .OrderByDescending(r => r.RequestDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TrainingRequest>> GetByStatusAsync(TrainingRequestStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.TrainingRequests
            .Where(r => r.Status == status)
            .Include(r => r.Employee)
            .Include(r => r.TrainingCourse)
            .Include(r => r.Reviewer)
            .OrderByDescending(r => r.RequestDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<TrainingRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TrainingRequests
            .Include(r => r.Employee)
            .Include(r => r.TrainingCourse)
            .Include(r => r.Reviewer)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}

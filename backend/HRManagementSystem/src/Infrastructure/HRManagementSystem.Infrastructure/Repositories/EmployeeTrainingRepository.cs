namespace HRManagementSystem.Infrastructure.Repositories;
public class EmployeeTrainingRepository(ApplicationDbContext context) : Repository<EmployeeTraining>(context), IEmployeeTrainingRepository
{
    public async Task<bool> IsEmployeeEnrolledAsync(int employeeId, int courseId, CancellationToken cancellationToken = default) => await _context.EmployeeTrainings
            .AnyAsync(et => et.EmployeeId == employeeId && et.TrainingCourseId == courseId, cancellationToken);

    public async Task<EmployeeTraining?> GetByEmployeeAndCourseAsync(int employeeId, int trainingCourseId, CancellationToken cancellationToken = default) => await _context.EmployeeTrainings
            .Include(et => et.Employee)
            .Include(et => et.TrainingCourse)
            .FirstOrDefaultAsync(et => et.EmployeeId == employeeId && et.TrainingCourseId == trainingCourseId, cancellationToken);


    public async Task<IEnumerable<EmployeeTraining>> GetEnrollmentsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default) => await _context.EmployeeTrainings
            .Where(et => et.EmployeeId == employeeId)
            .Include(et => et.TrainingCourse)
            .Include(et => et.Employee)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<EmployeeTraining>> GetEnrollmentsByCourseAsync(int courseId, CancellationToken cancellationToken = default) => await _context.EmployeeTrainings
            .Where(et => et.TrainingCourseId == courseId)
            .Include(et => et.Employee)
            .Include(et => et.TrainingCourse)
            .ToListAsync(cancellationToken);
}

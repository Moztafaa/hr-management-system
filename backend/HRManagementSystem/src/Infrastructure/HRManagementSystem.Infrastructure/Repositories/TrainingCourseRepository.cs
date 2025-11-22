namespace HRManagementSystem.Infrastructure.Repositories;
public class TrainingCourseRepository(ApplicationDbContext context) : Repository<TrainingCourse>(context), ITrainingCourseRepository
{
    public async Task<bool> ExistsWithTitleAsync(string title, CancellationToken cancellationToken = default) => await _context.TrainingCourses
            .AnyAsync(tc => tc.Title == title, cancellationToken);
}

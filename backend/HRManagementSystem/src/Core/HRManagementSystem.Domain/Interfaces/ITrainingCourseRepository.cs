using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface ITrainingCourseRepository : IRepository<TrainingCourse>
{
    Task<bool> ExistsWithTitleAsync(string title, CancellationToken cancellationToken = default);
}

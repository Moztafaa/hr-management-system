using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IEmployeeTrainingRepository : IRepository<EmployeeTraining>
{
    Task<bool> IsEmployeeEnrolledAsync(int employeeId, int courseId, CancellationToken cancellationToken = default);
    Task<EmployeeTraining?> GetByEmployeeAndCourseAsync(int employeeId, int trainingCourseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeTraining>> GetEnrollmentsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeTraining>> GetEnrollmentsByCourseAsync(int courseId, CancellationToken cancellationToken = default);
    
}

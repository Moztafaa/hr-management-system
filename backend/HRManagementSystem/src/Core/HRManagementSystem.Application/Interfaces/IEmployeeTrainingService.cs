namespace HRManagementSystem.Application.Interfaces;
public interface IEmployeeTrainingService
{
    Task<Response<EmployeeTrainingDto>> EnrollAsync(EmployeeEnrollDto dto,CancellationToken cancellationToken = default);
    Task<Response<bool>> CancelAsync(int employeeId, int courseId, CancellationToken cancellationToken = default);
    Task<Response<bool>> CompleteAsync(int employeeId, int courseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeTrainingDto>> GetEnrollmentsByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeTrainingDto>> GetEnrollmentsByCourseAsync(int courseId, CancellationToken cancellationToken = default);
}

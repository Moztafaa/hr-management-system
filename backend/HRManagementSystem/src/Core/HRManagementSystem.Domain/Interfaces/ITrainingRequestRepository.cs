using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Domain.Interfaces;
public interface ITrainingRequestRepository : IRepository<TrainingRequest>
{
    Task<bool> IsDuplicateRequestAsync(int employeeId, int courseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrainingRequest>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrainingRequest>> GetByStatusAsync(TrainingRequestStatus status, CancellationToken cancellationToken = default);
    Task<TrainingRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken cancellationToken = default);
}

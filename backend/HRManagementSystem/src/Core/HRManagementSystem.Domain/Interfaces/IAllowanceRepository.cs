using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IAllowanceRepository: IRepository<Allowance>
{
    Task<IEnumerable<Allowance>> GetByMinValueAsync(decimal minValue, CancellationToken cancellationToken = default);
    Task<Allowance?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

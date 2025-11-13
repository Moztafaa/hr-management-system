using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IAllowanceRepository: IRepository<Allowance>
{
    Task<IEnumerable<Allowance>> GetByMinAmountAsync(decimal minAmount, CancellationToken cancellationToken = default);
    Task<Allowance?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IDeductionRepository:IRepository<Deduction>
{
    Task<IEnumerable<Deduction>> GetByMinAmountAsync(decimal minAmount, CancellationToken cancellationToken = default);
    Task<Deduction?> GetByNameAsync(string name , CancellationToken cancellationToken = default);

}

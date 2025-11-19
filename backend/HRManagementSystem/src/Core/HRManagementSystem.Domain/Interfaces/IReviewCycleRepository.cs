using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IReviewCycleRepository : IRepository<ReviewCycle>
{
    Task<IEnumerable<ReviewCycle>> GetActiveAtAsync(DateTime at, CancellationToken ct = default);
}

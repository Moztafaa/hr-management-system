using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IPerformanceReviewRepository : IRepository<PerformanceReview>
{
    Task<IEnumerable<PerformanceReview>> GetByEmployeeAsync(int employeeId, CancellationToken ct = default);
}

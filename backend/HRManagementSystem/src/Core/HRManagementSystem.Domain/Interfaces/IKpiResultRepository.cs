using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IKpiResultRepository : IRepository<KPIResult>
{
    Task<IEnumerable<KPIResult>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default);
}

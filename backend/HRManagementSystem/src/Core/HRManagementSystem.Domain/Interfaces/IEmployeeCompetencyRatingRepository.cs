using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IEmployeeCompetencyRatingRepository : IRepository<EmployeeCompetencyRating>
{
    Task<IEnumerable<EmployeeCompetencyRating>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default);
}

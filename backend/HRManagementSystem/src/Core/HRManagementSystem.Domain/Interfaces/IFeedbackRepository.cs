using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Domain.Interfaces;
public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default);
    Task<IEnumerable<Feedback>> GetByReviewIdAndTypeAsync(int reviewId, FeedbackType type, CancellationToken ct = default);
}

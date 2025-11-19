using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Infrastructure.Repositories;
public class FeedbackRepository(ApplicationDbContext _ctx) : Repository<Feedback>(_ctx), IFeedbackRepository
{

    public Task<IEnumerable<Feedback>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<Feedback>>(_ctx.Feedbacks.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());

    public Task<IEnumerable<Feedback>> GetByReviewIdAndTypeAsync(int reviewId, FeedbackType type, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<Feedback>>(_ctx.Feedbacks.Where(x => x.PerformanceReviewId == reviewId && x.Type == type).AsEnumerable());
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Infrastructure.Repositories;
public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
{
    private readonly ApplicationDbContext _ctx;
    public FeedbackRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

    public Task<IEnumerable<Feedback>> GetByReviewIdAsync(int reviewId, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<Feedback>>(_ctx.Feedbacks.Where(x => x.PerformanceReviewId == reviewId).AsEnumerable());

    public Task<IEnumerable<Feedback>> GetByReviewIdAndTypeAsync(int reviewId, FeedbackType type, CancellationToken ct = default)
        => Task.FromResult<IEnumerable<Feedback>>(_ctx.Feedbacks.Where(x => x.PerformanceReviewId == reviewId && x.Type == type).AsEnumerable());
}


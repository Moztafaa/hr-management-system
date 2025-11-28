using System;

namespace HRManagementSystem.Infrastructure.Repositories;

public class NotificationRepository(ApplicationDbContext context) : Repository<Notification>(context), INotificationRepository
{
    public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(n => n.RecipientUserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    public async Task<int> GetUnreadCountByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(n => n.RecipientUserId == userId && !n.IsRead)
            .CountAsync(cancellationToken);
    }
    public async Task<IEnumerable<Notification>> GetUnreadNotificationsByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(n => n.RecipientUserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    public async Task MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default)
    {
        List<Notification> unreadNotifications = await _dbSet
            .Where(n => n.RecipientUserId == userId && !n.IsRead)
            .ToListAsync(cancellationToken);

        foreach (Notification? notification in unreadNotifications)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
        }

        if (unreadNotifications.Count != 0)
        {
            _dbSet.UpdateRange(unreadNotifications);
        }

    }
    public async Task MarkAsReadAsync(int notificationId, CancellationToken cancellationToken = default)
    {
        Notification? notification = await GetByIdAsync(notificationId, cancellationToken);
        if (notification != null && !notification.IsRead)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            await UpdateAsync(notification, cancellationToken);
        }
    }
}

using System;
using HRManagementSystem.Application.DTOs.Notification;

namespace HRManagementSystem.Application.Interfaces;

public interface INotificationService
{
    Task<Response<NotificationDto>> CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<NotificationDto>>> GetAllNotificationsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<NotificationDto>>> GetUnreadNotificationsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Response<int>> GetUnreadCountAsync(string userId, CancellationToken cancellationToken = default);
    Task<Response<NotificationDto>> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<bool>> MarkAsReadAsync(int notificationId, CancellationToken cancellationToken = default);
    Task<Response<bool>> MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteNotificationAsync(int id, CancellationToken cancellationToken = default);
}

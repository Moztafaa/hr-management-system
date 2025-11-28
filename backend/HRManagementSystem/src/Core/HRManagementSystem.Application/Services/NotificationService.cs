using System;
using HRManagementSystem.Application.DTOs.Notification;

namespace HRManagementSystem.Application.Services;

public class NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : INotificationService
{
    public async Task<Response<NotificationDto>> CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            Notification notification = mapper.Map<Notification>(dto);
            await unitOfWork.Notifications.AddAsync(notification, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            NotificationDto notificationDto = mapper.Map<NotificationDto>(notification);
            return new Response<NotificationDto>(notificationDto, string.Empty, false);

        }
        catch (Exception ex)
        {
            return new Response<NotificationDto>(default!, $"An error occurred while creating the notification: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> DeleteNotificationAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Notification? notification = await unitOfWork.Notifications.GetByIdAsync(id, cancellationToken);
            if (notification == null)
            {
                return new Response<bool>(false, "Notification not found.", true);
            }
            await unitOfWork.Notifications.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<bool>(false, $"An error occurred while deleting the notification: {ex.Message}", true);
        }
    }
    public async Task<Response<IEnumerable<NotificationDto>>> GetAllNotificationsAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Notification> notifications = await unitOfWork.Notifications.GetNotificationsByUserAsync(userId, cancellationToken);
            IEnumerable<NotificationDto> notificationDtos = mapper.Map<IEnumerable<NotificationDto>>(notifications);
            return new Response<IEnumerable<NotificationDto>>(notificationDtos, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<IEnumerable<NotificationDto>>(default!, $"An error occurred while retrieving notifications: {ex.Message}", true);
        }
    }
    public async Task<Response<NotificationDto>> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Notification? notification = await unitOfWork.Notifications.GetByIdAsync(id, cancellationToken);
            if (notification == null)
            {
                return new Response<NotificationDto>(default!, "Notification not found.", true);
            }
            NotificationDto notificationDto = mapper.Map<NotificationDto>(notification);
            return new Response<NotificationDto>(notificationDto, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<NotificationDto>(default!, $"An error occurred while retrieving the notification: {ex.Message}", true);
        }
    }
    public async Task<Response<int>> GetUnreadCountAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            int count = await unitOfWork.Notifications.GetUnreadCountByUserAsync(userId, cancellationToken);
            return new Response<int>(count, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<int>(0, $"An error occurred while retrieving unread notification count: {ex.Message}", true);
        }
    }
    public async Task<Response<IEnumerable<NotificationDto>>> GetUnreadNotificationsAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Notification> notifications = await unitOfWork.Notifications.GetUnreadNotificationsByUserAsync(userId, cancellationToken);
            IEnumerable<NotificationDto> notificationDtos = mapper.Map<IEnumerable<NotificationDto>>(notifications);
            return new Response<IEnumerable<NotificationDto>>(notificationDtos, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<IEnumerable<NotificationDto>>(default!, $"An error occurred while retrieving unread notifications: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            await unitOfWork.Notifications.MarkAllAsReadAsync(userId, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<bool>(false, $"An error occurred while marking all notifications as read: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> MarkAsReadAsync(int notificationId, CancellationToken cancellationToken = default)
    {
        try
        {
            await unitOfWork.Notifications.MarkAsReadAsync(notificationId, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (System.Exception ex)
        {
            return new Response<bool>(false, $"An error occurred while marking the notification as read: {ex.Message}", true);
        }
    }
}

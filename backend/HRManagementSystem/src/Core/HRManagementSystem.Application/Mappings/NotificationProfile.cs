using AutoMapper;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

        CreateMap<CreateNotificationDto, Notification>();
    }
}

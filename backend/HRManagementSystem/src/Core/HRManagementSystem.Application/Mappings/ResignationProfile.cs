using AutoMapper;
using HRManagementSystem.Application.DTOs.Resignation;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class ResignationProfile : Profile
{
    public ResignationProfile()
    {
        // Resignation
        CreateMap<Resignation, ResignationDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src =>
                src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : null))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                src.Employee != null && src.Employee.Department != null ? src.Employee.Department.Name : null))
            .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src =>
                src.Employee != null && src.Employee.Designation != null ? src.Employee.Designation.Title : null))
            .ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src =>
                src.Reviewer != null ? $"{src.Reviewer.FirstName} {src.Reviewer.LastName}" : null));

        CreateMap<CreateResignationDto, Resignation>()
            .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.NoticePeriodDays, opt => opt.MapFrom(src =>
                src.IsImmediateResignation ? 0 : (int)(src.LastWorkingDate - DateTime.UtcNow.Date).TotalDays));

        // ResignationApproval
        CreateMap<ResignationApproval, ResignationApprovalDto>()
            .ForMember(dest => dest.ApproverName, opt => opt.MapFrom(src =>
                src.Approver != null ? $"{src.Approver.FirstName} {src.Approver.LastName}" : null));
    }
}

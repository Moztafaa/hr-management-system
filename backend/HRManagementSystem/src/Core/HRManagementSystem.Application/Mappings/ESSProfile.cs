using AutoMapper;
using HRManagementSystem.Application.DTOs.ESS;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class ESSProfile : Profile
{
    public ESSProfile()
    {
        CreateMap<Employee, ESSProfileDto>()
            .ForMember(dest => dest.DeptId, opt => opt.MapFrom(src => src.DepartmentId ?? 0))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId ?? 0))
            .ForMember(dest => dest.DepartmentName, opt => opt.Ignore())
            .ForMember(dest => dest.DesignationName, opt => opt.Ignore());

        CreateMap<UpdateESSProfileDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.Ignore())
            .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.HireDate, opt => opt.Ignore())
            .ForMember(dest => dest.EFF_Start, opt => opt.Ignore())
            .ForMember(dest => dest.EFF_End, opt => opt.Ignore())
            .ForMember(dest => dest.BasicSalary, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore())
            .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
            .ForMember(dest => dest.DesignationId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Designation, opt => opt.Ignore())
            .ForMember(dest => dest.Payslips, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeAllowances, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeDeductions, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveRequests, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveApprovals, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveBalances, opt => opt.Ignore());
    }
}

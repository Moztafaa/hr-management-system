using HRManagementSystem.Application.DTOs;
using HRManagementSystem.Application.DTOs.Account;
using HRManagementSystem.Application.DTOs.Employee;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DeptId, opt => opt.MapFrom(src => src.DepartmentId ?? 0))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId ?? 0));
        
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DeptId))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId));
        
        CreateMap<CreateEmployeeDto, Employee>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DeptId))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId));
        
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DeptId))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId));
        
        CreateMap<RegisterEmployeeDto, Employee>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DeptId))
            .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId));
        
        CreateMap<Employee, EmployeeSummaryDto>();
    }
}

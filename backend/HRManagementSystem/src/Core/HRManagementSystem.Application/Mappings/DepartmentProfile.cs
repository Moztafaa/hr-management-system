using HRManagementSystem.Application.DTOs.Department;
using HRManagementSystem.Application.DTOs.Employee;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<CreateDepartmentDto, Department>();
        CreateMap<UpdateDepartmentDto, Department>();
        CreateMap<Department, DepartmentDetailDto>();
        CreateMap<Department, DepartmentWithEmployeesDto>();
        CreateMap<Employee, EmployeeSummaryDto>();
    }
}

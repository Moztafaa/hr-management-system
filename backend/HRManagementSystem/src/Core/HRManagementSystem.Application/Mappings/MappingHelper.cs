namespace HRManagementSystem.Application.Mappings;

/// <summary>
/// Manual mapping helper - Replace with your actual mapping logic
/// You can install AutoMapper later if needed
/// </summary>
public class MappingHelper:Profile
{
    // Example: Map entity to DTO manually
    // public static SampleDto ToDto(this SampleEntity entity)
    // {
    //     return new SampleDto
    //     {
    //         Id = entity.Id,
    //         Name = entity.Name
    //     };
    // }
    public MappingHelper()
    {
        // Define your mappings here
        CreateMap<Designation, CreateDesignationDto>().ReverseMap();
        CreateMap<Designation, UpdateDesignationDto>().ReverseMap();
        CreateMap<Designation, DesignationDto>().ReverseMap();
        CreateMap<Designation, DesignationDetailsDto>().ReverseMap();
        CreateMap<Designation, DesignationWithEmployeesDto>().ReverseMap();

        //CreateMap<Payslip, CreatePayslipDto>().ReverseMap();
        //CreateMap<Payslip, PayslipDto>()
        //    .ForMember(dest => dest.EmployeeName,
        //        opt => opt.MapFrom(src =>
        //            src.Employee.FirstName + " " + src.Employee.LastName))

        //    .ForMember(dest => dest.BasicSalary,
        //        opt => opt.MapFrom(src => src.Employee.BasicSalary))

        //    .ForMember(dest => dest.Allowances,
        //        opt => opt.MapFrom(src =>
        //            src.Employee.EmployeeAllowances.Select(ea => ea.Allowance)))

        //    .ForMember(dest => dest.Deductions,
        //        opt => opt.MapFrom(src =>
        //            src.Employee.EmployeeDeductions.Select(ed => ed.Deduction)));

        //Leave Request
        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveBalanceDto, EmployeeLeaveBalance>().ReverseMap();
        CreateMap<LeaveRequestDto, UpdateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveApproval, LeaveApprovalDto>().ReverseMap();
    }
}

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

        //Leave Request
        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveBalanceDto, EmployeeLeaveBalance>().ReverseMap();
        CreateMap<LeaveRequestDto, UpdateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveApproval, LeaveApprovalDto>().ReverseMap();




    }
}

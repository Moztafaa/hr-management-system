using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Leaves;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveTypeDtos;

namespace HRManagementSystem.Application.Mappings;
public class LeaveProfile : Profile
{
    public LeaveProfile()
    {
        // LeaveType
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<CreateLeaveTypeDto, LeaveType>();
        CreateMap<UpdateLeaveTypeDto, LeaveType>();

        // LeaveRequest
        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<CreateLeaveRequestDto, LeaveRequest>();

        // EmployeeLeaveBalance
        CreateMap<EmployeeLeaveBalance, EmployeeLeaveBalanceDto>().ReverseMap();
    }
}

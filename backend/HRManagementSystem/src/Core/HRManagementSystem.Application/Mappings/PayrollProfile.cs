namespace HRManagementSystem.Application.Mappings;

public class PayrollProfile : Profile
{
    public PayrollProfile()
    {
        // Allowance
        CreateMap<Allowance, AllowanceDto>().ReverseMap();
        CreateMap<Allowance, CreateAllowanceDto>().ReverseMap();
        CreateMap<Allowance, UpdateAllowanceDto>().ReverseMap();

        // Deduction
        CreateMap<Deduction, DeductionDto>().ReverseMap();
        CreateMap<Deduction, CreateDeductionDto>().ReverseMap();
        CreateMap<Deduction, UpdateDeductionDto>().ReverseMap();

        // Employee Allowance
        CreateMap<EmployeeAllowance, EmployeeAllowanceDto>().ReverseMap();
        CreateMap<EmployeeAllowance, CreateEmployeeAllowanceDto>().ReverseMap();
        CreateMap<EmployeeAllowance, UpdateEmployeeAllowanceDto>().ReverseMap();

        // Employee Deduction
        CreateMap<EmployeeDeduction, EmployeeDeductionDto>().ReverseMap();
        CreateMap<EmployeeDeduction, CreateEmployeeDeductionDto>().ReverseMap();
        CreateMap<EmployeeDeduction, UpdateEmployeeDeductionDto>().ReverseMap();

        // Payslip
        CreateMap<Payslip, CreatePayslipDto>().ReverseMap();
        CreateMap<Payslip, PayslipDto>()
            .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
            .ForMember(dest => dest.BasicSalary,
                opt => opt.MapFrom(src => src.Employee.BasicSalary))
            .ForMember(dest => dest.Allowances,
                opt => opt.MapFrom(src =>
                       src.Employee.EmployeeAllowances.Select(ea => new AllowanceDto
                       {
                           Id = ea.AllowanceId,
                           Name = ea.Allowance.Name,
                           IsPercentage = ea.Allowance.IsPercentage,
                           Value = ea.Amount.HasValue ? ea.Amount.Value : (ea.Allowance != null ? ea.Allowance.Value : 0m)
                       })))
            .ForMember(dest => dest.Deductions,
                opt => opt.MapFrom(src =>
                      src.Employee.EmployeeDeductions.Select(ed => new DeductionDto
                      {
                           Id = ed.DeductionId,
                           Name = ed.Deduction.Name,
                           IsPercentage = ed.Deduction.IsPercentage,
                           IsFixed = ed.Deduction.IsFixed,
                          Value = ed.Amount.HasValue ? ed.Amount.Value : (ed.Deduction != null ? ed.Deduction.Value : 0m)
                      })));

    }
}

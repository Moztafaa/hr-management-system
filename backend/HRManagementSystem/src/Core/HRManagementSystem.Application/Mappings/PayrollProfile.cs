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
        CreateMap<EmployeeAllowance, EmployeeAllowanceWithDetailsDto>()
                .ForMember(dest => dest.OverrideAmount,
                    opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OverrideIsPercentage,
                    opt => opt.MapFrom(src => src.IsPercentage))
                .ForMember(dest => dest.AllowanceName,
                    opt => opt.MapFrom(src => src.Allowance.Name))
                .ForMember(dest => dest.AllowanceDefaultAmount,
                    opt => opt.MapFrom(src => src.Allowance.Amount))
                .ForMember(dest => dest.AllowanceDefaultIsPercentage,
                    opt => opt.MapFrom(src => src.Allowance.IsPercentage))
                .ForMember(dest => dest.EffectiveAmount,
                    opt => opt.MapFrom(src => src.Amount ?? src.Allowance.Amount))
                .ForMember(dest => dest.EffectiveIsPercentage,
                    opt => opt.MapFrom(src => src.IsPercentage ?? src.Allowance.IsPercentage));

        // Employee Deduction
        CreateMap<EmployeeDeduction, EmployeeDeductionDto>().ReverseMap();
        CreateMap<EmployeeDeduction, CreateEmployeeDeductionDto>().ReverseMap();
        CreateMap<EmployeeDeduction, UpdateEmployeeDeductionDto>().ReverseMap();
        CreateMap<EmployeeDeduction, EmployeeDeductionWithDetailsDto>()
                .ForMember(dest => dest.OverrideAmount,
                    opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OverrideIsPercentage,
                    opt => opt.MapFrom(src => src.IsPercentage))
                .ForMember(dest => dest.DeductionName,
                    opt => opt.MapFrom(src => src.Deduction.Name))
                .ForMember(dest => dest.DeductionDefaultAmount,
                    opt => opt.MapFrom(src => src.Deduction.Amount))
                .ForMember(dest => dest.DeductionDefaultIsPercentage,
                    opt => opt.MapFrom(src => src.Deduction.IsPercentage))
                .ForMember(dest => dest.EffectiveAmount,
                    opt => opt.MapFrom(src => src.Amount ?? src.Deduction.Amount))
                .ForMember(dest => dest.EffectiveIsPercentage,
                    opt => opt.MapFrom(src => src.IsPercentage ?? src.Deduction.IsPercentage));

        // Payslip
        CreateMap<Payslip, CreatePayslipDto>().ReverseMap();

        CreateMap<Payslip, UpdatePayslipDto>().ReverseMap();
        CreateMap<Payslip, PayslipDto>()
            .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src =>
                    src.Employee.FirstName + " " + src.Employee.LastName))

            .ForMember(dest => dest.BasicSalary,
                opt => opt.MapFrom(src => src.Employee.BasicSalary))

            .ForMember(dest => dest.Allowances,
                opt => opt.MapFrom(src =>
                    src.Employee.EmployeeAllowances.Select(ea => ea.Allowance)))

            .ForMember(dest => dest.Deductions,
                opt => opt.MapFrom(src =>
                    src.Employee.EmployeeDeductions.Select(ed => ed.Deduction)));

    }
}

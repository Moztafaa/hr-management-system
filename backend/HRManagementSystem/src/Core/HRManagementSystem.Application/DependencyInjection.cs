using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Application.Mappings;

namespace HRManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDesignationService, DesignationService>();

        
        services.AddScoped<IPayslipService, PayslipService>();

        services.AddScoped<IAllowanceService, AllowanceService>();
        services.AddScoped<IDeductionService, DeductionService>();
        services.AddScoped<IEmployeeAllowanceService, EmployeeAllowanceService>();
        services.AddScoped<IEmployeeDeductionService, EmployeeDeductionService>();

        services.AddAutoMapper(x => x.AddProfile(new MappingHelper()));

        return services;
    }
}

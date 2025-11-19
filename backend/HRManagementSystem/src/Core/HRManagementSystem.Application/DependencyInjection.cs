
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
        services.AddScoped<IESSService, ESSService>();



        services.AddScoped<IPayslipService, PayslipService>();

        services.AddScoped<IAllowanceService, AllowanceService>();
        services.AddScoped<IDeductionService, DeductionService>();
        services.AddScoped<IEmployeeAllowanceService, EmployeeAllowanceService>();
        services.AddScoped<IEmployeeDeductionService, EmployeeDeductionService>();

        services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
        services.AddScoped<ILeaveApprovalService, LeaveApprovalService>();
        services.AddScoped<IJobPostingService, JobPostingService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
        services.AddScoped<INotificationService, NotificationService>();

        // services.AddScoped<ILeaveService, LeaveService>();

        services.AddAutoMapper(x => x.AddProfile(new MappingHelper()));
        services.AddAutoMapper(x => x.AddProfile(new LeaveProfile()));
        // Add other mapping profiles for Department, Employee, JobPosting, etc.
        services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
        services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));
        services.AddAutoMapper(x => x.AddProfile(new JobPostingProfile()));
        services.AddAutoMapper(x => x.AddProfile(new CandidateProfile()));
        services.AddAutoMapper(x => x.AddProfile(new JobApplicationProfile()));
        services.AddAutoMapper(x => x.AddProfile(new NotificationProfile()));
        services.AddAutoMapper(x => x.AddProfile(new ESSProfile()));


        return services;
    }
}

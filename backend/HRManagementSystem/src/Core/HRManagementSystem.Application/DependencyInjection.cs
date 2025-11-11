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

        services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
        services.AddScoped<ILeaveApprovalService, LeaveApprovalService>();
        services.AddScoped<IJobPostingService, JobPostingService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();

        // services.AddScoped<ILeaveService, LeaveService>();
        services.AddAutoMapper(x => x.AddProfile(new MappingHelper()));
        services.AddAutoMapper(x => x.AddProfile(new LeaveProfile()));
        // Add other mapping profiles for Department, Employee, JobPosting, etc.
        services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
        services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));
        services.AddAutoMapper(x => x.AddProfile(new JobPostingProfile()));
        services.AddAutoMapper(x => x.AddProfile(new CandidateProfile()));
        services.AddAutoMapper(x => x.AddProfile(new JobApplicationProfile()));


        return services;
    }
}

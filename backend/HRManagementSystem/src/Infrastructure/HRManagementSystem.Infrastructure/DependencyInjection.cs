using HRManagementSystem.Domain.Interfaces.LeaveRepository;
using HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
using HRManagementSystem.Infrastructure.Common;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Infrastructure.Services;

namespace HRManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //services.ConfigureApplicationCookie(options =>
        //{
        //    options.LoginPath = "/Account/Login";
        //    options.AccessDeniedPath = "/Account/AccessDenied";
        //});

        // Add JWT Authentication
        IConfigurationSection jwtSettings = configuration.GetSection("Jwt");
        byte[] key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        // Configure Email Settings
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        // Register Email Service
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<JwtTokenGenerator>();
        // Register AccountService
        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDesignationRepository, DesignationRepository>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        services.AddScoped<IPayslipRepository, PayslipRepository>();
        services.AddScoped<IAllowanceRepository, AllowanceRepository>();
        services.AddScoped<IDeductionRepository, DeductionRepository>();
        services.AddScoped<IEmployeeAllowanceRepository, EmployeeAllowanceRepository>();
        services.AddScoped<IEmployeeDeductionRepository, EmployeeDeductionRepository>();

        services.AddScoped<ILeaveApprovalRepository, LeaveApprovalRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<IEmployeeLeaveBalanceRepository, EmployeeLeaveBalanceRepository>();
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<IJobPostingRepository, JobPostingRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
        services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
        services.AddScoped<IReviewCycleRepository, ReviewCycleRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IKpiRepository, KpiRepository>();
        services.AddScoped<IKpiResultRepository, KpiResultRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddScoped<IEmployeeCompetencyRatingRepository, EmployeeCompetencyRatingRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

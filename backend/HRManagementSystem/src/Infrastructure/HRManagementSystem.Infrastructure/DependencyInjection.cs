using Hangfire;
using Hangfire.SqlServer;
using HRManagementSystem.Domain.Interfaces.LeaveRepository;
using HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
using HRManagementSystem.Infrastructure.Common;
using HRManagementSystem.Infrastructure.Services;
using HRManagementSystem.Infrastructure.Jobs;

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
        // Use stub face recognition service (native Dlib not available on Linux)
        // To enable real face recognition, install native libraries and use FaceRecognitionService
        //services.AddScoped<IFaceRecognitionService, StubFaceRecognitionService>();

         services.AddScoped<IFaceRecognitionService, FaceRecognitionService>(); // Uncomment to use real service

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
        services.AddScoped<IReportingRepository, ReportingRepository>();

        services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
        services.AddScoped<IEmployeeTrainingRepository, EmployeeTrainingRepository>();
        services.AddScoped<ITrainingRequestRepository, TrainingRequestRepository>();

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

        // Configure Hangfire with SQL Server storage
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
                PrepareSchemaIfNecessary = true,
                SchemaName = "Hangfire"
            }));

        // Add Hangfire server for processing jobs
        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Environment.ProcessorCount * 2;
            options.Queues = new[] { "critical", "default", "low" };
        });

        // Register Background Job Service
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();

        // Register Background Jobs
        services.AddScoped<IAttendanceReminderJob, AttendanceReminderJob>();
        services.AddScoped<IEmailQueueJob, EmailQueueJob>();
        services.AddScoped<IPayslipGenerationJob, PayslipGenerationJob>();
        services.AddScoped<ILeaveBalanceResetJob, LeaveBalanceResetJob>();

        return services;
    }
}

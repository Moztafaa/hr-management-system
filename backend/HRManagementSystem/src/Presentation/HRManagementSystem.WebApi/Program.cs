// using FaceRecognitionDotNet; // Disabled - requires native Dlib libraries for Linux
using Hangfire;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Application.Services;
using HRManagementSystem.WebApi.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Configure Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "HRSystem API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// TODO: Face recognition disabled - requires native Dlib libraries for Linux
// To enable, install: sudo apt-get install libdlib-dev libopenblas-dev liblapack-dev
// var modelPath = Path.Combine(builder.Environment.ContentRootPath, "models");
// Console.WriteLine($"Loading face models from: {modelPath}");
// builder.Services.AddSingleton(sp =>
// {
//     return FaceRecognition.Create(modelPath);
// });

builder.Services.AddScoped<IAccountService, AccountService>();
// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//link with angular
builder.Services.AddCors(options => options.AddPolicy("AllowAngular",
    policy => policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure Hangfire Dashboard
// In production, you should add authorization filter
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "HR Management System - Background Jobs",
    // For development, allow anonymous access
    // In production, use: Authorization = new[] { new HangfireAuthorizationFilter() }
    IsReadOnlyFunc = (context) => false
});

app.UseCors("AllowAngular");

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Register recurring background jobs
RegisterRecurringJobs();

await app.RunAsync();

/// <summary>
/// Registers all recurring background jobs for the HR Management System.
/// </summary>
static void RegisterRecurringJobs()
{
    // Daily check-in reminder at 8:30 AM (Monday to Friday)
    // Cron: "30 8 * * 1-5" = At 08:30, Monday through Friday
    RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
        recurringJobId: "daily-checkin-reminder",
        methodCall: job => job.SendCheckInRemindersAsync(),
        cronExpression: "30 8 * * 1-5",
        options: new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        });

    // Daily check-out reminder at 5:30 PM (Monday to Friday)
    // Cron: "30 17 * * 1-5" = At 17:30, Monday through Friday
    RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
        recurringJobId: "daily-checkout-reminder",
        methodCall: job => job.SendCheckOutRemindersAsync(),
        cronExpression: "30 17 * * 1-5",
        options: new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        });

    // Absent employees summary email at 10:00 AM (Monday to Friday)
    // Cron: "0 10 * * 1-5" = At 10:00, Monday through Friday
    RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
        recurringJobId: "absent-employees-summary",
        methodCall: job => job.SendAbsentEmployeesSummaryAsync(),
        cronExpression: "0 10 * * 1-5",
        options: new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        });
}

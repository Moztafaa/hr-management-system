 using FaceRecognitionDotNet; // Disabled - requires native Dlib libraries for Linux
using HRManagementSystem.Application.Services;
using HRManagementSystem.WebApi.Extensions;
using HRManagementSystem.WebApi.Middleware;
using QuestPDF.Infrastructure;

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
var modelPath = Path.Combine(builder.Environment.ContentRootPath, "models");
Console.WriteLine($"Loading face models from: {modelPath}");
builder.Services.AddSingleton(sp =>
{
    return FaceRecognition.Create(modelPath);
});

builder.Services.AddScoped<IAccountService, AccountService>();
// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//link with angular
builder.Services.AddCors(options => options.AddPolicy("AllowAngular",
    policy => policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

QuestPDF.Settings.License = LicenseType.Community;

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure Hangfire Dashboard and register recurring jobs
app.UseHangfireDashboardWithConfig();

app.UseCors("AllowAngular");

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Register recurring background jobs
app.RegisterHangfireRecurringJobs();

await app.RunAsync();

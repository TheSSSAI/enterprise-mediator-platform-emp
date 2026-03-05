using EnterpriseMediator.ProjectManagement.Application.Behaviors;
using EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.AwardProject;
using EnterpriseMediator.ProjectManagement.Application.Interfaces;
using EnterpriseMediator.ProjectManagement.Domain.Interfaces;
using EnterpriseMediator.ProjectManagement.Domain.Services;
using EnterpriseMediator.ProjectManagement.Infrastructure.Messaging;
using EnterpriseMediator.ProjectManagement.Infrastructure.Persistence;
using EnterpriseMediator.ProjectManagement.Infrastructure.Persistence.Repositories;
using EnterpriseMediator.ProjectManagement.Infrastructure.Services;
using EnterpriseMediator.ProjectManagement.WebAPI.Middleware;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// =================================================================================================
// 1. Service Registration
// =================================================================================================

// Add Services to the container (Controllers)
builder.Services.AddControllers();

// Add API Explorer and Swagger for documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Enterprise Mediator Project Management API",
        Version = "v1",
        Description = "Microservice responsible for Project Lifecycle, SOW Processing, and Vendor Matching."
    });
});

// -------------------------------------------------------------------------------------------------
// Persistence Configuration (Infrastructure Layer)
// -------------------------------------------------------------------------------------------------
// Register Entity Framework Core with PostgreSQL and pgvector support
// REQ-FUNC-014: Vector search requires the vector extension on the connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        // Enable vector extension support for Npgsql
        npgsqlOptions.UseVector();
        
        // Configure resiliency (retries)
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);
    });
});

// -------------------------------------------------------------------------------------------------
// Dependency Injection Wiring (Domain & Infrastructure Layers)
// -------------------------------------------------------------------------------------------------
// Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// Domain Services
// REQ-FUNC-014: Semantic Search Implementation
builder.Services.AddScoped<IVendorMatchingService, VectorVendorMatchingService>();

// Messaging Abstraction
builder.Services.AddScoped<IMessageBus, MassTransitMessageBus>();

// -------------------------------------------------------------------------------------------------
// Application Logic Configuration (Application Layer)
// -------------------------------------------------------------------------------------------------
// Locate the Application Assembly once for scanning
var applicationAssembly = typeof(AwardProjectCommand).Assembly;

// Register MediatR
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(applicationAssembly);
});

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Register Pipeline Behaviors (Validation, Logging, etc.)
// Order matters: Validation should happen before the handler
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// -------------------------------------------------------------------------------------------------
// Messaging Configuration (MassTransit)
// -------------------------------------------------------------------------------------------------
// REQ-FUN-003: Asynchronous Event Publishing
builder.Services.AddMassTransit(x =>
{
    // If we had consumers in this service, they would be added here
    // x.AddConsumers(assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        var rabbitMqUser = builder.Configuration["RabbitMQ:Username"] ?? "guest";
        var rabbitMqPass = builder.Configuration["RabbitMQ:Password"] ?? "guest";

        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPass);
        });

        // Configure endpoints automatically if consumers are added
        cfg.ConfigureEndpoints(context);
    });
});

// -------------------------------------------------------------------------------------------------
// Web API Cross-Cutting Concerns
// -------------------------------------------------------------------------------------------------
// Register Global Exception Handler Middleware
builder.Services.AddTransient<GlobalExceptionHandler>();

// =================================================================================================
// 2. Request Pipeline Configuration
// =================================================================================================

var app = builder.Build();

// Database Migration (ensure schema exists on startup)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
        // Apply pending migrations and create database if not exists
        // Note: In strict production environments, this might be handled by a separate pipeline step
        if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("APPLY_MIGRATIONS") == "true")
        {
            dbContext.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        // Log migration failure but don't crash unless critical
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Global Exception Handling - Must be first in the pipeline to catch downstream errors
app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization (Placeholder for future security integration)
// app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Start the application
app.Run();

// Expose Program class for integration tests
public partial class Program { }
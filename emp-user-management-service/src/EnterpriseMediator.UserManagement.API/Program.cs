using EnterpriseMediator.UserManagement.API.Middleware;
using EnterpriseMediator.UserManagement.Application.Behaviors;
using EnterpriseMediator.UserManagement.Application.Configuration;
using EnterpriseMediator.UserManagement.Application.Features.Clients.Commands.CreateClient;
using EnterpriseMediator.UserManagement.Application.Interfaces;
using EnterpriseMediator.UserManagement.Domain.Interfaces;
using EnterpriseMediator.UserManagement.Infrastructure.Persistence;
using EnterpriseMediator.UserManagement.Infrastructure.Persistence.Repositories;
using EnterpriseMediator.UserManagement.Infrastructure.Services;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// =================================================================================================
// 1. Configuration Bindings
// =================================================================================================
// Bind strictly typed configuration settings for Injection (IOptions<T>)
builder.Services.Configure<UserManagementSettings>(
    builder.Configuration.GetSection("UserManagement"));

// =================================================================================================
// 2. Infrastructure Layer Registration (Persistence & External Services)
// =================================================================================================

// Database Context (PostgreSQL)
builder.Services.AddDbContext<UserDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.MigrationsAssembly("EnterpriseMediator.UserManagement.Infrastructure");
        npgsqlOptions.EnableRetryOnFailure(3);
    });
    
    // Performance optimization for read-heavy scenarios if needed, usually managed per query
    // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
});

// Repositories
// Registering repositories generated in previous dependency levels
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
// Note: ClientRepository omitted as it was not explicitly in the generated file list for Level 6, 
// but would follow the same pattern: builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Infrastructure Services
builder.Services.AddScoped<IAuditServiceAdapter, AuditServiceAdapter>();
builder.Services.AddScoped<DomainEventDispatcher>();

// MassTransit (Message Bus)
builder.Services.AddMassTransit(busConfig =>
{
    busConfig.SetKebabCaseEndpointNameFormatter();

    busConfig.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        var rabbitUser = builder.Configuration["RabbitMQ:Username"] ?? "guest";
        var rabbitPass = builder.Configuration["RabbitMQ:Password"] ?? "guest";
        var virtualHost = builder.Configuration["RabbitMQ:VirtualHost"] ?? "/";

        cfg.Host(rabbitHost, virtualHost, h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// =================================================================================================
// 3. Application Layer Registration (MediatR & Logic)
// =================================================================================================

// Register MediatR: Scans the Application assembly for Handlers
// We reference a known type in the Application assembly to target the correct DLL
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateClientCommand).Assembly);
    
    // Pipeline Behaviors (Order matters)
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(AuditLoggingBehavior<,>));
});

// Register FluentValidation: Scans the Application assembly for Validators
builder.Services.AddValidatorsFromAssembly(typeof(CreateClientCommand).Assembly);

// =================================================================================================
// 4. API Layer Registration (Auth, Controllers, Swagger)
// =================================================================================================

// Authentication (AWS Cognito via JWT Bearer)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var region = builder.Configuration["AWS:Region"];
    var userPoolId = builder.Configuration["AWS:UserPoolId"];
    
    options.Authority = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = false, // Cognito Access Tokens do not always contain the Audience claim standardly
        ValidateIssuerSigningKey = true,
        // Map Cognito groups/roles to ClaimsIdentity Roles if needed
        RoleClaimType = "cognito:groups" 
    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Policy specifically for internal microservice-to-microservice communication
    options.AddPolicy("InternalServicePolicy", policy => 
        policy.RequireClaim("scope", "InternalService"));
        
    // Example role-based policy
    options.AddPolicy("RequireAdmin", policy => 
        policy.RequireRole("SystemAdmin"));
});

// Global Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "EnterpriseMediator User Management API", 
        Version = "v1" 
    });
    
    // Add Security Definition for Swagger UI
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });
});

// =================================================================================================
// 5. Application Pipeline Configuration
// =================================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // In dev, we might want to auto-apply migrations (use with caution)
    // using (var scope = app.Services.CreateScope())
    // {
    //     var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    //     db.Database.Migrate();
    // }
}

// Exception Handler Middleware (must be early in the pipeline)
app.UseExceptionHandler();

app.UseHttpsRedirection();

// Auth Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health Check (Basic)
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }));

app.Run();
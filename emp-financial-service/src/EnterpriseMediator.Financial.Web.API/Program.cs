using System.Reflection;
using System.Text.Json.Serialization;
using EnterpriseMediator.Financial.Application.Common.Behaviors;
using EnterpriseMediator.Financial.Application.Features.Invoices.Commands.GenerateInvoice;
using EnterpriseMediator.Financial.Domain.Interfaces;
using EnterpriseMediator.Financial.Infrastructure.Gateways.Stripe;
using EnterpriseMediator.Financial.Infrastructure.Gateways.Wise;
using EnterpriseMediator.Financial.Infrastructure.Interceptors;
using EnterpriseMediator.Financial.Infrastructure.Messaging;
using EnterpriseMediator.Financial.Infrastructure.Persistence;
using EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations;
using EnterpriseMediator.Financial.Infrastructure.Services;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Logging (Enterprise Grade Structured Logging)
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
                 .Enrich.FromLogContext()
                 .Enrich.WithMachineName()
                 .WriteTo.Console());

// 2. Add Configuration Settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<WiseSettings>(builder.Configuration.GetSection("Wise"));

// 3. Add Database Context
// Using Pooling for performance optimization in high-throughput financial scenarios
builder.Services.AddScoped<ISaveChangesInterceptor, FinancialAuditInterceptor>();
builder.Services.AddDbContextPool<FinancialDbContext>((sp, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.MigrationsAssembly(typeof(FinancialDbContext).Assembly.FullName);
        npgsqlOptions.EnableRetryOnFailure(3);
    });
    
    // Add Audit Interceptor
    options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
});

// 4. Add MediatR and Behaviors (Application Layer)
var applicationAssembly = typeof(GenerateInvoiceCommand).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(applicationAssembly);
    
    // Register Pipeline Behaviors
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(IdempotencyBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionalOutboxBehavior<,>));
});

// 5. Add Validators
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// 6. Add Infrastructure Services and Gateways
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

// Payment and Payout Gateway Adapters
builder.Services.AddScoped<IPaymentGateway, StripePaymentAdapter>();
builder.Services.AddScoped<IPayoutGateway, WisePayoutAdapter>();

// Note: Assuming FinancialRepository implementation exists in Infrastructure as per standard pattern
// If explicit class isn't available in generated list, we register the DbContext as the primary access point 
// or register the interface if a specific implementation was part of the infrastructure plan.
// Given the prompt context, we register the specific adapters which are confirmed present.

// 7. Configure MassTransit (Message Broker)
builder.Services.AddMassTransit(busConfig =>
{
    busConfig.SetKebabCaseEndpointNameFormatter();

    // Register Consumers from Application/Infrastructure assemblies if they exist
    // busConfig.AddConsumers(typeof(ProjectAwardedConsumer).Assembly); 

    busConfig.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqHost = builder.Configuration.GetValue<string>("RabbitMq:Host") ?? "localhost";
        var rabbitMqUser = builder.Configuration.GetValue<string>("RabbitMq:Username") ?? "guest";
        var rabbitMqPass = builder.Configuration.GetValue<string>("RabbitMq:Password") ?? "guest";

        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPass);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// 8. Add API Controllers and JSON Configuration
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 9. Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Enterprise Mediator Financial API", 
        Version = "v1",
        Description = "Microservice for managing Invoices, Payments, Payouts and Ledger."
    });
});

// 10. Add Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<FinancialDbContext>()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

// --- Build the Application ---
var app = builder.Build();

// --- Configure Middleware Pipeline ---

// Enable Request Buffering for Stripe Webhook Signature Verification
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

// Auto-migration on startup (Optional/Dev only strategy - use with caution in Prod)
if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FinancialDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while migrating the database.");
    }
}

try
{
    Log.Information("Starting Enterprise Mediator Financial Service...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

// Make Program public for Integration Tests
public partial class Program { }
# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-GW-API |
| Validation Timestamp | 2025-10-27T12:00:00Z |
| Original Component Count Claimed | 28 |
| Original Component Count Actual | 22 |
| Gaps Identified Count | 6 |
| Components Added Count | 10 |
| Final Component Count | 32 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic BFF Pattern Validation against Microser... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with API Gateway/BFF responsibilities. Correctly abstracts internal microservices.

#### 2.2.1.2 Gaps Identified

- Missing aggregation logic for composite dashboards (Project + Financials)
- Lack of centralized resiliency policy registry for downstream calls
- Missing Correlation ID propagation mechanism for distributed tracing

#### 2.2.1.3 Components Added

- GetProjectDashboardHandler (Aggregation)
- ResiliencyPolicyRegistry
- CorrelationIdMiddleware
- HttpHeaderPropagationHandler

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

95%

#### 2.2.2.3 Missing Requirement Components

- Rate limiting configuration for specific public endpoints
- Structured logging enrichment

#### 2.2.2.4 Added Requirement Components

- RateLimitExtensions
- SerilogConfiguration

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

BFF and Clean Architecture patterns present. Aggregation logic needs explicit handlers.

#### 2.2.3.2 Missing Pattern Components

- Mapping layer between Internal Microservice DTOs and Public API Contracts
- Centralized header propagation

#### 2.2.3.3 Added Pattern Components

- PublicApiMappingProfile
- HeaderPropagationMessageHandler

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - Stateless Gateway

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Controller-Mediator-Client flow valid.

#### 2.2.5.2 Missing Interaction Components

- Parallel execution orchestration for dashboard endpoints

#### 2.2.5.3 Added Interaction Components

- DashboardAggregationService

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-GW-API |
| Technology Stack | ASP.NET Core 8 Web API, MediatR, MassTransit, AWS ... |
| Technology Guidance Integration | Microsoft Cloud Design Patterns (Gateway Aggregati... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 32 |
| Specification Methodology | Backend for Frontend (BFF) with CQRS orchestration... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- BFF (Backend for Frontend)
- Gateway Aggregation
- Resilience Pipeline (Polly)
- Distributed Tracing (Correlation ID)
- CQRS (MediatR) for Request Orchestration
- Global Exception Handling Middleware

#### 2.3.2.2 Directory Structure Source

ASP.NET Core Clean Architecture

#### 2.3.2.3 Naming Conventions Source

Microsoft Framework Design Guidelines

#### 2.3.2.4 Architectural Patterns Source

Enterprise Application Architecture

#### 2.3.2.5 Performance Optimizations Applied

- Task.WhenAll for parallel downstream requests
- Output Caching for static configuration data
- HttpClient Factory Connection Pooling
- JSON source generators for DTOs

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.dockerignore

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .dockerignore

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.editorconfig

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .editorconfig

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.gitignore

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- .gitignore

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

Dockerfile

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- Dockerfile

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

Emp.ApiGateway.sln

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- Emp.ApiGateway.sln

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

global.json

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- global.json

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

nuget.config

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- nuget.config

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

src/Emp.ApiGateway.Application

###### 2.3.3.1.8.2 Purpose

BFF Logic: Orchestration, Aggregation, and Mapping

###### 2.3.3.1.8.3 Contains Files

- DependencyInjection.cs
- Mappings/PublicApiMappingProfile.cs
- Interfaces/Infrastructure/IProjectServiceClient.cs
- Interfaces/Infrastructure/IFinancialServiceClient.cs

###### 2.3.3.1.8.4 Organizational Reasoning

Application layer holds the business rules of the Gateway (aggregation logic) independent of HTTP implementation

###### 2.3.3.1.8.5 Framework Convention Alignment

Clean Architecture Application Layer

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

src/Emp.ApiGateway.Application/Emp.ApiGateway.Application.csproj

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- Emp.ApiGateway.Application.csproj

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

src/Emp.ApiGateway.Application/Features/Projects

###### 2.3.3.1.10.2 Purpose

Project-specific orchestration logic

###### 2.3.3.1.10.3 Contains Files

- Queries/GetProjectDashboardQuery.cs
- Queries/GetProjectDashboardHandler.cs
- Commands/CreateProjectCommand.cs
- Commands/UploadSowCommand.cs

###### 2.3.3.1.10.4 Organizational Reasoning

Vertical slice architecture within Application layer for feature cohesion

###### 2.3.3.1.10.5 Framework Convention Alignment

MediatR feature folders

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

src/Emp.ApiGateway.Infrastructure

###### 2.3.3.1.11.2 Purpose

Communication with downstream services and external infrastructure

###### 2.3.3.1.11.3 Contains Files

- DependencyInjection.cs
- Services/ProjectServiceClient.cs
- Services/FinancialServiceClient.cs
- Messaging/MassTransitPublisher.cs
- Configuration/ServiceUrls.cs

###### 2.3.3.1.11.4 Organizational Reasoning

Infrastructure layer handles I/O and external dependencies

###### 2.3.3.1.11.5 Framework Convention Alignment

Clean Architecture Infrastructure Layer

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

src/Emp.ApiGateway.Infrastructure/Emp.ApiGateway.Infrastructure.csproj

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- Emp.ApiGateway.Infrastructure.csproj

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

src/Emp.ApiGateway.Web

###### 2.3.3.1.13.2 Purpose

Hosting layer, Middleware pipeline, and Public Controllers

###### 2.3.3.1.13.3 Contains Files

- Program.cs
- appsettings.json
- Middleware/CorrelationIdMiddleware.cs
- Middleware/GlobalExceptionHandler.cs
- Extensions/RateLimitExtensions.cs

###### 2.3.3.1.13.4 Organizational Reasoning

Standard ASP.NET Core entry point and cross-cutting HTTP concerns

###### 2.3.3.1.13.5 Framework Convention Alignment

ASP.NET Core Web API

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

src/Emp.ApiGateway.Web/appsettings.Development.json

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

src/Emp.ApiGateway.Web/appsettings.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- appsettings.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

src/Emp.ApiGateway.Web/Controllers

###### 2.3.3.1.16.2 Purpose

Public REST endpoints defining the contract for the Frontend

###### 2.3.3.1.16.3 Contains Files

- ProjectsController.cs
- FinancialsController.cs
- UsersController.cs

###### 2.3.3.1.16.4 Organizational Reasoning

Grouping by functional resource

###### 2.3.3.1.16.5 Framework Convention Alignment

MVC Controller layout

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

src/Emp.ApiGateway.Web/Emp.ApiGateway.Web.csproj

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- Emp.ApiGateway.Web.csproj

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

src/Emp.ApiGateway.Web/Properties/launchSettings.json

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- launchSettings.json

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

tests/Emp.ApiGateway.IntegrationTests/Emp.ApiGateway.IntegrationTests.csproj

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- Emp.ApiGateway.IntegrationTests.csproj

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

tests/Emp.ApiGateway.UnitTests/Emp.ApiGateway.UnitTests.csproj

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- Emp.ApiGateway.UnitTests.csproj

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | Emp.ApiGateway |
| Namespace Organization | Emp.ApiGateway.{Layer}.{Feature} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET Standard |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

ProjectsController

##### 2.3.4.1.2.0 File Path

src/Emp.ApiGateway.Web/Controllers/ProjectsController.cs

##### 2.3.4.1.3.0 Class Type

Controller

##### 2.3.4.1.4.0 Inheritance

ControllerBase

##### 2.3.4.1.5.0 Purpose

Public API surface for Project related operations, delegating to MediatR

##### 2.3.4.1.6.0 Dependencies

- ISender (MediatR)
- ILogger<ProjectsController>

##### 2.3.4.1.7.0 Framework Specific Attributes

- [ApiController]
- [Route(\"api/v1/projects\")]
- [Authorize]

##### 2.3.4.1.8.0 Technology Integration Notes

Uses ASP.NET Core 8 Identity integration via attributes

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

###### 2.3.4.1.10.1 Method Name

####### 2.3.4.1.10.1.1 Method Name

GetProjectDashboard

####### 2.3.4.1.10.1.2 Method Signature

GetProjectDashboard(Guid projectId, CancellationToken ct)

####### 2.3.4.1.10.1.3 Return Type

Task<ActionResult<ProjectDashboardResponse>>

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

true

####### 2.3.4.1.10.1.6 Framework Specific Attributes

- [HttpGet(\"{projectId}/dashboard\")]
- [ProducesResponseType(typeof(ProjectDashboardResponse), 200)]
- [ProducesResponseType(404)]

####### 2.3.4.1.10.1.7 Parameters

######## 2.3.4.1.10.1.7.1 Parameter Name

######### 2.3.4.1.10.1.7.1.1 Parameter Name

projectId

######### 2.3.4.1.10.1.7.1.2 Parameter Type

Guid

######### 2.3.4.1.10.1.7.1.3 Is Nullable

false

######### 2.3.4.1.10.1.7.1.4 Purpose

Project Identifier

######### 2.3.4.1.10.1.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.10.1.7.2.0 Parameter Name

######### 2.3.4.1.10.1.7.2.1 Parameter Name

ct

######### 2.3.4.1.10.1.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.10.1.7.2.3 Is Nullable

false

######### 2.3.4.1.10.1.7.2.4 Purpose

Request cancellation

######### 2.3.4.1.10.1.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.10.1.8.0.0 Implementation Logic

Constructs GetProjectDashboardQuery, awaits MediatR Send, returns Ok with result.

####### 2.3.4.1.10.1.9.0.0 Exception Handling

Global middleware handles NotFound/Validation exceptions.

####### 2.3.4.1.10.1.10.0.0 Performance Considerations

Async throughout to prevent thread starvation.

####### 2.3.4.1.10.1.11.0.0 Validation Requirements

Guid validation.

####### 2.3.4.1.10.1.12.0.0 Technology Integration Details

MediatR pattern

###### 2.3.4.1.10.2.0.0.0 Method Name

####### 2.3.4.1.10.2.1.0.0 Method Name

UploadSow

####### 2.3.4.1.10.2.2.0.0 Method Signature

UploadSow(Guid projectId, IFormFile file, CancellationToken ct)

####### 2.3.4.1.10.2.3.0.0 Return Type

Task<ActionResult>

####### 2.3.4.1.10.2.4.0.0 Access Modifier

public

####### 2.3.4.1.10.2.5.0.0 Is Async

true

####### 2.3.4.1.10.2.6.0.0 Framework Specific Attributes

- [HttpPost(\"{projectId}/sow\")]
- [Consumes(\"multipart/form-data\")]

####### 2.3.4.1.10.2.7.0.0 Parameters

######## 2.3.4.1.10.2.7.1.0 Parameter Name

######### 2.3.4.1.10.2.7.1.1 Parameter Name

projectId

######### 2.3.4.1.10.2.7.1.2 Parameter Type

Guid

######### 2.3.4.1.10.2.7.1.3 Is Nullable

false

######### 2.3.4.1.10.2.7.1.4 Purpose

Project Identifier

######### 2.3.4.1.10.2.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.10.2.7.2.0 Parameter Name

######### 2.3.4.1.10.2.7.2.1 Parameter Name

file

######### 2.3.4.1.10.2.7.2.2 Parameter Type

IFormFile

######### 2.3.4.1.10.2.7.2.3 Is Nullable

false

######### 2.3.4.1.10.2.7.2.4 Purpose

SOW Document

######### 2.3.4.1.10.2.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.10.2.8.0.0 Implementation Logic

Validates file type/size, creates UploadSowCommand (converting IFormFile stream), sends to MediatR.

####### 2.3.4.1.10.2.9.0.0 Exception Handling

Returns 400 for invalid file types.

####### 2.3.4.1.10.2.10.0.0 Performance Considerations

Uses Stream processing to avoid loading large files into memory.

####### 2.3.4.1.10.2.11.0.0 Validation Requirements

File extension and size validation.

####### 2.3.4.1.10.2.12.0.0 Technology Integration Details

IFormFile handling

##### 2.3.4.1.11.0.0.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0.0.0 Implementation Notes

Strictly a pass-through to Application layer.

#### 2.3.4.2.0.0.0.0.0 Class Name

##### 2.3.4.2.1.0.0.0.0 Class Name

GetProjectDashboardHandler

##### 2.3.4.2.2.0.0.0.0 File Path

src/Emp.ApiGateway.Application/Features/Projects/Queries/GetProjectDashboardHandler.cs

##### 2.3.4.2.3.0.0.0.0 Class Type

Class

##### 2.3.4.2.4.0.0.0.0 Inheritance

IRequestHandler<GetProjectDashboardQuery, ProjectDashboardResponse>

##### 2.3.4.2.5.0.0.0.0 Purpose

Aggregates data from Project and Financial services to provide a unified dashboard view

##### 2.3.4.2.6.0.0.0.0 Dependencies

- IProjectServiceClient
- IFinancialServiceClient
- IMapper

##### 2.3.4.2.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0.0.0 Technology Integration Notes

Uses Task.WhenAll for parallel execution

##### 2.3.4.2.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'Handle(GetProjectDashboardQuery request, CancellationToken ct)', 'return_type': 'Task<ProjectDashboardResponse>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'GetProjectDashboardQuery', 'is_nullable': 'false', 'purpose': 'Query parameters', 'framework_attributes': []}], 'implementation_logic': '1. Start Project Details Task. 2. Start Financial Summary Task. 3. Await Task.WhenAll. 4. Map results to ProjectDashboardResponse. 5. Handle potential partial failures (optional degradation).', 'exception_handling': 'Propagates exceptions to middleware. If Financial service is down, logic could optionally return partial data.', 'performance_considerations': 'Parallel execution minimizes total latency.', 'validation_requirements': 'None.', 'technology_integration_details': 'Aggregation pattern'}

##### 2.3.4.2.11.0.0.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0.0.0 Implementation Notes

Core BFF aggregation logic resides here.

#### 2.3.4.3.0.0.0.0.0 Class Name

##### 2.3.4.3.1.0.0.0.0 Class Name

CorrelationIdMiddleware

##### 2.3.4.3.2.0.0.0.0 File Path

src/Emp.ApiGateway.Web/Middleware/CorrelationIdMiddleware.cs

##### 2.3.4.3.3.0.0.0.0 Class Type

Class

##### 2.3.4.3.4.0.0.0.0 Inheritance

IMiddleware

##### 2.3.4.3.5.0.0.0.0 Purpose

Ensures every request has a Correlation ID for distributed tracing across microservices

##### 2.3.4.3.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0.0.0 Technology Integration Notes

Standard ASP.NET Core Middleware

##### 2.3.4.3.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0.0.0 Methods

- {'method_name': 'InvokeAsync', 'method_signature': 'InvokeAsync(HttpContext context, RequestDelegate next)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'HttpContext', 'is_nullable': 'false', 'purpose': 'HTTP Context', 'framework_attributes': []}, {'parameter_name': 'next', 'parameter_type': 'RequestDelegate', 'is_nullable': 'false', 'purpose': 'Next middleware', 'framework_attributes': []}], 'implementation_logic': "Checks headers for 'X-Correlation-ID'. If missing, generates new Guid. Adds to Response headers and HttpContext.Items for logging scope.", 'exception_handling': 'None.', 'performance_considerations': 'Lightweight operations.', 'validation_requirements': 'None.', 'technology_integration_details': 'HTTP Header manipulation'}

##### 2.3.4.3.11.0.0.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0.0.0 Implementation Notes

Critical for observability.

#### 2.3.4.4.0.0.0.0.0 Class Name

##### 2.3.4.4.1.0.0.0.0 Class Name

ProjectServiceClient

##### 2.3.4.4.2.0.0.0.0 File Path

src/Emp.ApiGateway.Infrastructure/Services/ProjectServiceClient.cs

##### 2.3.4.4.3.0.0.0.0 Class Type

Class

##### 2.3.4.4.4.0.0.0.0 Inheritance

IProjectServiceClient

##### 2.3.4.4.5.0.0.0.0 Purpose

Typed HTTP Client for Project Microservice interactions

##### 2.3.4.4.6.0.0.0.0 Dependencies

- HttpClient
- ILogger<ProjectServiceClient>

##### 2.3.4.4.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0.0.0 Technology Integration Notes

Registered with HttpClientFactory and Microsoft.Extensions.Http.Resilience

##### 2.3.4.4.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0.0.0 Methods

- {'method_name': 'GetProjectDetailsAsync', 'method_signature': 'GetProjectDetailsAsync(Guid projectId, CancellationToken ct)', 'return_type': 'Task<InternalProjectDto?>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'projectId', 'parameter_type': 'Guid', 'is_nullable': 'false', 'purpose': 'ID', 'framework_attributes': []}], 'implementation_logic': 'Execute GET request. Check status code. Deserialize JSON to DTO. Pass Correlation ID in headers.', 'exception_handling': 'Resilience pipeline handles transient errors (429, 5xx). Throws generic ApiException on persistent failure.', 'performance_considerations': 'Uses pooled connections.', 'validation_requirements': 'None.', 'technology_integration_details': 'System.Net.Http'}

##### 2.3.4.4.11.0.0.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0.0.0 Implementation Notes

Infrastructure adapter for the Project Service.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0.0.0 Interface Name

##### 2.3.5.1.1.0.0.0.0 Interface Name

IProjectServiceClient

##### 2.3.5.1.2.0.0.0.0 File Path

src/Emp.ApiGateway.Application/Interfaces/Infrastructure/IProjectServiceClient.cs

##### 2.3.5.1.3.0.0.0.0 Purpose

Contract for interacting with the Project Microservice

##### 2.3.5.1.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.1.6.0.0.0.0 Method Contracts

###### 2.3.5.1.6.1.0.0.0 Method Name

####### 2.3.5.1.6.1.1.0.0 Method Name

GetProjectDetailsAsync

####### 2.3.5.1.6.1.2.0.0 Method Signature

GetProjectDetailsAsync(Guid projectId, CancellationToken ct)

####### 2.3.5.1.6.1.3.0.0 Return Type

Task<InternalProjectDto?>

####### 2.3.5.1.6.1.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.1.5.0.0 Parameters

- {'parameter_name': 'projectId', 'parameter_type': 'Guid', 'purpose': 'Project Identifier'}

####### 2.3.5.1.6.1.6.0.0 Contract Description

Fetches project details from downstream service

####### 2.3.5.1.6.1.7.0.0 Exception Contracts

Throws DownstreamServiceException on failure

###### 2.3.5.1.6.2.0.0.0 Method Name

####### 2.3.5.1.6.2.1.0.0 Method Name

UploadSowAsync

####### 2.3.5.1.6.2.2.0.0 Method Signature

UploadSowAsync(Guid projectId, Stream fileStream, string fileName, CancellationToken ct)

####### 2.3.5.1.6.2.3.0.0 Return Type

Task

####### 2.3.5.1.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.2.5.0.0 Parameters

######## 2.3.5.1.6.2.5.1.0 Parameter Name

######### 2.3.5.1.6.2.5.1.1 Parameter Name

projectId

######### 2.3.5.1.6.2.5.1.2 Parameter Type

Guid

######### 2.3.5.1.6.2.5.1.3 Purpose

Project ID

######## 2.3.5.1.6.2.5.2.0 Parameter Name

######### 2.3.5.1.6.2.5.2.1 Parameter Name

fileStream

######### 2.3.5.1.6.2.5.2.2 Parameter Type

Stream

######### 2.3.5.1.6.2.5.2.3 Purpose

File content stream

######## 2.3.5.1.6.2.5.3.0 Parameter Name

######### 2.3.5.1.6.2.5.3.1 Parameter Name

fileName

######### 2.3.5.1.6.2.5.3.2 Parameter Type

string

######### 2.3.5.1.6.2.5.3.3 Purpose

Original file name

####### 2.3.5.1.6.2.6.0.0 Contract Description

Uploads SOW document to project service

####### 2.3.5.1.6.2.7.0.0 Exception Contracts

Throws DownstreamServiceException on failure

##### 2.3.5.1.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0.0.0 Implementation Guidance

Implement using typed HttpClient.

#### 2.3.5.2.0.0.0.0.0 Interface Name

##### 2.3.5.2.1.0.0.0.0 Interface Name

IFinancialServiceClient

##### 2.3.5.2.2.0.0.0.0 File Path

src/Emp.ApiGateway.Application/Interfaces/Infrastructure/IFinancialServiceClient.cs

##### 2.3.5.2.3.0.0.0.0 Purpose

Contract for interacting with the Financial Microservice

##### 2.3.5.2.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0.0.0 Method Contracts

- {'method_name': 'GetProjectFinancialSummaryAsync', 'method_signature': 'GetProjectFinancialSummaryAsync(Guid projectId, CancellationToken ct)', 'return_type': 'Task<FinancialSummaryDto?>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'projectId', 'parameter_type': 'Guid', 'purpose': 'Project Identifier'}], 'contract_description': 'Fetches financial summary for a project', 'exception_contracts': 'Throws DownstreamServiceException on failure'}

##### 2.3.5.2.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0.0.0 Implementation Guidance

Implement using typed HttpClient.

### 2.3.6.0.0.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0.0.0 Dto Name

##### 2.3.7.1.1.0.0.0.0 Dto Name

ProjectDashboardResponse

##### 2.3.7.1.2.0.0.0.0 File Path

src/Emp.ApiGateway.Application/Features/Projects/Queries/ProjectDashboardResponse.cs

##### 2.3.7.1.3.0.0.0.0 Purpose

Composite DTO for the Frontend Dashboard view

##### 2.3.7.1.4.0.0.0.0 Framework Base Class

None

##### 2.3.7.1.5.0.0.0.0 Properties

###### 2.3.7.1.5.1.0.0.0 Property Name

####### 2.3.7.1.5.1.1.0.0 Property Name

Project

####### 2.3.7.1.5.1.2.0.0 Property Type

PublicProjectDto

####### 2.3.7.1.5.1.3.0.0 Validation Attributes

*No items available*

####### 2.3.7.1.5.1.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0.0.0 Property Name

####### 2.3.7.1.5.2.1.0.0 Property Name

Financials

####### 2.3.7.1.5.2.2.0.0 Property Type

PublicFinancialSummaryDto

####### 2.3.7.1.5.2.3.0.0 Validation Attributes

*No items available*

####### 2.3.7.1.5.2.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0.0.0 Validation Rules

None (Output DTO)

##### 2.3.7.1.7.0.0.0.0 Serialization Requirements

JSON serialization

#### 2.3.7.2.0.0.0.0.0 Dto Name

##### 2.3.7.2.1.0.0.0.0 Dto Name

CreateProjectRequest

##### 2.3.7.2.2.0.0.0.0 File Path

src/Emp.ApiGateway.Application/DTOs/Public/CreateProjectRequest.cs

##### 2.3.7.2.3.0.0.0.0 Purpose

Input DTO for project creation

##### 2.3.7.2.4.0.0.0.0 Framework Base Class

None

##### 2.3.7.2.5.0.0.0.0 Properties

###### 2.3.7.2.5.1.0.0.0 Property Name

####### 2.3.7.2.5.1.1.0.0 Property Name

Name

####### 2.3.7.2.5.1.2.0.0 Property Type

string

####### 2.3.7.2.5.1.3.0.0 Validation Attributes

- [Required]
- [MaxLength(100)]

####### 2.3.7.2.5.1.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.2.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0.0.0 Property Name

####### 2.3.7.2.5.2.1.0.0 Property Name

Description

####### 2.3.7.2.5.2.2.0.0 Property Type

string

####### 2.3.7.2.5.2.3.0.0 Validation Attributes

- [MaxLength(1000)]

####### 2.3.7.2.5.2.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.2.5.2.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0.0.0 Validation Rules

Standard Data Annotations

##### 2.3.7.2.7.0.0.0.0 Serialization Requirements

JSON deserialization

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0.0.0 Configuration Name

ServiceUrls

##### 2.3.8.1.2.0.0.0.0 File Path

src/Emp.ApiGateway.30801[REDACTED].cs

##### 2.3.8.1.3.0.0.0.0 Purpose

Configuration object for downstream service locations

##### 2.3.8.1.4.0.0.0.0 Framework Base Class

None

##### 2.3.8.1.5.0.0.0.0 Configuration Sections

- {'section_name': 'ServiceUrls', 'properties': [{'property_name': 'ProjectService', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Base URL'}, {'property_name': 'FinancialService', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Base URL'}, {'property_name': 'UserService', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Base URL'}]}

##### 2.3.8.1.6.0.0.0.0 Validation Requirements

Must be valid URIs

##### 2.3.8.1.7.0.0.0.0 Validation Notes

Loaded via Options pattern

#### 2.3.8.2.0.0.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0.0.0 Configuration Name

AwsCognitoSettings

##### 2.3.8.2.2.0.0.0.0 File Path

src/Emp.ApiGateway.Infrastructure/Configuration/AwsCognitoSettings.cs

##### 2.3.8.2.3.0.0.0.0 Purpose

Settings for JWT Validation

##### 2.3.8.2.4.0.0.0.0 Framework Base Class

None

##### 2.3.8.2.5.0.0.0.0 Configuration Sections

- {'section_name': 'AWS:Cognito', 'properties': [{'property_name': 'UserPoolId', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'User Pool ID'}, {'property_name': 'ClientId', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'App Client ID'}]}

##### 2.3.8.2.6.0.0.0.0 Validation Requirements

Required for auth

##### 2.3.8.2.7.0.0.0.0 Validation Notes

Used to configure JwtBearer options

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0.0.0 Service Interface

##### 2.3.9.1.1.0.0.0.0 Service Interface

IProjectServiceClient

##### 2.3.9.1.2.0.0.0.0 Service Implementation

ProjectServiceClient

##### 2.3.9.1.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0.0.0 Registration Reasoning

Uses HttpClient which is best managed as Scoped/Transient via Factory

##### 2.3.9.1.5.0.0.0.0 Framework Registration Pattern

services.AddHttpClient<IProjectServiceClient, ProjectServiceClient>().AddStandardResilienceHandler()

##### 2.3.9.1.6.0.0.0.0 Validation Notes

Includes resilience policies via Microsoft.Extensions.Http.Resilience

#### 2.3.9.2.0.0.0.0.0 Service Interface

##### 2.3.9.2.1.0.0.0.0 Service Interface

IMiddleware

##### 2.3.9.2.2.0.0.0.0 Service Implementation

CorrelationIdMiddleware

##### 2.3.9.2.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0.0.0 Registration Reasoning

Middleware is instantiated per request

##### 2.3.9.2.5.0.0.0.0 Framework Registration Pattern

services.AddScoped<CorrelationIdMiddleware>()

##### 2.3.9.2.6.0.0.0.0 Validation Notes

Registered in pipeline via app.UseMiddleware

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0.0.0 Integration Target

##### 2.3.10.1.1.0.0.0.0 Integration Target

AWS Cognito

##### 2.3.10.1.2.0.0.0.0 Integration Type

OIDC Identity Provider

##### 2.3.10.1.3.0.0.0.0 Required Client Classes

- Microsoft.AspNetCore.Authentication.JwtBearer

##### 2.3.10.1.4.0.0.0.0 Configuration Requirements

Valid Issuer, Audience, and Keys

##### 2.3.10.1.5.0.0.0.0 Error Handling Requirements

Challenge response for 401

##### 2.3.10.1.6.0.0.0.0 Authentication Requirements

Verify JWT signature and claims

##### 2.3.10.1.7.0.0.0.0 Framework Integration Patterns

services.AddAuthentication().AddJwtBearer()

##### 2.3.10.1.8.0.0.0.0 Validation Notes

Standard ASP.NET Core integration

#### 2.3.10.2.0.0.0.0.0 Integration Target

##### 2.3.10.2.1.0.0.0.0 Integration Target

Message Broker (RabbitMQ/SQS)

##### 2.3.10.2.2.0.0.0.0 Integration Type

Asynchronous Messaging

##### 2.3.10.2.3.0.0.0.0 Required Client Classes

- MassTransit

##### 2.3.10.2.4.0.0.0.0 Configuration Requirements

Host, Credentials, Exchange names

##### 2.3.10.2.5.0.0.0.0 Error Handling Requirements

Outbox pattern (optional), Retry

##### 2.3.10.2.6.0.0.0.0 Authentication Requirements

Connection string security

##### 2.3.10.2.7.0.0.0.0 Framework Integration Patterns

services.AddMassTransit()

##### 2.3.10.2.8.0.0.0.0 Validation Notes

Used for publishing integration events like 'SowUploaded'

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 0 |
| Total Dtos | 8 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 28 |
| Phase 2 Claimed Count | 28 |
| Phase 2 Actual Count | 22 |
| Validation Added Count | 6 |
| Final Validated Count | 28 |


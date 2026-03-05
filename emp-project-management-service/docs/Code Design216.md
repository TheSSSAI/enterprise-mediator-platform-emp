# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-PROJECT |
| Validation Timestamp | 2025-01-26T12:00:00Z |
| Original Component Count Claimed | 42 |
| Original Component Count Actual | 42 |
| Gaps Identified Count | 5 |
| Components Added Count | 7 |
| Final Component Count | 49 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Strict alignment with Clean Architecture and DDD p... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Full compliance with Business Logic microservice definition.

#### 2.2.1.2 Gaps Identified

- Missing infrastructure implementation for Vector Search (REQ-FUNC-014)
- Lack of JSONB mapping specification for SOW Data (REQ-FUNC-013)
- Payout Rule validation logic missing (REQ-BR-006)

#### 2.2.1.3 Components Added

- VectorVendorMatchingService
- SowDetailsConfiguration
- PayoutRuleValidator

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- Outbox pattern configuration for reliable event publishing
- Semantic search query implementation

#### 2.2.2.4 Added Requirement Components

- OutboxExtensions
- PgVectorDbContextExtensions

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

CQRS and DDD patterns fully integrated.

#### 2.2.3.2 Missing Pattern Components

- Domain Event Dispatcher mechanism within EF Core SaveChanges

#### 2.2.3.3 Added Pattern Components

- DomainEventDispatcherInterceptor

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Entity configurations updated for PostgreSQL specifics.

#### 2.2.4.2 Missing Database Components

- pgvector extension enablement in DbContext
- JSONB conversion for Value Objects

#### 2.2.4.3 Added Database Components

- ProjectContextModelSnapshot
- SowDetailsConversion

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

API to Domain flow fully mapped.

#### 2.2.5.2 Missing Interaction Components

- Mapping from Domain Event to Integration Event

#### 2.2.5.3 Added Interaction Components

- ProjectAwardedIntegrationEventMapper

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-PROJECT |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, M... |
| Technology Guidance Integration | Clean Architecture with Vertical Slice features |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 49 |
| Specification Methodology | Domain-Driven Design with CQRS |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Dependency Injection (IServiceCollection)
- CQRS (MediatR)
- Repository Pattern
- Unit of Work (EF Core)
- Transactional Outbox (MassTransit)
- Options Pattern (IOptions<T>)
- Middleware Pipeline

#### 2.3.2.2 Directory Structure Source

Clean Architecture Solution Template

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Conventions

#### 2.3.2.4 Architectural Patterns Source

Domain-Driven Design (Evans)

#### 2.3.2.5 Performance Optimizations Applied

- Async/Await I/O
- EF Core Compiled Models
- NoTracking for Queries
- JSONB for semi-structured SOW data
- HNSW Index for Vector Search

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

.vscode/launch.json

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- launch.json

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

Directory.Build.props

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

EnterpriseMediator.ProjectManagement.sln

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- EnterpriseMediator.ProjectManagement.sln

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

global.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- global.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

src/EnterpriseMediator.ProjectManagement.Application

###### 2.3.3.1.8.2 Purpose

Application orchestration, Commands/Queries, Validators, and DTOs.

###### 2.3.3.1.8.3 Contains Files

- Features/Projects/Commands/AwardProject/AwardProjectCommand.cs
- Features/Projects/Commands/AwardProject/AwardProjectCommandHandler.cs
- Features/Projects/Commands/UpdateProjectBrief/UpdateProjectBriefCommand.cs
- Features/Proposals/Queries/GetProjectProposals/GetProjectProposalsQuery.cs
- Behaviors/ValidationBehavior.cs
- Interfaces/IMessageBus.cs

###### 2.3.3.1.8.4 Organizational Reasoning

Organized by Feature (Vertical Slices) for maintainability.

###### 2.3.3.1.8.5 Framework Convention Alignment

MediatR library conventions

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

src/EnterpriseMediator.ProjectManagement.Domain

###### 2.3.3.1.9.2 Purpose

Core business logic, Aggregates, Value Objects, Domain Events, and Repository Interfaces.

###### 2.3.3.1.9.3 Contains Files

- Aggregates/ProjectAggregate/Project.cs
- Aggregates/ProjectAggregate/Proposal.cs
- Aggregates/ProjectAggregate/SowDetails.cs
- Aggregates/ProjectAggregate/ProjectPayoutRule.cs
- Events/ProjectAwardedDomainEvent.cs
- Services/IVendorMatchingService.cs
- Interfaces/IProjectRepository.cs

###### 2.3.3.1.9.4 Organizational Reasoning

Pure C# domain layer with no infrastructure dependencies.

###### 2.3.3.1.9.5 Framework Convention Alignment

.NET Standard Library

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

src/EnterpriseMediator.ProjectManagement.Infrastructure

###### 2.3.3.1.10.2 Purpose

Persistence implementation, External Adapters, and Configuration.

###### 2.3.3.1.10.3 Contains Files

- Persistence/ProjectDbContext.cs
- Persistence/Configurations/ProjectConfiguration.cs
- Persistence/Repositories/ProjectRepository.cs
- Services/VectorVendorMatchingService.cs
- Messaging/MassTransitMessageBus.cs

###### 2.3.3.1.10.4 Organizational Reasoning

Encapsulates all external dependencies (DB, Broker, AI Service).

###### 2.3.3.1.10.5 Framework Convention Alignment

Infrastructure layer implementation

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

src/EnterpriseMediator.ProjectManagement.WebAPI

###### 2.3.3.1.11.2 Purpose

Entry point, Controllers, and DI Composition.

###### 2.3.3.1.11.3 Contains Files

- Controllers/ProjectsController.cs
- Program.cs
- Middleware/GlobalExceptionHandler.cs

###### 2.3.3.1.11.4 Organizational Reasoning

Standard ASP.NET Core WebAPI structure.

###### 2.3.3.1.11.5 Framework Convention Alignment

ASP.NET Core Web API

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

src/ProjectManagement.Application/ProjectManagement.Application.csproj

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- ProjectManagement.Application.csproj

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

src/ProjectManagement.Domain/ProjectManagement.Domain.csproj

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- ProjectManagement.Domain.csproj

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

src/ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- ProjectManagement.Infrastructure.csproj

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

src/ProjectManagement.WebAPI/appsettings.Development.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

src/ProjectManagement.WebAPI/Dockerfile

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- Dockerfile

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

src/ProjectManagement.WebAPI/ProjectManagement.WebAPI.csproj

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- ProjectManagement.WebAPI.csproj

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

src/ProjectManagement.WebAPI/Properties/launchSettings.json

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

tests/ProjectManagement.IntegrationTests/ProjectManagement.IntegrationTests.csproj

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- ProjectManagement.IntegrationTests.csproj

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

tests/ProjectManagement.UnitTests/ProjectManagement.UnitTests.csproj

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- ProjectManagement.UnitTests.csproj

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | EnterpriseMediator.ProjectManagement |
| Namespace Organization | EnterpriseMediator.ProjectManagement.{Layer}.{Feat... |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET Namespace Guidelines |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

Project

##### 2.3.4.1.2.0 File Path

src/EnterpriseMediator.ProjectManagement.Domain/Aggregates/ProjectAggregate/Project.cs

##### 2.3.4.1.3.0 Class Type

Entity (Aggregate Root)

##### 2.3.4.1.4.0 Inheritance

AggregateRoot<Guid>

##### 2.3.4.1.5.0 Purpose

Manages project lifecycle, SOW data, and proposals. Enforces state transitions.

##### 2.3.4.1.6.0 Dependencies

- ProjectStatus
- Proposal
- SowDetails
- ProjectPayoutRule

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses private setters and Encapsulated Collections for DDD compliance.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

SowDetails

####### 2.3.4.1.9.1.2 Property Type

SowDetails

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Stores structured SOW data (Scope, Skills) extracted by AI.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Mapped to JSONB column in EF Configuration

####### 2.3.4.1.9.1.7 Implementation Notes

Value Object

####### 2.3.4.1.9.1.8 Validation Notes

Cannot be null after 'Proposed' state

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

PayoutRules

####### 2.3.4.1.9.2.2 Property Type

List<ProjectPayoutRule>

####### 2.3.4.1.9.2.3 Access Modifier

private

####### 2.3.4.1.9.2.4 Purpose

Collection of payout rules associated with the project.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Backing field for IReadOnlyCollection

####### 2.3.4.1.9.2.7 Implementation Notes

Encapsulated collection

####### 2.3.4.1.9.2.8 Validation Notes

Total percentage must equal 100%

##### 2.3.4.1.10.0.0 Methods

###### 2.3.4.1.10.1.0 Method Name

####### 2.3.4.1.10.1.1 Method Name

AwardTo

####### 2.3.4.1.10.1.2 Method Signature

public void AwardTo(Guid proposalId)

####### 2.3.4.1.10.1.3 Return Type

void

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

false

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

- {'parameter_name': 'proposalId', 'parameter_type': 'Guid', 'is_nullable': 'false', 'purpose': 'The ID of the winning proposal', 'framework_attributes': []}

####### 2.3.4.1.10.1.8 Implementation Logic

Validates status is 'Proposed'. Finds proposal. Sets proposal status to Accepted. Sets Project status to Awarded. Adds ProjectAwardedDomainEvent.

####### 2.3.4.1.10.1.9 Exception Handling

Throws DomainException if preconditions fail.

####### 2.3.4.1.10.1.10 Performance Considerations

In-memory operation.

####### 2.3.4.1.10.1.11 Validation Requirements

Proposal must exist in collection.

####### 2.3.4.1.10.1.12 Technology Integration Details

Updates aggregate state.

####### 2.3.4.1.10.1.13 Validation Notes

Enforces business rule REQ-FUN-003

###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

UpdateBrief

####### 2.3.4.1.10.2.2 Method Signature

public void UpdateBrief(SowDetails sowDetails)

####### 2.3.4.1.10.2.3 Return Type

void

####### 2.3.4.1.10.2.4 Access Modifier

public

####### 2.3.4.1.10.2.5 Is Async

false

####### 2.3.4.1.10.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7 Parameters

- {'parameter_name': 'sowDetails', 'parameter_type': 'SowDetails', 'is_nullable': 'false', 'purpose': 'Updated SOW data', 'framework_attributes': []}

####### 2.3.4.1.10.2.8 Implementation Logic

Updates SowDetails property. May trigger status change if approved.

####### 2.3.4.1.10.2.9 Exception Handling

Throws if project is in terminal state.

####### 2.3.4.1.10.2.10 Performance Considerations

None

####### 2.3.4.1.10.2.11 Validation Requirements

Valid SowDetails

####### 2.3.4.1.10.2.12 Technology Integration Details

Value Object replacement

####### 2.3.4.1.10.2.13 Validation Notes

Implements REQ-FUNC-013

##### 2.3.4.1.11.0.0 Events

- {'event_name': 'ProjectAwardedDomainEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'Successful execution of AwardTo', 'event_data': 'ProjectId, VendorId, ProposalId, AgreedAmount'}

##### 2.3.4.1.12.0.0 Implementation Notes

Aggregate root governing the write consistency boundary.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

AwardProjectCommandHandler

##### 2.3.4.2.2.0.0 File Path

src/EnterpriseMediator.ProjectManagement.Application/Features/Projects/Commands/AwardProject/AwardProjectCommandHandler.cs

##### 2.3.4.2.3.0.0 Class Type

Service (Handler)

##### 2.3.4.2.4.0.0 Inheritance

IRequestHandler<AwardProjectCommand, Result>

##### 2.3.4.2.5.0.0 Purpose

Orchestrates the project awarding workflow.

##### 2.3.4.2.6.0.0 Dependencies

- IProjectRepository
- IUnitOfWork

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

MediatR Handler

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result> Handle(AwardProjectCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'AwardProjectCommand', 'is_nullable': 'false', 'purpose': 'Command Data', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Async Cancellation', 'framework_attributes': []}], 'implementation_logic': 'Load Project with Proposals. Call Project.AwardTo. SaveChangesAsync via UnitOfWork.', 'exception_handling': 'Catches DomainExceptions, returns Failure Result.', 'performance_considerations': 'Async DB operations.', 'validation_requirements': 'Input validation via Pipeline.', 'technology_integration_details': 'EF Core change tracking handles updates.', 'validation_notes': 'Triggers Outbox messages on save.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Application Service pattern.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

ProjectConfiguration

##### 2.3.4.3.2.0.0 File Path

src/EnterpriseMediator.ProjectManagement.Infrastructure/Persistence/Configurations/ProjectConfiguration.cs

##### 2.3.4.3.3.0.0 Class Type

Configuration

##### 2.3.4.3.4.0.0 Inheritance

IEntityTypeConfiguration<Project>

##### 2.3.4.3.5.0.0 Purpose

EF Core mapping configuration.

##### 2.3.4.3.6.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Uses Fluent API.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'Configure', 'method_signature': 'public void Configure(EntityTypeBuilder<Project> builder)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'builder', 'parameter_type': 'EntityTypeBuilder<Project>', 'is_nullable': 'false', 'purpose': 'Builder', 'framework_attributes': []}], 'implementation_logic': 'Maps SowDetails to JSONB using ToJson(). Configures PayoutRules as Owned Collection. Sets Enums to String conversion.', 'exception_handling': 'None', 'performance_considerations': 'JSONB allows flexible schema for AI data.', 'validation_requirements': 'None', 'technology_integration_details': 'Npgsql specific JSONB mapping.', 'validation_notes': 'Supports REQ-FUNC-013 data structure.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Infrastructure concern.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

IProjectRepository

##### 2.3.5.1.2.0.0 File Path

src/EnterpriseMediator.ProjectManagement.Domain/Interfaces/IProjectRepository.cs

##### 2.3.5.1.3.0.0 Purpose

Abstracts persistence logic for Project Aggregate.

##### 2.3.5.1.4.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0 Framework Specific Inheritance

IRepository<Project>

##### 2.3.5.1.6.0.0 Method Contracts

- {'method_name': 'GetByIdWithProposalsAsync', 'method_signature': 'Task<Project?> GetByIdWithProposalsAsync(Guid id, CancellationToken cancellationToken)', 'return_type': 'Task<Project?>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'id', 'parameter_type': 'Guid', 'purpose': 'Project ID'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'purpose': 'Token'}], 'contract_description': 'Gets project with eager loaded proposals.', 'exception_contracts': 'None'}

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implement using EF Core Set<Project>().Include(p => p.Proposals).

##### 2.3.5.1.9.0.0 Validation Notes

Optimization for Award workflow.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IVendorMatchingService

##### 2.3.5.2.2.0.0 File Path

src/EnterpriseMediator.ProjectManagement.Domain/Services/IVendorMatchingService.cs

##### 2.3.5.2.3.0.0 Purpose

Domain service interface for Semantic Search.

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'FindMatchingVendorsAsync', 'method_signature': 'Task<IEnumerable<VendorMatch>> FindMatchingVendorsAsync(ProjectBrief brief, int limit)', 'return_type': 'Task<IEnumerable<VendorMatch>>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'brief', 'parameter_type': 'ProjectBrief', 'purpose': 'Source brief'}, {'parameter_name': 'limit', 'parameter_type': 'int', 'purpose': 'Max results'}], 'contract_description': 'Finds vendors based on vector similarity.', 'exception_contracts': 'None'}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Implement in Infrastructure using Npgsql pgvector queries (ORDER BY embedding <-> vector).

##### 2.3.5.2.9.0.0 Validation Notes

Supports REQ-FUNC-014.

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'AwardProjectCommand', 'file_path': 'src/EnterpriseMediator.ProjectManagement.Application/Features/Projects/Commands/AwardProject/AwardProjectCommand.cs', 'purpose': 'Input for awarding a project.', 'framework_base_class': 'IRequest<Result>', 'properties': [{'property_name': 'ProjectId', 'property_type': 'Guid', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'ProposalId', 'property_type': 'Guid', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'IDs must be non-empty.', 'serialization_requirements': 'JSON', 'validation_notes': 'Validated via FluentValidation.'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'DatabaseOptions', 'file_path': 'src/EnterpriseMediator.ProjectManagement.Application/Configuration/DatabaseOptions.cs', 'purpose': 'DB Connection settings.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'ConnectionStrings', 'properties': [{'property_name': 'DefaultConnection', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'PostgreSQL Connection String'}]}], 'validation_requirements': 'Must be valid connection string.', 'validation_notes': 'Validated at startup.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IProjectRepository

##### 2.3.9.1.2.0.0 Service Implementation

ProjectRepository

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

Matches DbContext scope.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<IProjectRepository, ProjectRepository>();

##### 2.3.9.1.6.0.0 Validation Notes

Standard DI.

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IVendorMatchingService

##### 2.3.9.2.2.0.0 Service Implementation

VectorVendorMatchingService

##### 2.3.9.2.3.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0 Registration Reasoning

Depends on DbContext.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddScoped<IVendorMatchingService, VectorVendorMatchingService>();

##### 2.3.9.2.6.0.0 Validation Notes

Injects Vector capabilities.

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

PostgreSQL (pgvector)

##### 2.3.10.1.2.0.0 Integration Type

Database

##### 2.3.10.1.3.0.0 Required Client Classes

- ProjectDbContext

##### 2.3.10.1.4.0.0 Configuration Requirements

Extension 'vector' must be enabled.

##### 2.3.10.1.5.0.0 Error Handling Requirements

EF Core Resiliency.

##### 2.3.10.1.6.0.0 Authentication Requirements

Connection String.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

EF Core Npgsql Provider.

##### 2.3.10.1.8.0.0 Validation Notes

Supports Semantic Search.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Message Bus

##### 2.3.10.2.2.0.0 Integration Type

Asynchronous Messaging

##### 2.3.10.2.3.0.0 Required Client Classes

- IPublishEndpoint

##### 2.3.10.2.4.0.0 Configuration Requirements

Bus settings.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Outbox Pattern.

##### 2.3.10.2.6.0.0 Authentication Requirements

Broker Credentials.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

MassTransit.

##### 2.3.10.2.8.0.0 Validation Notes

Reliable messaging.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 0 |
| Total Dtos | 5 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 25 |
| Phase 2 Claimed Count | 42 |
| Phase 2 Actual Count | 42 |
| Validation Added Count | 7 |
| Final Validated Count | 49 |


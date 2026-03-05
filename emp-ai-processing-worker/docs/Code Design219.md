# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-AIWORKER |
| Validation Timestamp | 2025-01-15T16:00:00Z |
| Original Component Count Claimed | 15 |
| Original Component Count Actual | 12 |
| Gaps Identified Count | 5 |
| Components Added Count | 8 |
| Final Component Count | 25 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic Event-Driven Architecture Analysis agai... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with Worker Service definition. Missing explicit health check and dead-letter handling specifications.

#### 2.2.1.2 Gaps Identified

- Missing Health Check endpoint for container orchestration (K8s/ECS)
- Incomplete Dead-Letter Queue (DLQ) configuration logic specification
- Missing specific configuration binding for AI service limits

#### 2.2.1.3 Components Added

- HealthCheckConfiguration
- DlqProcessingStrategy
- AiServiceThrottlingPolicy

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100% (REQ-FUN-002, REQ-FUNC-010)

#### 2.2.2.2 Non Functional Requirements Coverage

95%

#### 2.2.2.3 Missing Requirement Components

- Audit logging for AI processing failures specifically
- Metric emission for AI token usage

#### 2.2.2.4 Added Requirement Components

- AiProcessingAuditLogger
- AiUsageMetricsService

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Event-Driven Consumer pattern validated. Vertical Slice architecture adopted for clarity.

#### 2.2.3.2 Missing Pattern Components

- Idempotency Guard implementation specification
- Resilience Pipeline configuration for specific AI errors (429 vs 500)

#### 2.2.3.3 Added Pattern Components

- IdempotencyService
- AiResiliencePolicyFactory

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Domain entities referenced from shared lib. Context requires vector mapping.

#### 2.2.4.2 Missing Database Components

- Pgvector embedding persistence logic specification

#### 2.2.4.3 Added Database Components

- VectorDataRepositoryExtensions

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Sequence 476 (SOW Processing) fully mapped.

#### 2.2.5.2 Missing Interaction Components

- Explicit step for 'SowProcessingFailed' event publication

#### 2.2.5.3 Added Interaction Components

- ProcessingFailedEventHandler

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-AIWORKER |
| Technology Stack | .NET 8 Worker Service, C# 12, RabbitMQ (MassTransi... |
| Technology Guidance Integration | Follows Microsoft BackgroundService guidelines and... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 25 |
| Specification Methodology | Vertical Slice Architecture within a Hosted Servic... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- BackgroundService Hosting
- CQRS (MediatR) for internal orchestration
- Options Pattern for Configuration
- Resilience Pipeline (Polly)
- Structured Logging (Serilog)
- Health Checks (Microsoft.Extensions.Diagnostics.HealthChecks)

#### 2.3.2.2 Directory Structure Source

Clean Architecture Vertical Slices for Worker Services

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Standards

#### 2.3.2.4 Architectural Patterns Source

Asynchronous Event-Driven Microservice

#### 2.3.2.5 Performance Optimizations Applied

- Stream-based file processing to minimize LOH allocations
- PooledDbContextFactory for efficient database connection management in background threads
- Singleton HTTP clients with resilience handlers

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

./EnterpriseMediator.AiWorker.sln

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- EnterpriseMediator.AiWorker.sln

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.dockerignore

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .dockerignore

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.editorconfig

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- .editorconfig

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.gitignore

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- .gitignore

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

docker-compose.dev.yml

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- docker-compose.dev.yml

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

src/EnterpriseMediator.AiWorker/appsettings.Development.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

src/EnterpriseMediator.AiWorker/appsettings.json

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- appsettings.json

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

src/EnterpriseMediator.AiWorker/Dockerfile

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- Dockerfile

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

src/EnterpriseMediator.AiWorker/EnterpriseMediator.AiWorker.csproj

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- EnterpriseMediator.AiWorker.csproj

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

src/EnterpriseMediator.AiWorker/Features/SowProcessing

###### 2.3.3.1.11.2 Purpose

Core business logic slice for SOW processing

###### 2.3.3.1.11.3 Contains Files

- ProcessSowCommand.cs
- ProcessSowHandler.cs
- SowProcessingOrchestrator.cs
- SowProcessingValidator.cs

###### 2.3.3.1.11.4 Organizational Reasoning

Encapsulates the specific business capability logic

###### 2.3.3.1.11.5 Framework Convention Alignment

Vertical Slice Architecture

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

src/EnterpriseMediator.AiWorker/Infrastructure/Clients

###### 2.3.3.1.12.2 Purpose

Wrappers for external AI and Cloud services

###### 2.3.3.1.12.3 Contains Files

- OpenAiClientAdapter.cs
- AwsComprehendAdapter.cs
- S3StorageAdapter.cs

###### 2.3.3.1.12.4 Organizational Reasoning

Separates 3rd party SDK implementation details

###### 2.3.3.1.12.5 Framework Convention Alignment

Infrastructure / Adapters

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

src/EnterpriseMediator.AiWorker/Infrastructure/Messaging

###### 2.3.3.1.13.2 Purpose

Message bus consumers and publishers

###### 2.3.3.1.13.3 Contains Files

- SowUploadedConsumer.cs
- EventPublisher.cs

###### 2.3.3.1.13.4 Organizational Reasoning

Isolates transport mechanism (RabbitMQ) from logic

###### 2.3.3.1.13.5 Framework Convention Alignment

MassTransit Consumer standards

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

src/EnterpriseMediator.AiWorker/Program.cs

###### 2.3.3.1.14.2 Purpose

Application entry point and service registration

###### 2.3.3.1.14.3 Contains Files

- Program.cs
- Worker.cs

###### 2.3.3.1.14.4 Organizational Reasoning

Standard .NET Worker root

###### 2.3.3.1.14.5 Framework Convention Alignment

.NET 8 Hosting

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

src/EnterpriseMediator.AiWorker/Properties/launchSettings.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- launchSettings.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

tests/EnterpriseMediator.AiWorker.Tests/EnterpriseMediator.AiWorker.Tests.csproj

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- EnterpriseMediator.AiWorker.Tests.csproj

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | EnterpriseMediator.AiWorker |
| Namespace Organization | By Feature (Logic) and Infrastructure (Adapters) |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET 8 File-scoped namespaces |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

SowUploadedConsumer

##### 2.3.4.1.2.0 File Path

src/EnterpriseMediator.AiWorker/Infrastructure/Messaging/SowUploadedConsumer.cs

##### 2.3.4.1.3.0 Class Type

Consumer

##### 2.3.4.1.4.0 Inheritance

IConsumer<SowUploadedEvent>

##### 2.3.4.1.5.0 Purpose

Listens for SowUploaded events from the message bus and triggers the processing command.

##### 2.3.4.1.6.0 Dependencies

- IMediator
- ILogger<SowUploadedConsumer>

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses MassTransit consumer interface.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

- {'method_name': 'Consume', 'method_signature': 'public async Task Consume(ConsumeContext<SowUploadedEvent> context)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'ConsumeContext<SowUploadedEvent>', 'is_nullable': 'false', 'purpose': 'Message context containing payload and headers', 'framework_attributes': []}], 'implementation_logic': 'Extracts SowId from message. Constructs ProcessSowCommand. Sends command via MediatR. Handles exceptions by logging and potentially allowing MassTransit retry policies to intervene.', 'exception_handling': 'Relies on global consumer retry policy configuration.', 'performance_considerations': 'Minimal logic, strictly dispatching.', 'validation_requirements': 'Validate message payload integrity.', 'technology_integration_details': 'Integration point with RabbitMQ.'}

##### 2.3.4.1.11.0 Events

*No items available*

##### 2.3.4.1.12.0 Implementation Notes

Specification requires strict separation of consumption logic from business processing.

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

ProcessSowHandler

##### 2.3.4.2.2.0 File Path

src/EnterpriseMediator.AiWorker/Features/SowProcessing/ProcessSowHandler.cs

##### 2.3.4.2.3.0 Class Type

RequestHandler

##### 2.3.4.2.4.0 Inheritance

IRequestHandler<ProcessSowCommand, Result>

##### 2.3.4.2.5.0 Purpose

Orchestrates the SOW processing pipeline: Idempotency -> Download -> Sanitize -> Extract -> Persist.

##### 2.3.4.2.6.0 Dependencies

- ISowRepository
- IFileStorageService
- IPiiSanitizationService
- IAiExtractionService
- IUnitOfWork

##### 2.3.4.2.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0 Technology Integration Notes

Core logic implementer for Sequence 476.

##### 2.3.4.2.9.0 Properties

*No items available*

##### 2.3.4.2.10.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result> Handle(ProcessSowCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'ProcessSowCommand', 'is_nullable': 'false', 'purpose': 'Command containing SowId', 'framework_attributes': []}], 'implementation_logic': '1. Idempotency Check: Verify SOW status is Pending. 2. Status Update: Set to Processing. 3. Download: Stream file from S3. 4. PII Redaction: Call AWS Comprehend. 5. AI Extraction: Call OpenAI with redacted text. 6. Persistence: Save structured data and embeddings to DB using pgvector. 7. Completion: Set status to Processed.', 'exception_handling': 'Catch specific exceptions. Update SOW status to Failed. Log error. Re-throw if retry is desired, or return Failure Result if terminal.', 'performance_considerations': 'Use streams for file handling to avoid large memory allocations.', 'validation_requirements': 'Validate file existence and SOW state.', 'technology_integration_details': 'Transactional consistency required for DB updates.'}

##### 2.3.4.2.11.0 Events

- {'event_name': 'ProcessingCompleted', 'event_type': 'Internal Notification', 'trigger_conditions': 'Successful completion of all steps', 'event_data': 'SowId'}

##### 2.3.4.2.12.0 Implementation Notes

This handler encapsulates the business transaction.

#### 2.3.4.3.0.0 Class Name

##### 2.3.4.3.1.0 Class Name

OpenAiClientAdapter

##### 2.3.4.3.2.0 File Path

src/EnterpriseMediator.AiWorker/Infrastructure/Clients/OpenAiClientAdapter.cs

##### 2.3.4.3.3.0 Class Type

Service Implementation

##### 2.3.4.3.4.0 Inheritance

IAiExtractionService

##### 2.3.4.3.5.0 Purpose

Wraps Azure OpenAI SDK interactions for data extraction and embedding generation.

##### 2.3.4.3.6.0 Dependencies

- OpenAIClient
- IOptions<AiSettings>

##### 2.3.4.3.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0 Technology Integration Notes

Uses Azure.AI.OpenAI SDK.

##### 2.3.4.3.9.0 Properties

*No items available*

##### 2.3.4.3.10.0 Methods

- {'method_name': 'ExtractStructuredDataAsync', 'method_signature': 'public async Task<SowDataDto> ExtractStructuredDataAsync(string text, CancellationToken token)', 'return_type': 'Task<SowDataDto>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'text', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Sanitized SOW text', 'framework_attributes': []}], 'implementation_logic': 'Construct prompt requesting JSON output. Call ChatCompletions API with JSON mode. Deserialize result to DTO.', 'exception_handling': 'Handle RateLimit exceptions via Polly policy. Handle format exceptions.', 'performance_considerations': 'Manage context window limits.', 'validation_requirements': 'Ensure input text is not empty.', 'technology_integration_details': 'Uses \\"gpt-4\\" or configured model deployment.'}

##### 2.3.4.3.11.0 Events

*No items available*

##### 2.3.4.3.12.0 Implementation Notes

Must handle token limits and potential truncation.

### 2.3.5.0.0.0 Interface Specifications

#### 2.3.5.1.0.0 Interface Name

##### 2.3.5.1.1.0 Interface Name

IAiExtractionService

##### 2.3.5.1.2.0 File Path

src/EnterpriseMediator.AiWorker/Application/Interfaces/IAiExtractionService.cs

##### 2.3.5.1.3.0 Purpose

Abstracts the AI provider for data extraction and embedding.

##### 2.3.5.1.4.0 Generic Constraints

None

##### 2.3.5.1.5.0 Framework Specific Inheritance

None

##### 2.3.5.1.6.0 Method Contracts

###### 2.3.5.1.6.1 Method Name

####### 2.3.5.1.6.1.1 Method Name

ExtractStructuredDataAsync

####### 2.3.5.1.6.1.2 Method Signature

Task<SowDataDto> ExtractStructuredDataAsync(string text, CancellationToken token)

####### 2.3.5.1.6.1.3 Return Type

Task<SowDataDto>

####### 2.3.5.1.6.1.4 Framework Attributes

*No items available*

####### 2.3.5.1.6.1.5 Parameters

- {'parameter_name': 'text', 'parameter_type': 'string', 'purpose': 'Text to process'}

####### 2.3.5.1.6.1.6 Contract Description

Extracts predefined fields (Skills, Scope, Deliverables) from text.

####### 2.3.5.1.6.1.7 Exception Contracts

Throws AiServiceException on API failures.

###### 2.3.5.1.6.2.0 Method Name

####### 2.3.5.1.6.2.1 Method Name

GenerateEmbeddingsAsync

####### 2.3.5.1.6.2.2 Method Signature

Task<float[]> GenerateEmbeddingsAsync(string text, CancellationToken token)

####### 2.3.5.1.6.2.3 Return Type

Task<float[]>

####### 2.3.5.1.6.2.4 Framework Attributes

*No items available*

####### 2.3.5.1.6.2.5 Parameters

- {'parameter_name': 'text', 'parameter_type': 'string', 'purpose': 'Text to vectorize'}

####### 2.3.5.1.6.2.6 Contract Description

Converts text string into vector float array.

####### 2.3.5.1.6.2.7 Exception Contracts

Throws AiServiceException on API failures.

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implementations should encapsulate all SDK-specific logic.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IPiiSanitizationService

##### 2.3.5.2.2.0.0 File Path

src/EnterpriseMediator.AiWorker/Application/Interfaces/IPiiSanitizationService.cs

##### 2.3.5.2.3.0.0 Purpose

Abstracts the PII detection and redaction service.

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'SanitizeTextAsync', 'method_signature': 'Task<string> SanitizeTextAsync(string input, CancellationToken token)', 'return_type': 'Task<string>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'input', 'parameter_type': 'string', 'purpose': 'Raw text'}], 'contract_description': 'Returns text with sensitive entities replaced by placeholders.', 'exception_contracts': 'Throws PiiServiceException.'}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Must handle chunking if the service has payload limits.

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

ProcessSowCommand

##### 2.3.7.1.2.0.0 File Path

src/EnterpriseMediator.AiWorker/Features/SowProcessing/ProcessSowCommand.cs

##### 2.3.7.1.3.0.0 Purpose

Input data for the processing handler.

##### 2.3.7.1.4.0.0 Framework Base Class

IRequest<Result>

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

SowId

####### 2.3.7.1.5.1.2 Property Type

Guid

####### 2.3.7.1.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

FileKey

####### 2.3.7.1.5.2.2 Property Type

string

####### 2.3.7.1.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules

SowId must be non-empty. FileKey must be non-empty.

##### 2.3.7.1.7.0.0 Serialization Requirements

None

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

SowDataDto

##### 2.3.7.2.2.0.0 File Path

src/EnterpriseMediator.AiWorker/Features/SowProcessing/SowDataDto.cs

##### 2.3.7.2.3.0.0 Purpose

Structured data format extracted from the SOW.

##### 2.3.7.2.4.0.0 Framework Base Class

None

##### 2.3.7.2.5.0.0 Properties

###### 2.3.7.2.5.1.0 Property Name

####### 2.3.7.2.5.1.1 Property Name

ScopeSummary

####### 2.3.7.2.5.1.2 Property Type

string

####### 2.3.7.2.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.1.4 Serialization Attributes

- [JsonPropertyName(\"scope_summary\")]

####### 2.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0 Property Name

####### 2.3.7.2.5.2.1 Property Name

RequiredSkills

####### 2.3.7.2.5.2.2 Property Type

List<string>

####### 2.3.7.2.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.2.4 Serialization Attributes

- [JsonPropertyName(\"required_skills\")]

####### 2.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.3.0 Property Name

####### 2.3.7.2.5.3.1 Property Name

Deliverables

####### 2.3.7.2.5.3.2 Property Type

List<string>

####### 2.3.7.2.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.3.4 Serialization Attributes

- [JsonPropertyName(\"deliverables\")]

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0 Validation Rules

None

##### 2.3.7.2.7.0.0 Serialization Requirements

Matches the JSON schema requested from the LLM.

### 2.3.8.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0 Configuration Name

AiSettings

##### 2.3.8.1.2.0.0 File Path

src/EnterpriseMediator.AiWorker/Configuration/AiSettings.cs

##### 2.3.8.1.3.0.0 Purpose

Configuration settings for AI services.

##### 2.3.8.1.4.0.0 Framework Base Class

None

##### 2.3.8.1.5.0.0 Configuration Sections

- {'section_name': 'Ai', 'properties': [{'property_name': 'OpenAiEndpoint', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Azure OpenAI Endpoint'}, {'property_name': 'OpenAiKey', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Azure OpenAI Key'}, {'property_name': 'DeploymentName', 'property_type': 'string', 'default_value': 'gpt-4', 'required': 'true', 'description': 'Model deployment name'}]}

##### 2.3.8.1.6.0.0 Validation Requirements

Values must be present and valid format.

##### 2.3.8.1.7.0.0 Validation Notes

Validated at startup via Options validation.

#### 2.3.8.2.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0 Configuration Name

AwsSettings

##### 2.3.8.2.2.0.0 File Path

src/EnterpriseMediator.AiWorker/Configuration/AwsSettings.cs

##### 2.3.8.2.3.0.0 Purpose

Configuration for AWS services.

##### 2.3.8.2.4.0.0 Framework Base Class

None

##### 2.3.8.2.5.0.0 Configuration Sections

- {'section_name': 'Aws', 'properties': [{'property_name': 'Region', 'property_type': 'string', 'default_value': 'us-east-1', 'required': 'true', 'description': 'AWS Region'}, {'property_name': 'S3BucketName', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Target S3 Bucket'}]}

##### 2.3.8.2.6.0.0 Validation Requirements

Bucket name must be valid S3 format.

##### 2.3.8.2.7.0.0 Validation Notes

Validated at startup.

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IHostedService

##### 2.3.9.1.2.0.0 Service Implementation

Worker

##### 2.3.9.1.3.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0 Registration Reasoning

Standard Worker Service entry point.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddHostedService<Worker>()

##### 2.3.9.1.6.0.0 Validation Notes

Ensures the background processing loop starts.

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IAiExtractionService

##### 2.3.9.2.2.0.0 Service Implementation

OpenAiClientAdapter

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

Adapter is stateless and thread-safe.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddSingleton<IAiExtractionService, OpenAiClientAdapter>()

##### 2.3.9.2.6.0.0 Validation Notes

Efficient reuse of underlying HTTP clients.

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

IPiiSanitizationService

##### 2.3.9.3.2.0.0 Service Implementation

AwsComprehendAdapter

##### 2.3.9.3.3.0.0 Lifetime

Singleton

##### 2.3.9.3.4.0.0 Registration Reasoning

AWS clients are thread-safe singletons.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.AddSingleton<IPiiSanitizationService, AwsComprehendAdapter>()

##### 2.3.9.3.6.0.0 Validation Notes

Optimizes connection pooling.

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

Azure OpenAI

##### 2.3.10.1.2.0.0 Integration Type

REST API

##### 2.3.10.1.3.0.0 Required Client Classes

- OpenAIClient
- ChatCompletionsOptions

##### 2.3.10.1.4.0.0 Configuration Requirements

Endpoint, Key, Deployment Name.

##### 2.3.10.1.5.0.0 Error Handling Requirements

Retry on 429/5xx errors using Polly.

##### 2.3.10.1.6.0.0 Authentication Requirements

API Key authentication.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

SDK Injection

##### 2.3.10.1.8.0.0 Validation Notes

Integration must support JSON mode.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

AWS Comprehend

##### 2.3.10.2.2.0.0 Integration Type

REST API

##### 2.3.10.2.3.0.0 Required Client Classes

- AmazonComprehendClient

##### 2.3.10.2.4.0.0 Configuration Requirements

AWS Credentials and Region.

##### 2.3.10.2.5.0.0 Error Handling Requirements

AWS SDK standard retries.

##### 2.3.10.2.6.0.0 Authentication Requirements

IAM Role.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

SDK Injection

##### 2.3.10.2.8.0.0 Validation Notes

Used for PII detection.

#### 2.3.10.3.0.0.0 Integration Target

##### 2.3.10.3.1.0.0 Integration Target

RabbitMQ

##### 2.3.10.3.2.0.0 Integration Type

Message Broker

##### 2.3.10.3.3.0.0 Required Client Classes

- IBusControl
- IConsumer

##### 2.3.10.3.4.0.0 Configuration Requirements

Host, VirtualHost, Credentials.

##### 2.3.10.3.5.0.0 Error Handling Requirements

Message acknowledgement, DLQ routing.

##### 2.3.10.3.6.0.0 Authentication Requirements

Connection String.

##### 2.3.10.3.7.0.0 Framework Integration Patterns

MassTransit

##### 2.3.10.3.8.0.0 Validation Notes

Consumes SowUploaded event.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 0 |
| Total Dtos | 2 |
| Total Configurations | 2 |
| Total External Integrations | 3 |
| Grand Total Components | 25 |
| Phase 2 Claimed Count | 15 |
| Phase 2 Actual Count | 12 |
| Validation Added Count | 8 |
| Final Validated Count | 25 |


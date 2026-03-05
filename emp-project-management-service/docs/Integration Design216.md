# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-PROJECT |
| Extraction Timestamp | 2025-05-23T14:45:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-FUNC-009

#### 1.2.1.2 Requirement Text

The system shall allow Admins to create projects and upload SOW documents, triggering an asynchronous AI analysis process.

#### 1.2.1.3 Validation Criteria

- API accepts multipart/form-data for SOW
- SOW file is persisted to secure object storage (S3)
- SowUploaded integration event is published to the message bus

#### 1.2.1.4 Implementation Implications

- Implement 'UploadSowCommandHandler' using 'IFileStorageService'
- Configure MassTransit to publish 'SowUploadedEvent' to the exchange
- Ensure atomic transaction between DB record creation and event publication (Outbox)

#### 1.2.1.5 Extraction Reasoning

Core entry point for the SOW processing workflow involving File I/O and Messaging.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FUNC-014

#### 1.2.2.2 Requirement Text

The system shall perform a semantic search to identify and rank vendors whose skills match the requirements of the approved Project Brief.

#### 1.2.2.3 Validation Criteria

- System executes a vector similarity query against Vendor embeddings
- Results are ranked by cosine similarity distance

#### 1.2.2.4 Implementation Implications

- Implement 'VectorVendorMatchingService' in Infrastructure layer
- Utilize Entity Framework Core with Npgsql.EntityFrameworkCore.PostgreSQL and pgvector
- Execute raw SQL or EF extension methods for 'ORDER BY embedding <-> query_vector'

#### 1.2.2.5 Extraction Reasoning

Determines the specific database integration pattern (pgvector) required for this service.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUN-003

#### 1.2.3.2 Requirement Text

The system shall manage project lifecycle states and trigger downstream financial workflows upon project award.

#### 1.2.3.3 Validation Criteria

- Project status transition to 'Awarded' triggers 'ProjectAwarded' event
- Event payload contains necessary financial data (Amount, VendorId, ProjectId)

#### 1.2.3.4 Implementation Implications

- Define 'ProjectAwardedIntegrationEvent' in Shared Contracts
- Implement domain event handler to bridge Domain Events to Integration Events

#### 1.2.3.5 Extraction Reasoning

Critical integration point with REPO-SVC-FINANCIAL.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

ProjectLifecycleManager

#### 1.3.1.2 Component Specification

Application service responsible for orchestrating state changes (Create, Approve Brief, Award, Complete) and ensuring side effects are propagated.

#### 1.3.1.3 Implementation Requirements

- Use MediatR for command handling
- Implement Transactional Outbox pattern for reliable event publishing

#### 1.3.1.4 Architectural Context

Application Layer - Use Case Orchestrator

#### 1.3.1.5 Extraction Reasoning

Central hub for logic connecting API inputs to Domain state and Event outputs.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

VectorMatchingService

#### 1.3.2.2 Component Specification

Infrastructure service that translates Project Brief requirements into vector queries against the database.

#### 1.3.2.3 Implementation Requirements

- Dependency on OpenAI API (via AI Worker or direct) to vectorize query text if not pre-calculated
- Dependency on Npgsql for vector operations

#### 1.3.2.4 Architectural Context

Infrastructure Layer - Domain Service Implementation

#### 1.3.2.5 Extraction Reasoning

Encapsulates the specialized logic for REQ-FUNC-014.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Application Layer

#### 1.4.1.2 Layer Responsibilities

Orchestrates business use cases, manages transactions, defines ports (interfaces) for infrastructure, and handles DTO mapping.

#### 1.4.1.3 Layer Constraints

- Must not depend on EF Core directly
- Must not depend on HTTP contexts

#### 1.4.1.4 Implementation Patterns

- CQRS (Command Query Responsibility Segregation)
- Mediator Pattern

#### 1.4.1.5 Extraction Reasoning

Standard Clean Architecture layer for business logic orchestration.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Infrastructure Layer

#### 1.4.2.2 Layer Responsibilities

Implements data access, file storage, and message publishing interfaces defined by the Application/Domain layers.

#### 1.4.2.3 Layer Constraints

- Handles specific technology implementations (AWS SDK, Npgsql, MassTransit)

#### 1.4.2.4 Implementation Patterns

- Repository Pattern
- Adapter Pattern

#### 1.4.2.5 Extraction Reasoning

Physical integration point for Cloud and DB resources.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IFileStorageService

#### 1.5.1.2 Source Repository

AWS S3 (Infrastructure)

#### 1.5.1.3 Method Contracts

- {'method_name': 'UploadAsync', 'method_signature': 'Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken ct)', 'method_purpose': 'Persists SOW document to blob storage and returns the object key.', 'integration_context': 'Executed during Handle(UploadSowCommand).'}

#### 1.5.1.4 Integration Pattern

Gateway/Adapter

#### 1.5.1.5 Communication Protocol

HTTPS (AWS SDK)

#### 1.5.1.6 Extraction Reasoning

Required for REQ-FUNC-009 (SOW Upload).

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IUserIntegrationService

#### 1.5.2.2 Source Repository

REPO-SVC-USER

#### 1.5.2.3 Method Contracts

##### 1.5.2.3.1 Method Name

###### 1.5.2.3.1.1 Method Name

ValidateClientExistsAsync

###### 1.5.2.3.1.2 Method Signature

Task<bool> ValidateClientExistsAsync(Guid clientId, CancellationToken ct)

###### 1.5.2.3.1.3 Method Purpose

Ensures a client ID provided in project creation is valid.

###### 1.5.2.3.1.4 Integration Context

Validation pipeline behavior for CreateProjectCommand.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

ValidateVendorExistsAsync

###### 1.5.2.3.2.2 Method Signature

Task<bool> ValidateVendorExistsAsync(Guid vendorId, CancellationToken ct)

###### 1.5.2.3.2.3 Method Purpose

Ensures a vendor ID provided in proposal submission is valid.

###### 1.5.2.3.2.4 Integration Context

Validation pipeline behavior for CreateProposalCommand.

#### 1.5.2.4.0.0 Integration Pattern

Synchronous HTTP/gRPC Client

#### 1.5.2.5.0.0 Communication Protocol

HTTP/2 (gRPC) or REST

#### 1.5.2.6.0.0 Extraction Reasoning

Ensures referential integrity across microservice boundaries.

### 1.5.3.0.0.0 Interface Name

#### 1.5.3.1.0.0 Interface Name

IPublishEndpoint

#### 1.5.3.2.0.0 Source Repository

Message Broker (RabbitMQ)

#### 1.5.3.3.0.0 Method Contracts

- {'method_name': 'Publish', 'method_signature': 'Task Publish<T>(T message, CancellationToken ct)', 'method_purpose': 'Broadcasts integration events to the bus.', 'integration_context': 'Post-transaction commit in Command Handlers.'}

#### 1.5.3.4.0.0 Integration Pattern

Asynchronous Pub/Sub

#### 1.5.3.5.0.0 Communication Protocol

AMQP

#### 1.5.3.6.0.0 Extraction Reasoning

Mechanism for driving the Event-Driven Architecture.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

Project Management API

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-GW-API

#### 1.6.1.3.0.0 Method Contracts

##### 1.6.1.3.1.0 Method Name

###### 1.6.1.3.1.1 Method Name

POST /api/v1/projects

###### 1.6.1.3.1.2 Method Signature

ActionResult<Guid> CreateProject([FromBody] CreateProjectRequest request)

###### 1.6.1.3.1.3 Method Purpose

Creates a new project entity.

###### 1.6.1.3.1.4 Implementation Requirements

Authorize: Admin only. Validates ClientId via IUserIntegrationService.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

POST /api/v1/projects/{id}/sow

###### 1.6.1.3.2.2 Method Signature

ActionResult UploadSow(Guid id, IFormFile file)

###### 1.6.1.3.2.3 Method Purpose

Uploads SOW and triggers processing.

###### 1.6.1.3.2.4 Implementation Requirements

Multipart/form-data support. Publishes SowUploadedEvent.

##### 1.6.1.3.3.0 Method Name

###### 1.6.1.3.3.1 Method Name

POST /api/v1/projects/{id}/award

###### 1.6.1.3.3.2 Method Signature

ActionResult AwardProject(Guid id, [FromBody] AwardProjectRequest request)

###### 1.6.1.3.3.3 Method Purpose

Awards project to a vendor proposal.

###### 1.6.1.3.3.4 Implementation Requirements

Updates state to Awarded. Publishes ProjectAwardedEvent.

#### 1.6.1.4.0.0 Service Level Requirements

- P95 Response Time < 250ms
- High Availability

#### 1.6.1.5.0.0 Implementation Constraints

- Input validation using FluentValidation
- Idempotency support using RequestId header

#### 1.6.1.6.0.0 Extraction Reasoning

Primary REST surface area consumed by the BFF/Gateway.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

Project Integration Events

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-SVC-AIWORKER
- REPO-SVC-FINANCIAL
- REPO-SVC-USER

#### 1.6.2.3.0.0 Method Contracts

##### 1.6.2.3.1.0 Method Name

###### 1.6.2.3.1.1 Method Name

SowUploadedEvent

###### 1.6.2.3.1.2 Method Signature

record SowUploadedEvent(Guid ProjectId, Guid SowId, string S3Key)

###### 1.6.2.3.1.3 Method Purpose

Triggers AI Worker to begin processing.

###### 1.6.2.3.1.4 Implementation Requirements

Must be published transactionally (Outbox).

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

ProjectAwardedEvent

###### 1.6.2.3.2.2 Method Signature

record ProjectAwardedEvent(Guid ProjectId, Guid VendorId, decimal Amount, string Currency)

###### 1.6.2.3.2.3 Method Purpose

Triggers Financial Service to generate Invoice.

###### 1.6.2.3.2.4 Implementation Requirements

Must contain agreed financial terms from Proposal.

#### 1.6.2.4.0.0 Service Level Requirements

- Guaranteed Delivery (At-least-once)

#### 1.6.2.5.0.0 Implementation Constraints

- Schema must match REPO-LIB-CONTRACTS definitions

#### 1.6.2.6.0.0 Extraction Reasoning

The asynchronous contract this service fulfills for the ecosystem.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

ASP.NET Core 8 Web API

### 1.7.2.0.0.0 Integration Technologies

- MassTransit (RabbitMQ)
- Entity Framework Core 8 (PostgreSQL + pgvector)
- AWS SDK (S3)
- Polly (Resilience)

### 1.7.3.0.0.0 Performance Constraints

Search queries must utilize HNSW indexes for vector similarity to meet < 500ms response targets.

### 1.7.4.0.0.0 Security Requirements

Service-to-Service communication must be authenticated (mTLS or Token). Public endpoints secured via Gateway JWT.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all inbound (API, Events) and outbound (DB,... |
| Cross Reference Validation | Validated Event schemas against Consumer needs (Fi... |
| Implementation Readiness Assessment | High. Contracts, technologies, and patterns are ex... |
| Quality Assurance Confirmation | Integration patterns (Outbox, Circuit Breaker) inc... |


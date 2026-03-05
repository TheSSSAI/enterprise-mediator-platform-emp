# 1 Analysis Metadata

| Property | Value |
|----------|-------|
| Analysis Timestamp | 2025-05-15T14:30:00Z |
| Repository Component Id | REPO-SVC-AIWORKER |
| Analysis Completeness Score | 100 |
| Critical Findings Count | 4 |
| Analysis Methodology | Systematic decomposition of .NET 8 Worker Service ... |

# 2 Repository Analysis

## 2.1 Repository Definition

### 2.1.1 Scope Boundaries

- Asynchronous ingestion and processing of SOW documents triggered by message bus events
- Orchestration of AI services (AWS Comprehend, OpenAI) for data extraction and vectorization
- Strict isolation from user-facing API performance via background processing pattern

### 2.1.2 Technology Stack

- .NET 8 Worker Service (HostApplicationBuilder)
- RabbitMQ (MassTransit or Raw Client)
- AWS SDK for .NET (S3, Comprehend)
- Azure.AI.OpenAI (Client SDK)
- TikaOnDotNet (Document Text Extraction)
- Entity Framework Core 8 (Npgsql, pgvector)

### 2.1.3 Architectural Constraints

- Must implement idempotent message processing to handle potential duplicate queue deliveries
- Must utilize the .NET 8 BackgroundService base class for lifecycle management
- Must handle long-running operations without blocking thread pool threads (async/await)
- Must implement resilient retry policies (Polly) for external AI and Cloud API calls

### 2.1.4 Dependency Relationships

#### 2.1.4.1 Upstream_Trigger: REPO-GW-API

##### 2.1.4.1.1 Dependency Type

Upstream_Trigger

##### 2.1.4.1.2 Target Component

REPO-GW-API

##### 2.1.4.1.3 Integration Pattern

Asynchronous Messaging (AMQP)

##### 2.1.4.1.4 Reasoning

Consumes 'SowUploaded' events published by the API layer to decouple upload from processing.

#### 2.1.4.2.0 Infrastructure_Service: AWS S3

##### 2.1.4.2.1 Dependency Type

Infrastructure_Service

##### 2.1.4.2.2 Target Component

AWS S3

##### 2.1.4.2.3 Integration Pattern

SDK / Rest API

##### 2.1.4.2.4 Reasoning

Retrieves the raw SOW document stream for processing.

#### 2.1.4.3.0 AI_Service: OpenAI API

##### 2.1.4.3.1 Dependency Type

AI_Service

##### 2.1.4.3.2 Target Component

OpenAI API

##### 2.1.4.3.3 Integration Pattern

SDK / HTTP

##### 2.1.4.3.4 Reasoning

Performs LLM-based entity extraction and generates vector embeddings.

#### 2.1.4.4.0 Data_Persistence: PostgreSQL (pgvector)

##### 2.1.4.4.1 Dependency Type

Data_Persistence

##### 2.1.4.4.2 Target Component

PostgreSQL (pgvector)

##### 2.1.4.4.3 Integration Pattern

ORM (EF Core)

##### 2.1.4.4.4 Reasoning

Persists extracted project briefs and vector embeddings for semantic search.

### 2.1.5.0.0 Analysis Insights

This repository acts as a classic 'Worker' in a Competing Consumers pattern. It is computationally intensive and I/O bound. The structure must strictly separate the 'Hosting' logic (RabbitMQ listener) from the 'Business' logic (AI orchestration) to allow for unit testing of the AI workflows without the messaging infrastructure.

# 3.0.0.0.0 Requirements Mapping

## 3.1.0.0.0 Functional Requirements

### 3.1.1.0.0 Requirement Id

#### 3.1.1.1.0 Requirement Id

REQ-FUNC-010

#### 3.1.1.2.0 Requirement Description

The system shall process uploaded SOW documents asynchronously.

#### 3.1.1.3.0 Implementation Implications

- Implementation of a BackgroundService listening to RabbitMQ
- State management in Postgres to update status (PROCESSING -> PROCESSED/FAILED)

#### 3.1.1.4.0 Required Components

- SowProcessingWorker
- SowStatusService

#### 3.1.1.5.0 Analysis Reasoning

Direct mapping to the core responsibility of this worker service.

### 3.1.2.0.0 Requirement Id

#### 3.1.2.1.0 Requirement Id

REQ-FUNC-002

#### 3.1.2.2.0 Requirement Description

AI analysis of SOW including PII sanitization and structured data extraction.

#### 3.1.2.3.0 Implementation Implications

- Integration with AWS Comprehend for PII detection
- Integration with OpenAI for JSON extraction
- Integration with TikaOnDotNet for text extraction from PDF/DOCX

#### 3.1.2.4.0 Required Components

- PiiSanitizationService
- DataExtractionService
- DocumentParser

#### 3.1.2.5.0 Analysis Reasoning

Defines the specific orchestration steps within the worker's processing pipeline.

### 3.1.3.0.0 Requirement Id

#### 3.1.3.1.0 Requirement Id

REQ-FUNC-014

#### 3.1.3.2.0 Requirement Description

Generate vector embeddings for semantic search.

#### 3.1.3.3.0 Implementation Implications

- Call to OpenAI Embeddings API
- Storage of vectors using pgvector

#### 3.1.3.4.0 Required Components

- VectorEmbeddingService
- ProjectBriefRepository

#### 3.1.3.5.0 Analysis Reasoning

Required to enable the downstream semantic search capability.

## 3.2.0.0.0 Non Functional Requirements

### 3.2.1.0.0 Requirement Type

#### 3.2.1.1.0 Requirement Type

Reliability

#### 3.2.1.2.0 Requirement Specification

Handle processing failures gracefully and update status to 'Failed'.

#### 3.2.1.3.0 Implementation Impact

Global exception handling wrapper around the processing logic; Dead Letter Queue configuration.

#### 3.2.1.4.0 Design Constraints

- Transactional outbox or atomic updates for status changes

#### 3.2.1.5.0 Analysis Reasoning

Critical for system observability and preventing 'stuck' processing states.

### 3.2.2.0.0 Requirement Type

#### 3.2.2.1.0 Requirement Type

Scalability

#### 3.2.2.2.0 Requirement Specification

Scale horizontally based on resource utilization (REQ-SCAL-001).

#### 3.2.2.3.0 Implementation Impact

Stateless worker design; Containerization compatibility; Kubernetes readiness probes.

#### 3.2.2.4.0 Design Constraints

- No local file system persistence (use streams or temp folders only)

#### 3.2.2.5.0 Analysis Reasoning

Ensures the worker can scale independently of the API to handle bursts of uploads.

## 3.3.0.0.0 Requirements Analysis Summary

The repository is strictly bound by the SOW processing lifecycle. It must handle high-latency external API calls (OpenAI) robustly, requiring advanced resilience patterns like Circuit Breakers and Retries via Microsoft.Extensions.Http.Resilience.

# 4.0.0.0.0 Architecture Analysis

## 4.1.0.0.0 Architectural Patterns

### 4.1.1.0.0 Pattern Name

#### 4.1.1.1.0 Pattern Name

Event-Driven Architecture

#### 4.1.1.2.0 Pattern Application

Decouples the upload action (API) from the processing action (Worker).

#### 4.1.1.3.0 Required Components

- MessageBroker (RabbitMQ)
- EventConsumer

#### 4.1.1.4.0 Implementation Strategy

Use MassTransit or raw RabbitMQ client within a BackgroundService to consume messages.

#### 4.1.1.5.0 Analysis Reasoning

Mandated by the need for asynchronous processing and non-blocking UI.

### 4.1.2.0.0 Pattern Name

#### 4.1.2.1.0 Pattern Name

Pipeline Pattern

#### 4.1.2.2.0 Pattern Application

Sequential processing of the SOW document (Download -> Parse -> Sanitize -> Extract -> Embed).

#### 4.1.2.3.0 Required Components

- OrchestratorService

#### 4.1.2.4.0 Implementation Strategy

Implement a 'SowProcessingOrchestrator' that calls distinct domain services in order.

#### 4.1.2.5.0 Analysis Reasoning

Ensures separation of concerns and testability of individual processing steps.

### 4.1.3.0.0 Pattern Name

#### 4.1.3.1.0 Pattern Name

Options Pattern

#### 4.1.3.2.0 Pattern Application

Strongly typed configuration for external SDKs.

#### 4.1.3.3.0 Required Components

- AwsOptions
- OpenAiOptions

#### 4.1.3.4.0 Implementation Strategy

Use IOptions<T> with DataAnnotation validation at startup.

#### 4.1.3.5.0 Analysis Reasoning

.NET 8 standard for managing configuration complexity and validation.

## 4.2.0.0.0 Integration Points

### 4.2.1.0.0 Integration Type

#### 4.2.1.1.0 Integration Type

Asynchronous_Messaging

#### 4.2.1.2.0 Target Components

- RabbitMQ

#### 4.2.1.3.0 Communication Pattern

Subscribe/Listen

#### 4.2.1.4.0 Interface Requirements

- IIntegrationEventBus
- IConsumer<SowUploaded>

#### 4.2.1.5.0 Analysis Reasoning

The entry point for the worker's logic.

### 4.2.2.0.0 Integration Type

#### 4.2.2.1.0 Integration Type

External_API_Command

#### 4.2.2.2.0 Target Components

- AWS Comprehend
- OpenAI API

#### 4.2.2.3.0 Communication Pattern

Request/Response (Async)

#### 4.2.2.4.0 Interface Requirements

- IAiService
- IPiiService

#### 4.2.2.5.0 Analysis Reasoning

Core business logic relies on these external cognitive services.

## 4.3.0.0.0 Layering Strategy

| Property | Value |
|----------|-------|
| Layer Organization | Vertical Slices (Feature-based) wrapped in a Worke... |
| Component Placement | Host logic in 'Workers/', Orchestration in 'Servic... |
| Analysis Reasoning | Aligns with the provided .NET 8 Worker Service str... |

# 5.0.0.0.0 Database Analysis

## 5.1.0.0.0 Entity Mappings

### 5.1.1.0.0 Entity Name

#### 5.1.1.1.0 Entity Name

Project

#### 5.1.1.2.0 Database Table

Projects

#### 5.1.1.3.0 Required Properties

- Id
- Status

#### 5.1.1.4.0 Relationship Mappings

- HasOne ProjectBrief

#### 5.1.1.5.0 Access Patterns

- Read-Only (for context)

#### 5.1.1.6.0 Analysis Reasoning

Needed to verify project existence and state before processing.

### 5.1.2.0.0 Entity Name

#### 5.1.2.1.0 Entity Name

SowDocument

#### 5.1.2.2.0 Database Table

SowDocuments

#### 5.1.2.3.0 Required Properties

- Id
- ProcessingStatus
- StorageKey

#### 5.1.2.4.0 Relationship Mappings

- BelongsTo Project

#### 5.1.2.5.0 Access Patterns

- Read StorageKey
- Update ProcessingStatus

#### 5.1.2.6.0 Analysis Reasoning

The primary entity being processed. State transitions are critical.

### 5.1.3.0.0 Entity Name

#### 5.1.3.1.0 Entity Name

ProjectBrief

#### 5.1.3.2.0 Database Table

ProjectBriefs

#### 5.1.3.3.0 Required Properties

- ExtractedData (JSON)
- VectorEmbedding (vector)

#### 5.1.3.4.0 Relationship Mappings

- BelongsTo Project

#### 5.1.3.5.0 Access Patterns

- Create/Update

#### 5.1.3.6.0 Analysis Reasoning

The output of the processing worker.

## 5.2.0.0.0 Data Access Requirements

- {'operation_type': 'Transactional_Write', 'required_methods': ['SaveProcessedBriefAsync'], 'performance_constraints': 'Must commit status update and data insertion atomically.', 'analysis_reasoning': "Prevents data inconsistency where a document is marked 'Processed' but data is missing."}

## 5.3.0.0.0 Persistence Strategy

| Property | Value |
|----------|-------|
| Orm Configuration | Entity Framework Core 8 |
| Migration Requirements | Worker service does NOT run migrations; it expects... |
| Analysis Reasoning | Separation of concerns; migrations should be handl... |

# 6.0.0.0.0 Sequence Analysis

## 6.1.0.0.0 Interaction Patterns

- {'sequence_name': 'SOW Processing Pipeline', 'repository_role': 'Orchestrator', 'required_interfaces': ['IStorageService', 'IDocumentParser', 'IPiiSanitizer', 'IAiExtractor', 'IRepository'], 'method_specifications': [{'method_name': 'ProcessSowAsync', 'interaction_context': 'Triggered by SowUploaded event', 'parameter_analysis': 'SowUploadedEvent message (containing SowId, ProjectId)', 'return_type_analysis': 'Task', 'analysis_reasoning': 'The main entry point for the business logic.'}, {'method_name': 'ExtractDataAsync', 'interaction_context': 'After PII sanitization', 'parameter_analysis': 'SanitizedText string', 'return_type_analysis': 'Task<ExtractedDataDto>', 'analysis_reasoning': 'Calls OpenAI to get structured JSON from unstructured text.'}], 'analysis_reasoning': 'Matches Sequence Diagram #476 strictly.'}

## 6.2.0.0.0 Communication Protocols

### 6.2.1.0.0 Protocol Type

#### 6.2.1.1.0 Protocol Type

AMQP

#### 6.2.1.2.0 Implementation Requirements

RabbitMQ Consumer with Manual Acknowledgement (Ack/Nack).

#### 6.2.1.3.0 Analysis Reasoning

Ensures messages are not lost if the worker crashes mid-process.

### 6.2.2.0.0 Protocol Type

#### 6.2.2.1.0 Protocol Type

HTTPS

#### 6.2.2.2.0 Implementation Requirements

Resilient HTTP Client with Retries for AWS and OpenAI calls.

#### 6.2.2.3.0 Analysis Reasoning

Required for reliability against transient external service failures.

# 7.0.0.0.0 Critical Analysis Findings

## 7.1.0.0.0 Finding Category

### 7.1.1.0.0 Finding Category

Resilience

### 7.1.2.0.0 Finding Description

OpenAI API dependencies are prone to rate limiting (429) and timeouts.

### 7.1.3.0.0 Implementation Impact

Must implement Polly policies specifically handling 429 Too Many Requests with exponential backoff and jitter.

### 7.1.4.0.0 Priority Level

High

### 7.1.5.0.0 Analysis Reasoning

Without this, the worker will fail frequently under load, causing message backlog.

## 7.2.0.0.0 Finding Category

### 7.2.1.0.0 Finding Category

Data_Privacy

### 7.2.2.0.0 Finding Description

PII Sanitization (AWS Comprehend) must occur BEFORE sending data to OpenAI.

### 7.2.3.0.0 Implementation Impact

The processing pipeline order is strict: Download -> Parse -> Sanitize -> OpenAI. The Sanitized text must be the payload for OpenAI.

### 7.2.4.0.0 Priority Level

High

### 7.2.5.0.0 Analysis Reasoning

Regulatory compliance (GDPR) and system requirements mandate PII protection.

## 7.3.0.0.0 Finding Category

### 7.3.1.0.0 Finding Category

Performance

### 7.3.2.0.0 Finding Description

PDF/Docx parsing can be memory intensive.

### 7.3.3.0.0 Implementation Impact

Streams should be used throughout the pipeline (S3 -> Tika) rather than loading full files into byte arrays where possible.

### 7.3.4.0.0 Priority Level

Medium

### 7.3.5.0.0 Analysis Reasoning

Prevents OutOfMemory exceptions in containerized environments with limited RAM.

## 7.4.0.0.0 Finding Category

### 7.4.1.0.0 Finding Category

Idempotency

### 7.4.2.0.0 Finding Description

Message redelivery is possible.

### 7.4.3.0.0 Implementation Impact

Worker must check if SOW status is already 'Processed' before starting work.

### 7.4.4.0.0 Priority Level

Medium

### 7.4.5.0.0 Analysis Reasoning

Prevents wasted credits on AI calls and duplicate data generation.

# 8.0.0.0.0 Analysis Traceability

## 8.1.0.0.0 Cached Context Utilization

Utilized Sequence Diagram #476 for pipeline steps, Requirement REQ-FUN-002 for AI logic, and Repository definition for tech stack.

## 8.2.0.0.0 Analysis Decision Trail

- Selected .NET 8 BackgroundService over external functions for better integration with existing EF Core models.
- Chose Polly for resilience due to high dependency on unstable external AI APIs.
- Enforced strict layering (Workers/Services) to match the provided Technology Integration Guide.

## 8.3.0.0.0 Assumption Validations

- Assuming RabbitMQ infrastructure is provisioned and connection strings are injected via Kubernetes/Secrets.
- Assuming AWS and OpenAI API keys are available in configuration.

## 8.4.0.0.0 Cross Reference Checks

- Verified 'pgvector' requirement against Architecture Context.
- Validated SOW Lifecycle states against Sequence #476.


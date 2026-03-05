# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-AIWORKER |
| Extraction Timestamp | 2025-01-26T16:45:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High - Protocols and Contracts Defined |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-FUNC-010

#### 1.2.1.2 Requirement Text

The system shall process uploaded SOW documents asynchronously to avoid blocking the user interface.

#### 1.2.1.3 Validation Criteria

- SOW status updates from 'Processing' to 'Processed' or 'Failed'
- Background task handles long-running AI operations

#### 1.2.1.4 Implementation Implications

- Implement RabbitMQ consumer for 'SowUploadedEvent'
- Implement Idempotency check to prevent duplicate processing
- Publish completion events to trigger downstream notifications

#### 1.2.1.5 Extraction Reasoning

Core architectural driver for this repository.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FUNC-002

#### 1.2.2.2 Requirement Text

AI analysis of SOW including PII sanitization and structured data extraction.

#### 1.2.2.3 Validation Criteria

- PII is redacted before LLM analysis
- JSON data is extracted and validated against schema
- Vector embeddings are generated for search

#### 1.2.2.4 Implementation Implications

- Orchestrate AWS Comprehend API calls
- Orchestrate Azure OpenAI API calls with JSON mode
- Persist vector data using pgvector in PostgreSQL

#### 1.2.2.5 Extraction Reasoning

Defines the external integration touchpoints (AI Services).

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

SowUploadedConsumer

#### 1.3.1.2 Component Specification

MassTransit consumer that handles the entry point for the processing workflow.

#### 1.3.1.3 Implementation Requirements

- Consume SowUploadedEvent
- Acknowledge message only after successful persistence
- Route to DLQ on permanent failure

#### 1.3.1.4 Architectural Context

Infrastructure / Messaging Adapter

#### 1.3.1.5 Extraction Reasoning

Primary entry point for the service.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

SowProcessingOrchestrator

#### 1.3.2.2 Component Specification

Domain service that coordinates the pipeline: Download -> PII Redact -> Extract -> Embed -> Persist.

#### 1.3.2.3 Implementation Requirements

- Maintain transactional consistency
- Handle transient failures with Polly policies
- Publish SowProcessedEvent/SowFailedEvent upon completion

#### 1.3.2.4 Architectural Context

Application / Domain Service

#### 1.3.2.5 Extraction Reasoning

Core business logic coordinator.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Infrastructure.Messaging

#### 1.4.1.2 Layer Responsibilities

Handling asynchronous communication with the Message Broker.

#### 1.4.1.3 Layer Constraints

- Must use MassTransit for abstraction
- Must implement The Outbox Pattern for event publication

#### 1.4.1.4 Implementation Patterns

- Competing Consumers
- Dead Letter Queue

#### 1.4.1.5 Extraction Reasoning

Critical for decoupling the worker from the API.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Infrastructure.Adapters

#### 1.4.2.2 Layer Responsibilities

Communicating with external AI and Cloud APIs.

#### 1.4.2.3 Layer Constraints

- Must handle rate limiting (HTTP 429)
- Must secure API keys

#### 1.4.2.4 Implementation Patterns

- Gateway Pattern
- Circuit Breaker

#### 1.4.2.5 Extraction Reasoning

Necessary for interacting with AWS and OpenAI.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

Message Broker

#### 1.5.1.2 Source Repository

REPO-GW-API

#### 1.5.1.3 Method Contracts

- {'method_name': 'SowUploadedEvent', 'method_signature': 'record SowUploadedEvent(Guid SowId, Guid ProjectId, string S3ObjectKey)', 'method_purpose': 'Signals that a file is ready for processing.', 'integration_context': 'Published by API Gateway, Consumed by AI Worker.'}

#### 1.5.1.4 Integration Pattern

Asynchronous Event-Driven (Pub/Sub)

#### 1.5.1.5 Communication Protocol

AMQP (RabbitMQ)

#### 1.5.1.6 Extraction Reasoning

The trigger mechanism for this worker service.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IFileStorageService

#### 1.5.2.2 Source Repository

AWS S3

#### 1.5.2.3 Method Contracts

- {'method_name': 'GetFileStreamAsync', 'method_signature': 'Task<Stream> GetFileStreamAsync(string key, CancellationToken ct)', 'method_purpose': 'Retrieves the raw document stream for analysis.', 'integration_context': 'Infrastructure Adapter Call'}

#### 1.5.2.4 Integration Pattern

Client SDK

#### 1.5.2.5 Communication Protocol

HTTPS

#### 1.5.2.6 Extraction Reasoning

Required to access the document payload.

### 1.5.3.0 Interface Name

#### 1.5.3.1 Interface Name

IAiService

#### 1.5.3.2 Source Repository

Azure OpenAI / AWS Comprehend

#### 1.5.3.3 Method Contracts

##### 1.5.3.3.1 Method Name

###### 1.5.3.3.1.1 Method Name

DetectPiiAsync

###### 1.5.3.3.1.2 Method Signature

Task<PiiResult> DetectPiiAsync(string text)

###### 1.5.3.3.1.3 Method Purpose

Identifies sensitive entities in text.

###### 1.5.3.3.1.4 Integration Context

Infrastructure Adapter Call

##### 1.5.3.3.2.0 Method Name

###### 1.5.3.3.2.1 Method Name

GetCompletionAsync

###### 1.5.3.3.2.2 Method Signature

Task<string> GetCompletionAsync(string prompt, JsonSchema schema)

###### 1.5.3.3.2.3 Method Purpose

Extracts structured data from sanitized text.

###### 1.5.3.3.2.4 Integration Context

Infrastructure Adapter Call

#### 1.5.3.4.0.0 Integration Pattern

Rest API / SDK

#### 1.5.3.5.0.0 Communication Protocol

HTTPS

#### 1.5.3.6.0.0 Extraction Reasoning

Core dependency for functional requirements.

## 1.6.0.0.0.0 Exposed Interfaces

- {'interface_name': 'Event Publisher', 'consumer_repositories': ['REPO-SVC-PROJECT', 'REPO-SVC-USER'], 'method_contracts': [{'method_name': 'SowProcessedEvent', 'method_signature': 'record SowProcessedEvent(Guid SowId, Guid ProjectId, bool Success)', 'method_purpose': 'Notifies the system that processing is complete, triggering UI updates and state transitions.', 'implementation_requirements': 'Must be published via MassTransit IPublishEndpoint.'}, {'method_name': 'SowFailedEvent', 'method_signature': 'record SowFailedEvent(Guid SowId, Guid ProjectId, string ErrorCode, string Message)', 'method_purpose': 'Notifies the system of processing failure for error handling workflows.', 'implementation_requirements': 'Must be published to a dedicated error exchange.'}], 'service_level_requirements': ['Event publication must be atomic with database updates (Outbox Pattern).'], 'implementation_constraints': ['Events must adhere to the schemas defined in REPO-LIB-CONTRACTS.'], 'extraction_reasoning': 'This worker produces these events to drive the Project lifecycle forward.'}

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 Worker Service

### 1.7.2.0.0.0 Integration Technologies

- MassTransit (RabbitMQ)
- AWSSDK.Comprehend
- Azure.AI.OpenAI
- Npgsql (PostgreSQL)

### 1.7.3.0.0.0 Performance Constraints

Long-running processes (up to minutes for large files) must not block threads. Parallelism should be throttled to avoid hitting AI API rate limits.

### 1.7.4.0.0.0 Security Requirements

PII must be redacted in memory before sending to OpenAI. API Keys must be loaded from AWS Secrets Manager.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Verified inputs (SowUploaded) and outputs (SowProc... |
| Cross Reference Validation | Confirmed contracts align with REPO-LIB-CONTRACTS ... |
| Implementation Readiness Assessment | High. Specific SDKs and patterns (Outbox, Polly) a... |
| Quality Assurance Confirmation | Integration architecture ensures decoupling and re... |


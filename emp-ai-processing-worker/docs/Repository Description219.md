# 1 Id

REPO-SVC-AIWORKER

# 2 Name

emp-ai-processing-worker

# 3 Description

A dedicated, asynchronous background service that performs the entire AI-powered SOW processing workflow. This repository contains the message consumer that listens for `SowUploaded` events. It's responsible for interacting with AWS Comprehend for PII sanitization and an OpenAI model for data extraction and vector embedding generation, as per REQ-FUN-002. It was extracted from the worker project in the monorepo to isolate its heavy computational workload, unique dependencies (AI SDKs), and distinct scaling requirements. This ensures that SOW processing does not impact the performance of the user-facing API.

# 4 Type

🔹 Application Services

# 5 Namespace

EnterpriseMediator.AiProcessing.Worker

# 6 Output Path

workers/ai-processing

# 7 Framework

.NET 8 Worker Service

# 8 Language

C#

# 9 Technology

.NET, RabbitMQ, AWS SDK, OpenAI SDK

# 10 Thirdparty Libraries

- AWSSDK.Comprehend
- Azure.AI.OpenAI

# 11 Layer Ids

- application-layer
- infrastructure-layer

# 12 Dependencies

- REPO-LIB-DOMAIN
- REPO-LIB-CONTRACTS
- REPO-LIB-SHARED KERNEL

# 13 Requirements

- {'requirementId': 'REQ-FUN-002'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Event-Driven Worker

# 17 Architecture Map

- sow-processing-worker-013

# 18 Components Map

- SowProcessingWorker

# 19 Requirements Map

- REQ-FUN-002

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

EMP-MONOREPO-001

## 20.3 Decomposition Reasoning

Crucially separated to isolate a long-running, resource-intensive, and I/O-bound process. This prevents the AI workload from degrading the performance and availability of the synchronous API. It allows for independent scaling based on message queue depth and isolates specialized, heavy dependencies (AI SDKs).

## 20.4 Extracted Responsibilities

- Asynchronous Event Consumption
- PII Sanitization (AWS Comprehend)
- SOW Data Extraction (OpenAI)
- Vector Embedding Generation

## 20.5 Reusability Scope

- The logic is specific to the SOW processing task.

## 20.6 Development Benefits

- Decouples long-running tasks from the user request.
- Independent scaling and resource allocation.
- Isolates third-party AI SDKs and their dependencies.

# 21.0 Dependency Contracts

## 21.1 Repo-Gw-Api

### 21.1.1 Required Interfaces

- {'interface': 'Event Consumer', 'methods': ['Handles SowUploadedEvent'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Asynchronous Event-Driven

### 21.1.3 Communication Protocol

AMQP (RabbitMQ)

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

*No items available*

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | DI for injecting AI clients, repositories, and oth... |
| Event Communication | Primary consumer of events. May publish completion... |
| Data Flow | Consume message -> Fetch document from S3 -> Call ... |
| Error Handling | Implements retry logic for transient AI API failur... |
| Async Patterns | Fundamentally asynchronous, using async/await thro... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Structure as a hosted service within the .NET Work... |
| Performance Considerations | Long-running by nature. Focus on reliability and e... |
| Security Considerations | Securely manage API keys for AWS and OpenAI using ... |
| Testing Approach | Integration tests that mock the external AI servic... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- The entire SOW processing pipeline.
- Robust retry and error handling logic.

## 25.2.0 Must Not Implement

- Any synchronous API endpoints.
- Any business logic outside of SOW ingestion and analysis.

## 25.3.0 Extension Points

- Adding new data points to be extracted from SOWs.
- Integrating a different LLM provider.

## 25.4.0 Validation Rules

- Validate the structure of the response from the AI services.


# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-GW-API |
| Extraction Timestamp | 2025-10-27T12:15:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High - Architecture and Integration Patterns Fully... |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-TEC-002

#### 1.2.1.2 Requirement Text

The system shall implement a Microservices Architecture with an API Gateway/BFF pattern to abstract internal complexity.

#### 1.2.1.3 Validation Criteria

- Acts as single entry point for REPO-FE-WEBAPP
- Implements response aggregation for dashboard views
- Decouples frontend contracts from internal microservice contracts

#### 1.2.1.4 Implementation Implications

- Implement Aggregation Service to combine Project and Financial data
- Define specific BFF ViewModels distinct from Domain DTOs
- Configure HttpClientFactory with resilience policies

#### 1.2.1.5 Extraction Reasoning

Core architectural mandate defining this repository's role as the orchestration layer.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-NFR-003

#### 1.2.2.2 Requirement Text

The system shall enforce strict security controls including Authentication (JWT), Authorization (RBAC), and Rate Limiting.

#### 1.2.2.3 Validation Criteria

- Validate JWTs against AWS Cognito JWKS
- Enforce rate limits per tenant/IP
- Propagate identity context to downstream services

#### 1.2.2.4 Implementation Implications

- Configure Microsoft.AspNetCore.Authentication.JwtBearer
- Implement Token Forwarding Handler for internal HTTP requests
- Apply AspNetCoreRateLimit middleware

#### 1.2.2.5 Extraction Reasoning

Security constraints dictate how this gateway integrates with the Identity Provider and protects downstream services.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

GatewayOrchestrator

#### 1.3.1.2 Component Specification

Central aggregation component responsible for parallel data fetching from microservices and constructing composite responses for the UI.

#### 1.3.1.3 Implementation Requirements

- Use Task.WhenAll for parallel downstream requests
- Handle partial failures gracefully (e.g., return Project data even if Finance is slow)
- Map internal DTOs to public ViewModels

#### 1.3.1.4 Architectural Context

Application Layer - Implementation of the BFF Pattern

#### 1.3.1.5 Extraction Reasoning

Required to satisfy the Dashboard Aggregation requirement found in the code specifications.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

SecurityMiddlewarePipeline

#### 1.3.2.2 Component Specification

Ordered pipeline components handling AuthN, AuthZ, Correlation IDs, and Rate Limiting before requests reach controllers.

#### 1.3.2.3 Implementation Requirements

- Extract Bearer Token
- Inject X-Correlation-ID into downstream headers
- Enforce policy-based authorization

#### 1.3.2.4 Architectural Context

Presentation Layer - Cross-Cutting Concerns

#### 1.3.2.5 Extraction Reasoning

Essential for securing the entry point and enabling observability.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Presentation Layer

#### 1.4.1.2 Layer Responsibilities

Exposing REST endpoints, Content Negotiation, API Versioning, and Global Error Handling.

#### 1.4.1.3 Layer Constraints

- Must be stateless
- Must not contain business rules (delegated to microservices)

#### 1.4.1.4 Implementation Patterns

- Controller-Service-Repository
- Global Exception Handler

#### 1.4.1.5 Extraction Reasoning

Standard layer for an API Gateway host.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Infrastructure Layer

#### 1.4.2.2 Layer Responsibilities

Managing HTTP communication with downstream microservices, Service Discovery, and Message Bus publishing.

#### 1.4.2.3 Layer Constraints

- Must handle transient faults (Retry, Circuit Breaker)
- Must manage connection pooling

#### 1.4.2.4 Implementation Patterns

- Typed HttpClient
- Polly Resilience Pipeline

#### 1.4.2.5 Extraction Reasoning

Handles the physical integration with REPO-SVC-* repositories.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IProjectServiceClient

#### 1.5.1.2 Source Repository

REPO-SVC-PROJECT

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

GetProjectDetailsAsync

###### 1.5.1.3.1.2 Method Signature

Task<InternalProjectDto> GetProjectDetailsAsync(Guid id, CancellationToken ct)

###### 1.5.1.3.1.3 Method Purpose

Retrieves core project data

###### 1.5.1.3.1.4 Integration Context

Dashboard aggregation

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

CreateProjectAsync

###### 1.5.1.3.2.2 Method Signature

Task<Guid> CreateProjectAsync(CreateProjectDto dto, CancellationToken ct)

###### 1.5.1.3.2.3 Method Purpose

Proxies project creation requests

###### 1.5.1.3.2.4 Integration Context

Project Wizard flow

#### 1.5.1.4.0.0 Integration Pattern

Synchronous HTTP/REST via Refit/HttpClient

#### 1.5.1.5.0.0 Communication Protocol

HTTP/1.1 or HTTP/2

#### 1.5.1.6.0.0 Extraction Reasoning

Gateway connects to Project Service for all project-related data.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

IFinancialServiceClient

#### 1.5.2.2.0.0 Source Repository

REPO-SVC-FINANCIAL

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'GetProjectFinancialSummaryAsync', 'method_signature': 'Task<FinancialSummaryDto> GetProjectFinancialSummaryAsync(Guid projectId, CancellationToken ct)', 'method_purpose': 'Retrieves invoice and payout status', 'integration_context': 'Dashboard aggregation'}

#### 1.5.2.4.0.0 Integration Pattern

Synchronous HTTP/REST via Refit/HttpClient

#### 1.5.2.5.0.0 Communication Protocol

HTTP/1.1 or HTTP/2

#### 1.5.2.6.0.0 Extraction Reasoning

Gateway aggregates financial data into the project dashboard.

### 1.5.3.0.0.0 Interface Name

#### 1.5.3.1.0.0 Interface Name

IUserServiceClient

#### 1.5.3.2.0.0 Source Repository

REPO-SVC-USER

#### 1.5.3.3.0.0 Method Contracts

- {'method_name': 'GetUserPermissionsAsync', 'method_signature': 'Task<string[]> GetUserPermissionsAsync(Guid userId, CancellationToken ct)', 'method_purpose': 'Fetches granular permissions for RBAC enforcement', 'integration_context': 'Authorization Middleware'}

#### 1.5.3.4.0.0 Integration Pattern

Synchronous HTTP/REST

#### 1.5.3.5.0.0 Communication Protocol

HTTP/1.1

#### 1.5.3.6.0.0 Extraction Reasoning

Gateway enforces security decisions based on User Service data.

### 1.5.4.0.0.0 Interface Name

#### 1.5.4.1.0.0 Interface Name

IMessageBus

#### 1.5.4.2.0.0 Source Repository

Message Broker (RabbitMQ)

#### 1.5.4.3.0.0 Method Contracts

- {'method_name': 'Publish<SowUploadedEvent>', 'method_signature': 'Task Publish(SowUploadedEvent event, CancellationToken ct)', 'method_purpose': 'Notifies AI Worker of new document upload', 'integration_context': 'SOW Upload Endpoint'}

#### 1.5.4.4.0.0 Integration Pattern

Asynchronous Event Publication

#### 1.5.4.5.0.0 Communication Protocol

AMQP

#### 1.5.4.6.0.0 Extraction Reasoning

Gateway decouples file upload from processing via messaging.

## 1.6.0.0.0.0 Exposed Interfaces

- {'interface_name': 'BFF REST API', 'consumer_repositories': ['REPO-FE-WEBAPP'], 'method_contracts': [{'method_name': 'GET /api/v1/dashboard/{projectId}', 'method_signature': 'Task<ActionResult<ProjectDashboardResponse>> GetDashboard(Guid projectId)', 'method_purpose': 'Returns aggregated view model for the main dashboard', 'implementation_requirements': 'Aggregation of Project and Financial service data'}, {'method_name': 'POST /api/v1/projects/{id}/sow', 'method_signature': 'Task<ActionResult> UploadSow(Guid id, IFormFile file)', 'method_purpose': 'Accepts SOW file and triggers processing', 'implementation_requirements': 'Multipart/form-data handling, stream passthrough to S3'}], 'service_level_requirements': ['P95 Latency < 250ms', 'Availability 99.9%'], 'implementation_constraints': ['Strict Content-Type validation', 'CORS restriction to trusted domains'], 'extraction_reasoning': 'The primary contract provided to the frontend application.'}

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

ASP.NET Core 8 Web API

### 1.7.2.0.0.0 Integration Technologies

- Microsoft.Extensions.Http.Resilience (Polly)
- MassTransit (RabbitMQ)
- AWS SDK (Cognito, S3)
- Refit (Declarative HTTP Client)

### 1.7.3.0.0.0 Performance Constraints

Must maintain low latency overhead (< 20ms) added to downstream service calls. High throughput required for handling all ingress traffic.

### 1.7.4.0.0.0 Security Requirements

TLS 1.2+ Termination, JWT Validation, Claims Transformation, OWASP Secure Headers.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped connections to Frontend, Project Service, F... |
| Cross Reference Validation | Validated against Sequence 476 (SOW Upload) and 48... |
| Implementation Readiness Assessment | Production-Ready. Integration patterns (BFF, Aggre... |
| Quality Assurance Confirmation | Resilience patterns (Circuit Breaker, Retry) inclu... |


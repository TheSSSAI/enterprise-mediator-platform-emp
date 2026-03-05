# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-LIB-CONTRACTS |
| Extraction Timestamp | 2025-04-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-INT-002

#### 1.2.1.2 Requirement Text

Establish formal schemas for asynchronous event communication between distributed services

#### 1.2.1.3 Validation Criteria

- Must define immutable event message structures
- Must support serialization for message brokers (RabbitMQ/SQS)
- Must include standard metadata (EventId, CorrelationId)

#### 1.2.1.4 Implementation Implications

- Implement IIntegrationEvent marker interface
- Define 'SowUploadedEvent', 'ProjectAwardedEvent', 'VendorDeactivatedEvent' records

#### 1.2.1.5 Extraction Reasoning

This repository acts as the central schema registry for the Event-Driven Architecture.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-INT-003

#### 1.2.2.2 Requirement Text

Formalize API communication protocols through standardized Data Transfer Objects (DTOs)

#### 1.2.2.3 Validation Criteria

- Must provide type-safe contract definitions for all API endpoints
- Must be versionable via NuGet
- Must support JSON serialization/deserialization standards

#### 1.2.2.4 Implementation Implications

- Define API Request/Response models for Projects, Financials, and Users
- Enforce camelCase serialization naming policies via attributes

#### 1.2.2.5 Extraction Reasoning

This repository defines the contract between the API Gateway and downstream microservices.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

IntegrationEvents

#### 1.3.1.2 Component Specification

A collection of immutable records defining the payload structure for asynchronous messages used in the event bus.

#### 1.3.1.3 Implementation Requirements

- Implement IIntegrationEvent interface
- Ensure System.Text.Json compatibility
- Include CorrelationId for distributed tracing

#### 1.3.1.4 Architectural Context

Integration Layer - Message Contracts

#### 1.3.1.5 Extraction Reasoning

Critical for decoupling services (e.g., Project Service -> AI Worker).

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ApiDtos

#### 1.3.2.2 Component Specification

Standardized Data Transfer Objects for synchronous HTTP API communication.

#### 1.3.2.3 Implementation Requirements

- Use C# Records for immutability
- Apply DataAnnotation attributes for validation rules
- Define Enums for shared state (e.g., ProjectStatus)

#### 1.3.2.4 Architectural Context

Integration Layer - API Contracts

#### 1.3.2.5 Extraction Reasoning

Ensures type safety and contract adherence between Gateway and Microservices.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Shared Kernel / Integration', 'layer_responsibilities': 'Hosting shared definitions, contracts, and constants used across the entire distributed system.', 'layer_constraints': ['Zero logic policy: No business rules or persistence logic', 'Minimal dependencies: Only System.* and basic extensions', 'Strict versioning: Backward compatibility is mandatory'], 'implementation_patterns': ['Shared Library', 'Schema-First Design'], 'extraction_reasoning': "This library represents the 'Language' spoken by all services."}

## 1.5.0.0 Dependency Interfaces

*No items available*

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

NuGet Package: EnterpriseMediator.Contracts

#### 1.6.1.2 Consumer Repositories

- REPO-GW-API
- REPO-SVC-PROJECT
- REPO-SVC-FINANCIAL
- REPO-SVC-USER
- REPO-SVC-AIWORKER

#### 1.6.1.3 Method Contracts

##### 1.6.1.3.1 Method Name

###### 1.6.1.3.1.1 Method Name

Event Definitions

###### 1.6.1.3.1.2 Method Signature

public record SowUploadedEvent(Guid SowId, ...)

###### 1.6.1.3.1.3 Method Purpose

Defines payload for SOW processing trigger

###### 1.6.1.3.1.4 Implementation Requirements

Must match consumer expectations in AI Worker

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

DTO Definitions

###### 1.6.1.3.2.2 Method Signature

public record ProjectDto(...)

###### 1.6.1.3.2.3 Method Purpose

Defines payload for Project API responses

###### 1.6.1.3.2.4 Implementation Requirements

Must match frontend data requirements

##### 1.6.1.3.3.0 Method Name

###### 1.6.1.3.3.1 Method Name

Shared Enums

###### 1.6.1.3.3.2 Method Signature

public enum ProjectStatus

###### 1.6.1.3.3.3 Method Purpose

Standardizes lifecycle states across services

###### 1.6.1.3.3.4 Implementation Requirements

Must serialize to string for API readability

#### 1.6.1.4.0.0 Service Level Requirements

- High stability: Breaking changes require major version increments
- Universal compatibility: Must work in all .NET 8 services

#### 1.6.1.5.0.0 Implementation Constraints

- Must be published to internal NuGet feed
- Must not depend on EF Core or ASP.NET Core

#### 1.6.1.6.0.0 Extraction Reasoning

Primary mechanism for sharing code artifacts.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

TypeScript Definition Generation

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-FE-WEBAPP

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'Type Export', 'method_signature': 'Automated Process', 'method_purpose': 'Generates .ts interfaces from C# DTOs during CI/CD', 'implementation_requirements': 'Use tools like TypeGen or NSwag in the build pipeline'}

#### 1.6.2.4.0.0 Service Level Requirements

- Exact type matching between Backend and Frontend

#### 1.6.2.5.0.0 Implementation Constraints

- Generated files must be committed or packaged for frontend consumption

#### 1.6.2.6.0.0 Extraction Reasoning

Ensures full-stack type safety.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 Class Library

### 1.7.2.0.0.0 Integration Technologies

- NuGet (Package Management)
- System.Text.Json (Serialization)
- NSwag/TypeGen (Cross-language generation)

### 1.7.3.0.0.0 Performance Constraints

Serialization logic must be highly optimized (use source generators where possible).

### 1.7.4.0.0.0 Security Requirements

Contracts must not expose internal-only fields (e.g., database internal IDs or PII that shouldn't leave the service boundary).

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Verified coverage of all integration points: API G... |
| Cross Reference Validation | Confirmed SowUploadedEvent structure matches AI Wo... |
| Implementation Readiness Assessment | High. The repository structure is simple, but the ... |
| Quality Assurance Confirmation | The 'Schema-First' approach minimizes integration ... |


# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-USER |
| Extraction Timestamp | 2025-05-23T15:15:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-SEC-001

#### 1.2.1.2 Requirement Text

The system shall enforce Role-Based Access Control (RBAC) where the User Service acts as the policy information point.

#### 1.2.1.3 Validation Criteria

- Must expose endpoints to retrieve user roles
- Must manage mapping between Cognito identities and internal User entities
- Must support hierarchy of permissions for System Admin vs Finance Manager

#### 1.2.1.4 Implementation Implications

- Implement 'InternalUsersController' with caching headers for high-performance role lookups
- Store RBAC definitions in PostgreSQL but sync critical claims to Cognito custom attributes

#### 1.2.1.5 Extraction Reasoning

Central responsibility of this microservice.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

US-028

#### 1.2.2.2 Requirement Text

Vendor Manages Own Profile including secure payment details.

#### 1.2.2.3 Validation Criteria

- Payment details must be encrypted at rest
- Payment details must be retrievable only by authorized services (Finance) and the owner (masked)

#### 1.2.2.4 Implementation Implications

- Implement 'PaymentInfo' Value Object with EF Core encryption conversion
- Expose specific internal endpoint 'GetVendorPaymentDetails' secured by internal network policies for the Financial Service

#### 1.2.2.5 Extraction Reasoning

Data ownership of sensitive vendor information.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUN-001

#### 1.2.3.2 Requirement Text

User Onboarding and Registration triggers asynchronous workflows.

#### 1.2.3.3 Validation Criteria

- New user registration must trigger a 'UserRegistered' event
- Vendor profile creation must trigger 'VendorCreated' event

#### 1.2.3.4 Implementation Implications

- Configure MassTransit to publish events to RabbitMQ/SNS upon transaction commit
- Define event contracts in REPO-LIB-CONTRACTS

#### 1.2.3.5 Extraction Reasoning

Event-driven integration requirement.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

InternalUsersController

#### 1.3.1.2 Component Specification

API controller exposing synchronous endpoints specifically for other microservices (Project, Financial).

#### 1.3.1.3 Implementation Requirements

- Authorize requests using 'InternalServicePolicy' (e.g., Client Credentials flow or IP allowlist)
- Optimize payload size for minimal network latency

#### 1.3.1.4 Architectural Context

Presentation Layer (Internal API)

#### 1.3.1.5 Extraction Reasoning

Necessary to provide data to dependent services without data duplication.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

AuditServiceAdapter

#### 1.3.2.2 Component Specification

Infrastructure service responsible for publishing audit logs to the message bus.

#### 1.3.2.3 Implementation Requirements

- Implement IAuditServiceAdapter
- Map domain changes to AuditLogEntryDTO

#### 1.3.2.4 Architectural Context

Infrastructure Layer / Integration

#### 1.3.2.5 Extraction Reasoning

Required to fulfill cross-cutting concern of system auditing.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

CognitoIdentityService

#### 1.3.3.2 Component Specification

Wrapper around AWS SDK for Cognito User Pool management.

#### 1.3.3.3 Implementation Requirements

- Handle user creation in Cognito
- Sync password policies
- Manage group/role assignments in Cognito

#### 1.3.3.4 Architectural Context

Infrastructure Layer / Identity Provider Adapter

#### 1.3.3.5 Extraction Reasoning

Required for external identity management integration.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Presentation Layer

#### 1.4.1.2 Layer Responsibilities

Handles incoming HTTP requests, maps DTOs, and orchestrates authentication checks.

#### 1.4.1.3 Layer Constraints

- Must separate Public API (Gateway consumption) from Internal API (Service-to-Service)
- Must not contain business rules

#### 1.4.1.4 Implementation Patterns

- Controller Pattern
- API Versioning

#### 1.4.1.5 Extraction Reasoning

Standard Microservice entry point.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Infrastructure Layer

#### 1.4.2.2 Layer Responsibilities

Implements persistence (EF Core), Messaging (MassTransit), and Identity (Cognito).

#### 1.4.2.3 Layer Constraints

- Must handle database migrations
- Must manage connection resilience (Polly)

#### 1.4.2.4 Implementation Patterns

- Repository Pattern
- Outbox Pattern

#### 1.4.2.5 Extraction Reasoning

Handles all I/O and external integrations.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IIdentityService

#### 1.5.1.2 Source Repository

AWS Cognito

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

CreateUserAsync

###### 1.5.1.3.1.2 Method Signature

Task<string> CreateUserAsync(string email, string password, string role)

###### 1.5.1.3.1.3 Method Purpose

Creates the authentication principal in the IDP

###### 1.5.1.3.1.4 Integration Context

During 'RegisterUserCommandHandler' execution

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

DeleteUserAsync

###### 1.5.1.3.2.2 Method Signature

Task DeleteUserAsync(string identityId)

###### 1.5.1.3.2.3 Method Purpose

Removes authentication principal for GDPR compliance

###### 1.5.1.3.2.4 Integration Context

During 'AnonymizeUserCommand' execution

#### 1.5.1.4.0.0 Integration Pattern

SDK Wrapper

#### 1.5.1.5.0.0 Communication Protocol

HTTPS (AWS SigV4)

#### 1.5.1.6.0.0 Extraction Reasoning

Dependency on external IDP for auth.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

IPublishEndpoint

#### 1.5.2.2.0.0 Source Repository

Message Broker (RabbitMQ)

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'Publish', 'method_signature': 'Task Publish<T>(T message, CancellationToken ct)', 'method_purpose': 'Broadcasts domain events to the system', 'integration_context': 'After successful database transaction'}

#### 1.5.2.4.0.0 Integration Pattern

Fire-and-Forget (with Outbox)

#### 1.5.2.5.0.0 Communication Protocol

AMQP

#### 1.5.2.6.0.0 Extraction Reasoning

Event bus dependency for decoupling.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

Internal User API

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-SVC-PROJECT
- REPO-SVC-FINANCIAL

#### 1.6.1.3.0.0 Method Contracts

##### 1.6.1.3.1.0 Method Name

###### 1.6.1.3.1.1 Method Name

GET /api/v1/internal/users/{id}/role

###### 1.6.1.3.1.2 Method Signature

ActionResult<string> GetUserRole(Guid id)

###### 1.6.1.3.1.3 Method Purpose

Returns the role of a user for authorization checks in other services

###### 1.6.1.3.1.4 Implementation Requirements

High performance (cached)

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

GET /api/v1/internal/clients/{id}

###### 1.6.1.3.2.2 Method Signature

ActionResult<ClientDto> GetClient(Guid id)

###### 1.6.1.3.2.3 Method Purpose

Returns client details for project association

###### 1.6.1.3.2.4 Implementation Requirements

Read-only DTO

##### 1.6.1.3.3.0 Method Name

###### 1.6.1.3.3.1 Method Name

GET /api/v1/internal/vendors/{id}/payment-details

###### 1.6.1.3.3.2 Method Signature

ActionResult<PaymentInfoDto> GetVendorPaymentDetails(Guid id)

###### 1.6.1.3.3.3 Method Purpose

Returns secure payment details for the Financial Service

###### 1.6.1.3.3.4 Implementation Requirements

Strictly secured endpoint, encrypted payload

#### 1.6.1.4.0.0 Service Level Requirements

- Response time < 100ms
- 99.99% Availability

#### 1.6.1.5.0.0 Implementation Constraints

- Must be inaccessible from the public internet
- Requires 'InternalService' scope in JWT

#### 1.6.1.6.0.0 Extraction Reasoning

Defined as the system of record for User/Client/Vendor data.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

Integration Events

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-SVC-PROJECT
- REPO-SVC-AIWORKER

#### 1.6.2.3.0.0 Method Contracts

##### 1.6.2.3.1.0 Method Name

###### 1.6.2.3.1.1 Method Name

VendorProfileUpdated

###### 1.6.2.3.1.2 Method Signature

event VendorProfileUpdated { Guid VendorId, string[] NewSkills }

###### 1.6.2.3.1.3 Method Purpose

Signals changes in vendor skills so Project Service can update vector embeddings for matching

###### 1.6.2.3.1.4 Implementation Requirements

Include changed skills in payload

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

UserRegistered

###### 1.6.2.3.2.2 Method Signature

event UserRegistered { Guid UserId, string Email, string Role }

###### 1.6.2.3.2.3 Method Purpose

Triggers welcome notification workflows

###### 1.6.2.3.2.4 Implementation Requirements

Standard event metadata

#### 1.6.2.4.0.0 Service Level Requirements

- Guaranteed Delivery (At-least-once)

#### 1.6.2.5.0.0 Implementation Constraints

- Schema managed in REPO-LIB-CONTRACTS

#### 1.6.2.6.0.0 Extraction Reasoning

Asynchronous integration points identified in sequence diagrams.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

ASP.NET Core 8

### 1.7.2.0.0.0 Integration Technologies

- MassTransit (Message Bus)
- AWS SDK.CognitoIdentityProvider
- Entity Framework Core (PostgreSQL)

### 1.7.3.0.0.0 Performance Constraints

Role lookup endpoint must be highly optimized (cached) as it may be called frequently by other services.

### 1.7.4.0.0.0 Security Requirements

PaymentInfo must be encrypted at rest (EF Core Value Converter). PII must be handled according to GDPR (Erasure command).

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Verified all inbound (Gateway, Project, Finance) a... |
| Cross Reference Validation | Validated that exposed internal endpoints match th... |
| Implementation Readiness Assessment | High. Interface definitions and technology stack a... |
| Quality Assurance Confirmation | Security constraints regarding PaymentInfo and PII... |


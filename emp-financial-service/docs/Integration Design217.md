# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-FINANCIAL |
| Extraction Timestamp | 2025-01-26T14:45:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High - Protocols and Interfaces Defined |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

US-056

#### 1.2.1.2 Requirement Text

Admin Triggers Client Invoice Creation

#### 1.2.1.3 Validation Criteria

- System receives trigger from Project Service or Admin UI
- Invoice generation requires Project and Client data
- Stripe Payment Link must be generated via external API

#### 1.2.1.4 Implementation Implications

- Implement `IConsumer<ProjectAwardedEvent>` to automate invoice drafts
- Expose `POST /api/v1/invoices` for manual triggers via API Gateway
- Integrate Stripe SDK for Payment Intent creation

#### 1.2.1.5 Extraction Reasoning

Core integration point linking Project lifecycle to Financial initiation.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

US-058

#### 1.2.2.2 Requirement Text

Client Pays Invoice via Secure Page

#### 1.2.2.3 Validation Criteria

- System must handle asynchronous payment confirmation from Stripe
- Project status must update upon payment success

#### 1.2.2.4 Implementation Implications

- Implement `StripeWebhookController` to receive `payment_intent.succeeded`
- Publish `InvoicePaidEvent` to the Event Bus for Project Service consumption

#### 1.2.2.5 Extraction Reasoning

Critical external integration (Stripe) and internal event propagation.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

US-060

#### 1.2.3.2 Requirement Text

Finance Manager Initiates Vendor Payout

#### 1.2.3.3 Validation Criteria

- System must interact with Wise API for fund transfer
- Payouts require approval workflow validation

#### 1.2.3.4 Implementation Implications

- Implement `WisePayoutAdapter` implementing `IPayoutGateway`
- Secure storage of Wise API tokens via configuration injection

#### 1.2.3.5 Extraction Reasoning

Major external integration point for accounts payable.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-INT-002

#### 1.2.4.2 Requirement Text

Establish formal schemas for asynchronous event communication

#### 1.2.4.3 Validation Criteria

- Events must use standardized contracts from Shared Library

#### 1.2.4.4 Implementation Implications

- Depend on `REPO-LIB-CONTRACTS` for `ProjectAwardedEvent` and `InvoicePaidEvent` schemas
- Use MassTransit for abstraction over RabbitMQ

#### 1.2.4.5 Extraction Reasoning

Ensures type-safe asynchronous communication.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

StripePaymentAdapter

#### 1.3.1.2 Component Specification

Infrastructure adapter wrapping the Stripe.net SDK to handle Payment Intents and Webhook signature verification.

#### 1.3.1.3 Implementation Requirements

- Implement `IPaymentGateway` interface
- Handle idempotency keys for Stripe requests
- Map Stripe exceptions to domain exceptions

#### 1.3.1.4 Architectural Context

Infrastructure Layer / External Service Adapter

#### 1.3.1.5 Extraction Reasoning

Encapsulates the external dependency on Stripe, protecting the domain from API changes.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

WisePayoutAdapter

#### 1.3.2.2 Component Specification

Infrastructure adapter wrapping HTTP calls to the Wise API for vendor payouts.

#### 1.3.2.3 Implementation Requirements

- Implement `IPayoutGateway` interface
- Manage authentication tokens for Wise
- Handle asynchronous payout status updates

#### 1.3.2.4 Architectural Context

Infrastructure Layer / External Service Adapter

#### 1.3.2.5 Extraction Reasoning

Encapsulates the external dependency on Wise.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

ProjectAwardedConsumer

#### 1.3.3.2 Component Specification

Message consumer listening for project award events to trigger invoice generation logic.

#### 1.3.3.3 Implementation Requirements

- Implement `IConsumer<ProjectAwardedEvent>`
- Ensure idempotent processing of the event
- Trigger `GenerateInvoiceCommand` internally

#### 1.3.3.4 Architectural Context

Application Layer / Event Handler

#### 1.3.3.5 Extraction Reasoning

Primary inbound asynchronous integration point.

### 1.3.4.0 Component Name

#### 1.3.4.1 Component Name

FinancialsController

#### 1.3.4.2 Component Specification

REST API endpoints exposed to the API Gateway for UI-driven financial operations.

#### 1.3.4.3 Implementation Requirements

- Expose endpoints for Ledger, Invoices, and Payouts
- Authorize requests using JWT policies
- Delegate to MediatR

#### 1.3.4.4 Architectural Context

Presentation Layer / API

#### 1.3.4.5 Extraction Reasoning

Primary inbound synchronous integration point.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Infrastructure Layer

#### 1.4.1.2 Layer Responsibilities

External API communication (Stripe, Wise), Database Persistence (EF Core), Message Bus transport (MassTransit).

#### 1.4.1.3 Layer Constraints

- Must implement interfaces defined in Domain/Application layers
- Must handle transient faults (Polly)

#### 1.4.1.4 Implementation Patterns

- Adapter Pattern
- Repository Pattern

#### 1.4.1.5 Extraction Reasoning

Handles the physical connections to external systems.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Application Layer

#### 1.4.2.2 Layer Responsibilities

Message Consumption, Command Orchestration, Event Publishing.

#### 1.4.2.3 Layer Constraints

- Depends on REPO-LIB-CONTRACTS for DTOs
- No direct dependency on external SDKs

#### 1.4.2.4 Implementation Patterns

- CQRS
- Event Consumer

#### 1.4.2.5 Extraction Reasoning

Orchestrates the flow of data between the external world and the domain.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IProjectEvents

#### 1.5.1.2 Source Repository

REPO-SVC-PROJECT

#### 1.5.1.3 Method Contracts

- {'method_name': 'ProjectAwardedEvent', 'method_signature': 'public record ProjectAwardedEvent(Guid ProjectId, Guid VendorId, decimal Amount, string Currency)', 'method_purpose': 'Signals that a project has been awarded, triggering the need for an invoice.', 'integration_context': "Consumed via MassTransit (RabbitMQ) when a project state changes to 'Awarded'."}

#### 1.5.1.4 Integration Pattern

Asynchronous Event-Driven (Pub/Sub)

#### 1.5.1.5 Communication Protocol

AMQP / MassTransit

#### 1.5.1.6 Extraction Reasoning

Financial workflow dependency on Project Lifecycle state changes.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

Stripe API

#### 1.5.2.2 Source Repository

External (Stripe)

#### 1.5.2.3 Method Contracts

##### 1.5.2.3.1 Method Name

###### 1.5.2.3.1.1 Method Name

CreatePaymentIntent

###### 1.5.2.3.1.2 Method Signature

POST /v1/payment_intents

###### 1.5.2.3.1.3 Method Purpose

Initiates a payment transaction for a client invoice.

###### 1.5.2.3.1.4 Integration Context

Called by StripePaymentAdapter during Invoice Generation.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

Webhook: payment_intent.succeeded

###### 1.5.2.3.2.2 Method Signature

POST /api/webhooks/stripe

###### 1.5.2.3.2.3 Method Purpose

Notifies the system that a payment has been successfully captured.

###### 1.5.2.3.2.4 Integration Context

Received by StripeWebhookController from Stripe servers.

#### 1.5.2.4.0.0 Integration Pattern

REST API & Webhooks

#### 1.5.2.5.0.0 Communication Protocol

HTTPS

#### 1.5.2.6.0.0 Extraction Reasoning

Core revenue collection mechanism.

### 1.5.3.0.0.0 Interface Name

#### 1.5.3.1.0.0 Interface Name

Wise API

#### 1.5.3.2.0.0 Source Repository

External (Wise)

#### 1.5.3.3.0.0 Method Contracts

- {'method_name': 'CreateTransfer', 'method_signature': 'POST /v1/transfers', 'method_purpose': "Executes a payout to a vendor's bank account.", 'integration_context': 'Called by WisePayoutAdapter when a Payout is Approved.'}

#### 1.5.3.4.0.0 Integration Pattern

REST API

#### 1.5.3.5.0.0 Communication Protocol

HTTPS

#### 1.5.3.6.0.0 Extraction Reasoning

Core vendor payment mechanism.

### 1.5.4.0.0.0 Interface Name

#### 1.5.4.1.0.0 Interface Name

Shared Contracts

#### 1.5.4.2.0.0 Source Repository

REPO-LIB-CONTRACTS

#### 1.5.4.3.0.0 Method Contracts

- {'method_name': 'InvoicePaidEvent', 'method_signature': 'public record InvoicePaidEvent(Guid InvoiceId, Guid ProjectId)', 'method_purpose': 'Contract definition for the event published upon payment success.', 'integration_context': 'Referenced at compile time to ensure schema compatibility.'}

#### 1.5.4.4.0.0 Integration Pattern

Library Reference (NuGet)

#### 1.5.4.5.0.0 Communication Protocol

In-Process

#### 1.5.4.6.0.0 Extraction Reasoning

Ensures type safety across microservices.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

Financial Operations API

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-GW-API

#### 1.6.1.3.0.0 Method Contracts

##### 1.6.1.3.1.0 Method Name

###### 1.6.1.3.1.1 Method Name

GenerateInvoice

###### 1.6.1.3.1.2 Method Signature

POST /api/v1/invoices

###### 1.6.1.3.1.3 Method Purpose

Allows manual trigger of invoice generation for edge cases.

###### 1.6.1.3.1.4 Implementation Requirements

Accepts `GenerateInvoiceCommand` payload defined in Contracts.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

GetTransactionLedger

###### 1.6.1.3.2.2 Method Signature

GET /api/v1/transactions

###### 1.6.1.3.2.3 Method Purpose

Returns paginated list of financial transactions for the dashboard.

###### 1.6.1.3.2.4 Implementation Requirements

Returns `PagedResult<TransactionSummaryDto>`.

##### 1.6.1.3.3.0 Method Name

###### 1.6.1.3.3.1 Method Name

ApprovePayout

###### 1.6.1.3.3.2 Method Signature

POST /api/v1/payouts/{id}/approve

###### 1.6.1.3.3.3 Method Purpose

Changes payout status and triggers Wise transfer.

###### 1.6.1.3.3.4 Implementation Requirements

Requires Finance Manager role authorization.

#### 1.6.1.4.0.0 Service Level Requirements

- 99.9% Availability
- Response time < 500ms

#### 1.6.1.5.0.0 Implementation Constraints

- Must be stateless
- Must validate JWT via Gateway

#### 1.6.1.6.0.0 Extraction Reasoning

Endpoints required by the Frontend via the API Gateway.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

Financial Domain Events

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-SVC-PROJECT
- REPO-SVC-NOTIFICATION

#### 1.6.2.3.0.0 Method Contracts

##### 1.6.2.3.1.0 Method Name

###### 1.6.2.3.1.1 Method Name

InvoicePaidEvent

###### 1.6.2.3.1.2 Method Signature

Topic: enterprise-mediator.financial.invoice-paid

###### 1.6.2.3.1.3 Method Purpose

Notifies listeners that a project has been funded (moving Project to Active).

###### 1.6.2.3.1.4 Implementation Requirements

Published via MassTransit after successful Stripe webhook processing.

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

PayoutProcessedEvent

###### 1.6.2.3.2.2 Method Signature

Topic: enterprise-mediator.financial.payout-processed

###### 1.6.2.3.2.3 Method Purpose

Notifies listeners that a vendor has been paid.

###### 1.6.2.3.2.4 Implementation Requirements

Published via MassTransit after successful Wise transfer.

#### 1.6.2.4.0.0 Service Level Requirements

- At-least-once delivery

#### 1.6.2.5.0.0 Implementation Constraints

- Must use Transactional Outbox pattern

#### 1.6.2.6.0.0 Extraction Reasoning

Events required for system choreography.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

ASP.NET Core 8, MassTransit

### 1.7.2.0.0.0 Integration Technologies

- RabbitMQ (AMQP)
- Stripe.net SDK
- HTTP Client (Wise)
- PostgreSQL (Persistence)

### 1.7.3.0.0.0 Performance Constraints

Webhook processing must return 200 OK immediately and process logic asynchronously to avoid Stripe timeouts.

### 1.7.4.0.0.0 Security Requirements

Stripe Signature Verification, TLS 1.2+, Secrets Management for API Keys, RBAC for Approval Endpoints.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all inbound (API, Project Events, Webhooks)... |
| Cross Reference Validation | Validated against REPO-GW-API (upstream) and REPO-... |
| Implementation Readiness Assessment | High - Specific SDKs, event types, and patterns ar... |
| Quality Assurance Confirmation | Integration design supports SOC 2 auditability via... |


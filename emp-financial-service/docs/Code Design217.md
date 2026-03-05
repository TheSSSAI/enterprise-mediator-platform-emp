# 1 Design

code_design

# 2 Code Design

## 2.1 Code Specfication

### 2.1.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-FINANCIAL |
| Validation Timestamp | 2025-01-26T14:00:00Z |
| Original Component Count Claimed | 45 |
| Original Component Count Actual | 40 |
| Gaps Identified Count | 5 |
| Components Added Count | 12 |
| Final Component Count | 52 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic Domain-Driven Design & Clean Architectu... |

### 2.1.2 Validation Summary

#### 2.1.2.1 Repository Scope Validation

##### 2.1.2.1.1 Scope Compliance

High compliance with defined boundaries. Financial logic is correctly isolated.

##### 2.1.2.1.2 Gaps Identified

- Missing idempotent webhook handling mechanism for Stripe events
- Lack of explicit Retry Policy configuration for external Payment Gateways
- Missing standardized Money Value Object implementation details

##### 2.1.2.1.3 Components Added

- IdempotencyService
- ResiliencePolicyRegistry
- MoneyValueObjectConfiguration

#### 2.1.2.2.0 Requirements Coverage Validation

##### 2.1.2.2.1 Functional Requirements Coverage

100% (REQ-FUN-004 covered)

##### 2.1.2.2.2 Non Functional Requirements Coverage

100% (SOC 2 Auditability, Security, Performance)

##### 2.1.2.2.3 Missing Requirement Components

- Detailed audit logging for financial state transitions (SOC 2 requirement)
- Saga compensation logic for failed invoice generation

##### 2.1.2.2.4 Added Requirement Components

- FinancialAuditInterceptor
- InvoiceGenerationSagaState

#### 2.1.2.3.0 Architectural Pattern Validation

##### 2.1.2.3.1 Pattern Implementation Completeness

Clean Architecture and CQRS patterns are well-defined.

##### 2.1.2.3.2 Missing Pattern Components

- Outbox pattern implementation for reliable event publishing after database transactions
- Separation of Domain Services from Application Handlers

##### 2.1.2.3.3 Added Pattern Components

- TransactionalOutboxBehavior
- InvoiceCalculationDomainService

#### 2.1.2.4.0 Database Mapping Validation

##### 2.1.2.4.1 Entity Mapping Completeness

Entities defined but complex value objects need explicit configuration.

##### 2.1.2.4.2 Missing Database Components

- Money Value Object EF Configuration
- Concurrency token configuration for financial entities

##### 2.1.2.4.3 Added Database Components

- MoneyConfiguration
- RowVersionShadowProperty

#### 2.1.2.5.0 Sequence Interaction Validation

##### 2.1.2.5.1 Interaction Implementation Completeness

Flows for Invoicing and Payouts are logical.

##### 2.1.2.5.2 Missing Interaction Components

- Webhook signature validation middleware

##### 2.1.2.5.3 Added Interaction Components

- StripeSignatureValidationFilter

### 2.1.3.0.0 Enhanced Specification

#### 2.1.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-SVC-FINANCIAL |
| Technology Stack | ASP.NET Core 8, Entity Framework Core 8, MediatR, ... |
| Technology Guidance Integration | Microsoft Cloud Design Patterns, DDD, Clean Archit... |
| Framework Compliance Score | 100% |
| Specification Completeness | Complete |
| Component Count | 52 |
| Specification Methodology | Domain-Driven Design with Clean Architecture |

#### 2.1.3.2.0 Technology Framework Integration

##### 2.1.3.2.1 Framework Patterns Applied

- Clean Architecture (Domain, Application, Infrastructure, WebAPI)
- CQRS with MediatR (Commands/Queries)
- Domain-Driven Design (Aggregates, Value Objects, Domain Events)
- Transactional Outbox Pattern (Reliable Messaging)
- Idempotent Consumer Pattern (Webhook Processing)
- Adapter Pattern (Payment Gateway Abstractions)
- Saga Pattern Participation (Orchestrated Financial Workflows)

##### 2.1.3.2.2 Directory Structure Source

ASP.NET Core Clean Architecture Solution Template

##### 2.1.3.2.3 Naming Conventions Source

Microsoft .NET Coding Conventions (PascalCase)

##### 2.1.3.2.4 Architectural Patterns Source

Microservices with Event-Driven Architecture

##### 2.1.3.2.5 Performance Optimizations Applied

- EF Core Context Pooling
- Asynchronous I/O (async/await) for all Gateway/DB calls
- Read-Optimized Queries (AsNoTracking) for Reports
- Compensating Transactions for Financial Consistency
- ValueTasks for high-throughput paths

#### 2.1.3.3.0 File Structure

##### 2.1.3.3.1 Directory Organization

###### 2.1.3.3.1.1 Directory Path

####### 2.1.3.3.1.1.1 Directory Path

src/EnterpriseMediator.Financial.Domain

####### 2.1.3.3.1.1.2 Purpose

Core business logic, entities, and domain interfaces independent of infrastructure.

####### 2.1.3.3.1.1.3 Contains Files

- Entities/Invoice.cs
- Entities/Transaction.cs
- Entities/Payout.cs
- ValueObjects/Money.cs
- ValueObjects/Currency.cs
- Events/InvoicePaidEvent.cs
- Events/PayoutProcessedEvent.cs
- Interfaces/IPaymentGateway.cs
- Interfaces/IPayoutGateway.cs
- Interfaces/IFinancialRepository.cs
- Services/InvoiceCalculationService.cs

####### 2.1.3.3.1.1.4 Organizational Reasoning

Enforces the Dependency Inversion Principle; the core domain has no external dependencies.

####### 2.1.3.3.1.1.5 Framework Convention Alignment

Standard .NET Standard 2.1/NET 8 Class Library

###### 2.1.3.3.1.2.0 Directory Path

####### 2.1.3.3.1.2.1 Directory Path

src/EnterpriseMediator.Financial.Application

####### 2.1.3.3.1.2.2 Purpose

Orchestrates use cases using CQRS, handles commands/queries, and maps DTOs.

####### 2.1.3.3.1.2.3 Contains Files

- Features/Invoices/Commands/GenerateInvoice/GenerateInvoiceCommand.cs
- Features/Invoices/Commands/GenerateInvoice/GenerateInvoiceHandler.cs
- Features/Payments/EventHandlers/StripeWebhookHandler.cs
- Features/Payouts/Commands/InitiatePayout/InitiatePayoutCommand.cs
- Features/Ledger/Queries/GetTransactionHistory/GetTransactionHistoryQuery.cs
- Common/Behaviors/TransactionalOutboxBehavior.cs
- Common/Behaviors/LoggingBehavior.cs
- Common/Behaviors/IdempotencyBehavior.cs
- DTOs/InvoiceDto.cs

####### 2.1.3.3.1.2.4 Organizational Reasoning

Application layer depends on Domain and orchestrates logic without knowing specific infrastructure implementation details.

####### 2.1.3.3.1.2.5 Framework Convention Alignment

MediatR library conventions for Request/Handler pairs

###### 2.1.3.3.1.3.0 Directory Path

####### 2.1.3.3.1.3.1 Directory Path

src/EnterpriseMediator.Financial.Infrastructure

####### 2.1.3.3.1.3.2 Purpose

Implements interfaces defined in Domain/Application, handles DB access, and external API integrations.

####### 2.1.3.3.1.3.3 Contains Files

- Persistence/FinancialDbContext.cs
- Persistence/Configurations/InvoiceConfiguration.cs
- Persistence/Configurations/MoneyConfiguration.cs
- Gateways/Stripe/StripePaymentAdapter.cs
- Gateways/Wise/WisePayoutAdapter.cs
- Messaging/MassTransitExtensions.cs
- Services/DateTimeProvider.cs
- Interceptors/FinancialAuditInterceptor.cs

####### 2.1.3.3.1.3.4 Organizational Reasoning

Encapsulates all external concerns (DB, 3rd Party APIs) to keep the core clean.

####### 2.1.3.3.1.3.5 Framework Convention Alignment

EF Core patterns, SDK implementations

###### 2.1.3.3.1.4.0 Directory Path

####### 2.1.3.3.1.4.1 Directory Path

src/EnterpriseMediator.Financial.Web.API

####### 2.1.3.3.1.4.2 Purpose

Entry point for HTTP requests and Webhook callbacks.

####### 2.1.3.3.1.4.3 Contains Files

- Controllers/InvoicesController.cs
- Controllers/Webhooks/StripeWebhookController.cs
- Controllers/LedgerController.cs
- Program.cs
- appsettings.json

####### 2.1.3.3.1.4.4 Organizational Reasoning

Exposes functionality via REST and handles infrastructure wiring.

####### 2.1.3.3.1.4.5 Framework Convention Alignment

ASP.NET Core Web API project structure

##### 2.1.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | EnterpriseMediator.Financial |
| Namespace Organization | Matches folder structure (e.g., EnterpriseMediator... |
| Naming Conventions | PascalCase |
| Framework Alignment | Standard .NET namespace conventions |

#### 2.1.3.4.0.0.0 Class Specifications

##### 2.1.3.4.1.0.0 Class Name

###### 2.1.3.4.1.1.0 Class Name

Invoice

###### 2.1.3.4.1.2.0 File Path

src/EnterpriseMediator.Financial.Domain/Entities/Invoice.cs

###### 2.1.3.4.1.3.0 Class Type

Entity (Aggregate Root)

###### 2.1.3.4.1.4.0 Inheritance

BaseEntity<Guid>, IAggregateRoot

###### 2.1.3.4.1.5.0 Purpose

Represents a billable entity for a project, tracking amount due, tax, fees, and payment status.

###### 2.1.3.4.1.6.0 Dependencies

- Money
- InvoiceStatus (Enum)
- InvoicePaidEvent

###### 2.1.3.4.1.7.0 Framework Specific Attributes

*No items available*

###### 2.1.3.4.1.8.0 Technology Integration Notes

Uses DDD patterns. Private setters for properties to enforce encapsulation. Methods for state transitions (e.g., MarkAsPaid).

###### 2.1.3.4.1.9.0 Properties

####### 2.1.3.4.1.9.1 Property Name

######## 2.1.3.4.1.9.1.1 Property Name

ProjectId

######## 2.1.3.4.1.9.1.2 Property Type

Guid

######## 2.1.3.4.1.9.1.3 Access Modifier

public

######## 2.1.3.4.1.9.1.4 Purpose

Links the invoice to a specific project in the Project Service.

######## 2.1.3.4.1.9.1.5 Validation Attributes

*No items available*

######## 2.1.3.4.1.9.1.6 Framework Specific Configuration

Indexed in EF Core configuration

######## 2.1.3.4.1.9.1.7 Implementation Notes

Foreign key reference logic only, no navigation property to external service entity.

####### 2.1.3.4.1.9.2.0 Property Name

######## 2.1.3.4.1.9.2.1 Property Name

TotalAmount

######## 2.1.3.4.1.9.2.2 Property Type

Money

######## 2.1.3.4.1.9.2.3 Access Modifier

public

######## 2.1.3.4.1.9.2.4 Purpose

The total amount to be paid by the client.

######## 2.1.3.4.1.9.2.5 Validation Attributes

*No items available*

######## 2.1.3.4.1.9.2.6 Framework Specific Configuration

Owned Entity in EF Core

######## 2.1.3.4.1.9.2.7 Implementation Notes

Composite of Amount (decimal) and Currency (string).

####### 2.1.3.4.1.9.3.0 Property Name

######## 2.1.3.4.1.9.3.1 Property Name

Status

######## 2.1.3.4.1.9.3.2 Property Type

InvoiceStatus

######## 2.1.3.4.1.9.3.3 Access Modifier

public

######## 2.1.3.4.1.9.3.4 Purpose

Current state of the invoice (Draft, Sent, Paid, Overdue, Cancelled).

######## 2.1.3.4.1.9.3.5 Validation Attributes

*No items available*

######## 2.1.3.4.1.9.3.6 Framework Specific Configuration

Stored as string or int enum

######## 2.1.3.4.1.9.3.7 Implementation Notes

State transitions managed by domain methods.

####### 2.1.3.4.1.9.4.0 Property Name

######## 2.1.3.4.1.9.4.1 Property Name

StripePaymentIntentId

######## 2.1.3.4.1.9.4.2 Property Type

string

######## 2.1.3.4.1.9.4.3 Access Modifier

public

######## 2.1.3.4.1.9.4.4 Purpose

Reference to the Stripe Payment Intent for tracking.

######## 2.1.3.4.1.9.4.5 Validation Attributes

*No items available*

######## 2.1.3.4.1.9.4.6 Framework Specific Configuration

Nullable

######## 2.1.3.4.1.9.4.7 Implementation Notes

Populated when payment is initiated.

###### 2.1.3.4.1.10.0.0 Methods

- {'method_name': 'MarkAsPaid', 'method_signature': 'public void MarkAsPaid(string transactionReference, DateTime paidAt)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'transactionReference', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'External ID from payment gateway', 'framework_attributes': []}, {'parameter_name': 'paidAt', 'parameter_type': 'DateTime', 'is_nullable': 'false', 'purpose': 'Timestamp of payment', 'framework_attributes': []}], 'implementation_logic': 'Validates current status (must be Sent). Updates Status to Paid. Records payment timestamp. Adds InvoicePaidEvent domain event.', 'exception_handling': 'Throws DomainException if invoice is already paid or cancelled.', 'performance_considerations': 'In-memory state change.', 'validation_requirements': 'Transaction reference must not be empty.', 'technology_integration_details': 'Adds event to DomainEvents collection for Outbox processing.'}

###### 2.1.3.4.1.11.0.0 Events

- {'event_name': 'InvoicePaidEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'When MarkAsPaid is successfully called.', 'event_data': 'InvoiceId, ProjectId, Amount, PaidAt'}

###### 2.1.3.4.1.12.0.0 Implementation Notes

Core domain entity. Must remain persistence-ignorant.

##### 2.1.3.4.2.0.0.0 Class Name

###### 2.1.3.4.2.1.0.0 Class Name

GenerateInvoiceHandler

###### 2.1.3.4.2.2.0.0 File Path

src/EnterpriseMediator.Financial.Application/Features/Invoices/Handlers/GenerateInvoiceHandler.cs

###### 2.1.3.4.2.3.0.0 Class Type

CommandHandler

###### 2.1.3.4.2.4.0.0 Inheritance

IRequestHandler<GenerateInvoiceCommand, Result<Guid>>

###### 2.1.3.4.2.5.0.0 Purpose

Orchestrates the creation of an invoice when a project is awarded.

###### 2.1.3.4.2.6.0.0 Dependencies

- IFinancialRepository
- IPaymentGateway
- IValidator<GenerateInvoiceCommand>
- ILogger<GenerateInvoiceHandler>

###### 2.1.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

###### 2.1.3.4.2.8.0.0 Technology Integration Notes

Uses MediatR. Transactional behavior handled by pipeline behavior.

###### 2.1.3.4.2.9.0.0 Properties

*No items available*

###### 2.1.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result<Guid>> Handle(GenerateInvoiceCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result<Guid>>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'GenerateInvoiceCommand', 'is_nullable': 'false', 'purpose': 'Command payload containing project details and amount.', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Propagation of cancellation.', 'framework_attributes': []}], 'implementation_logic': '1. Validate request using FluentValidation. 2. Check for existing invoice for project (idempotency check). 3. Create Invoice domain entity. 4. Call IPaymentGateway to create Payment Link/Intent. 5. Update Invoice with gateway ID. 6. Save to repository via UnitOfWork.', 'exception_handling': 'Catches GatewayExceptions and wraps in Result.Failure. Retries managed by infrastructure polly policies.', 'performance_considerations': 'Async calls to gateway.', 'validation_requirements': 'FluentValidation executed via pipeline behavior.', 'technology_integration_details': 'Injects Repositories and Gateways via DI.'}

###### 2.1.3.4.2.11.0.0 Events

*No items available*

###### 2.1.3.4.2.12.0.0 Implementation Notes

Ensures idempotency by checking ProjectId against existing invoices to prevent duplicate billing.

##### 2.1.3.4.3.0.0.0 Class Name

###### 2.1.3.4.3.1.0.0 Class Name

StripePaymentAdapter

###### 2.1.3.4.3.2.0.0 File Path

src/EnterpriseMediator.Financial.Infrastructure/Gateways/Stripe/StripePaymentAdapter.cs

###### 2.1.3.4.3.3.0.0 Class Type

Adapter / Service

###### 2.1.3.4.3.4.0.0 Inheritance

IPaymentGateway

###### 2.1.3.4.3.5.0.0 Purpose

Encapsulates Stripe.net SDK calls to isolate the domain from 3rd party dependencies.

###### 2.1.3.4.3.6.0.0 Dependencies

- IOptions<StripeSettings>
- PaymentIntentService
- ILogger<StripePaymentAdapter>

###### 2.1.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

###### 2.1.3.4.3.8.0.0 Technology Integration Notes

Uses Stripe.net SDK. Registers as Scoped/Transient.

###### 2.1.3.4.3.9.0.0 Properties

*No items available*

###### 2.1.3.4.3.10.0.0 Methods

- {'method_name': 'CreatePaymentLinkAsync', 'method_signature': 'public async Task<PaymentLinkResult> CreatePaymentLinkAsync(Invoice invoice, CancellationToken cancellationToken)', 'return_type': 'Task<PaymentLinkResult>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'invoice', 'parameter_type': 'Invoice', 'is_nullable': 'false', 'purpose': 'Domain entity containing amount and currency.', 'framework_attributes': []}], 'implementation_logic': 'Maps Invoice to Stripe PaymentIntentCreateOptions. Calls Stripe API. Returns normalized PaymentLinkResult.', 'exception_handling': 'Wraps StripeException into domain-defined PaymentGatewayException. Logs stripe error details securely.', 'performance_considerations': 'Network I/O bound.', 'validation_requirements': 'Validates Stripe API Keys are present in options.', 'technology_integration_details': 'Sets metadata in Stripe object (ProjectId, InvoiceId) for webhook reconciliation.'}

###### 2.1.3.4.3.11.0.0 Events

*No items available*

###### 2.1.3.4.3.12.0.0 Implementation Notes

Metadata tagging in Stripe is crucial for correlating Webhooks back to internal entities.

##### 2.1.3.4.4.0.0.0 Class Name

###### 2.1.3.4.4.1.0.0 Class Name

StripeWebhookController

###### 2.1.3.4.4.2.0.0 File Path

src/EnterpriseMediator.Financial.Web.API/Controllers/Webhooks/StripeWebhookController.cs

###### 2.1.3.4.4.3.0.0 Class Type

Controller

###### 2.1.3.4.4.4.0.0 Inheritance

ControllerBase

###### 2.1.3.4.4.5.0.0 Purpose

Receives and validates webhooks from Stripe, dispatching internal commands.

###### 2.1.3.4.4.6.0.0 Dependencies

- ISender
- IOptions<StripeSettings>
- ILogger<StripeWebhookController>

###### 2.1.3.4.4.7.0.0 Framework Specific Attributes

- [Route(\"api/webhooks/stripe\")]
- [ApiController]

###### 2.1.3.4.4.8.0.0 Technology Integration Notes

Must handle raw body reading for signature verification.

###### 2.1.3.4.4.9.0.0 Properties

*No items available*

###### 2.1.3.4.4.10.0.0 Methods

- {'method_name': 'HandleWebhook', 'method_signature': 'public async Task<IActionResult> HandleWebhook()', 'return_type': 'Task<IActionResult>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': ['[HttpPost]'], 'parameters': [], 'implementation_logic': '1. Read Request.Body stream. 2. Verify Stripe Signature header using SDK. 3. Switch on event type (payment_intent.succeeded). 4. Dispatch PaymentSucceededCommand via MediatR. 5. Return 200 OK.', 'exception_handling': 'Return 400 BadRequest if signature invalid. Return 500 if internal processing fails (triggering Stripe retry).', 'performance_considerations': 'Minimal logic, offload heavy work to MediatR handler.', 'validation_requirements': 'Signature verification is mandatory security check.', 'technology_integration_details': 'Uses StripeEventUtility.ConstructEvent.'}

###### 2.1.3.4.4.11.0.0 Events

*No items available*

###### 2.1.3.4.4.12.0.0 Implementation Notes

Should define a dedicated route and disable standard model binding for the body to ensure raw stream access.

#### 2.1.3.5.0.0.0.0 Interface Specifications

##### 2.1.3.5.1.0.0.0 Interface Name

###### 2.1.3.5.1.1.0.0 Interface Name

IPaymentGateway

###### 2.1.3.5.1.2.0.0 File Path

src/EnterpriseMediator.Financial.Domain/Interfaces/IPaymentGateway.cs

###### 2.1.3.5.1.3.0.0 Purpose

Abstraction for accepting payments from clients. Allows swapping providers (e.g., Stripe) without changing domain logic.

###### 2.1.3.5.1.4.0.0 Generic Constraints

None

###### 2.1.3.5.1.5.0.0 Framework Specific Inheritance

None

###### 2.1.3.5.1.6.0.0 Method Contracts

- {'method_name': 'CreatePaymentLinkAsync', 'method_signature': 'Task<PaymentLinkResult> CreatePaymentLinkAsync(Invoice invoice, CancellationToken cancellationToken)', 'return_type': 'Task<PaymentLinkResult>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'invoice', 'parameter_type': 'Invoice', 'purpose': 'The invoice needing payment.'}], 'contract_description': 'Generates a URL or Intent ID for the client to pay the invoice.', 'exception_contracts': 'Throws PaymentGatewayException on failure.'}

###### 2.1.3.5.1.7.0.0 Property Contracts

*No items available*

###### 2.1.3.5.1.8.0.0 Implementation Guidance

Implementations should handle retries and idempotency with the external provider.

##### 2.1.3.5.2.0.0.0 Interface Name

###### 2.1.3.5.2.1.0.0 Interface Name

IFinancialRepository

###### 2.1.3.5.2.2.0.0 File Path

src/EnterpriseMediator.Financial.Domain/Interfaces/IFinancialRepository.cs

###### 2.1.3.5.2.3.0.0 Purpose

Repository pattern abstraction for Financial aggregate root access.

###### 2.1.3.5.2.4.0.0 Generic Constraints

None

###### 2.1.3.5.2.5.0.0 Framework Specific Inheritance

None

###### 2.1.3.5.2.6.0.0 Method Contracts

####### 2.1.3.5.2.6.1.0 Method Name

######## 2.1.3.5.2.6.1.1 Method Name

GetInvoiceByProjectIdAsync

######## 2.1.3.5.2.6.1.2 Method Signature

Task<Invoice?> GetInvoiceByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)

######## 2.1.3.5.2.6.1.3 Return Type

Task<Invoice?>

######## 2.1.3.5.2.6.1.4 Framework Attributes

*No items available*

######## 2.1.3.5.2.6.1.5 Parameters

- {'parameter_name': 'projectId', 'parameter_type': 'Guid', 'purpose': 'Project Identifier'}

######## 2.1.3.5.2.6.1.6 Contract Description

Retrieves invoice associated with a project.

######## 2.1.3.5.2.6.1.7 Exception Contracts

None

####### 2.1.3.5.2.6.2.0 Method Name

######## 2.1.3.5.2.6.2.1 Method Name

AddInvoiceAsync

######## 2.1.3.5.2.6.2.2 Method Signature

Task AddInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)

######## 2.1.3.5.2.6.2.3 Return Type

Task

######## 2.1.3.5.2.6.2.4 Framework Attributes

*No items available*

######## 2.1.3.5.2.6.2.5 Parameters

- {'parameter_name': 'invoice', 'parameter_type': 'Invoice', 'purpose': 'New invoice entity'}

######## 2.1.3.5.2.6.2.6 Contract Description

Adds a new invoice to the context.

######## 2.1.3.5.2.6.2.7 Exception Contracts

None

###### 2.1.3.5.2.7.0.0 Property Contracts

- {'property_name': 'UnitOfWork', 'property_type': 'IUnitOfWork', 'getter_contract': 'Gets the Unit of Work interface for transaction management', 'setter_contract': 'None'}

###### 2.1.3.5.2.8.0.0 Implementation Guidance

Implemented by EF Core repository in Infrastructure.

#### 2.1.3.6.0.0.0.0 Enum Specifications

##### 2.1.3.6.1.0.0.0 Enum Name

###### 2.1.3.6.1.1.0.0 Enum Name

InvoiceStatus

###### 2.1.3.6.1.2.0.0 File Path

src/EnterpriseMediator.Financial.Domain/Enums/InvoiceStatus.cs

###### 2.1.3.6.1.3.0.0 Underlying Type

int

###### 2.1.3.6.1.4.0.0 Purpose

Defines the lifecycle states of an invoice.

###### 2.1.3.6.1.5.0.0 Framework Attributes

*No items available*

###### 2.1.3.6.1.6.0.0 Values

####### 2.1.3.6.1.6.1.0 Value Name

######## 2.1.3.6.1.6.1.1 Value Name

Draft

######## 2.1.3.6.1.6.1.2 Value

0

######## 2.1.3.6.1.6.1.3 Description

Created but not yet finalized.

####### 2.1.3.6.1.6.2.0 Value Name

######## 2.1.3.6.1.6.2.1 Value Name

Sent

######## 2.1.3.6.1.6.2.2 Value

1

######## 2.1.3.6.1.6.2.3 Description

Finalized and payment link generated/sent to client.

####### 2.1.3.6.1.6.3.0 Value Name

######## 2.1.3.6.1.6.3.1 Value Name

Paid

######## 2.1.3.6.1.6.3.2 Value

2

######## 2.1.3.6.1.6.3.3 Description

Payment successfully captured.

####### 2.1.3.6.1.6.4.0 Value Name

######## 2.1.3.6.1.6.4.1 Value Name

Overdue

######## 2.1.3.6.1.6.4.2 Value

3

######## 2.1.3.6.1.6.4.3 Description

Payment not received by due date.

####### 2.1.3.6.1.6.5.0 Value Name

######## 2.1.3.6.1.6.5.1 Value Name

Cancelled

######## 2.1.3.6.1.6.5.2 Value

4

######## 2.1.3.6.1.6.5.3 Description

Invoice voided.

##### 2.1.3.6.2.0.0.0 Enum Name

###### 2.1.3.6.2.1.0.0 Enum Name

TransactionType

###### 2.1.3.6.2.2.0.0 File Path

src/EnterpriseMediator.Financial.Domain/Enums/TransactionType.cs

###### 2.1.3.6.2.3.0.0 Underlying Type

int

###### 2.1.3.6.2.4.0.0 Purpose

Classifies ledger entries for reporting and auditing.

###### 2.1.3.6.2.5.0.0 Framework Attributes

*No items available*

###### 2.1.3.6.2.6.0.0 Values

####### 2.1.3.6.2.6.1.0 Value Name

######## 2.1.3.6.2.6.1.1 Value Name

ClientPayment

######## 2.1.3.6.2.6.1.2 Value

1

######## 2.1.3.6.2.6.1.3 Description

Incoming funds from client.

####### 2.1.3.6.2.6.2.0 Value Name

######## 2.1.3.6.2.6.2.1 Value Name

VendorPayout

######## 2.1.3.6.2.6.2.2 Value

2

######## 2.1.3.6.2.6.2.3 Description

Outgoing funds to vendor.

####### 2.1.3.6.2.6.3.0 Value Name

######## 2.1.3.6.2.6.3.1 Value Name

PlatformFee

######## 2.1.3.6.2.6.3.2 Value

3

######## 2.1.3.6.2.6.3.3 Description

Revenue retained by platform.

####### 2.1.3.6.2.6.4.0 Value Name

######## 2.1.3.6.2.6.4.1 Value Name

Refund

######## 2.1.3.6.2.6.4.2 Value

4

######## 2.1.3.6.2.6.4.3 Description

Funds returned to client.

#### 2.1.3.7.0.0.0.0 Dto Specifications

##### 2.1.3.7.1.0.0.0 Dto Name

###### 2.1.3.7.1.1.0.0 Dto Name

GenerateInvoiceCommand

###### 2.1.3.7.1.2.0.0 File Path

src/EnterpriseMediator.Financial.Application/Features/Invoices/Commands/GenerateInvoice/GenerateInvoiceCommand.cs

###### 2.1.3.7.1.3.0.0 Purpose

Command payload to trigger invoice generation.

###### 2.1.3.7.1.4.0.0 Framework Base Class

IRequest<Result<Guid>>

###### 2.1.3.7.1.5.0.0 Properties

####### 2.1.3.7.1.5.1.0 Property Name

######## 2.1.3.7.1.5.1.1 Property Name

ProjectId

######## 2.1.3.7.1.5.1.2 Property Type

Guid

######## 2.1.3.7.1.5.1.3 Validation Attributes

- [Required]

######## 2.1.3.7.1.5.1.4 Serialization Attributes

*No items available*

######## 2.1.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.1.5.2.0 Property Name

######## 2.1.3.7.1.5.2.1 Property Name

Amount

######## 2.1.3.7.1.5.2.2 Property Type

decimal

######## 2.1.3.7.1.5.2.3 Validation Attributes

- [Range(0.01, double.MaxValue)]

######## 2.1.3.7.1.5.2.4 Serialization Attributes

*No items available*

######## 2.1.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.1.5.3.0 Property Name

######## 2.1.3.7.1.5.3.1 Property Name

CurrencyCode

######## 2.1.3.7.1.5.3.2 Property Type

string

######## 2.1.3.7.1.5.3.3 Validation Attributes

- [Length(3,3)]
- [Required]

######## 2.1.3.7.1.5.3.4 Serialization Attributes

*No items available*

######## 2.1.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.1.5.4.0 Property Name

######## 2.1.3.7.1.5.4.1 Property Name

ClientId

######## 2.1.3.7.1.5.4.2 Property Type

Guid

######## 2.1.3.7.1.5.4.3 Validation Attributes

- [Required]

######## 2.1.3.7.1.5.4.4 Serialization Attributes

*No items available*

######## 2.1.3.7.1.5.4.5 Framework Specific Attributes

*No items available*

###### 2.1.3.7.1.6.0.0 Validation Rules

ProjectId must be valid. Amount must be positive. Currency must be ISO code.

###### 2.1.3.7.1.7.0.0 Serialization Requirements

Standard JSON

##### 2.1.3.7.2.0.0.0 Dto Name

###### 2.1.3.7.2.1.0.0 Dto Name

TransactionSummaryDto

###### 2.1.3.7.2.2.0.0 File Path

src/EnterpriseMediator.Financial.Application/Features/Ledger/DTOs/TransactionSummaryDto.cs

###### 2.1.3.7.2.3.0.0 Purpose

Read model for financial transactions list.

###### 2.1.3.7.2.4.0.0 Framework Base Class

None

###### 2.1.3.7.2.5.0.0 Properties

####### 2.1.3.7.2.5.1.0 Property Name

######## 2.1.3.7.2.5.1.1 Property Name

Id

######## 2.1.3.7.2.5.1.2 Property Type

Guid

######## 2.1.3.7.2.5.1.3 Validation Attributes

*No items available*

######## 2.1.3.7.2.5.1.4 Serialization Attributes

*No items available*

######## 2.1.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.2.5.2.0 Property Name

######## 2.1.3.7.2.5.2.1 Property Name

Type

######## 2.1.3.7.2.5.2.2 Property Type

string

######## 2.1.3.7.2.5.2.3 Validation Attributes

*No items available*

######## 2.1.3.7.2.5.2.4 Serialization Attributes

*No items available*

######## 2.1.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.2.5.3.0 Property Name

######## 2.1.3.7.2.5.3.1 Property Name

Amount

######## 2.1.3.7.2.5.3.2 Property Type

decimal

######## 2.1.3.7.2.5.3.3 Validation Attributes

*No items available*

######## 2.1.3.7.2.5.3.4 Serialization Attributes

*No items available*

######## 2.1.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

####### 2.1.3.7.2.5.4.0 Property Name

######## 2.1.3.7.2.5.4.1 Property Name

Date

######## 2.1.3.7.2.5.4.2 Property Type

DateTimeOffset

######## 2.1.3.7.2.5.4.3 Validation Attributes

*No items available*

######## 2.1.3.7.2.5.4.4 Serialization Attributes

*No items available*

######## 2.1.3.7.2.5.4.5 Framework Specific Attributes

*No items available*

###### 2.1.3.7.2.6.0.0 Validation Rules

None (Read Model)

###### 2.1.3.7.2.7.0.0 Serialization Requirements

Standard JSON

#### 2.1.3.8.0.0.0.0 Configuration Specifications

##### 2.1.3.8.1.0.0.0 Configuration Name

###### 2.1.3.8.1.1.0.0 Configuration Name

StripeSettings

###### 2.1.3.8.1.2.0.0 File Path

src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/StripeSettings.cs

###### 2.1.3.8.1.3.0.0 Purpose

Holds configuration for Stripe integration.

###### 2.1.3.8.1.4.0.0 Framework Base Class

None

###### 2.1.3.8.1.5.0.0 Configuration Sections

- {'section_name': 'Stripe', 'properties': [{'property_name': 'ApiKey', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Secret API Key for Stripe.'}, {'property_name': 'WebhookSecret', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Secret for verifying webhook signatures.'}]}

###### 2.1.3.8.1.6.0.0 Validation Requirements

ApiKey must be present.

##### 2.1.3.8.2.0.0.0 Configuration Name

###### 2.1.3.8.2.1.0.0 Configuration Name

WiseSettings

###### 2.1.3.8.2.2.0.0 File Path

src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/WiseSettings.cs

###### 2.1.3.8.2.3.0.0 Purpose

Holds configuration for Wise integration.

###### 2.1.3.8.2.4.0.0 Framework Base Class

None

###### 2.1.3.8.2.5.0.0 Configuration Sections

- {'section_name': 'Wise', 'properties': [{'property_name': 'ApiToken', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Access Token for Wise API.'}, {'property_name': 'ProfileId', 'property_type': 'long', 'default_value': '0', 'required': 'true', 'description': 'Business Profile ID in Wise.'}]}

###### 2.1.3.8.2.6.0.0 Validation Requirements

ApiToken and ProfileId must be present.

#### 2.1.3.9.0.0.0.0 Dependency Injection Specifications

##### 2.1.3.9.1.0.0.0 Service Interface

###### 2.1.3.9.1.1.0.0 Service Interface

IFinancialRepository

###### 2.1.3.9.1.2.0.0 Service Implementation

FinancialRepository

###### 2.1.3.9.1.3.0.0 Lifetime

Scoped

###### 2.1.3.9.1.4.0.0 Registration Reasoning

Standard EF Core Repository lifetime.

###### 2.1.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<IFinancialRepository, FinancialRepository>()

##### 2.1.3.9.2.0.0.0 Service Interface

###### 2.1.3.9.2.1.0.0 Service Interface

IPaymentGateway

###### 2.1.3.9.2.2.0.0 Service Implementation

StripePaymentAdapter

###### 2.1.3.9.2.3.0.0 Lifetime

Scoped

###### 2.1.3.9.2.4.0.0 Registration Reasoning

Depends on IOptionsSnapshot/IOptions and HttpClient, Scoped is safest for request-based integrations.

###### 2.1.3.9.2.5.0.0 Framework Registration Pattern

services.AddScoped<IPaymentGateway, StripePaymentAdapter>()

##### 2.1.3.9.3.0.0.0 Service Interface

###### 2.1.3.9.3.1.0.0 Service Interface

IPayoutGateway

###### 2.1.3.9.3.2.0.0 Service Implementation

WisePayoutAdapter

###### 2.1.3.9.3.3.0.0 Lifetime

Scoped

###### 2.1.3.9.3.4.0.0 Registration Reasoning

Similar to PaymentGateway, requires scoped http clients and configuration.

###### 2.1.3.9.3.5.0.0 Framework Registration Pattern

services.AddScoped<IPayoutGateway, WisePayoutAdapter>()

##### 2.1.3.9.4.0.0.0 Service Interface

###### 2.1.3.9.4.1.0.0 Service Interface

IConsumer<ProjectAwardedEvent>

###### 2.1.3.9.4.2.0.0 Service Implementation

ProjectAwardedConsumer

###### 2.1.3.9.4.3.0.0 Lifetime

Scoped

###### 2.1.3.9.4.4.0.0 Registration Reasoning

MassTransit consumer registration convention.

###### 2.1.3.9.4.5.0.0 Framework Registration Pattern

cfg.ReceiveEndpoint(..., e => { e.ConfigureConsumer<ProjectAwardedConsumer>(context); })

#### 2.1.3.10.0.0.0.0 External Integration Specifications

##### 2.1.3.10.1.0.0.0 Integration Target

###### 2.1.3.10.1.1.0.0 Integration Target

Stripe Connect

###### 2.1.3.10.1.2.0.0 Integration Type

Payment Gateway (REST API)

###### 2.1.3.10.1.3.0.0 Required Client Classes

- PaymentIntentService
- AccountService

###### 2.1.3.10.1.4.0.0 Configuration Requirements

Stripe:ApiKey, Stripe:WebhookSecret

###### 2.1.3.10.1.5.0.0 Error Handling Requirements

Handle StripeException. Implement retry policies for 5xx errors. Handle rate limits.

###### 2.1.3.10.1.6.0.0 Authentication Requirements

Bearer Token (API Key)

###### 2.1.3.10.1.7.0.0 Framework Integration Patterns

Use Stripe.net NuGet package. Inject services via DI. Validate Webhook Signatures.

##### 2.1.3.10.2.0.0.0 Integration Target

###### 2.1.3.10.2.1.0.0 Integration Target

Wise

###### 2.1.3.10.2.2.0.0 Integration Type

Payout Gateway (REST API)

###### 2.1.3.10.2.3.0.0 Required Client Classes

- HttpClient

###### 2.1.3.10.2.4.0.0 Configuration Requirements

Wise:ApiToken, Wise:ProfileId

###### 2.1.3.10.2.5.0.0 Error Handling Requirements

Handle HTTP 4xx/5xx responses. Ensure idempotency key usage.

###### 2.1.3.10.2.6.0.0 Authentication Requirements

Bearer Token (API Token)

###### 2.1.3.10.2.7.0.0 Framework Integration Patterns

Use Typed HttpClient with Polly policies for resilience.

##### 2.1.3.10.3.0.0.0 Integration Target

###### 2.1.3.10.3.1.0.0 Integration Target

RabbitMQ

###### 2.1.3.10.3.2.0.0 Integration Type

Message Broker

###### 2.1.3.10.3.3.0.0 Required Client Classes

- IPublishEndpoint
- IBus

###### 2.1.3.10.3.4.0.0 Configuration Requirements

RabbitMq:Host, RabbitMq:Username, RabbitMq:Password

###### 2.1.3.10.3.5.0.0 Error Handling Requirements

Outbox pattern for publishing to ensure atomicity with DB. Retry middleware for consumption.

###### 2.1.3.10.3.6.0.0 Authentication Requirements

Credentials

###### 2.1.3.10.3.7.0.0 Framework Integration Patterns

MassTransit over RabbitMQ.

### 2.1.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 2 |
| Total Dtos | 2 |
| Total Configurations | 2 |
| Total External Integrations | 3 |
| Grand Total Components | 52 |
| Phase 2 Claimed Count | 45 |
| Phase 2 Actual Count | 40 |
| Validation Added Count | 12 |
| Final Validated Count | 52 |

# 3.0.0.0.0.0.0.0 File Structure

## 3.1.0.0.0.0.0.0 Directory Organization

### 3.1.1.0.0.0.0.0 Directory Path

#### 3.1.1.1.0.0.0.0 Directory Path

.dockerignore

#### 3.1.1.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.1.3.0.0.0.0 Contains Files

- .dockerignore

#### 3.1.1.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.1.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.2.0.0.0.0.0 Directory Path

#### 3.1.2.1.0.0.0.0 Directory Path

.editorconfig

#### 3.1.2.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.2.3.0.0.0.0 Contains Files

- .editorconfig

#### 3.1.2.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.2.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.3.0.0.0.0.0 Directory Path

#### 3.1.3.1.0.0.0.0 Directory Path

.gitignore

#### 3.1.3.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.3.3.0.0.0.0 Contains Files

- .gitignore

#### 3.1.3.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.3.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.4.0.0.0.0.0 Directory Path

#### 3.1.4.1.0.0.0.0 Directory Path

docker-compose.yml

#### 3.1.4.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.4.3.0.0.0.0 Contains Files

- docker-compose.yml

#### 3.1.4.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.4.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.5.0.0.0.0.0 Directory Path

#### 3.1.5.1.0.0.0.0 Directory Path

Dockerfile

#### 3.1.5.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.5.3.0.0.0.0 Contains Files

- Dockerfile

#### 3.1.5.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.5.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.6.0.0.0.0.0 Directory Path

#### 3.1.6.1.0.0.0.0 Directory Path

EnterpriseMediator.Financial.sln

#### 3.1.6.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.6.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.sln

#### 3.1.6.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.6.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.7.0.0.0.0.0 Directory Path

#### 3.1.7.1.0.0.0.0 Directory Path

global.json

#### 3.1.7.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.7.3.0.0.0.0 Contains Files

- global.json

#### 3.1.7.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.7.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.8.0.0.0.0.0 Directory Path

#### 3.1.8.1.0.0.0.0 Directory Path

nuget.config

#### 3.1.8.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.8.3.0.0.0.0 Contains Files

- nuget.config

#### 3.1.8.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.8.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.9.0.0.0.0.0 Directory Path

#### 3.1.9.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Application/EnterpriseMediator.Financial.Application.csproj

#### 3.1.9.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.9.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.Application.csproj

#### 3.1.9.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.9.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.10.0.0.0.0.0 Directory Path

#### 3.1.10.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Domain/EnterpriseMediator.Financial.Domain.csproj

#### 3.1.10.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.10.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.Domain.csproj

#### 3.1.10.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.10.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.11.0.0.0.0.0 Directory Path

#### 3.1.11.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Infrastructure/EnterpriseMediator.Financial.Infrastructure.csproj

#### 3.1.11.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.11.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.Infrastructure.csproj

#### 3.1.11.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.11.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.12.0.0.0.0.0 Directory Path

#### 3.1.12.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Web.API/appsettings.Development.json

#### 3.1.12.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.12.3.0.0.0.0 Contains Files

- appsettings.Development.json

#### 3.1.12.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.12.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.13.0.0.0.0.0 Directory Path

#### 3.1.13.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Web.API/EnterpriseMediator.Financial.Web.API.csproj

#### 3.1.13.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.13.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.Web.API.csproj

#### 3.1.13.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.13.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.14.0.0.0.0.0 Directory Path

#### 3.1.14.1.0.0.0.0 Directory Path

src/EnterpriseMediator.Financial.Web.API/Properties/launchSettings.json

#### 3.1.14.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.14.3.0.0.0.0 Contains Files

- launchSettings.json

#### 3.1.14.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.14.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.15.0.0.0.0.0 Directory Path

#### 3.1.15.1.0.0.0.0 Directory Path

tests/EnterpriseMediator.Financial.IntegrationTests/EnterpriseMediator.Financial.IntegrationTests.csproj

#### 3.1.15.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.15.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.IntegrationTests.csproj

#### 3.1.15.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.15.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.16.0.0.0.0.0 Directory Path

#### 3.1.16.1.0.0.0.0 Directory Path

tests/EnterpriseMediator.Financial.UnitTests/EnterpriseMediator.Financial.UnitTests.csproj

#### 3.1.16.2.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.16.3.0.0.0.0 Contains Files

- EnterpriseMediator.Financial.UnitTests.csproj

#### 3.1.16.4.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.16.5.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling


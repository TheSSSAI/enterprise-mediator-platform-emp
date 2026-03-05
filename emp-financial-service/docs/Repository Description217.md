# 1 Id

REPO-SVC-FINANCIAL

# 2 Name

emp-financial-service

# 3 Description

A backend microservice dedicated to managing all financial aspects of the platform. This repository handles invoicing, payment processing via Stripe Connect, vendor payouts via Wise, and financial reporting. It was created by extracting all financial logic from the monorepo. This service maintains its own database schema for transactions, invoices, and payouts, ensuring high security and auditability. Its isolation is critical for simplifying compliance (SOC 2) and protecting sensitive financial data. It subscribes to events from the Project Management service (e.g., `ProjectAwarded`) to trigger financial workflows.

# 4 Type

🔹 Business Logic

# 5 Namespace

EnterpriseMediator.Financial

# 6 Output Path

services/financial

# 7 Framework

ASP.NET Core 8

# 8 Language

C#

# 9 Technology

.NET, Entity Framework Core, Stripe SDK, Wise SDK

# 10 Thirdparty Libraries

- Stripe.net
- Wise.net

# 11 Layer Ids

- application-layer
- domain-layer
- infrastructure-layer

# 12 Dependencies

- REPO-LIB-DOMAIN
- REPO-LIB-CONTRACTS
- REPO-LIB-SHARED KERNEL

# 13 Requirements

- {'requirementId': 'REQ-FUN-004'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Microservice

# 17 Architecture Map

*No items available*

# 18 Components Map

*No items available*

# 19 Requirements Map

- REQ-FUN-004

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

EMP-MONOREPO-001

## 20.3 Decomposition Reasoning

Extracted to isolate highly sensitive and complex financial logic. This separation is crucial for security, auditability (SOC 2), and compliance (GDPR). It allows a specialized team to manage integrations with payment gateways and financial rules without affecting other parts of the system.

## 20.4 Extracted Responsibilities

- Client Invoicing
- Payment Processing (Stripe Connect)
- Vendor Payouts (Wise)
- Financial Ledger and Reporting

## 20.5 Reusability Scope

- The logic is highly specific to the EMP platform's business model.

## 20.6 Development Benefits

- Enhanced security by isolating financial data and logic.
- Simplified compliance and auditing.
- Independent deployment of financial features and payment gateway updates.

# 21.0 Dependency Contracts

## 21.1 Repo-Svc-Project

### 21.1.1 Required Interfaces

- {'interface': 'Event Consumer', 'methods': ['Handles ProjectAwardedEvent', 'Handles MilestoneCompletedEvent'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Asynchronous Event-Driven

### 21.1.3 Communication Protocol

AMQP (RabbitMQ)

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'Internal Financial Service API', 'methods': ['GenerateInvoice(GenerateInvoiceCommand)', 'GetTransactionHistory(GetHistoryQuery)'], 'events': ['PaymentReceivedEvent', 'PayoutCompletedEvent'], 'properties': [], 'consumers': ['REPO-GW-API']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Used for repositories, payment gateway clients, an... |
| Event Communication | Consumes events to trigger workflows (e.g., 'Proje... |
| Data Flow | Event-driven workflows and internal API for querie... |
| Error Handling | Robust error handling for payment gateway interact... |
| Async Patterns | All external API calls and database operations are... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Implement the Saga pattern for distributed transac... |
| Performance Considerations | Ensure idempotent processing of payment webhooks a... |
| Security Considerations | Highest level of security. All sensitive data (API... |
| Testing Approach | Heavy reliance on integration testing with mocked ... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- All financial transaction logic.
- Integration with Stripe and Wise.
- Internal ledger management.

## 25.2.0 Must Not Implement

- Project lifecycle management.
- User profile management.

## 25.3.0 Extension Points

- Adding new payment gateways.
- Supporting new currencies or payout methods.

## 25.4.0 Validation Rules

- Validate all financial amounts are positive.
- Ensure payouts do not exceed escrowed funds.


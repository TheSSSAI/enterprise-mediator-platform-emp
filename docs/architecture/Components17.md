# 1 Components

## 1.1 Components

### 1.1.1 WebFrontend

#### 1.1.1.1 Id

web-frontend-spa-001

#### 1.1.1.2 Name

WebFrontend

#### 1.1.1.3 Description

Client-side Single Page Application (SPA) that provides the entire user interface for the system. It is responsible for all rendering, client-side state, and user interaction, communicating with the backend via a RESTful API.

#### 1.1.1.4 Type

🔹 FrontendApplication

#### 1.1.1.5 Dependencies

- api-gateway-001

#### 1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Framework | Next.js |
| Version | 14 |

#### 1.1.1.7 Interfaces

*No items available*

#### 1.1.1.8 Technology

Next.js, React, TypeScript, Radix UI, Tailwind CSS

#### 1.1.1.9 Resources

| Property | Value |
|----------|-------|
| Cpu | N/A (Client-Side) |
| Memory | N/A (Client-Side) |
| Storage | N/A (Client-Side) |
| Network | Standard HTTPS |

#### 1.1.1.10 Configuration

##### 1.1.1.10.1 Api Base Url

process.env.NEXT_PUBLIC_API_URL

#### 1.1.1.11.0 Health Check

*Not specified*

#### 1.1.1.12.0 Responsible Features

- REQ-UI-001
- REQ-UI-002
- REQ-UI-003
- REQ-FUNC-005
- REQ-FUNC-007
- REQ-FUNC-008
- REQ-FUNC-013
- REQ-FUNC-018
- REQ-FUNC-019
- REQ-FUNC-024

#### 1.1.1.13.0 Security

##### 1.1.1.13.1 Requires Authentication

✅ Yes

##### 1.1.1.13.2 Requires Authorization

✅ Yes

##### 1.1.1.13.3 Allowed Roles

- SystemAdministrator
- VendorContact
- ClientContact

### 1.1.2.0.0 BackendApi

#### 1.1.2.1.0 Id

api-gateway-001

#### 1.1.2.2.0 Name

BackendApi

#### 1.1.2.3.0 Description

The main entry point for the Modular Monolith. It exposes all RESTful API endpoints, handles request validation, authentication, authorization, and orchestrates calls to the underlying application services.

#### 1.1.2.4.0 Type

🔹 ApiGateway

#### 1.1.2.5.0 Dependencies

- client-management-service-002
- vendor-management-service-003
- sow-processing-service-004
- proposal-workflow-service-005
- dashboard-service-006
- notification-service-007
- financial-config-service-008
- reporting-service-009

#### 1.1.2.6.0 Properties

| Property | Value |
|----------|-------|
| Framework | ASP.NET Core |
| Version | 8 |

#### 1.1.2.7.0 Interfaces

*No items available*

#### 1.1.2.8.0 Technology

ASP.NET Core 8

#### 1.1.2.9.0 Resources

| Property | Value |
|----------|-------|
| Cpu | 2 vCPU |
| Memory | 4GB |
| Storage | 1GB |
| Network | 1Gbps |

#### 1.1.2.10.0 Configuration

##### 1.1.2.10.1 Jwt Secret

from-secrets-manager

##### 1.1.2.10.2 Cors Origins

[https://app.domain.com]

#### 1.1.2.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /healthz |
| Interval | 30 |
| Timeout | 5 |

#### 1.1.2.12.0 Responsible Features

- All Functional Requirements (as entry point)

#### 1.1.2.13.0 Security

##### 1.1.2.13.1 Requires Authentication

✅ Yes

##### 1.1.2.13.2 Requires Authorization

✅ Yes

##### 1.1.2.13.3 Allowed Roles

- SystemAdministrator
- VendorContact

### 1.1.3.0.0 SowProcessingService

#### 1.1.3.1.0 Id

sow-processing-service-004

#### 1.1.3.2.0 Name

SowProcessingService

#### 1.1.3.3.0 Description

Application service responsible for handling the initial SOW document upload. It saves the document metadata, stores the file, and publishes an 'SowUploaded' event to a message queue for asynchronous processing.

#### 1.1.3.4.0 Type

🔹 Service

#### 1.1.3.5.0 Dependencies

- message-queue-publisher-010
- sow-document-repository-011
- unit-of-work-012

#### 1.1.3.6.0 Properties

*No data available*

#### 1.1.3.7.0 Interfaces

- ISowProcessingService

#### 1.1.3.8.0 Technology

.NET 8

#### 1.1.3.9.0 Resources

##### 1.1.3.9.1 Cpu

Shared with API

##### 1.1.3.9.2 Memory

Shared with API

#### 1.1.3.10.0 Configuration

##### 1.1.3.10.1 Sow Upload Queue Name

sow-processing-queue

#### 1.1.3.11.0 Health Check

*Not specified*

#### 1.1.3.12.0 Responsible Features

- REQ-FUNC-010

#### 1.1.3.13.0 Security

##### 1.1.3.13.1 Requires Authentication

✅ Yes

##### 1.1.3.13.2 Requires Authorization

✅ Yes

##### 1.1.3.13.3 Allowed Roles

- SystemAdministrator

### 1.1.4.0.0 SowProcessingWorker

#### 1.1.4.1.0 Id

sow-processing-worker-013

#### 1.1.4.2.0 Name

SowProcessingWorker

#### 1.1.4.3.0 Description

A separate, event-driven background service that consumes 'SowUploaded' events. It orchestrates the AI pipeline: text extraction, LLM data parsing, and database updates for the project brief.

#### 1.1.4.4.0 Type

🔹 BackgroundService

#### 1.1.4.5.0 Dependencies

- message-queue-client-014
- llm-client-015
- document-text-extractor-016
- project-brief-repository-017

#### 1.1.4.6.0 Properties

| Property | Value |
|----------|-------|
| Consumer Group | sow-processors |

#### 1.1.4.7.0 Interfaces

*No items available*

#### 1.1.4.8.0 Technology

.NET 8 Worker Service

#### 1.1.4.9.0 Resources

| Property | Value |
|----------|-------|
| Cpu | 2 vCPU (burst to 4) |
| Memory | 8GB |
| Storage | 5GB |
| Network | 1Gbps |

#### 1.1.4.10.0 Configuration

##### 1.1.4.10.1 Llm Api Endpoint

from-env

##### 1.1.4.10.2 Llm Api Key

from-secrets-manager

#### 1.1.4.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/worker |
| Interval | 60 |
| Timeout | 10 |

#### 1.1.4.12.0 Responsible Features

- REQ-FUNC-010

#### 1.1.4.13.0 Security

##### 1.1.4.13.1 Requires Authentication

✅ Yes

##### 1.1.4.13.2 Requires Authorization

❌ No

### 1.1.5.0.0 VendorMatchingService

#### 1.1.5.1.0 Id

vendor-matching-service-018

#### 1.1.5.2.0 Name

VendorMatchingService

#### 1.1.5.3.0 Description

Application service that performs semantic search using vector embeddings to identify and rank vendors whose skills match the requirements of an approved project brief.

#### 1.1.5.4.0 Type

🔹 Service

#### 1.1.5.5.0 Dependencies

- vendor-repository-019
- project-brief-repository-017

#### 1.1.5.6.0 Properties

*No data available*

#### 1.1.5.7.0 Interfaces

- IVendorMatchingService

#### 1.1.5.8.0 Technology

.NET 8

#### 1.1.5.9.0 Resources

##### 1.1.5.9.1 Cpu

Shared with API

##### 1.1.5.9.2 Memory

Shared with API

#### 1.1.5.10.0 Configuration

##### 1.1.5.10.1 Search Result Limit

25

#### 1.1.5.11.0 Health Check

*Not specified*

#### 1.1.5.12.0 Responsible Features

- REQ-FUNC-014

#### 1.1.5.13.0 Security

##### 1.1.5.13.1 Requires Authentication

✅ Yes

##### 1.1.5.13.2 Requires Authorization

✅ Yes

##### 1.1.5.13.3 Allowed Roles

- SystemAdministrator

### 1.1.6.0.0 VendorRepository

#### 1.1.6.1.0 Id

vendor-repository-019

#### 1.1.6.2.0 Name

VendorRepository

#### 1.1.6.3.0 Description

Infrastructure component that implements data access logic for Vendor and Skill entities. It handles CRUD operations and executes the vector similarity search against the PostgreSQL database using the pgvector extension.

#### 1.1.6.4.0 Type

🔹 Repository

#### 1.1.6.5.0 Dependencies

- database-context-020

#### 1.1.6.6.0 Properties

| Property | Value |
|----------|-------|
| Orm | Entity Framework Core 8 |
| Db Extension | pgvector |

#### 1.1.6.7.0 Interfaces

- IVendorRepository

#### 1.1.6.8.0 Technology

Entity Framework Core 8, Npgsql, pgvector.EntityFrameworkCore

#### 1.1.6.9.0 Resources

##### 1.1.6.9.1 Cpu

Shared with API

##### 1.1.6.9.2 Memory

Shared with API

#### 1.1.6.10.0 Configuration

*No data available*

#### 1.1.6.11.0 Health Check

*Not specified*

#### 1.1.6.12.0 Responsible Features

- REQ-FUNC-007
- REQ-FUNC-008
- REQ-FUNC-014

#### 1.1.6.13.0 Security

##### 1.1.6.13.1 Requires Authentication

❌ No

##### 1.1.6.13.2 Requires Authorization

❌ No

### 1.1.7.0.0 NotificationService

#### 1.1.7.1.0 Id

notification-service-007

#### 1.1.7.2.0 Name

NotificationService

#### 1.1.7.3.0 Description

Central application service for dispatching notifications for key system and project events. It orchestrates sending notifications through various configured channels (in-app, email, webhook).

#### 1.1.7.4.0 Type

🔹 Service

#### 1.1.7.5.0 Dependencies

- notification-repository-021
- email-dispatcher-022
- webhook-dispatcher-023

#### 1.1.7.6.0 Properties

*No data available*

#### 1.1.7.7.0 Interfaces

- INotificationService

#### 1.1.7.8.0 Technology

.NET 8

#### 1.1.7.9.0 Resources

##### 1.1.7.9.1 Cpu

Shared with API

##### 1.1.7.9.2 Memory

Shared with API

#### 1.1.7.10.0 Configuration

*No data available*

#### 1.1.7.11.0 Health Check

*Not specified*

#### 1.1.7.12.0 Responsible Features

- REQ-FUNC-025

#### 1.1.7.13.0 Security

##### 1.1.7.13.1 Requires Authentication

✅ Yes

##### 1.1.7.13.2 Requires Authorization

❌ No

### 1.1.8.0.0 EmailDispatcher

#### 1.1.8.1.0 Id

email-dispatcher-022

#### 1.1.8.2.0 Name

EmailDispatcher

#### 1.1.8.3.0 Description

Infrastructure component that handles sending outbound transactional emails by integrating with the AWS Simple Email Service (SES) API.

#### 1.1.8.4.0 Type

🔹 InfrastructureClient

#### 1.1.8.5.0 Dependencies

*No items available*

#### 1.1.8.6.0 Properties

| Property | Value |
|----------|-------|
| Provider | AWS SES |

#### 1.1.8.7.0 Interfaces

- IEmailDispatcher

#### 1.1.8.8.0 Technology

AWS SDK for .NET

#### 1.1.8.9.0 Resources

##### 1.1.8.9.1 Cpu

Shared with API

##### 1.1.8.9.2 Memory

Shared with API

#### 1.1.8.10.0 Configuration

##### 1.1.8.10.1 Aws Region

from-env

##### 1.1.8.10.2 Aws Access Key

from-secrets-manager

#### 1.1.8.11.0 Health Check

*Not specified*

#### 1.1.8.12.0 Responsible Features

- REQ-INTG-005
- REQ-FUNC-025

#### 1.1.8.13.0 Security

##### 1.1.8.13.1 Requires Authentication

❌ No

##### 1.1.8.13.2 Requires Authorization

❌ No

### 1.1.9.0.0 DashboardMetricsCalculator

#### 1.1.9.1.0 Id

dashboard-metrics-calculator-024

#### 1.1.9.2.0 Name

DashboardMetricsCalculator

#### 1.1.9.3.0 Description

A scheduled background job that periodically pre-calculates and aggregates key business metrics, populating a denormalized 'DashboardMetrics' table to ensure the main dashboard loads quickly.

#### 1.1.9.4.0 Type

🔹 ScheduledTask

#### 1.1.9.5.0 Dependencies

- database-context-020

#### 1.1.9.6.0 Properties

| Property | Value |
|----------|-------|
| Schedule | Every 15 minutes |

#### 1.1.9.7.0 Interfaces

*No items available*

#### 1.1.9.8.0 Technology

.NET 8 Worker Service with a scheduled trigger

#### 1.1.9.9.0 Resources

| Property | Value |
|----------|-------|
| Cpu | 1 vCPU |
| Memory | 2GB |
| Storage | 1GB |
| Network | 500Mbps |

#### 1.1.9.10.0 Configuration

##### 1.1.9.10.1 Cron Expression

*/15 * * * *

#### 1.1.9.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/scheduler |
| Interval | 300 |
| Timeout | 30 |

#### 1.1.9.12.0 Responsible Features

- REQ-FUNC-024

#### 1.1.9.13.0 Security

##### 1.1.9.13.1 Requires Authentication

✅ Yes

##### 1.1.9.13.2 Requires Authorization

❌ No

### 1.1.10.0.0 ReportingService

#### 1.1.10.1.0 Id

reporting-service-009

#### 1.1.10.2.0 Name

ReportingService

#### 1.1.10.3.0 Description

Application service responsible for generating business reports. This includes orchestrating data retrieval and formatting for reports like the Project Profitability Report.

#### 1.1.10.4.0 Type

🔹 Service

#### 1.1.10.5.0 Dependencies

- project-repository-025
- invoice-repository-026
- payout-repository-027

#### 1.1.10.6.0 Properties

*No data available*

#### 1.1.10.7.0 Interfaces

- IReportingService

#### 1.1.10.8.0 Technology

.NET 8

#### 1.1.10.9.0 Resources

##### 1.1.10.9.1 Cpu

Shared with API

##### 1.1.10.9.2 Memory

Shared with API

#### 1.1.10.10.0 Configuration

*No data available*

#### 1.1.10.11.0 Health Check

*Not specified*

#### 1.1.10.12.0 Responsible Features

- REQ-FUNC-026

#### 1.1.10.13.0 Security

##### 1.1.10.13.1 Requires Authentication

✅ Yes

##### 1.1.10.13.2 Requires Authorization

✅ Yes

##### 1.1.10.13.3 Allowed Roles

- SystemAdministrator

### 1.1.11.0.0 DataImportService

#### 1.1.11.1.0 Id

data-import-service-028

#### 1.1.11.2.0 Name

DataImportService

#### 1.1.11.3.0 Description

Application service that provides the logic for bulk-importing Client and Vendor data from a CSV file. It handles parsing, validation, and batch insertion into the database.

#### 1.1.11.4.0 Type

🔹 Service

#### 1.1.11.5.0 Dependencies

- client-repository-029
- vendor-repository-019
- unit-of-work-012

#### 1.1.11.6.0 Properties

*No data available*

#### 1.1.11.7.0 Interfaces

- IDataImportService

#### 1.1.11.8.0 Technology

.NET 8

#### 1.1.11.9.0 Resources

##### 1.1.11.9.1 Cpu

Shared with API

##### 1.1.11.9.2 Memory

Shared with API

#### 1.1.11.10.0 Configuration

##### 1.1.11.10.1 Batch Size

100

#### 1.1.11.11.0 Health Check

*Not specified*

#### 1.1.11.12.0 Responsible Features

- REQ-NFR-009: The system shall provide a secure, dedicated admin UI for the initial bulk import of existing Client and Vendor data from a CSV format.

#### 1.1.11.13.0 Security

##### 1.1.11.13.1 Requires Authentication

✅ Yes

##### 1.1.11.13.2 Requires Authorization

✅ Yes

##### 1.1.11.13.3 Allowed Roles

- SystemAdministrator

## 1.2.0.0.0 Configuration

### 1.2.1.0.0 Environment

production

### 1.2.2.0.0 Logging Level

Information

### 1.2.3.0.0 Database Url

from-secrets-manager

### 1.2.4.0.0 Cache Url

from-secrets-manager

### 1.2.5.0.0 Message Queue Url

from-secrets-manager

### 1.2.6.0.0 Performance Monitoring

#### 1.2.6.1.0 Enabled

✅ Yes

#### 1.2.6.2.0 Provider

APM-Provider

# 2.0.0.0.0 Component Relations

## 2.1.0.0.0 Architecture

### 2.1.1.0.0 Components

#### 2.1.1.1.0 Application.API.Host

##### 2.1.1.1.1 Id

web-api-001

##### 2.1.1.1.2 Name

Application.API.Host

##### 2.1.1.1.3 Description

The primary web application host for the Modular Monolith. It serves as the main entry point, hosting all API controllers, application services, middleware, and orchestrating the overall request/response pipeline.

##### 2.1.1.1.4 Type

🔹 Application

##### 2.1.1.1.5 Dependencies

- client-management-service-001
- vendor-management-service-001
- project-management-service-001
- notification-service-001
- infrastructure-persistence-001
- infrastructure-messaging-001

##### 2.1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Version | 1.0.0 |
| Environment | production |

##### 2.1.1.1.7 Interfaces

- RESTful API

##### 2.1.1.1.8 Technology

ASP.NET Core 8

##### 2.1.1.1.9 Resources

| Property | Value |
|----------|-------|
| Cpu | 2 vCPU |
| Memory | 4GB |
| Network | 1Gbps |

##### 2.1.1.1.10 Configuration

| Property | Value |
|----------|-------|
| Aspnetcore Environment | Production |
| Jwt Settings | { "Issuer": "...", "Audience": "..." } |
| Serilog | { "MinimumLevel": "Information" } |

##### 2.1.1.1.11 Health Check

| Property | Value |
|----------|-------|
| Path | /healthz |
| Interval | 30 |
| Timeout | 5 |

##### 2.1.1.1.12 Responsible Features

- REQ-FUNC-005
- REQ-FUNC-007
- REQ-FUNC-008
- REQ-FUNC-010
- REQ-FUNC-013
- REQ-FUNC-018
- REQ-FUNC-019
- REQ-FUNC-024
- REQ-FUNC-026
- REQ-NFR-009
- REQ-BR-005

##### 2.1.1.1.13 Security

###### 2.1.1.1.13.1 Requires Authentication

✅ Yes

###### 2.1.1.1.13.2 Requires Authorization

✅ Yes

###### 2.1.1.1.13.3 Allowed Roles

- SystemAdministrator
- VendorContact

#### 2.1.1.2.0.0 Presentation.Frontend.SPA

##### 2.1.1.2.1.0 Id

frontend-spa-001

##### 2.1.1.2.2.0 Name

Presentation.Frontend.SPA

##### 2.1.1.2.3.0 Description

A client-side Single Page Application (SPA) that renders all user interfaces. It handles client-side state, user interaction, and communication with the backend API. It is built to be responsive and accessible.

##### 2.1.1.2.4.0 Type

🔹 UI Shell

##### 2.1.1.2.5.0 Dependencies

- web-api-001

##### 2.1.1.2.6.0 Properties

*No data available*

##### 2.1.1.2.7.0 Interfaces

*No items available*

##### 2.1.1.2.8.0 Technology

Next.js 14, React 18, TypeScript, Radix UI, Tailwind CSS

##### 2.1.1.2.9.0 Resources

###### 2.1.1.2.9.1 Cpu

N/A (Client-Side)

###### 2.1.1.2.9.2 Memory

N/A (Client-Side)

##### 2.1.1.2.10.0 Configuration

###### 2.1.1.2.10.1 Next Public Api Url

🔗 [https://api.example.com](https://api.example.com)

##### 2.1.1.2.11.0 Health Check

*Not specified*

##### 2.1.1.2.12.0 Responsible Features

- REQ-UI-001
- REQ-UI-002
- REQ-UI-003
- Client ListView UI
- Vendor Profile UI
- SOW Upload UI
- HITL Review UI
- Proposal Dashboard UI

##### 2.1.1.2.13.0 Security

###### 2.1.1.2.13.1 Requires Authentication

✅ Yes

###### 2.1.1.2.13.2 Requires Authorization

❌ No

#### 2.1.1.3.0.0 SowProcessing.Worker

##### 2.1.1.3.1.0 Id

sow-processing-worker-001

##### 2.1.1.3.2.0 Name

SowProcessing.Worker

##### 2.1.1.3.3.0 Description

A dedicated, asynchronous background worker that processes uploaded SOW documents. It consumes events from a message queue, extracts text, uses an LLM to parse data, and updates the database, ensuring the main API is not blocked.

##### 2.1.1.3.4.0 Type

🔹 BackgroundWorker

##### 2.1.1.3.5.0 Dependencies

- infrastructure-messaging-001
- infrastructure-llm-client-001
- infrastructure-persistence-001
- infrastructure-doc-parser-001

##### 2.1.1.3.6.0 Properties

| Property | Value |
|----------|-------|
| Queue Name | sow-processing-queue |

##### 2.1.1.3.7.0 Interfaces

- ISowUploadedEventConsumer

##### 2.1.1.3.8.0 Technology

.NET 8 Worker Service

##### 2.1.1.3.9.0 Resources

###### 2.1.1.3.9.1 Cpu

2 vCPU

###### 2.1.1.3.9.2 Memory

4GB

##### 2.1.1.3.10.0 Configuration

###### 2.1.1.3.10.1 Message Broker Host

rabbitmq.internal

###### 2.1.1.3.10.2 Open Aiapi Key

secret

##### 2.1.1.3.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/worker |
| Interval | 60 |
| Timeout | 10 |

##### 2.1.1.3.12.0 Responsible Features

- REQ-FUNC-010

##### 2.1.1.3.13.0 Security

###### 2.1.1.3.13.1 Requires Authentication

❌ No

###### 2.1.1.3.13.2 Requires Authorization

❌ No

#### 2.1.1.4.0.0 ProjectManagement.VendorMatchingService

##### 2.1.1.4.1.0 Id

vendor-matching-service-001

##### 2.1.1.4.2.0 Name

ProjectManagement.VendorMatchingService

##### 2.1.1.4.3.0 Description

An application service responsible for the core logic of matching vendors to projects. It orchestrates the semantic search against vendor skill embeddings to find the best candidates for a given project brief.

##### 2.1.1.4.4.0 Type

🔹 Service

##### 2.1.1.4.5.0 Dependencies

- vendor-repository-001
- project-brief-repository-001

##### 2.1.1.4.6.0 Properties

*No data available*

##### 2.1.1.4.7.0 Interfaces

- IVendorMatchingService

##### 2.1.1.4.8.0 Technology

.NET 8

##### 2.1.1.4.9.0 Resources

*Not specified*

##### 2.1.1.4.10.0 Configuration

###### 2.1.1.4.10.1 Min Similarity Score

0.75

##### 2.1.1.4.11.0 Health Check

*Not specified*

##### 2.1.1.4.12.0 Responsible Features

- REQ-FUNC-014

##### 2.1.1.4.13.0 Security

###### 2.1.1.4.13.1 Requires Authentication

✅ Yes

###### 2.1.1.4.13.2 Requires Authorization

✅ Yes

###### 2.1.1.4.13.3 Allowed Roles

- SystemAdministrator

#### 2.1.1.5.0.0 Notification.NotificationService

##### 2.1.1.5.1.0 Id

notification-service-001

##### 2.1.1.5.2.0 Name

Notification.NotificationService

##### 2.1.1.5.3.0 Description

A central application service that handles the creation and dispatching of notifications for key system and project events. It acts as a domain event handler, delegating delivery to specific gateway implementations.

##### 2.1.1.5.4.0 Type

🔹 Service

##### 2.1.1.5.5.0 Dependencies

- email-notification-gateway-001
- webhook-notification-gateway-001
- notification-repository-001

##### 2.1.1.5.6.0 Properties

*No data available*

##### 2.1.1.5.7.0 Interfaces

- INotificationService

##### 2.1.1.5.8.0 Technology

.NET 8

##### 2.1.1.5.9.0 Resources

*Not specified*

##### 2.1.1.5.10.0 Configuration

*Not specified*

##### 2.1.1.5.11.0 Health Check

*Not specified*

##### 2.1.1.5.12.0 Responsible Features

- REQ-FUNC-025

##### 2.1.1.5.13.0 Security

###### 2.1.1.5.13.1 Requires Authentication

❌ No

###### 2.1.1.5.13.2 Requires Authorization

❌ No

#### 2.1.1.6.0.0 Infrastructure.Email.SesGateway

##### 2.1.1.6.1.0 Id

email-notification-gateway-001

##### 2.1.1.6.2.0 Name

Infrastructure.Email.SesGateway

##### 2.1.1.6.3.0 Description

An infrastructure component implementing the email notification interface. It encapsulates all logic for communicating with the AWS Simple Email Service (SES) API to send transactional emails.

##### 2.1.1.6.4.0 Type

🔹 Gateway

##### 2.1.1.6.5.0 Dependencies

*No items available*

##### 2.1.1.6.6.0 Properties

*No data available*

##### 2.1.1.6.7.0 Interfaces

- IEmailNotificationGateway

##### 2.1.1.6.8.0 Technology

AWS SDK for .NET

##### 2.1.1.6.9.0 Resources

###### 2.1.1.6.9.1 Network

To AWS SES Endpoints

##### 2.1.1.6.10.0 Configuration

| Property | Value |
|----------|-------|
| Aws Region | us-east-1 |
| Aws Access Key | secret |
| Default From Address | noreply@example.com |

##### 2.1.1.6.11.0 Health Check

*Not specified*

##### 2.1.1.6.12.0 Responsible Features

- REQ-INTG-005

##### 2.1.1.6.13.0 Security

###### 2.1.1.6.13.1 Requires Authentication

❌ No

###### 2.1.1.6.13.2 Requires Authorization

❌ No

#### 2.1.1.7.0.0 Infrastructure.Persistence.EFCore

##### 2.1.1.7.1.0 Id

infrastructure-persistence-001

##### 2.1.1.7.2.0 Name

Infrastructure.Persistence.EFCore

##### 2.1.1.7.3.0 Description

A foundational component in the Infrastructure layer that contains the Entity Framework Core DbContext, all repository implementations, and database migration logic. It handles all data persistence for the application.

##### 2.1.1.7.4.0 Type

🔹 Data Access Layer

##### 2.1.1.7.5.0 Dependencies

*No items available*

##### 2.1.1.7.6.0 Properties

| Property | Value |
|----------|-------|
| Database Provider | PostgreSQL |
| Orm | Entity Framework Core 8 |

##### 2.1.1.7.7.0 Interfaces

- IClientRepository
- IVendorRepository
- IProjectRepository
- IUnitOfWork

##### 2.1.1.7.8.0 Technology

Entity Framework Core 8, Npgsql, pgvector.EntityFrameworkCore

##### 2.1.1.7.9.0 Resources

###### 2.1.1.7.9.1 Storage

Dependent on Database

##### 2.1.1.7.10.0 Configuration

###### 2.1.1.7.10.1 Connection String

secret

##### 2.1.1.7.11.0 Health Check

*Not specified*

##### 2.1.1.7.12.0 Responsible Features

- All data persistence
- REQ-FUNC-014 (via pgvector)

##### 2.1.1.7.13.0 Security

###### 2.1.1.7.13.1 Requires Authentication

❌ No

###### 2.1.1.7.13.2 Requires Authorization

❌ No

#### 2.1.1.8.0.0 Dashboard.MetricsCalculator

##### 2.1.1.8.1.0 Id

dashboard-metrics-calculator-001

##### 2.1.1.8.2.0 Name

Dashboard.MetricsCalculator

##### 2.1.1.8.3.0 Description

A scheduled background job (e.g., cron job) that periodically calculates and aggregates data for the main dashboard, populating the denormalized 'DashboardMetrics' table to ensure fast dashboard load times.

##### 2.1.1.8.4.0 Type

🔹 ScheduledTask

##### 2.1.1.8.5.0 Dependencies

- infrastructure-persistence-001

##### 2.1.1.8.6.0 Properties

| Property | Value |
|----------|-------|
| Schedule | */15 * * * * |

##### 2.1.1.8.7.0 Interfaces

*No items available*

##### 2.1.1.8.8.0 Technology

.NET 8 Worker Service

##### 2.1.1.8.9.0 Resources

###### 2.1.1.8.9.1 Cpu

0.5 vCPU

###### 2.1.1.8.9.2 Memory

1GB

##### 2.1.1.8.10.0 Configuration

*Not specified*

##### 2.1.1.8.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/job |
| Interval | 900 |
| Timeout | 60 |

##### 2.1.1.8.12.0 Responsible Features

- REQ-FUNC-024 (Data Aggregation)

##### 2.1.1.8.13.0 Security

###### 2.1.1.8.13.1 Requires Authentication

❌ No

###### 2.1.1.8.13.2 Requires Authorization

❌ No

#### 2.1.1.9.0.0 Infrastructure.AI.LlmClient

##### 2.1.1.9.1.0 Id

infrastructure-llm-client-001

##### 2.1.1.9.2.0 Name

Infrastructure.AI.LlmClient

##### 2.1.1.9.3.0 Description

An infrastructure gateway component that provides a client for interacting with Large Language Models (LLMs) like those from Azure OpenAI. It is used for extracting structured data from SOWs and generating vector embeddings.

##### 2.1.1.9.4.0 Type

🔹 Gateway

##### 2.1.1.9.5.0 Dependencies

*No items available*

##### 2.1.1.9.6.0 Properties

*No data available*

##### 2.1.1.9.7.0 Interfaces

- ILlmClient

##### 2.1.1.9.8.0 Technology

Azure.AI.OpenAI SDK

##### 2.1.1.9.9.0 Resources

###### 2.1.1.9.9.1 Network

To Azure OpenAI Endpoints

##### 2.1.1.9.10.0 Configuration

| Property | Value |
|----------|-------|
| Azure Open Aiendpoint | secret |
| Azure Open Aiapi Key | secret |
| Model Deployment Name | gpt-4-turbo |

##### 2.1.1.9.11.0 Health Check

*Not specified*

##### 2.1.1.9.12.0 Responsible Features

- REQ-FUNC-010 (AI processing)
- REQ-FUNC-014 (Vector embedding generation)

##### 2.1.1.9.13.0 Security

###### 2.1.1.9.13.1 Requires Authentication

❌ No

###### 2.1.1.9.13.2 Requires Authorization

❌ No

### 2.1.2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Environment | production |
| Logging Level | Information |
| Database Url | jdbc:postgresql://prod-db:5432/appdb |
| Cache Ttl | 3600 |
| Max Threads | 50 |


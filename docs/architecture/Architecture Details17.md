# 1 Style

ModularMonolith

# 2 Patterns

## 2.1 CleanArchitecture

### 2.1.1 Name

CleanArchitecture

### 2.1.2 Description

The backend is structured into Presentation (API), Application, Domain, and Infrastructure layers to enforce separation of concerns, testability, and independence from external frameworks and databases. The Domain layer contains the core business logic, and dependencies flow inwards.

### 2.1.3 Benefits

- High maintainability and testability
- Decoupling from technology-specific implementations
- Clear separation of business rules from infrastructure concerns

### 2.1.4 Tradeoffs

- Slightly higher initial setup complexity compared to a simple layered architecture

### 2.1.5 Applicability

#### 2.1.5.1 Scenarios

- Enterprise applications with complex business logic
- Systems intended for long-term maintenance and evolution

#### 2.1.5.2 Constraints

*No items available*

## 2.2.0.0 EventDriven

### 2.2.1.0 Name

EventDriven

### 2.2.2.0 Description

Utilized for the SOW processing workflow as per REQ-FUNC-010. The main API application publishes an 'SowUploaded' event to a message queue, which is consumed by a dedicated, asynchronous worker service. This decouples the time-consuming AI processing from the user-facing API, preventing UI blocking.

### 2.2.3.0 Benefits

- Improved responsiveness and user experience
- Enhanced scalability and resilience for background processing
- Loose coupling between the main application and the processing worker

### 2.2.4.0 Tradeoffs

- Adds a message broker as a new infrastructure component to manage

### 2.2.5.0 Applicability

#### 2.2.5.1 Scenarios

- Long-running tasks that should not block user interaction
- Workflows that need to be fault-tolerant and scalable independently

#### 2.2.5.2 Constraints

*No items available*

## 2.3.0.0 Repository

### 2.3.1.0 Name

Repository

### 2.3.2.0 Description

Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects. Each aggregate root will have a corresponding repository to encapsulate data access logic, ensuring the domain model is ignorant of the persistence mechanism.

### 2.3.3.0 Benefits

- Decouples domain logic from data access code
- Centralizes data access logic, making it easier to manage and test
- Allows for easier switching of persistence technologies

### 2.3.4.0 Tradeoffs

*No items available*

### 2.3.5.0 Applicability

#### 2.3.5.1 Scenarios

- Applications using an ORM like Entity Framework Core
- Systems following Domain-Driven Design principles

#### 2.3.5.2 Constraints

*No items available*

# 3.0.0.0 Layers

## 3.1.0.0 Presentation.Frontend

### 3.1.1.0 Id

presentation_frontend

### 3.1.2.0 Name

Presentation.Frontend

### 3.1.3.0 Description

A client-side Single Page Application (SPA) built as a responsive web application. It handles all user interface rendering, client-side state management, and interaction. It communicates with the backend via a RESTful API. Implements REQ-UI-001, REQ-UI-002, and REQ-UI-003.

### 3.1.4.0 Technologystack

Next.js 14, React 18, TypeScript 5.4, Radix UI, Tailwind CSS 3, React Query, React Hook Form

### 3.1.5.0 Language

TypeScript

### 3.1.6.0 Type

🔹 Presentation

### 3.1.7.0 Responsibilities

- Render user interfaces for all feature groups (Client/Vendor Management, Dashboards, etc.).
- Ensure WCAG 2.1 AA compliance.
- Provide a responsive layout for desktop, tablet, and mobile.
- Handle user input and client-side validation.
- Manage client-side application state.
- Securely communicate with the backend API.

### 3.1.8.0 Components

- ClientListView
- VendorProfileForm
- SowUploadComponent
- HumanInTheLoopReviewInterface
- ProposalDashboard
- AdminDashboard

### 3.1.9.0 Interfaces

*No items available*

### 3.1.10.0 Dependencies

- {'layerId': 'application_api', 'type': 'Required'}

### 3.1.11.0 Constraints

- {'type': 'Technology', 'description': 'Must use Radix UI for components and Tailwind CSS for styling as per REQ-UI-001.'}

## 3.2.0.0 Application.API

### 3.2.1.0 Id

application_api

### 3.2.2.0 Name

Application.API

### 3.2.3.0 Description

The backend API layer that exposes endpoints for the frontend. It receives requests, orchestrates business logic by calling application services, and returns responses. This layer acts as the entry point to the backend system and implements security.

### 3.2.4.0 Technologystack

.NET 8, ASP.NET Core 8

### 3.2.5.0 Language

C#

### 3.2.6.0 Type

🔹 ApplicationServices

### 3.2.7.0 Responsibilities

- Expose RESTful API endpoints for all system functionalities.
- Handle user authentication (JWT validation) and authorization (role-based checks).
- Validate incoming Data Transfer Objects (DTOs).
- Coordinate application workflows by invoking domain and infrastructure services.
- Map domain entities to DTOs for client consumption.

### 3.2.8.0 Components

- Controllers (ClientsController, VendorsController, ProjectsController)
- Middleware (ExceptionHandler, Authentication)
- Application Services (e.g., IProjectService, IClientService)
- DTOs and Mappers

### 3.2.9.0 Interfaces

*No items available*

### 3.2.10.0 Dependencies

#### 3.2.10.1 Required

##### 3.2.10.1.1 Layer Id

domain_core

##### 3.2.10.1.2 Type

🔹 Required

#### 3.2.10.2.0 Required

##### 3.2.10.2.1 Layer Id

infrastructure

##### 3.2.10.2.2 Type

🔹 Required

### 3.2.11.0.0 Constraints

*No items available*

## 3.3.0.0.0 Domain.Core

### 3.3.1.0.0 Id

domain_core

### 3.3.2.0.0 Name

Domain.Core

### 3.3.3.0.0 Description

The core of the backend, containing all business logic, entities, aggregates, and domain-specific rules. This layer is completely independent of any technology concerns like databases or web frameworks.

### 3.3.4.0.0 Technologystack

.NET 8

### 3.3.5.0.0 Language

C#

### 3.3.6.0.0 Type

🔹 Domain

### 3.3.7.0.0 Responsibilities

- Define and enforce business rules and invariants.
- Model the core business concepts (e.g., Project, Vendor, Proposal, Client).
- Contain domain services for logic that doesn't naturally fit within a single entity.
- Define interfaces for repositories that the Infrastructure layer will implement.

### 3.3.8.0.0 Components

- Entities (Project, Vendor, User, Proposal)
- Aggregates (e.g., Project with its Proposals)
- Value Objects (e.g., Money)
- Domain Events (e.g., ProjectCreated, ProposalSubmitted)
- Repository Interfaces (e.g., IProjectRepository)

### 3.3.9.0.0 Interfaces

*No items available*

### 3.3.10.0.0 Dependencies

*No items available*

### 3.3.11.0.0 Constraints

- {'type': 'Dependency', 'description': 'This layer must not have dependencies on any other layer in the solution.'}

## 3.4.0.0.0 Infrastructure

### 3.4.1.0.0 Id

infrastructure

### 3.4.2.0.0 Name

Infrastructure

### 3.4.3.0.0 Description

Provides implementations for interfaces defined in the Application and Domain layers. It handles all external concerns such as database access, file storage, sending emails, and communicating with third-party APIs.

### 3.4.4.0.0 Technologystack

Entity Framework Core 8, Npgsql, pgvector.EntityFrameworkCore, AWS SDK for .NET (SES), Serilog, FluentValidation

### 3.4.5.0.0 Language

C#

### 3.4.6.0.0 Type

🔹 Infrastructure

### 3.4.7.0.0 Responsibilities

- Implement repository interfaces for data persistence using Entity Framework Core.
- Interact with the PostgreSQL database, including vector search via pgvector.
- Send emails using AWS SES (REQ-INTG-005).
- Publish messages to the message queue for SOW processing.
- Provide implementation for logging, configuration, and other cross-cutting concerns.

### 3.4.8.0.0 Components

- DbContext
- Repository Implementations (ProjectRepository, VendorRepository)
- EmailSenderService
- MessageQueuePublisher
- LoggingService

### 3.4.9.0.0 Interfaces

*No items available*

### 3.4.10.0.0 Dependencies

- {'layerId': 'domain_core', 'type': 'Required'}

### 3.4.11.0.0 Constraints

*No items available*

# 4.0.0.0.0 Quality Attributes

## 4.1.0.0.0 Performance

### 4.1.1.0.0 Tactics

- Asynchronous processing of SOW documents to avoid UI blocking (REQ-FUNC-010).
- Use of a denormalized 'DashboardMetrics' table for fast dashboard loads (REQ-FUNC-024).
- Efficient semantic search using a HNSW index on vector embeddings in PostgreSQL (REQ-FUNC-014).
- Server-Side Rendering (SSR) and code-splitting in the Next.js frontend for fast LCP (REQ-PERF-002).
- Distributed caching with Redis for frequently accessed, low-volatility data.

### 4.1.2.0.0 Metrics

- 95th percentile API response time < 250ms (REQ-PERF-001).
- Largest Contentful Paint (LCP) < 2.5 seconds (REQ-PERF-002).

## 4.2.0.0.0 Scalability

### 4.2.1.0.0 Tactics

- All services (API, Frontend, Worker) are designed to be stateless and containerized (e.g., with Docker).
- Deployment on a container orchestrator (e.g., Kubernetes, AWS ECS) to manage horizontal scaling based on CPU/memory utilization.

### 4.2.2.0.0 Approach

Horizontal

## 4.3.0.0.0 Security

| Property | Value |
|----------|-------|
| Authentication | Stateless JWT Bearer Token authentication. |
| Authorization | Role-Based Access Control (RBAC) enforced via poli... |
| Data Protection | All traffic encrypted via HTTPS/TLS. Passwords sto... |

## 4.4.0.0.0 Reliability

### 4.4.1.0.0 Tactics

- Deploying multiple container instances for each service for redundancy.
- Utilizing a managed database service (e.g., AWS RDS) with automated backups and failover capabilities.
- Implementing health check endpoints in all services for monitoring.
- Documented and tested disaster recovery plan as per REQ-REL-002.

## 4.5.0.0.0 Maintainability

### 4.5.1.0.0 Tactics

- Modular Monolith structure with clear boundaries between business capabilities.
- Strict adherence to Clean Architecture and the Dependency Inversion Principle.
- Heavy use of Dependency Injection for loose coupling.
- Comprehensive test suite including unit, integration, and end-to-end tests.

## 4.6.0.0.0 Extensibility

### 4.6.1.0.0 Tactics

- The modular design allows for new features to be added as self-contained modules with minimal impact on existing code.

# 5.0.0.0.0 Technology Stack

## 5.1.0.0.0 Primary Language

C# 12, TypeScript 5.4

## 5.2.0.0.0 Frameworks

- .NET 8
- ASP.NET Core 8
- Next.js 14
- React 18

## 5.3.0.0.0 Database

| Property | Value |
|----------|-------|
| Type | PostgreSQL |
| Version | 16 |
| Orm | Entity Framework Core 8 with pgvector extension |

## 5.4.0.0.0 Domain Specific Libraries

### 5.4.1.0.0 pgvector

#### 5.4.1.1.0 Name

pgvector

#### 5.4.1.2.0 Version

0.7.0+

#### 5.4.1.3.0 Purpose

Enables efficient vector similarity search directly within PostgreSQL to fulfill semantic search requirement REQ-FUNC-014 without a separate vector database.

#### 5.4.1.4.0 Domain

AI/Search

### 5.4.2.0.0 Azure.AI.OpenAI

#### 5.4.2.1.0 Name

Azure.AI.OpenAI

#### 5.4.2.2.0 Version

1.0.0-beta+

#### 5.4.2.3.0 Purpose

Provides a client SDK for interacting with Large Language Models (LLMs) to extract structured data from SOW documents and to generate vector embeddings.

#### 5.4.2.4.0 Domain

AI/NLP

### 5.4.3.0.0 TikaOnDotNet

#### 5.4.3.1.0 Name

TikaOnDotNet

#### 5.4.3.2.0 Version

1.17.1

#### 5.4.3.3.0 Purpose

Extracts text content from various document formats (PDF, DOCX) uploaded as SOWs, which is a prerequisite for AI processing.

#### 5.4.3.4.0 Domain

Document Processing

## 5.5.0.0.0 Infrastructure

| Property | Value |
|----------|-------|
| Logging | Serilog |
| Caching | Redis |
| Testing | xUnit, Moq (Backend); Jest, React Testing Library ... |

# 6.0.0.0.0 Backend Services

## 6.1.0.0.0 SowProcessingWorker

### 6.1.1.0.0 Name

SowProcessingWorker

### 6.1.2.0.0 Purpose

Executes the asynchronous SOW processing pipeline. It listens for messages, retrieves the SOW file, extracts text, uses an LLM to extract data and skills, generates vector embeddings, and updates the project brief in the database. Fulfills REQ-FUNC-010.

### 6.1.3.0.0 Type

🔹 Background Worker/Queue Consumer

### 6.1.4.0.0 Communication

Subscribes to a message queue (e.g., RabbitMQ, AWS SQS) for 'SowUploaded' events.

## 6.2.0.0.0 DashboardMetricsCalculator

### 6.2.1.0.0 Name

DashboardMetricsCalculator

### 6.2.2.0.0 Purpose

A scheduled background job that periodically calculates and aggregates data for the main dashboard (REQ-FUNC-024) and populates the 'DashboardMetrics' table to ensure fast dashboard load times.

### 6.2.3.0.0 Type

🔹 Scheduled Task/Cron Job

### 6.2.4.0.0 Communication

Directly interacts with the database on a schedule.

# 7.0.0.0.0 Cross Cutting Concerns

| Property | Value |
|----------|-------|
| Logging | Implemented using Serilog with structured logging,... |
| Exception Handling | Centralized strategy using custom ASP.NET Core mid... |
| Configuration | Managed via ASP.NET Core's IConfiguration system (... |
| Validation | Handled using FluentValidation for DTOs at the API... |
| Security | Implemented via ASP.NET Core Authentication and Au... |


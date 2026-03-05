# 1 Id

REPO-SVC-PROJECT

# 2 Name

emp-project-management-service

# 3 Description

A backend microservice responsible for the complete project lifecycle. This repository contains the domain logic for Projects, Statements of Work (SOWs), Proposals, and the vendor matching process. It was extracted from the corresponding modules within the monorepo's backend. It exposes an internal API consumed by the API Gateway and is the primary publisher of project-related events (e.g., `ProposalAccepted`, `ProjectCompleted`). It owns its database schema for project-related tables and is a core component of the business logic, enabling independent development and scaling of project-related features.

# 4 Type

🔹 Business Logic

# 5 Namespace

EnterpriseMediator.ProjectManagement

# 6 Output Path

services/project-management

# 7 Framework

ASP.NET Core 8

# 8 Language

C#

# 9 Technology

.NET, Entity Framework Core

# 10 Thirdparty Libraries

- Microsoft.EntityFrameworkCore

# 11 Layer Ids

- application-layer
- domain-layer

# 12 Dependencies

- REPO-LIB-DOMAIN
- REPO-LIB-CONTRACTS
- REPO-LIB-SHARED KERNEL

# 13 Requirements

- {'requirementId': 'REQ-SCP-001'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Domain-Driven Design (DDD)

# 17 Architecture Map

- project-management-service-001

# 18 Components Map

*No items available*

# 19 Requirements Map

- REQ-FUN-003

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

EMP-MONOREPO-001

## 20.3 Decomposition Reasoning

Isolated into its own service to represent the core business domain of the platform. This allows a dedicated team to focus on the complex project and proposal workflows. It can be scaled independently based on the number of active projects, and its data is self-contained, improving data integrity and simplifying maintenance.

## 20.4 Extracted Responsibilities

- Project Lifecycle Management
- Proposal Workflow Management
- SOW Data Storage and Retrieval
- Vendor Matching Logic

## 20.5 Reusability Scope

- The business logic of this service is specific to the EMP platform.

## 20.6 Development Benefits

- Clear ownership of the core business domain.
- Independent scaling and deployment.
- Reduced cognitive load for developers.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

- {'interface': 'Internal Project Service API', 'methods': ['AwardProject(AwardProjectCommand)', 'GetProjectProposals(GetProposalsQuery)'], 'events': ['ProposalAcceptedEvent', 'ProjectStatusChangedEvent'], 'properties': [], 'consumers': ['REPO-GW-API']}

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Heavy use of DI for repositories, services, and me... |
| Event Communication | Publishes domain events to a message bus upon sign... |
| Data Flow | Follows Clean Architecture principles: API -> Appl... |
| Error Handling | Domain exceptions are caught in the application la... |
| Async Patterns | Fully asynchronous for all I/O-bound operations (d... |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Implement using a DDD approach with aggregates (Pr... |
| Performance Considerations | Optimize database queries for fetching project and... |
| Security Considerations | Enforce business rules and authorization at the ap... |
| Testing Approach | Unit tests for domain logic, integration tests for... |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- All state transitions for Projects and Proposals.
- Business rules related to project management.

## 25.2 Must Not Implement

- Financial transaction processing.
- User authentication.
- AI document parsing.

## 25.3 Extension Points

- Adding new project statuses or proposal workflow steps.

## 25.4 Validation Rules

- Enforce domain invariants (e.g., a project cannot be awarded without an accepted proposal).


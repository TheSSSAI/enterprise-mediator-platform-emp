# 1 Id

REPO-LIB-CONTRACTS

# 2 Name

emp-shared-contracts

# 3 Description

A lightweight, cross-cutting library defining the public data contracts for communication between services. This repository contains API Data Transfer Objects (DTOs), event message schemas, and public interface definitions. It was created to formalize the communication protocols between the frontend, API Gateway, and backend services. By sharing these contracts as a versioned package (NuGet for backend, NPM for frontend via generation), it enables type-safe communication and decouples service implementations from their consumers, allowing them to evolve independently as long as the contract is respected.

# 4 Type

🔹 Model Library

# 5 Namespace

EnterpriseMediator.Contracts

# 6 Output Path

libs/contracts

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- integration-layer

# 12 Dependencies

*No items available*

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-INT-002

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-INT-003

# 14.0.0 Generate Tests

❌ No

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Data Transfer Objects (DTOs)

# 17.0.0 Architecture Map

*No items available*

# 18.0.0 Components Map

*No items available*

# 19.0.0 Requirements Map

*No items available*

# 20.0.0 Decomposition Rationale

## 20.1.0 Operation Type

NEW_DECOMPOSED

## 20.2.0 Source Repository

EMP-MONOREPO-001

## 20.3.0 Decomposition Reasoning

Extracted to establish a formal, technology-agnostic contract between distributed components. This is the cornerstone of a loosely-coupled architecture, allowing consumers (like the frontend) and providers (like a backend service) to be developed and deployed independently, as long as they adhere to the shared contract.

## 20.4.0 Extracted Responsibilities

- API Data Transfer Objects (DTOs)
- Asynchronous Event Schemas
- Shared Enum Definitions

## 20.5.0 Reusability Scope

- Consumed by all communicating components: frontend, API Gateway, and all backend services.

## 20.6.0 Development Benefits

- Enforces clear service boundaries.
- Enables independent evolution of services.
- Provides type safety for inter-service communication.

# 21.0.0 Dependency Contracts

*No data available*

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'NuGet/NPM Package API', 'methods': ['public record ProjectDto { ... }', 'public record SowUploadedEvent { ... }'], 'events': [], 'properties': [], 'consumers': ['REPO-FE-WEBAPP', 'REPO-GW-API', 'REPO-SVC-PROJECT', 'REPO-SVC-FINANCIAL', 'REPO-SVC-USER', 'REPO-SVC-AIWORKER']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A |
| Event Communication | Defines the very schemas used for event communicat... |
| Data Flow | Defines the data structures that flow between comp... |
| Error Handling | May contain standardized error DTOs. |
| Async Patterns | N/A |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Keep this library free of logic and dependencies. ... |
| Performance Considerations | Contracts should be lightweight and optimized for ... |
| Security Considerations | Do not include sensitive data fields that should n... |
| Testing Approach | Minimal testing, primarily for serialization compa... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- Data structures for APIs and events.

## 25.2.0 Must Not Implement

- Any business logic.
- Any I/O operations.

## 25.3.0 Extension Points

- Versioning of contracts to support non-breaking changes.

## 25.4.0 Validation Rules

- Can include data annotations for basic DTO validation.


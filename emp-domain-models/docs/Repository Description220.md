# 1 Id

REPO-LIB-DOMAIN

# 2 Name

emp-domain-models

# 3 Description

A foundational, cross-cutting library that contains the core business entities and domain logic for the entire platform. This repository holds the Plain Old CLR Object (POCO) classes for Project, Client, Vendor, User, Proposal, etc., along with their validation rules and domain invariants. Extracted from the shared `EMP.Domain` project in the monorepo, it has zero external dependencies and serves as the 'single source of truth' for the business model. It is consumed as a NuGet package by all backend services to ensure consistency and prevent logic duplication.

# 4 Type

🔹 Domain Library

# 5 Namespace

EnterpriseMediator.Domain

# 6 Output Path

libs/domain

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- domain-layer

# 12 Dependencies

*No items available*

# 13 Requirements

- {'requirementId': 'REQ-DAT-001'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Domain-Driven Design (DDD) Kernel

# 17 Architecture Map

*No items available*

# 18 Components Map

*No items available*

# 19 Requirements Map

- REQ-DAT-001

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

EMP-MONOREPO-001

## 20.3 Decomposition Reasoning

Extracted to create a shared, versionable kernel of business logic. This ensures all services operate on a consistent and canonical data model, preventing data silos and logic duplication. As a dependency-free library, it forms the stable core of the architecture.

## 20.4 Extracted Responsibilities

- Core Business Entity Definitions (Client, Project, etc.)
- Domain Invariants and Validation Logic
- Shared Value Objects (e.g., Money, Address)

## 20.5 Reusability Scope

- Consumed by every backend service in the EMP ecosystem.

## 20.6 Development Benefits

- Single source of truth for business logic.
- Enforces consistency across the platform.
- Decouples business rules from application and infrastructure concerns.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

- {'interface': 'NuGet Package API', 'methods': ['public class Project { ... }', 'public class Client { ... }'], 'events': [], 'properties': [], 'consumers': ['REPO-SVC-PROJECT', 'REPO-SVC-FINANCIAL', 'REPO-SVC-USER', 'REPO-SVC-AIWORKER']}

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A |
| Event Communication | N/A |
| Data Flow | N/A |
| Error Handling | Throws domain-specific exceptions when invariants ... |
| Async Patterns | N/A |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Keep this library free of any framework-specific d... |
| Performance Considerations | Entities should be designed for efficient persiste... |
| Security Considerations | Domain logic should enforce non-null constraints a... |
| Testing Approach | Unit tests are essential to verify all domain logi... |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- Core business entities and their relationships.
- Business rules that are always true (invariants).

## 25.2 Must Not Implement

- Data persistence logic (repositories).
- Application-level workflow logic.
- Any I/O operations.

## 25.3 Extension Points

- Entities are designed to be extensible with new properties and methods.

## 25.4 Validation Rules

- Encapsulates the core validation logic for the business model.


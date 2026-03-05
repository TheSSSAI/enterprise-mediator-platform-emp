# 1 Id

REPO-LIB-SHARED KERNEL

# 2 Name

emp-core-shared-kernel

# 3 Description

A shared utility library for common, non-domain-specific logic and infrastructure abstractions used across all backend services. This repository centralizes cross-cutting concerns like custom logging configurations, base classes for repositories or services, common exception types, and other reusable helper functions. It was created by identifying and extracting all shared infrastructure and application code from the monorepo that was not part of the core domain model or a specific business service. It helps to reduce code duplication and enforce consistent patterns for infrastructure interactions across the backend.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

EnterpriseMediator.Core.SharedKernel

# 6 Output Path

libs/shared-kernel

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET

# 10 Thirdparty Libraries

- Serilog
- Polly

# 11 Layer Ids

- application-layer
- infrastructure-layer

# 12 Dependencies

- REPO-LIB-DOMAIN
- REPO-LIB-CONTRACTS

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Shared Library

# 17 Architecture Map

*No items available*

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

EMP-MONOREPO-001

## 20.3 Decomposition Reasoning

Created to provide a single, versionable location for all truly cross-cutting code that is not business logic. This avoids code duplication across microservices for common concerns like logging setup, resiliency policies (e.g., Polly), and base infrastructure patterns. It helps enforce technical consistency.

## 20.4 Extracted Responsibilities

- Centralized Logging Configuration
- Resiliency Policies (Retry, Circuit Breaker)
- Common Exception Types
- Base Repository/Service Interfaces

## 20.5 Reusability Scope

- Consumed by all backend services and workers.

## 20.6 Development Benefits

- Reduces boilerplate code in services.
- Enforces consistent implementation of cross-cutting concerns.
- Simplifies dependency management for common libraries like Serilog or Polly.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

- {'interface': 'NuGet Package API', 'methods': ['public static class LoggerConfigurationExtensions { ... }', 'public abstract class BaseRepository<T> { ... }'], 'events': [], 'properties': [], 'consumers': ['REPO-GW-API', 'REPO-SVC-PROJECT', 'REPO-SVC-FINANCIAL', 'REPO-SVC-USER', 'REPO-SVC-AIWORKER']}

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Provides extension methods for simplified DI regis... |
| Event Communication | N/A |
| Data Flow | N/A |
| Error Handling | Provides base exception types and centralized resi... |
| Async Patterns | May contain helpers for common asynchronous operat... |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Provide extension methods for ASP.NET Core and .NE... |
| Performance Considerations | Ensure utility functions are highly performant and... |
| Security Considerations | N/A |
| Testing Approach | Thorough unit testing for all utility functions an... |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- Generic, application-agnostic utilities.
- Abstractions for infrastructure concerns.

## 25.2 Must Not Implement

- Any business domain logic.
- Concrete implementations that depend on a specific service.

## 25.3 Extension Points

- Designed for extension and reuse.

## 25.4 Validation Rules

*No items available*


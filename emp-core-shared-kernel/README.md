# EnterpriseMediator.Core.SharedKernel

## Overview
The Shared Kernel is a foundational class library for the Enterprise Mediator Platform. It enforces architectural consistency by centralizing cross-cutting concerns, infrastructure abstractions, and common utilities that are domain-agnostic.

## Architecture
This library follows **Clean Architecture** principles and acts as the "Shared Kernel" in Domain-Driven Design (DDD). It is referenced by the Domain, Application, and Infrastructure layers of all microservices within the Modular Monolith.

### Key Components
- **Abstractions**: Pure interfaces for Repositories, Domain Events, and Specifications.
- **Common**: Result pattern implementation, DateTime providers, and Type extensions.
- **Configuration**: Strongly-typed options classes for Serilog, Resiliency (Polly), and general settings.
- **Exceptions**: Base exception classes for standardized error handling.

## Usage
To use the Shared Kernel in a consuming service:
1. Add a project reference to `EnterpriseMediator.Core.SharedKernel`.
2. Register services using the provided extension methods (Level 3).
3. Implement interfaces (like `IRepository<T>`) in the Infrastructure layer.

## Dependencies
- .NET 8.0
- MediatR.Contracts
- Microsoft.Extensions.Configuration.Abstractions
- Microsoft.Extensions.Logging.Abstractions
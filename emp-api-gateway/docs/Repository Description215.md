# 1 Id

REPO-GW-API

# 2 Name

emp-api-gateway

# 3 Description

The primary backend entry point, acting as a gateway and Backend-For-Frontend (BFF). This repository contains the main ASP.NET Core application that exposes the public REST API to the frontend. It is responsible for cross-cutting concerns such as request routing to downstream services, authentication (JWT validation), authorization (RBAC), rate limiting, and request aggregation. It was formed by extracting the API hosting and public-facing controller logic from the monorepo's `EMP.Api` project. It ensures a single, secure, and managed entry point for all client interactions, abstracting the internal microservice architecture.

# 4 Type

🔹 Application Services

# 5 Namespace

EnterpriseMediator.ApiGateway

# 6 Output Path

services/api-gateway

# 7 Framework

ASP.NET Core 8

# 8 Language

C#

# 9 Technology

ASP.NET Core, AWS Cognito

# 10 Thirdparty Libraries

- Microsoft.AspNetCore

# 11 Layer Ids

- application-layer

# 12 Dependencies

- REPO-SVC-PROJECT
- REPO-SVC-FINANCIAL
- REPO-SVC-USER
- REPO-LIB-CONTRACTS

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-TEC-002

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-NFR-003

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

API Gateway / BFF

# 17.0.0 Architecture Map

- api-gateway-001

# 18.0.0 Components Map

- BackendApi

# 19.0.0 Requirements Map

- REQ-TEC-002

# 20.0.0 Decomposition Rationale

## 20.1.0 Operation Type

NEW_DECOMPOSED

## 20.2.0 Source Repository

EMP-MONOREPO-001

## 20.3.0 Decomposition Reasoning

Created to act as a dedicated facade for the backend system. This isolates cross-cutting concerns like authentication, routing, and rate limiting from business logic, simplifying the downstream services. It provides a stable, versioned API contract for the frontend, decoupling it from the backend's internal service boundaries.

## 20.4.0 Extracted Responsibilities

- Request Authentication & Authorization
- API Routing and Composition
- Rate Limiting and Security Policies
- Serving as the Backend-For-Frontend (BFF)

## 20.5.0 Reusability Scope

- This is the single entry point for all external clients, not just the primary web app.

## 20.6.0 Development Benefits

- Centralizes management of cross-cutting concerns.
- Simplifies downstream business services.
- Protects internal services from direct external exposure.

# 21.0.0 Dependency Contracts

## 21.1.0 Repo-Svc-Project

### 21.1.1 Required Interfaces

- {'interface': 'Internal gRPC or REST API', 'methods': ['GetProjectDetails(id)', 'CreateProject(request)'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Internal Request-Reply

### 21.1.3 Communication Protocol

gRPC or HTTP

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'Public REST API v1', 'methods': ['POST /api/v1/projects', 'GET /api/v1/projects/{projectId}'], 'events': [], 'properties': [], 'consumers': ['REPO-FE-WEBAPP']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Used for injecting service clients (for internal s... |
| Event Communication | Publishes events (e.g., SowUploaded) to a message ... |
| Data Flow | Receives external requests, authenticates, enriche... |
| Error Handling | Centralized exception handling middleware that con... |
| Async Patterns | Heavy use of async/await for non-blocking I/O when... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Leverage ASP.NET Core middleware pipeline for auth... |
| Performance Considerations | Gateway should be lightweight and stateless to ens... |
| Security Considerations | Primary point for enforcing security policies: JWT... |
| Testing Approach | Integration tests to verify routing and authentica... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- Authentication, Authorization, Routing, Rate Limiting.

## 25.2.0 Must Not Implement

- Core business logic.
- Direct database persistence.

## 25.3.0 Extension Points

- API versioning through URL path or headers.
- Pluggable authentication schemes.

## 25.4.0 Validation Rules

- Validates request format and authentication tokens.


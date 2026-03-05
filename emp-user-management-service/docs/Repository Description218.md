# 1 Id

REPO-SVC-USER

# 2 Name

emp-user-management-service

# 3 Description

A backend microservice that manages all user-related data and entities, including Clients, Vendors, and Internal Users. This repository is responsible for CRUD operations on these entities and their profiles, as well as managing roles and permissions (RBAC). It was formed by separating all user and company profile management logic from the monorepo. It serves as the system of record for user and entity data, providing this information to other services via an internal API. This separation simplifies data ownership and supports GDPR compliance by centralizing PII management.

# 4 Type

🔹 Business Logic

# 5 Namespace

EnterpriseMediator.UserManagement

# 6 Output Path

services/user-management

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

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-FUN-001

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-SEC-001

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Microservice

# 17.0.0 Architecture Map

*No items available*

# 18.0.0 Components Map

*No items available*

# 19.0.0 Requirements Map

- REQ-FUN-001
- REQ-SCP-001

# 20.0.0 Decomposition Rationale

## 20.1.0 Operation Type

NEW_DECOMPOSED

## 20.2.0 Source Repository

EMP-MONOREPO-001

## 20.3.0 Decomposition Reasoning

Created to centralize the management of all user and company (Client, Vendor) data. This is critical for security and compliance (GDPR), as it creates a single point of control for Personally Identifiable Information (PII). It decouples the core identity and profile data from the business workflows that use them.

## 20.4.0 Extracted Responsibilities

- Client and Vendor Profile CRUD
- Internal User Management
- Role-Based Access Control (RBAC) data management

## 20.5.0 Reusability Scope

- Serves as the central user directory for any service within the EMP ecosystem.

## 20.6.0 Development Benefits

- Clear data ownership for user and entity profiles.
- Simplifies GDPR data access and erasure requests.
- Independent management of user-related features.

# 21.0.0 Dependency Contracts

*No data available*

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'Internal User Service API', 'methods': ['GetClientDetails(id)', 'UpdateVendorProfile(command)', 'GetUserRole(userId)'], 'events': [], 'properties': [], 'consumers': ['REPO-GW-API', 'REPO-SVC-PROJECT', 'REPO-SVC-FINANCIAL']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Standard DI for services and repositories. |
| Event Communication | Publishes events like 'VendorProfileUpdated' that ... |
| Data Flow | Standard API-driven request/response. |
| Error Handling | Standard exception handling for database and valid... |
| Async Patterns | Fully asynchronous for all database operations. |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Focus on clean API design for querying user and en... |
| Performance Considerations | Cache frequently accessed, non-volatile data like ... |
| Security Considerations | Primary service for handling PII. Implement strong... |
| Testing Approach | Integration tests for all CRUD and query operation... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- Storage and management of Client, Vendor, and User profiles.
- Association of users to roles and companies.

## 25.2.0 Must Not Implement

- Authentication (handled by Cognito/Gateway).
- Project or financial data.

## 25.3.0 Extension Points

- Adding new metadata fields to Client or Vendor profiles.
- Implementing more granular permission models.

## 25.4.0 Validation Rules

- Ensure data integrity (e.g., unique email addresses for users).


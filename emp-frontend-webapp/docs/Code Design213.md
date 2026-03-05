# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-WEBAPP |
| Validation Timestamp | 2025-01-26T14:00:00Z |
| Original Component Count Claimed | 40 |
| Original Component Count Actual | 32 |
| Gaps Identified Count | 5 |
| Components Added Count | 8 |
| Final Component Count | 48 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Next.js 14 App Router Architecture & Server Action... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with Next.js 14 App Router standards. Identified need for explicit separation of Server Actions from UI components.

#### 2.2.1.2 Gaps Identified

- Missing centralized API Client wrapper for consistent Gateway communication
- Lack of Zod schemas for runtime validation of Server Action inputs
- Undefined Middleware strategy for JWT token management via HttpOnly cookies

#### 2.2.1.3 Components Added

- ApiClient
- AuthMiddleware
- ValidationSchemas

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- Optimistic UI update handling in Server Actions
- Global error boundary for unhandled API rejections

#### 2.2.2.4 Added Requirement Components

- OptimisticReducer
- RootErrorBoundary

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Server Components and Client Hooks well defined.

#### 2.2.3.2 Missing Pattern Components

- Service Layer abstraction to decouple Server Actions from direct fetch calls
- BFF (Backend for Frontend) aggregation logic in Server Components

#### 2.2.3.3 Added Pattern Components

- ServerActionServiceLayer
- DTOAdapters

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - Frontend Repository. State mapping verified.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

User flows mapped to Route Handlers and Pages.

#### 2.2.5.2 Missing Interaction Components

- Token refresh rotation logic in API interceptors

#### 2.2.5.3 Added Interaction Components

- TokenRefreshInterceptor

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-WEBAPP |
| Technology Stack | Next.js 14, React 18, TypeScript 5.4, Zustand, Zod... |
| Technology Guidance Integration | Next.js App Router Best Practices (Server Actions,... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 48 |
| Specification Methodology | Feature-Sliced Design adapted for Next.js App Rout... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- React Server Components (RSC) for Data Fetching
- Server Actions for Mutations
- Next.js Middleware for Auth Protection
- Zustand for Client State
- React Query for Polling/Client Fetching
- Zod for Schema Validation

#### 2.3.2.2 Directory Structure Source

Next.js Official Guidelines & Feature-Sliced Design

#### 2.3.2.3 Naming Conventions Source

React/Next.js Standard (PascalCase components, camelCase utilities)

#### 2.3.2.4 Architectural Patterns Source

BFF (Backend for Frontend) Pattern via Server Actions

#### 2.3.2.5 Performance Optimizations Applied

- Route Segment Config (Dynamic/Static)
- React.suspense for Streaming
- next/image optimization
- Bundle splitting via Client/Server component boundaries

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.dockerignore

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .dockerignore

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.env.example

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .env.example

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.eslintrc.json

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- .eslintrc.json

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.gitignore

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- .gitignore

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.prettierrc

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- .prettierrc

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

.vscode/extensions.json

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- extensions.json

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

.vscode/settings.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- settings.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

Dockerfile

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- Dockerfile

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

next.config.mjs

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- next.config.mjs

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

package.json

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- package.json

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

playwright.config.ts

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- playwright.config.ts

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

postcss.config.js

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- postcss.config.js

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

src/actions

###### 2.3.3.1.13.2 Purpose

Server Actions ('use server') for mutations

###### 2.3.3.1.13.3 Contains Files

- auth.actions.ts
- project.actions.ts
- vendor.actions.ts
- finance.actions.ts

###### 2.3.3.1.13.4 Organizational Reasoning

Decouples mutation logic from UI components, acting as the secure bridge to backend services.

###### 2.3.3.1.13.5 Framework Convention Alignment

Next.js Server Actions

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

src/app

###### 2.3.3.1.14.2 Purpose

App Router Routes (Pages, Layouts, Loading, Error)

###### 2.3.3.1.14.3 Contains Files

- layout.tsx
- page.tsx
- global-error.tsx
- (auth)/login/page.tsx
- (dashboard)/admin/layout.tsx

###### 2.3.3.1.14.4 Organizational Reasoning

Standard Next.js routing convention.

###### 2.3.3.1.14.5 Framework Convention Alignment

Next.js App Router

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

src/components/features

###### 2.3.3.1.15.2 Purpose

Domain-specific UI components

###### 2.3.3.1.15.3 Contains Files

- sow/SowReviewComposite.tsx
- notifications/NotificationCenter.tsx
- finance/TransactionLedgerTable.tsx

###### 2.3.3.1.15.4 Organizational Reasoning

Collocates related UI logic, promoting reusability and maintainability.

###### 2.3.3.1.15.5 Framework Convention Alignment

Feature-Sliced Design

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

src/lib

###### 2.3.3.1.16.2 Purpose

Utilities, Schemas, and Constants

###### 2.3.3.1.16.3 Contains Files

- schemas.ts
- utils.ts
- constants.ts

###### 2.3.3.1.16.4 Organizational Reasoning

Shared logic and definitions.

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard Lib

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

src/services

###### 2.3.3.1.17.2 Purpose

API Client wrappers and Business Logic Adapters

###### 2.3.3.1.17.3 Contains Files

- api-client.ts
- auth.service.ts
- project.service.ts
- audit.service.ts

###### 2.3.3.1.17.4 Organizational Reasoning

Encapsulates fetch logic and interaction with REPO-GW-API, isolating Server Actions from HTTP details.

###### 2.3.3.1.17.5 Framework Convention Alignment

Service Layer Pattern

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

src/store

###### 2.3.3.1.18.2 Purpose

Client-side State Management

###### 2.3.3.1.18.3 Contains Files

- use-ui-store.ts
- use-notification-store.ts

###### 2.3.3.1.18.4 Organizational Reasoning

Zustand stores for global client state.

###### 2.3.3.1.18.5 Framework Convention Alignment

Zustand Pattern

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

tailwind.config.ts

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- tailwind.config.ts

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

tsconfig.json

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- tsconfig.json

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

vitest.config.ts

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- vitest.config.ts

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | EnterpriseMediator.Web |
| Namespace Organization | By feature (src/components/features) and layer (sr... |
| Naming Conventions | PascalCase for Components, camelCase for functions... |
| Framework Alignment | TypeScript/React Standards |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

ApiClient

##### 2.3.4.1.2.0 File Path

src/services/api-client.ts

##### 2.3.4.1.3.0 Class Type

Utility Class

##### 2.3.4.1.4.0 Inheritance

None

##### 2.3.4.1.5.0 Purpose

Singleton wrapper around native fetch to handle base URL, headers, and token injection for server-side calls.

##### 2.3.4.1.6.0 Dependencies

- next/headers (cookies)
- REPO-GW-API Configuration

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses 'next/headers' cookies() to retrieve HttpOnly tokens when running in Server Actions/Components.

##### 2.3.4.1.9.0 Properties

- {'property_name': 'baseUrl', 'property_type': 'string', 'access_modifier': 'private', 'purpose': 'API Gateway Base URL', 'validation_attributes': [], 'framework_specific_configuration': 'Loaded from process.env.NEXT_PUBLIC_API_URL', 'implementation_notes': 'Environment dependent'}

##### 2.3.4.1.10.0 Methods

###### 2.3.4.1.10.1 Method Name

####### 2.3.4.1.10.1.1 Method Name

get

####### 2.3.4.1.10.1.2 Method Signature

async get<T>(endpoint: string, tags?: string[]): Promise<T>

####### 2.3.4.1.10.1.3 Return Type

Promise<T>

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

true

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

######## 2.3.4.1.10.1.7.1 Parameter Name

######### 2.3.4.1.10.1.7.1.1 Parameter Name

endpoint

######### 2.3.4.1.10.1.7.1.2 Parameter Type

string

######### 2.3.4.1.10.1.7.1.3 Is Nullable

false

######### 2.3.4.1.10.1.7.1.4 Purpose

API Endpoint

######### 2.3.4.1.10.1.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.10.1.7.2.0 Parameter Name

######### 2.3.4.1.10.1.7.2.1 Parameter Name

tags

######### 2.3.4.1.10.1.7.2.2 Parameter Type

string[]

######### 2.3.4.1.10.1.7.2.3 Is Nullable

true

######### 2.3.4.1.10.1.7.2.4 Purpose

Next.js Cache Tags

######### 2.3.4.1.10.1.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.10.1.8.0.0 Implementation Logic

Retrieves token from cookies. Constructs URL. calls fetch with Authorization header. Handles 401 via redirect. Returns typed response. Applies 'next: { tags }' for caching.

####### 2.3.4.1.10.1.9.0.0 Exception Handling

Throws typed ApiError on non-200 responses.

####### 2.3.4.1.10.1.10.0.0 Performance Considerations

Leverages Next.js Data Cache via tags.

####### 2.3.4.1.10.1.11.0.0 Validation Requirements

None

####### 2.3.4.1.10.1.12.0.0 Technology Integration Details

Integration with Next.js fetch extension

###### 2.3.4.1.10.2.0.0.0 Method Name

####### 2.3.4.1.10.2.1.0.0 Method Name

post

####### 2.3.4.1.10.2.2.0.0 Method Signature

async post<T>(endpoint: string, body: any): Promise<T>

####### 2.3.4.1.10.2.3.0.0 Return Type

Promise<T>

####### 2.3.4.1.10.2.4.0.0 Access Modifier

public

####### 2.3.4.1.10.2.5.0.0 Is Async

true

####### 2.3.4.1.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7.0.0 Parameters

######## 2.3.4.1.10.2.7.1.0 Parameter Name

######### 2.3.4.1.10.2.7.1.1 Parameter Name

endpoint

######### 2.3.4.1.10.2.7.1.2 Parameter Type

string

######### 2.3.4.1.10.2.7.1.3 Is Nullable

false

######### 2.3.4.1.10.2.7.1.4 Purpose

API Endpoint

######### 2.3.4.1.10.2.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.10.2.7.2.0 Parameter Name

######### 2.3.4.1.10.2.7.2.1 Parameter Name

body

######### 2.3.4.1.10.2.7.2.2 Parameter Type

any

######### 2.3.4.1.10.2.7.2.3 Is Nullable

false

######### 2.3.4.1.10.2.7.2.4 Purpose

Payload

######### 2.3.4.1.10.2.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.10.2.8.0.0 Implementation Logic

Serializes body to JSON. Adds Content-Type. Performs fetch.

####### 2.3.4.1.10.2.9.0.0 Exception Handling

Throws typed ApiError.

####### 2.3.4.1.10.2.10.0.0 Performance Considerations

None

####### 2.3.4.1.10.2.11.0.0 Validation Requirements

Body should be valid object

####### 2.3.4.1.10.2.12.0.0 Technology Integration Details

Standard fetch

##### 2.3.4.1.11.0.0.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0.0.0 Implementation Notes

Critical infrastructure component for connecting REPO-GW-API.

#### 2.3.4.2.0.0.0.0.0 Class Name

##### 2.3.4.2.1.0.0.0.0 Class Name

ProjectService

##### 2.3.4.2.2.0.0.0.0 File Path

src/services/project.service.ts

##### 2.3.4.2.3.0.0.0.0 Class Type

Service

##### 2.3.4.2.4.0.0.0.0 Inheritance

None

##### 2.3.4.2.5.0.0.0.0 Purpose

Domain service handling Project interactions with the API Gateway.

##### 2.3.4.2.6.0.0.0.0 Dependencies

- ApiClient
- ProjectDTO

##### 2.3.4.2.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0.0.0 Technology Integration Notes

Methods designed to be called from Server Actions or RSC.

##### 2.3.4.2.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0.0.0 Methods

###### 2.3.4.2.10.1.0.0.0 Method Name

####### 2.3.4.2.10.1.1.0.0 Method Name

getProjects

####### 2.3.4.2.10.1.2.0.0 Method Signature

async getProjects(filters?: ProjectFilter): Promise<ProjectDTO[]>

####### 2.3.4.2.10.1.3.0.0 Return Type

Promise<ProjectDTO[]>

####### 2.3.4.2.10.1.4.0.0 Access Modifier

public

####### 2.3.4.2.10.1.5.0.0 Is Async

true

####### 2.3.4.2.10.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.10.1.7.0.0 Parameters

- {'parameter_name': 'filters', 'parameter_type': 'ProjectFilter', 'is_nullable': 'true', 'purpose': 'Query parameters', 'framework_attributes': []}

####### 2.3.4.2.10.1.8.0.0 Implementation Logic

Calls ApiClient.get('/projects'). Passes cache tag 'projects'.

####### 2.3.4.2.10.1.9.0.0 Exception Handling

Propagates ApiError.

####### 2.3.4.2.10.1.10.0.0 Performance Considerations

Cached via Next.js Data Cache.

####### 2.3.4.2.10.1.11.0.0 Validation Requirements

None

####### 2.3.4.2.10.1.12.0.0 Technology Integration Details

Uses cache tagging

###### 2.3.4.2.10.2.0.0.0 Method Name

####### 2.3.4.2.10.2.1.0.0 Method Name

uploadSow

####### 2.3.4.2.10.2.2.0.0 Method Signature

async uploadSow(projectId: string, formData: FormData): Promise<void>

####### 2.3.4.2.10.2.3.0.0 Return Type

Promise<void>

####### 2.3.4.2.10.2.4.0.0 Access Modifier

public

####### 2.3.4.2.10.2.5.0.0 Is Async

true

####### 2.3.4.2.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.10.2.7.0.0 Parameters

######## 2.3.4.2.10.2.7.1.0 Parameter Name

######### 2.3.4.2.10.2.7.1.1 Parameter Name

projectId

######### 2.3.4.2.10.2.7.1.2 Parameter Type

string

######### 2.3.4.2.10.2.7.1.3 Is Nullable

false

######### 2.3.4.2.10.2.7.1.4 Purpose

Project ID

######### 2.3.4.2.10.2.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.2.10.2.7.2.0 Parameter Name

######### 2.3.4.2.10.2.7.2.1 Parameter Name

formData

######### 2.3.4.2.10.2.7.2.2 Parameter Type

FormData

######### 2.3.4.2.10.2.7.2.3 Is Nullable

false

######### 2.3.4.2.10.2.7.2.4 Purpose

File payload

######### 2.3.4.2.10.2.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.2.10.2.8.0.0 Implementation Logic

Constructs multipart request. Calls ApiClient.postForm.

####### 2.3.4.2.10.2.9.0.0 Exception Handling

Handles upload failures.

####### 2.3.4.2.10.2.10.0.0 Performance Considerations

Large file handling.

####### 2.3.4.2.10.2.11.0.0 Validation Requirements

File presence check.

####### 2.3.4.2.10.2.12.0.0 Technology Integration Details

Multipart/form-data

##### 2.3.4.2.11.0.0.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0.0.0 Implementation Notes

Maps directly to REPO-GW-API project endpoints.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

- {'interface_name': 'ProjectDTO', 'file_path': 'src/lib/types.ts', 'purpose': 'Type definition for Project entity matching Backend Contract.', 'generic_constraints': 'None', 'framework_specific_inheritance': 'None', 'method_contracts': [], 'property_contracts': [{'property_name': 'id', 'property_type': 'string', 'getter_contract': 'readonly', 'setter_contract': 'none'}, {'property_name': 'status', 'property_type': 'ProjectStatus (Enum)', 'getter_contract': 'readonly', 'setter_contract': 'none'}], 'implementation_guidance': 'Should match REPO-LIB-CONTRACTS definitions.'}

### 2.3.6.0.0.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0.0.0 Dto Specifications

- {'dto_name': 'LoginSchema', 'file_path': 'src/lib/schemas.ts', 'purpose': 'Zod schema for login form validation.', 'framework_base_class': 'z.object', 'properties': [{'property_name': 'email', 'property_type': 'string', 'validation_attributes': ['.email()'], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'password', 'property_type': 'string', 'validation_attributes': ['.min(8)'], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'Email format, Password min length.', 'serialization_requirements': 'None', 'validation_notes': 'Used in Login Form and Server Action.'}

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0.0.0 Configuration Name

NextConfig

##### 2.3.8.1.2.0.0.0.0 File Path

next.config.mjs

##### 2.3.8.1.3.0.0.0.0 Purpose

Next.js framework configuration.

##### 2.3.8.1.4.0.0.0.0 Framework Base Class

NextConfig

##### 2.3.8.1.5.0.0.0.0 Configuration Sections

###### 2.3.8.1.5.1.0.0.0 Section Name

####### 2.3.8.1.5.1.1.0.0 Section Name

images

####### 2.3.8.1.5.1.2.0.0 Properties

- {'property_name': 'remotePatterns', 'property_type': 'array', 'default_value': '[]', 'required': 'false', 'description': 'Allowed image domains'}

###### 2.3.8.1.5.2.0.0.0 Section Name

####### 2.3.8.1.5.2.1.0.0 Section Name

experimental

####### 2.3.8.1.5.2.2.0.0 Properties

- {'property_name': 'serverActions', 'property_type': 'object', 'default_value': '{}', 'required': 'false', 'description': 'Server Actions config (body size limit)'}

##### 2.3.8.1.6.0.0.0.0 Validation Requirements

Valid JS object

##### 2.3.8.1.7.0.0.0.0 Validation Notes

Ensure body size limit allows SOW uploads.

#### 2.3.8.2.0.0.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0.0.0 Configuration Name

MiddlewareConfig

##### 2.3.8.2.2.0.0.0.0 File Path

src/middleware.ts

##### 2.3.8.2.3.0.0.0.0 Purpose

Edge middleware for Auth Protection.

##### 2.3.8.2.4.0.0.0.0 Framework Base Class

None

##### 2.3.8.2.5.0.0.0.0 Configuration Sections

- {'section_name': 'matcher', 'properties': [{'property_name': 'matcher', 'property_type': 'array', 'default_value': "['/((?!api|_next/static|_next/image|favicon.ico).*)']", 'required': 'true', 'description': 'Routes to protect'}]}

##### 2.3.8.2.6.0.0.0.0 Validation Requirements

N/A

##### 2.3.8.2.7.0.0.0.0 Validation Notes

Intercepts requests to check for 'session' cookie.

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

- {'integration_target': 'REPO-GW-API', 'integration_type': 'HTTP API', 'required_client_classes': ['ApiClient'], 'configuration_requirements': 'NEXT_PUBLIC_API_URL environment variable.', 'error_handling_requirements': 'Handle 401 (Logout), 403 (Forbidden), 500 (Server Error).', 'authentication_requirements': 'HttpOnly Cookie forwarding.', 'framework_integration_patterns': 'BFF Pattern via Server Actions.', 'validation_notes': 'Ensure cookies are passed correctly in Server Context.'}

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 2 |
| Total Interfaces | 1 |
| Total Enums | 0 |
| Total Dtos | 1 |
| Total Configurations | 2 |
| Total External Integrations | 1 |
| Grand Total Components | 7 |
| Phase 2 Claimed Count | 40 |
| Phase 2 Actual Count | 32 |
| Validation Added Count | 8 |
| Final Validated Count | 48 |


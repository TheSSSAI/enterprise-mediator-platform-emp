# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-WEBAPP |
| Extraction Timestamp | 2025-05-23T14:45:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High - Next.js 14 Patterns Defined |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-INT-003

#### 1.2.1.2 Requirement Text

Standardized Secure API Communication Contracts

#### 1.2.1.3 Validation Criteria

- Frontend must consume standardized DTOs defined in the shared contracts library
- API requests must adhere to the RESTful contracts exposed by the API Gateway

#### 1.2.1.4 Implementation Implications

- Import TypeScript definitions generated from 'EnterpriseMediator.Contracts'
- Implement Zod schemas in 'src/lib/schemas.ts' that mirror backend DTO validation rules

#### 1.2.1.5 Extraction Reasoning

Ensures type safety and contract adherence between the SPA and the Backend API.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-NFR-003

#### 1.2.2.2 Requirement Text

Strict security controls including Authentication (JWT) and Rate Limiting

#### 1.2.2.3 Validation Criteria

- Session tokens must be handled via HttpOnly cookies (not accessible to client-side JS)
- Authentication state must be synchronized between Server Components and Client Components

#### 1.2.2.4 Implementation Implications

- Implement Next.js Middleware to manage session cookies and redirection
- Use Server Actions as a proxy for sensitive mutations to keep tokens server-side

#### 1.2.2.5 Extraction Reasoning

Defines the integration pattern for the Authentication Service via the Gateway.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUNC-010

#### 1.2.3.2 Requirement Text

The system shall process uploaded SOW documents asynchronously

#### 1.2.3.3 Validation Criteria

- Frontend must support multipart/form-data upload to the API Gateway
- UI must poll or listen for status updates (Processing -> Processed)

#### 1.2.3.4 Implementation Implications

- Implement 'useFileUpload' hook to handle FormData construction
- Implement 'useNotificationPolling' to check for 'SowProcessed' status updates

#### 1.2.3.5 Extraction Reasoning

Specific integration logic required for the SOW upload workflow.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

ApiClient

#### 1.3.1.2 Component Specification

A typed HTTP client wrapper (singleton) configured with the API Gateway base URL and interceptors for token injection.

#### 1.3.1.3 Implementation Requirements

- Automatically inject 'Authorization: Bearer <token>' from cookies in Server Context
- Handle global error responses (401 Unauthorized -> Redirect to Login)
- Standardize JSON serialization/deserialization

#### 1.3.1.4 Architectural Context

Infrastructure / Service Layer - The single point of egress for all HTTP traffic to the backend.

#### 1.3.1.5 Extraction Reasoning

Centralizes integration logic to ensure consistency and security across all features.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ServerActionProxy

#### 1.3.2.2 Component Specification

Next.js Server Actions acting as a secure BFF layer, receiving form data, validating it, and calling the ApiClient.

#### 1.3.2.3 Implementation Requirements

- Validate inputs using Zod schemas before calling upstream API
- Handle 'revalidatePath' and 'revalidateTag' for cache invalidation upon successful mutation
- Return typed Result objects to the client components

#### 1.3.2.4 Architectural Context

Application Layer - Bridges the UI interactions with the backend services securely.

#### 1.3.2.5 Extraction Reasoning

Next.js 14 standard pattern for secure data mutation and integration.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Presentation Layer (Client)

#### 1.4.1.2 Layer Responsibilities

React components, Client Hooks, State Management (Zustand), UI Interactions.

#### 1.4.1.3 Layer Constraints

- Must NOT contain API secrets or direct database connections
- Must fetch data via Props passed from Server Components or via Route Handlers

#### 1.4.1.4 Implementation Patterns

- Client Components ('use client')
- Compound Components (Radix UI wrappers)

#### 1.4.1.5 Extraction Reasoning

The visible interface consuming the integration.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

BFF / Integration Layer (Server)

#### 1.4.2.2 Layer Responsibilities

Data fetching (RSC), Form submission handling (Server Actions), Token management, Cookie manipulation.

#### 1.4.2.3 Layer Constraints

- Must validate all inputs from the Presentation Layer
- Must handle upstream API failures gracefully

#### 1.4.2.4 Implementation Patterns

- React Server Components
- Next.js Middleware
- BFF (Backend for Frontend)

#### 1.4.2.5 Extraction Reasoning

The server-side runtime of Next.js that integrates with the API Gateway.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

Gateway REST API

#### 1.5.1.2 Source Repository

REPO-GW-API

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

Auth API

###### 1.5.1.3.1.2 Method Signature

POST /api/v1/auth/login

###### 1.5.1.3.1.3 Method Purpose

Exchanges credentials for JWT Access/Refresh tokens.

###### 1.5.1.3.1.4 Integration Context

Called by 'auth.actions.ts' during user login flow.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

Project API

###### 1.5.1.3.2.2 Method Signature

GET /api/v1/projects/{id}

###### 1.5.1.3.2.3 Method Purpose

Retrieves project details, SOW status, and financials.

###### 1.5.1.3.2.4 Integration Context

Called by 'sow-review/[projectId]/page.tsx' (RSC) to pre-render the dashboard.

##### 1.5.1.3.3.0 Method Name

###### 1.5.1.3.3.1 Method Name

Upload API

###### 1.5.1.3.3.2 Method Signature

POST /api/v1/projects/{id}/sow

###### 1.5.1.3.3.3 Method Purpose

Uploads SOW document stream.

###### 1.5.1.3.3.4 Integration Context

Called by 'SowUploadZone.tsx' via 'project.actions.ts'.

#### 1.5.1.4.0.0 Integration Pattern

Synchronous Request-Response (HTTPS)

#### 1.5.1.5.0.0 Communication Protocol

JSON / Multipart-Form-Data

#### 1.5.1.6.0.0 Extraction Reasoning

The primary upstream dependency providing all business logic and data.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

Component Library

#### 1.5.2.2.0.0 Source Repository

REPO-LIB-UICOMP

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'UI Primitives', 'method_signature': "import { Button, Dialog, DataTable } from '@emp/ui-components'", 'method_purpose': 'Provides consistent, accessible UI elements.', 'integration_context': "Imported in all 'src/components/features/*' files."}

#### 1.5.2.4.0.0 Integration Pattern

Build-Time Module Import

#### 1.5.2.5.0.0 Communication Protocol

ESM / TypeScript

#### 1.5.2.6.0.0 Extraction Reasoning

Ensures UI consistency and accessibility compliance (REQ-UI-001, REQ-INT-001).

## 1.6.0.0.0.0 Exposed Interfaces

- {'interface_name': 'User Interface', 'consumer_repositories': ['End Users (Browser)'], 'method_contracts': [{'method_name': 'Public Routes', 'method_signature': 'https://app.enterprisemediator.com/*', 'method_purpose': 'Delivers the HTML/JS/CSS bundle to the browser.', 'implementation_requirements': 'Must pass LCP < 2.5s and WCAG 2.1 AA checks.'}], 'service_level_requirements': ['High Availability (CDN)', 'Low Latency (Edge caching)'], 'implementation_constraints': ['Responsive Design', 'Theme Awareness'], 'extraction_reasoning': "The 'service' provided by this repository is the UI itself."}

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

Next.js 14 App Router

### 1.7.2.0.0.0 Integration Technologies

- Fetch API (extended by Next.js)
- Zod (Validation)
- TypeScript (Contract Enforcement)
- Cookies (Session Management)

### 1.7.3.0.0.0 Performance Constraints

Use React Server Components for data fetching to minimize client-side JavaScript bundle size and optimize LCP.

### 1.7.4.0.0.0 Security Requirements

HttpOnly Cookies for token storage; CSRF protection via Server Actions; Content Security Policy (CSP) headers.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all upstream API dependencies (Auth, Projec... |
| Cross Reference Validation | Validated against 'REPO-GW-API' exposed contracts ... |
| Implementation Readiness Assessment | High. The 'ApiClient' and 'Server Actions' pattern... |
| Quality Assurance Confirmation | Integration architecture enforces security (Server... |


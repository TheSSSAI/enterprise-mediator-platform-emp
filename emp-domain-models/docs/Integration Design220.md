# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-LIB-DOMAIN |
| Extraction Timestamp | 2025-10-27T10:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High - Pure Domain Model |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-DAT-001

#### 1.2.1.2 Requirement Text

The system shall define a core data schema supporting Users, Roles, Clients, Vendors, Projects, SOWs, Proposals, and Financial Transactions.

#### 1.2.1.3 Validation Criteria

- Domain entities must accurately reflect the ER Diagram relationships
- Entities must encapsulate validation logic for data integrity
- Primary keys and foreign key relationships must be represented

#### 1.2.1.4 Implementation Implications

- Implement Aggregate Roots for Project, Vendor, Client, User, and Invoice
- Define Entity relationships via object references (navigation properties) or ID references
- Use C# Records for Value Objects (Money, Address) to ensure immutability

#### 1.2.1.5 Extraction Reasoning

The Domain Library is the direct code representation of the conceptual data model and business rules.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FUNC-014

#### 1.2.2.2 Requirement Text

The system shall perform a semantic search to identify and rank vendors whose skills match the requirements of the approved Project Brief.

#### 1.2.2.3 Validation Criteria

- Domain model must support Vector Embeddings as a first-class citizen
- Repository interfaces must define vector similarity search contracts

#### 1.2.2.4 Implementation Implications

- Implement `EmbeddingVector` Value Object wrapping `float[]`
- Define `IVendorRepository.GetByVectorSimilarityAsync` interface method

#### 1.2.2.5 Extraction Reasoning

The Domain must define the *interface* for vector search to allow the Infrastructure layer to implement it using pgvector without polluting the domain with DB concerns.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-BR-005

#### 1.2.3.2 Requirement Text

The system shall allow System Administrators to configure default margin/fee structures.

#### 1.2.3.3 Validation Criteria

- Financial logic must handle margin calculations accurately
- Precision loss must be avoided in currency calculations

#### 1.2.3.4 Implementation Implications

- Implement `Money` Value Object with arithmetic operations and rounding logic
- Embed financial calculation logic within the `Invoice` and `Project` aggregates

#### 1.2.3.5 Extraction Reasoning

Financial rules are core business logic and belong in the Domain layer to ensure consistency across all services.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

ProjectAggregate

#### 1.3.1.2 Component Specification

Encapsulates the lifecycle of a Project, including its SOW, Milestones, and Proposals. Enforces state transitions (e.g., Pending -> Active).

#### 1.3.1.3 Implementation Requirements

- Implement `ChangeStatus()` method with state machine logic
- Implement `AttachSow()` ensuring only one SOW exists
- Raise `ProjectStatusChanged` and `SowUploaded` domain events

#### 1.3.1.4 Architectural Context

Core Domain / Aggregate Root

#### 1.3.1.5 Extraction Reasoning

Central entity managing the primary business workflow.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

RepositoryInterfaces

#### 1.3.2.2 Component Specification

Defines the contracts for data persistence and retrieval, abstracting the underlying database technology.

#### 1.3.2.3 Implementation Requirements

- Define `IProjectRepository`, `IVendorRepository`, `IFinancialRepository`
- Include methods for Domain-specific queries (e.g., `GetBySkill`)
- Return Domain Entities, not DTOs

#### 1.3.2.4 Architectural Context

Core Domain / Interface Definitions

#### 1.3.2.5 Extraction Reasoning

Implements the Dependency Inversion Principle, allowing the Domain to define its data needs.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

SharedKernelValueObjects

#### 1.3.3.2 Component Specification

Reusable, immutable objects representing descriptive aspects of the domain (Money, Address, Email, EmbeddingVector).

#### 1.3.3.3 Implementation Requirements

- Must override equality operators (Equals, GetHashCode)
- Must be immutable (C# records recommended)
- Must contain self-validation logic (e.g., Money cannot be negative)

#### 1.3.3.4 Architectural Context

Core Domain / Shared Kernel

#### 1.3.3.5 Extraction Reasoning

Prevents logic duplication and primitive obsession across different aggregates.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Domain Layer', 'layer_responsibilities': 'Encapsulates enterprise-wide business rules, entities, and logic. It is the innermost layer of the architecture.', 'layer_constraints': ['Must have ZERO dependencies on Infrastructure, Application, or UI layers', 'Must NOT reference Entity Framework Core or ASP.NET Core libraries', 'Must be pure .NET Standard / .NET 8 code'], 'implementation_patterns': ['Domain Model Pattern', 'Aggregate Pattern', 'Domain Events'], 'extraction_reasoning': 'This repository represents the definition of the Domain Layer for the entire solution.'}

## 1.5.0.0 Dependency Interfaces

- {'interface_name': 'REPO-LIB-SHARED KERNEL', 'source_repository': 'REPO-LIB-SHARED KERNEL', 'method_contracts': [{'method_name': 'Entity', 'method_signature': 'public abstract class Entity<TId>', 'method_purpose': 'Base class providing ID and Domain Event management', 'integration_context': 'Inherited by all Domain Entities'}, {'method_name': 'IAggregateRoot', 'method_signature': 'public interface IAggregateRoot', 'method_purpose': 'Marker interface for repository constraints', 'integration_context': 'Implemented by Aggregate Roots'}], 'integration_pattern': 'Inheritance / Composition', 'communication_protocol': 'In-Process (Assembly Reference)', 'extraction_reasoning': 'The Domain likely relies on the Shared Kernel for foundational base classes to avoid duplication.'}

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

Domain Aggregates & Entities

#### 1.6.1.2 Consumer Repositories

- REPO-SVC-PROJECT
- REPO-SVC-FINANCIAL
- REPO-SVC-USER
- REPO-SVC-AIWORKER

#### 1.6.1.3 Method Contracts

##### 1.6.1.3.1 Method Name

###### 1.6.1.3.1.1 Method Name

Project

###### 1.6.1.3.1.2 Method Signature

public class Project : AggregateRoot

###### 1.6.1.3.1.3 Method Purpose

Exposes project business logic and state

###### 1.6.1.3.1.4 Implementation Requirements

Must be persistent-ignorant but mappable by EF Core

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

Vendor

###### 1.6.1.3.2.2 Method Signature

public class Vendor : AggregateRoot

###### 1.6.1.3.2.3 Method Purpose

Exposes vendor business logic and profile data

###### 1.6.1.3.2.4 Implementation Requirements

Includes vector embedding properties

#### 1.6.1.4.0.0 Service Level Requirements

- Stable API surface (Semantic Versioning)
- Thread-safety for static factory methods

#### 1.6.1.5.0.0 Implementation Constraints

- Immutability where possible
- Encapsulation of state (private setters)

#### 1.6.1.6.0.0 Extraction Reasoning

These types are the primary artifacts consumed by all Business Logic services.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

Repository Contracts

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-SVC-PROJECT (Infrastructure)
- REPO-SVC-FINANCIAL (Infrastructure)
- REPO-SVC-USER (Infrastructure)

#### 1.6.2.3.0.0 Method Contracts

##### 1.6.2.3.1.0 Method Name

###### 1.6.2.3.1.1 Method Name

IProjectRepository

###### 1.6.2.3.1.2 Method Signature

Task<Project?> GetByIdAsync(ProjectId id)

###### 1.6.2.3.1.3 Method Purpose

Contract for project data access

###### 1.6.2.3.1.4 Implementation Requirements

Must be implemented by Infrastructure layer using EF Core

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

IVendorRepository

###### 1.6.2.3.2.2 Method Signature

Task<IEnumerable<Vendor>> GetByVectorSimilarityAsync(EmbeddingVector v)

###### 1.6.2.3.2.3 Method Purpose

Contract for semantic search

###### 1.6.2.3.2.4 Implementation Requirements

Must be implemented by Infrastructure layer using pgvector

#### 1.6.2.4.0.0 Service Level Requirements

- Async-first design (Task/ValueTask)

#### 1.6.2.5.0.0 Implementation Constraints

- Must use CancellationToken

#### 1.6.2.6.0.0 Extraction Reasoning

Defines the mandatory behavior for the Persistence layer, enforcing Dependency Inversion.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 Class Library

### 1.7.2.0.0.0 Integration Technologies

- NuGet (Distribution)
- System.Collections.Immutable

### 1.7.3.0.0.0 Performance Constraints

Minimize heap allocations by using `struct` records for IDs and `ValueTask` for repository interfaces where appropriate.

### 1.7.4.0.0.0 Security Requirements

Domain logic must enforce authorization rules (e.g., 'User cannot approve their own expense') via state checks.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All core entities (Project, Vendor, Client) and bu... |
| Cross Reference Validation | Validated dependencies: Infrastructure layers impl... |
| Implementation Readiness Assessment | High. The scope is purely logical (C# code), with ... |
| Quality Assurance Confirmation | Confirmed 'Zero external dependencies' constraint ... |


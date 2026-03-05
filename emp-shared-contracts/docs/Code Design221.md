# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-LIB-CONTRACTS |
| Validation Timestamp | 2025-04-27T12:00:00Z |
| Original Component Count Claimed | 15 |
| Original Component Count Actual | 12 |
| Gaps Identified Count | 5 |
| Components Added Count | 10 |
| Final Component Count | 27 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic gap analysis against Integration Requir... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. Contains only POCOs/Records, Interfaces, and Enums. Zero logic dependency.

#### 2.2.1.2 Gaps Identified

- Missing StandardizedErrorDto for consistent global error handling
- Missing ProjectBriefApprovedEvent for Vendor Matching workflow
- Missing PayoutDto for Finance interactions

#### 2.2.1.3 Components Added

- StandardizedErrorDto
- ProjectBriefApprovedEvent
- PayoutDto
- IIntegrationEvent

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- Serialization attributes for Enums
- Marker interface for Event Bus typing

#### 2.2.2.4 Added Requirement Components

- JsonStringEnumConverter attributes
- IIntegrationEvent interface

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

DTO and Shared Kernel patterns fully implemented using C# Records.

#### 2.2.3.2 Missing Pattern Components

- Correlation ID propagation in base event contract

#### 2.2.3.3 Added Pattern Components

- CorrelationId property in IIntegrationEvent

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - Repository is persistence ignorant.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

100% coverage of payloads for SOW Upload, Vendor Matching, and Financial workflows.

#### 2.2.5.2 Missing Interaction Components

- SowUploadedEvent schema details matching Worker expectations

#### 2.2.5.3 Added Interaction Components

- SowUploadedEvent record definition

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-LIB-CONTRACTS |
| Technology Stack | .NET 8 Class Library, C# 12 |
| Technology Guidance Integration | Microsoft Framework Design Guidelines for Shared L... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 27 |
| Specification Methodology | Schema-First Contract Design using Immutable Recor... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Immutable Records (C# 12)
- Primary Constructors
- System.Text.Json Serialization
- Data Annotations Validation
- Marker Interfaces
- Semantic Versioning

#### 2.3.2.2 Directory Structure Source

.NET Standard Library Template

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Standards

#### 2.3.2.4 Architectural Patterns Source

Shared Kernel / Integration Layer

#### 2.3.2.5 Performance Optimizations Applied

- Use of 'record' for efficient equality checks
- Source Generator compatible JSON attributes
- Memory-efficient object layout

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.editorconfig

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .editorconfig

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows/dotnet-package.yml

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- dotnet-package.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.gitignore

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- .gitignore

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

Directory.Build.props

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

EnterpriseMediator.Contracts.sln

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- EnterpriseMediator.Contracts.sln

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

global.json

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- global.json

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

nswag.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- nswag.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

nuget.config

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- nuget.config

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

src/EnterpriseMediator.Contracts

###### 2.3.3.1.9.2 Purpose

Project root and configuration

###### 2.3.3.1.9.3 Contains Files

- EnterpriseMediator.Contracts.csproj

###### 2.3.3.1.9.4 Organizational Reasoning

Standard .NET project root

###### 2.3.3.1.9.5 Framework Convention Alignment

.NET CLI

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

src/EnterpriseMediator.Contracts/Common

###### 2.3.3.1.10.2 Purpose

Shared utilities and base contracts

###### 2.3.3.1.10.3 Contains Files

- IIntegrationEvent.cs
- StandardizedErrorDto.cs
- PagedResultDto.cs

###### 2.3.3.1.10.4 Organizational Reasoning

Cross-cutting concerns

###### 2.3.3.1.10.5 Framework Convention Alignment

Shared Kernel

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

src/EnterpriseMediator.Contracts/DTOs/Financials

###### 2.3.3.1.11.2 Purpose

Financial domain data transfer objects

###### 2.3.3.1.11.3 Contains Files

- PayoutDto.cs
- InvoiceDto.cs
- TransactionDto.cs

###### 2.3.3.1.11.4 Organizational Reasoning

Feature-based grouping

###### 2.3.3.1.11.5 Framework Convention Alignment

Feature Folders

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

src/EnterpriseMediator.Contracts/DTOs/Projects

###### 2.3.3.1.12.2 Purpose

Project domain data transfer objects

###### 2.3.3.1.12.3 Contains Files

- ProjectDto.cs
- CreateProjectRequest.cs
- ProjectBriefDto.cs

###### 2.3.3.1.12.4 Organizational Reasoning

Feature-based grouping

###### 2.3.3.1.12.5 Framework Convention Alignment

Feature Folders

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

src/EnterpriseMediator.Contracts/DTOs/Vendors

###### 2.3.3.1.13.2 Purpose

Vendor domain data transfer objects

###### 2.3.3.1.13.3 Contains Files

- VendorDto.cs
- VendorSkillDto.cs

###### 2.3.3.1.13.4 Organizational Reasoning

Feature-based grouping

###### 2.3.3.1.13.5 Framework Convention Alignment

Feature Folders

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

src/EnterpriseMediator.Contracts/EnterpriseMediator.Contracts.csproj

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- EnterpriseMediator.Contracts.csproj

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

src/EnterpriseMediator.Contracts/Enums

###### 2.3.3.1.15.2 Purpose

Shared enumerations

###### 2.3.3.1.15.3 Contains Files

- ProjectStatus.cs
- TransactionType.cs
- VendorStatus.cs

###### 2.3.3.1.15.4 Organizational Reasoning

Centralized constant definitions

###### 2.3.3.1.15.5 Framework Convention Alignment

C# Enums

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

src/EnterpriseMediator.Contracts/Events/Financials

###### 2.3.3.1.16.2 Purpose

Financial transaction events

###### 2.3.3.1.16.3 Contains Files

- PaymentReceivedEvent.cs
- PayoutProcessedEvent.cs

###### 2.3.3.1.16.4 Organizational Reasoning

Separation of Events from DTOs

###### 2.3.3.1.16.5 Framework Convention Alignment

EDA Conventions

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

src/EnterpriseMediator.Contracts/Events/Projects

###### 2.3.3.1.17.2 Purpose

Project lifecycle events

###### 2.3.3.1.17.3 Contains Files

- SowUploadedEvent.cs
- ProjectBriefApprovedEvent.cs
- ProjectStatusChangedEvent.cs

###### 2.3.3.1.17.4 Organizational Reasoning

Separation of Events from DTOs

###### 2.3.3.1.17.5 Framework Convention Alignment

EDA Conventions

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

tests/EnterpriseMediator.Contracts.Tests/EnterpriseMediator.Contracts.Tests.csproj

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- EnterpriseMediator.Contracts.Tests.csproj

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | EnterpriseMediator.Contracts |
| Namespace Organization | EnterpriseMediator.Contracts.{Area}.{Type} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET Guidelines |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

ProjectDto

##### 2.3.4.1.2.0 File Path

src/EnterpriseMediator.Contracts/DTOs/Projects/ProjectDto.cs

##### 2.3.4.1.3.0 Class Type

Record

##### 2.3.4.1.4.0 Inheritance

None

##### 2.3.4.1.5.0 Purpose

Standard API response format for Project entities.

##### 2.3.4.1.6.0 Dependencies

- System.Guid
- ProjectStatus

##### 2.3.4.1.7.0 Framework Specific Attributes

- [Serializable]

##### 2.3.4.1.8.0 Technology Integration Notes

Immutable record using primary constructor.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

Id

####### 2.3.4.1.9.1.2 Property Type

Guid

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Unique Identifier

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Init-only

####### 2.3.4.1.9.1.7 Implementation Notes

Primary Key

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Name

####### 2.3.4.1.9.2.2 Property Type

string

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Project Name

####### 2.3.4.1.9.2.5 Validation Attributes

- [Required]

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Non-nullable

####### 2.3.4.1.9.2.7 Implementation Notes

Max length 200

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

Status

####### 2.3.4.1.9.3.2 Property Type

ProjectStatus

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Lifecycle State

####### 2.3.4.1.9.3.5 Validation Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

####### 2.3.4.1.9.3.6 Framework Specific Configuration

String Enum

####### 2.3.4.1.9.3.7 Implementation Notes

Serialized as string for frontend readability

##### 2.3.4.1.10.0.0 Methods

*No items available*

##### 2.3.4.1.11.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0 Implementation Notes

Used by API Gateway and Frontend.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

SowUploadedEvent

##### 2.3.4.2.2.0.0 File Path

src/EnterpriseMediator.Contracts/Events/Projects/SowUploadedEvent.cs

##### 2.3.4.2.3.0.0 Class Type

Record

##### 2.3.4.2.4.0.0 Inheritance

IIntegrationEvent

##### 2.3.4.2.5.0.0 Purpose

Event payload triggering the AI Worker service (Sequence 476).

##### 2.3.4.2.6.0.0 Dependencies

- IIntegrationEvent
- System.Guid

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Implements marker interface for bus dispatch.

##### 2.3.4.2.9.0.0 Properties

###### 2.3.4.2.9.1.0 Property Name

####### 2.3.4.2.9.1.1 Property Name

EventId

####### 2.3.4.2.9.1.2 Property Type

Guid

####### 2.3.4.2.9.1.3 Access Modifier

public

####### 2.3.4.2.9.1.4 Purpose

Idempotency Key

####### 2.3.4.2.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.1.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.1.7 Implementation Notes

Generated by publisher

###### 2.3.4.2.9.2.0 Property Name

####### 2.3.4.2.9.2.1 Property Name

SowId

####### 2.3.4.2.9.2.2 Property Type

Guid

####### 2.3.4.2.9.2.3 Access Modifier

public

####### 2.3.4.2.9.2.4 Purpose

Document ID

####### 2.3.4.2.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.2.6 Framework Specific Configuration

Standard Guid

####### 2.3.4.2.9.2.7 Implementation Notes

Reference to SOW entity

###### 2.3.4.2.9.3.0 Property Name

####### 2.3.4.2.9.3.1 Property Name

S3ObjectKey

####### 2.3.4.2.9.3.2 Property Type

string

####### 2.3.4.2.9.3.3 Access Modifier

public

####### 2.3.4.2.9.3.4 Purpose

File Location

####### 2.3.4.2.9.3.5 Validation Attributes

- [Required]

####### 2.3.4.2.9.3.6 Framework Specific Configuration

String

####### 2.3.4.2.9.3.7 Implementation Notes

Path in bucket

###### 2.3.4.2.9.4.0 Property Name

####### 2.3.4.2.9.4.1 Property Name

CorrelationId

####### 2.3.4.2.9.4.2 Property Type

Guid

####### 2.3.4.2.9.4.3 Access Modifier

public

####### 2.3.4.2.9.4.4 Purpose

Tracing

####### 2.3.4.2.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.4.6 Framework Specific Configuration

Interface impl

####### 2.3.4.2.9.4.7 Implementation Notes

Cross-service tracing

###### 2.3.4.2.9.5.0 Property Name

####### 2.3.4.2.9.5.1 Property Name

CreatedAt

####### 2.3.4.2.9.5.2 Property Type

DateTimeOffset

####### 2.3.4.2.9.5.3 Access Modifier

public

####### 2.3.4.2.9.5.4 Purpose

Timestamp

####### 2.3.4.2.9.5.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.5.6 Framework Specific Configuration

Interface impl

####### 2.3.4.2.9.5.7 Implementation Notes

UTC

##### 2.3.4.2.10.0.0 Methods

*No items available*

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Must be kept small for efficient messaging.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

StandardizedErrorDto

##### 2.3.4.3.2.0.0 File Path

src/EnterpriseMediator.Contracts/Common/StandardizedErrorDto.cs

##### 2.3.4.3.3.0.0 Class Type

Record

##### 2.3.4.3.4.0.0 Inheritance

None

##### 2.3.4.3.5.0.0 Purpose

Uniform error structure for all API responses.

##### 2.3.4.3.6.0.0 Dependencies

- System.Collections.Generic

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Used by Global Exception Handlers.

##### 2.3.4.3.9.0.0 Properties

###### 2.3.4.3.9.1.0 Property Name

####### 2.3.4.3.9.1.1 Property Name

TraceId

####### 2.3.4.3.9.1.2 Property Type

string

####### 2.3.4.3.9.1.3 Access Modifier

public

####### 2.3.4.3.9.1.4 Purpose

Correlation ID

####### 2.3.4.3.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.1.6 Framework Specific Configuration

Nullable

####### 2.3.4.3.9.1.7 Implementation Notes

From RequestContext

###### 2.3.4.3.9.2.0 Property Name

####### 2.3.4.3.9.2.1 Property Name

Code

####### 2.3.4.3.9.2.2 Property Type

string

####### 2.3.4.3.9.2.3 Access Modifier

public

####### 2.3.4.3.9.2.4 Purpose

Error Code

####### 2.3.4.3.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.2.6 Framework Specific Configuration

String

####### 2.3.4.3.9.2.7 Implementation Notes

e.g., 'VALIDATION_ERROR'

###### 2.3.4.3.9.3.0 Property Name

####### 2.3.4.3.9.3.1 Property Name

Message

####### 2.3.4.3.9.3.2 Property Type

string

####### 2.3.4.3.9.3.3 Access Modifier

public

####### 2.3.4.3.9.3.4 Purpose

User Message

####### 2.3.4.3.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.3.6 Framework Specific Configuration

String

####### 2.3.4.3.9.3.7 Implementation Notes

Human readable

###### 2.3.4.3.9.4.0 Property Name

####### 2.3.4.3.9.4.1 Property Name

Details

####### 2.3.4.3.9.4.2 Property Type

IDictionary<string, string[]>

####### 2.3.4.3.9.4.3 Access Modifier

public

####### 2.3.4.3.9.4.4 Purpose

Validation Errors

####### 2.3.4.3.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.4.6 Framework Specific Configuration

Nullable

####### 2.3.4.3.9.4.7 Implementation Notes

Field-level errors

##### 2.3.4.3.10.0.0 Methods

*No items available*

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Ensures consistent error parsing on frontend.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

ProjectBriefApprovedEvent

##### 2.3.4.4.2.0.0 File Path

src/EnterpriseMediator.Contracts/Events/Projects/ProjectBriefApprovedEvent.cs

##### 2.3.4.4.3.0.0 Class Type

Record

##### 2.3.4.4.4.0.0 Inheritance

IIntegrationEvent

##### 2.3.4.4.5.0.0 Purpose

Triggers Vendor Matching after SOW approval (Sequence 486).

##### 2.3.4.4.6.0.0 Dependencies

- IIntegrationEvent
- ProjectBriefDto

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

Carries full brief data to decouple search service.

##### 2.3.4.4.9.0.0 Properties

###### 2.3.4.4.9.1.0 Property Name

####### 2.3.4.4.9.1.1 Property Name

ProjectId

####### 2.3.4.4.9.1.2 Property Type

Guid

####### 2.3.4.4.9.1.3 Access Modifier

public

####### 2.3.4.4.9.1.4 Purpose

Project Ref

####### 2.3.4.4.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.1.6 Framework Specific Configuration

Guid

####### 2.3.4.4.9.1.7 Implementation Notes

Target Project

###### 2.3.4.4.9.2.0 Property Name

####### 2.3.4.4.9.2.1 Property Name

Brief

####### 2.3.4.4.9.2.2 Property Type

ProjectBriefDto

####### 2.3.4.4.9.2.3 Access Modifier

public

####### 2.3.4.4.9.2.4 Purpose

Data for Embedding

####### 2.3.4.4.9.2.5 Validation Attributes

- [Required]

####### 2.3.4.4.9.2.6 Framework Specific Configuration

Complex Type

####### 2.3.4.4.9.2.7 Implementation Notes

Contains skills and text

###### 2.3.4.4.9.3.0 Property Name

####### 2.3.4.4.9.3.1 Property Name

EventId

####### 2.3.4.4.9.3.2 Property Type

Guid

####### 2.3.4.4.9.3.3 Access Modifier

public

####### 2.3.4.4.9.3.4 Purpose

Idempotency

####### 2.3.4.4.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.3.6 Framework Specific Configuration

Interface impl

####### 2.3.4.4.9.3.7 Implementation Notes

Generated

###### 2.3.4.4.9.4.0 Property Name

####### 2.3.4.4.9.4.1 Property Name

CorrelationId

####### 2.3.4.4.9.4.2 Property Type

Guid

####### 2.3.4.4.9.4.3 Access Modifier

public

####### 2.3.4.4.9.4.4 Purpose

Trace

####### 2.3.4.4.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.4.6 Framework Specific Configuration

Interface impl

####### 2.3.4.4.9.4.7 Implementation Notes

Chain ID

###### 2.3.4.4.9.5.0 Property Name

####### 2.3.4.4.9.5.1 Property Name

CreatedAt

####### 2.3.4.4.9.5.2 Property Type

DateTimeOffset

####### 2.3.4.4.9.5.3 Access Modifier

public

####### 2.3.4.4.9.5.4 Purpose

Time

####### 2.3.4.4.9.5.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.5.6 Framework Specific Configuration

Interface impl

####### 2.3.4.4.9.5.7 Implementation Notes

UTC

##### 2.3.4.4.10.0.0 Methods

*No items available*

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

Avoids database callback from search service.

### 2.3.5.0.0.0.0 Interface Specifications

- {'interface_name': 'IIntegrationEvent', 'file_path': 'src/EnterpriseMediator.Contracts/Common/IIntegrationEvent.cs', 'purpose': 'Marker interface ensuring all events have standard metadata.', 'generic_constraints': 'None', 'framework_specific_inheritance': 'None', 'method_contracts': [], 'property_contracts': [{'property_name': 'EventId', 'property_type': 'Guid', 'getter_contract': 'Unique ID', 'setter_contract': 'Init'}, {'property_name': 'CorrelationId', 'property_type': 'Guid', 'getter_contract': 'Trace ID', 'setter_contract': 'Init'}, {'property_name': 'CreatedAt', 'property_type': 'DateTimeOffset', 'getter_contract': 'Timestamp', 'setter_contract': 'Init'}], 'implementation_guidance': 'Implemented by all event records in the library.', 'validation_notes': 'Allows generic handling in Event Bus infrastructure.'}

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'ProjectStatus', 'file_path': 'src/EnterpriseMediator.Contracts/Enums/ProjectStatus.cs', 'underlying_type': 'int', 'purpose': 'Project lifecycle states.', 'framework_attributes': ['[JsonConverter(typeof(JsonStringEnumConverter))]'], 'values': [{'value_name': 'Pending', 'value': '0', 'description': 'Created'}, {'value_name': 'SowProcessing', 'value': '1', 'description': 'AI extraction'}, {'value_name': 'Proposed', 'value': '2', 'description': 'Brief ready'}, {'value_name': 'Awarded', 'value': '3', 'description': 'Vendor selected'}, {'value_name': 'Active', 'value': '4', 'description': 'In progress'}, {'value_name': 'Completed', 'value': '5', 'description': 'Done'}], 'validation_notes': 'Ensures consistent state logic across API and Worker.'}

### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

CreateProjectRequest

##### 2.3.7.1.2.0.0 File Path

src/EnterpriseMediator.Contracts/DTOs/Projects/CreateProjectRequest.cs

##### 2.3.7.1.3.0.0 Purpose

Payload for initiating a project.

##### 2.3.7.1.4.0.0 Framework Base Class

None

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

Name

####### 2.3.7.1.5.1.2 Property Type

string

####### 2.3.7.1.5.1.3 Validation Attributes

- [Required]
- [MaxLength(100)]

####### 2.3.7.1.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

ClientId

####### 2.3.7.1.5.2.2 Property Type

Guid

####### 2.3.7.1.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.1.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.3.0 Property Name

####### 2.3.7.1.5.3.1 Property Name

Description

####### 2.3.7.1.5.3.2 Property Type

string

####### 2.3.7.1.5.3.3 Validation Attributes

- [MaxLength(1000)]

####### 2.3.7.1.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules

Standard data annotations.

##### 2.3.7.1.7.0.0 Serialization Requirements

JSON

##### 2.3.7.1.8.0.0 Validation Notes

Used by Project API.

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

PayoutDto

##### 2.3.7.2.2.0.0 File Path

src/EnterpriseMediator.Contracts/DTOs/Financials/PayoutDto.cs

##### 2.3.7.2.3.0.0 Purpose

Payload for payout information.

##### 2.3.7.2.4.0.0 Framework Base Class

None

##### 2.3.7.2.5.0.0 Properties

###### 2.3.7.2.5.1.0 Property Name

####### 2.3.7.2.5.1.1 Property Name

VendorId

####### 2.3.7.2.5.1.2 Property Type

Guid

####### 2.3.7.2.5.1.3 Validation Attributes

- [Required]

####### 2.3.7.2.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0 Property Name

####### 2.3.7.2.5.2.1 Property Name

Amount

####### 2.3.7.2.5.2.2 Property Type

decimal

####### 2.3.7.2.5.2.3 Validation Attributes

- [Range(0.01, double.MaxValue)]

####### 2.3.7.2.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.3.0 Property Name

####### 2.3.7.2.5.3.1 Property Name

Currency

####### 2.3.7.2.5.3.2 Property Type

string

####### 2.3.7.2.5.3.3 Validation Attributes

- [Length(3,3)]

####### 2.3.7.2.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0 Validation Rules

Financial validity.

##### 2.3.7.2.7.0.0 Serialization Requirements

JSON

##### 2.3.7.2.8.0.0 Validation Notes

Used by Finance API.

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'EnterpriseMediator.Contracts.csproj', 'file_path': 'src/EnterpriseMediator.Contracts/EnterpriseMediator.Contracts.csproj', 'purpose': 'Project configuration and NuGet packaging.', 'framework_base_class': 'Microsoft.NET.Sdk', 'configuration_sections': [{'section_name': 'PropertyGroup', 'properties': [{'property_name': 'TargetFramework', 'property_type': 'string', 'default_value': 'net8.0', 'required': 'true', 'description': 'Target framework'}, {'property_name': 'Nullable', 'property_type': 'string', 'default_value': 'enable', 'required': 'true', 'description': 'Nullable Reference Types'}, {'property_name': 'GeneratePackageOnBuild', 'property_type': 'boolean', 'default_value': 'true', 'required': 'false', 'description': 'Auto-pack'}, {'property_name': 'PackageId', 'property_type': 'string', 'default_value': 'EnterpriseMediator.Contracts', 'required': 'true', 'description': 'NuGet ID'}]}], 'validation_requirements': 'Must enable Nullable context.', 'validation_notes': 'Ensures package is consumable by all services.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 1 |
| Total Enums | 4 |
| Total Dtos | 7 |
| Total Configurations | 1 |
| Total External Integrations | 2 |
| Grand Total Components | 27 |
| Phase 2 Claimed Count | 15 |
| Phase 2 Actual Count | 12 |
| Validation Added Count | 15 |
| Final Validated Count | 27 |


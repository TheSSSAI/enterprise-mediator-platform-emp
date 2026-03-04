# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- Entity Framework Core 8
- PostgreSQL 16
- Next.js 14
- TypeScript 5.4

## 1.3 Service Interfaces

- Application.API (RESTful)
- SowProcessingWorker (Message Consumer)
- DashboardMetricsCalculator (Scheduled Job)

## 1.4 Data Models

- Domain Entities (e.g., Project, Client, Vendor)
- API Data Transfer Objects (DTOs)
- CSV File Schema
- Unstructured SOW Text

# 2.0 Data Mapping Strategy

## 2.1 Essential Mappings

### 2.1.1 Mapping Id

#### 2.1.1.1 Mapping Id

ETD_001

#### 2.1.1.2 Source

Domain Entities (e.g., Client, Vendor, Project)

#### 2.1.1.3 Target

API DTOs (e.g., ClientSummaryDto, ProjectDetailDto)

#### 2.1.1.4 Transformation

flattened

#### 2.1.1.5 Configuration

*No data available*

#### 2.1.1.6 Mapping Technique

Object-to-Object Mapping Library (e.g., AutoMapper)

#### 2.1.1.7 Justification

Required by the Clean Architecture to decouple the internal domain model from the external API contract, preventing breaking changes and exposing only necessary data.

#### 2.1.1.8 Complexity

medium

### 2.1.2.0 Mapping Id

#### 2.1.2.1 Mapping Id

DTE_001

#### 2.1.2.2 Source

API DTOs (e.g., CreateClientRequestDto)

#### 2.1.2.3 Target

Domain Entities

#### 2.1.2.4 Transformation

direct

#### 2.1.2.5 Configuration

*No data available*

#### 2.1.2.6 Mapping Technique

Object-to-Object Mapping Library

#### 2.1.2.7 Justification

Essential for processing incoming POST and PUT requests to create or update data in the persistence layer.

#### 2.1.2.8 Complexity

simple

### 2.1.3.0 Mapping Id

#### 2.1.3.1 Mapping Id

SOW_PB_001

#### 2.1.3.2 Source

Unstructured SOW Document Text

#### 2.1.3.3 Target

ProjectBrief Domain Entity

#### 2.1.3.4 Transformation

custom

#### 2.1.3.5 Configuration

##### 2.1.3.5.1 Prompt Engineering

LLM prompts designed to extract specific fields like scope summary and required skills.

#### 2.1.3.6.0 Mapping Technique

Large Language Model (LLM) Extraction via Azure.AI.OpenAI SDK

#### 2.1.3.7.0 Justification

Core requirement REQ-FUNC-013 to automatically extract structured data from uploaded SOWs.

#### 2.1.3.8.0 Complexity

complex

### 2.1.4.0.0 Mapping Id

#### 2.1.4.1.0 Mapping Id

AGG_DM_001

#### 2.1.4.2.0 Source

Project, Invoice, Payout Entities

#### 2.1.4.3.0 Target

DashboardMetrics Entity

#### 2.1.4.4.0 Transformation

aggregation

#### 2.1.4.5.0 Configuration

##### 2.1.4.5.1 Aggregates

- COUNT(Projects) by status
- SUM(Invoice.amount)
- SUM(Payout.amount)

#### 2.1.4.6.0 Mapping Technique

SQL Queries within DashboardMetricsCalculator service

#### 2.1.4.7.0 Justification

Fulfills REQ-FUNC-024 for a high-performance dashboard by pre-calculating and denormalizing key business metrics.

#### 2.1.4.8.0 Complexity

medium

### 2.1.5.0.0 Mapping Id

#### 2.1.5.1.0 Mapping Id

CSV_ENT_001

#### 2.1.5.2.0 Source

CSV Row (Client or Vendor data)

#### 2.1.5.3.0 Target

Client or Vendor Domain Entity

#### 2.1.5.4.0 Transformation

direct

#### 2.1.5.5.0 Configuration

##### 2.1.5.5.1 Header Mapping

✅ Yes

#### 2.1.5.6.0 Mapping Technique

CSV Parsing Library

#### 2.1.5.7.0 Justification

Required for the bulk data import feature specified in REQ-NFR-009.

#### 2.1.5.8.0 Complexity

simple

## 2.2.0.0.0 Object To Object Mappings

- {'sourceObject': 'Project (Entity)', 'targetObject': 'ProjectSummaryDto (API DTO)', 'fieldMappings': [{'sourceField': 'projectId', 'targetField': 'id', 'transformation': 'direct', 'dataTypeConversion': 'Guid to string'}, {'sourceField': 'name', 'targetField': 'projectName', 'transformation': 'direct', 'dataTypeConversion': 'none'}, {'sourceField': 'status', 'targetField': 'status', 'transformation': 'direct', 'dataTypeConversion': 'none'}, {'sourceField': 'Client.companyName', 'targetField': 'clientName', 'transformation': 'flattening', 'dataTypeConversion': 'none'}]}

## 2.3.0.0.0 Data Type Conversions

### 2.3.1.0.0 From

#### 2.3.1.1.0 From

DECIMAL (database)

#### 2.3.1.2.0 To

string (JSON for currency display)

#### 2.3.1.3.0 Conversion Method

Formatted string conversion

#### 2.3.1.4.0 Validation Required

❌ No

### 2.3.2.0.0 From

#### 2.3.2.1.0 From

DateTime (database)

#### 2.3.2.2.0 To

string (ISO 8601 in JSON)

#### 2.3.2.3.0 Conversion Method

Standard serialization

#### 2.3.2.4.0 Validation Required

❌ No

### 2.3.3.0.0 From

#### 2.3.3.1.0 From

JSON (database column)

#### 2.3.3.2.0 To

C# Object/Dictionary

#### 2.3.3.3.0 Conversion Method

JSON Deserialization

#### 2.3.3.4.0 Validation Required

✅ Yes

## 2.4.0.0.0 Bidirectional Mappings

- {'entity': 'Vendor', 'forwardMapping': 'Vendor Entity to VendorProfileDto', 'reverseMapping': 'UpdateVendorProfileDto to Vendor Entity', 'consistencyStrategy': 'Update operations will fetch the existing entity, apply changes from the DTO, and then persist.'}

# 3.0.0.0.0 Schema Validation Requirements

## 3.1.0.0.0 Field Level Validations

### 3.1.1.0.0 Field

#### 3.1.1.1.0 Field

User.email (in DTO)

#### 3.1.1.2.0 Rules

- NotNull
- NotEmpty
- EmailAddress

#### 3.1.1.3.0 Priority

🚨 critical

#### 3.1.1.4.0 Error Message

A valid email address is required.

### 3.1.2.0.0 Field

#### 3.1.2.1.0 Field

Proposal.cost (in DTO)

#### 3.1.2.2.0 Rules

- NotNull
- GreaterThan(0)

#### 3.1.2.3.0 Priority

🔴 high

#### 3.1.2.4.0 Error Message

Proposed cost must be a positive value.

## 3.2.0.0.0 Cross Field Validations

- {'validationId': 'INV_DATES_001', 'fields': ['Invoice.issuedDate', 'Invoice.dueDate'], 'rule': 'dueDate must be greater than or equal to issuedDate.', 'condition': 'Applies when creating or updating an Invoice.', 'errorHandling': 'Reject request with a 400 Bad Request status and a descriptive error message.'}

## 3.3.0.0.0 Business Rule Validations

- {'ruleId': 'BR_MARGIN_OVERRIDE_001', 'description': "Ensure that a margin override can only be set by a user with the 'SystemAdmin' role.", 'fields': ['Project.marginOverride'], 'logic': 'Check user role during the transformation from DTO to Entity. If the field is present and the user is not an Admin, reject the change.', 'priority': 'critical'}

## 3.4.0.0.0 Conditional Validations

- {'condition': "When Project.status is 'COMPLETED'", 'applicableFields': ['Project.totalInvoicedAmount'], 'validationRules': ['Must be greater than zero.']}

## 3.5.0.0.0 Validation Groups

- {'groupName': 'UserCreation', 'validations': ['User.email NotNull', 'User.passwordHash NotNull', 'User.firstName NotNull'], 'executionOrder': 1, 'stopOnFirstFailure': True}

# 4.0.0.0.0 Transformation Pattern Evaluation

## 4.1.0.0.0 Selected Patterns

### 4.1.1.0.0 Pattern

#### 4.1.1.1.0 Pattern

pipeline

#### 4.1.1.2.0 Use Case

SOW Document Processing

#### 4.1.1.3.0 Implementation

Implemented in the SowProcessingWorker service.

#### 4.1.1.4.0 Justification

The transformation from an unstructured file to a structured ProjectBrief is a multi-step process: text extraction, LLM data extraction, skill vectorization, and database persistence. A pipeline pattern organizes this flow logically.

### 4.1.2.0.0 Pattern

#### 4.1.2.1.0 Pattern

adapter

#### 4.1.2.2.0 Use Case

CSV Data Import

#### 4.1.2.3.0 Implementation

An adapter class that conforms the interface of a generic data import service to a specific CSV parsing library.

#### 4.1.2.4.0 Justification

Decouples the core import logic from the specific file format and parsing library, allowing for easier future expansion to other formats like XML or JSON.

## 4.2.0.0.0 Pipeline Processing

### 4.2.1.0.0 Required

✅ Yes

### 4.2.2.0.0 Stages

#### 4.2.2.1.0 Stage

##### 4.2.2.1.1 Stage

TextExtraction

##### 4.2.2.1.2 Transformation

Convert SOW file (PDF, DOCX) to plain text using TikaOnDotNet.

##### 4.2.2.1.3 Dependencies

*No items available*

#### 4.2.2.2.0 Stage

##### 4.2.2.2.1 Stage

DataExtraction

##### 4.2.2.2.2 Transformation

Send text to LLM to extract structured JSON data.

##### 4.2.2.2.3 Dependencies

- TextExtraction

#### 4.2.2.3.0 Stage

##### 4.2.2.3.1 Stage

DataValidationAndPersistence

##### 4.2.2.3.2 Transformation

Validate extracted data and map to ProjectBrief entity.

##### 4.2.2.3.3 Dependencies

- DataExtraction

### 4.2.3.0.0 Parallelization

❌ No

## 4.3.0.0.0 Processing Mode

### 4.3.1.0.0 Real Time

#### 4.3.1.1.0 Required

✅ Yes

#### 4.3.1.2.0 Scenarios

- API request/response (Entity to DTO and DTO to Entity)

#### 4.3.1.3.0 Latency Requirements

< 250ms p95 as per REQ-PERF-001

### 4.3.2.0.0 Batch

| Property | Value |
|----------|-------|
| Required | ✅ |
| Batch Size | 1000 |
| Frequency | On-demand for CSV import; Nightly for Dashboard Me... |

### 4.3.3.0.0 Streaming

| Property | Value |
|----------|-------|
| Required | ❌ |
| Streaming Framework | N/A |
| Windowing Strategy | N/A |

## 4.4.0.0.0 Canonical Data Model

### 4.4.1.0.0 Applicable

❌ No

### 4.4.2.0.0 Scope

*No items available*

### 4.4.3.0.0 Benefits

- Not applicable. A canonical data model would introduce unnecessary complexity for a modular monolith with a limited number of services and well-defined integration points.

# 5.0.0.0.0 Version Handling Strategy

## 5.1.0.0.0 Schema Evolution

### 5.1.1.0.0 Strategy

Additive changes for API DTOs. New versions for breaking changes.

### 5.1.2.0.0 Versioning Scheme

URL Path Versioning (e.g., /api/v1/...)

### 5.1.3.0.0 Compatibility

| Property | Value |
|----------|-------|
| Backward | ✅ |
| Forward | ❌ |
| Reasoning | API clients should be tolerant of new fields being... |

## 5.2.0.0.0 Transformation Versioning

| Property | Value |
|----------|-------|
| Mechanism | Code-based versioning within the mapping profiles ... |
| Version Identification | Git commit hash |
| Migration Strategy | Deploy new transformation logic with the new appli... |

## 5.3.0.0.0 Data Model Changes

| Property | Value |
|----------|-------|
| Migration Path | Handled by Entity Framework Core migrations, which... |
| Rollback Strategy | Execute the 'Down' script of the corresponding EF ... |
| Validation Strategy | Run integration tests against the migrated databas... |

## 5.4.0.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | The API contract serves as the schema definition. ... |

# 6.0.0.0.0 Performance Optimization

## 6.1.0.0.0 Critical Requirements

### 6.1.1.0.0 Operation

#### 6.1.1.1.0 Operation

API Entity-to-DTO Transformation

#### 6.1.1.2.0 Max Latency

< 50ms

#### 6.1.1.3.0 Throughput Target

N/A

#### 6.1.1.4.0 Justification

Must be a small fraction of the total API response time target of 250ms (REQ-PERF-001).

### 6.1.2.0.0 Operation

#### 6.1.2.1.0 Operation

Dashboard Metrics Aggregation

#### 6.1.2.2.0 Max Latency

< 5 minutes (for nightly batch job)

#### 6.1.2.3.0 Throughput Target

Process all active projects

#### 6.1.2.4.0 Justification

The transformation populates the denormalized DashboardMetrics table, which is critical for meeting dashboard LCP targets (REQ-PERF-002).

## 6.2.0.0.0 Parallelization Opportunities

- {'transformation': 'CSV Bulk Import', 'parallelizationStrategy': 'Process file in chunks of rows in parallel tasks.', 'expectedGain': 'Significant reduction in import time for large files.'}

## 6.3.0.0.0 Caching Strategies

- {'cacheType': 'In-Memory (Distributed, e.g., Redis)', 'cacheScope': 'Application-wide', 'evictionPolicy': 'Time-To-Live (TTL)', 'applicableTransformations': ['Master list transformations (e.g., Skill entity to Skill DTO)', 'System Settings transformation']}

## 6.4.0.0.0 Memory Optimization

### 6.4.1.0.0 Techniques

- Using IQueryable projections in EF Core to only select required columns from the database before mapping to DTOs, avoiding loading full entities into memory.

### 6.4.2.0.0 Thresholds

N/A

### 6.4.3.0.0 Monitoring Required

✅ Yes

## 6.5.0.0.0 Lazy Evaluation

### 6.5.1.0.0 Applicable

✅ Yes

### 6.5.2.0.0 Scenarios

- Database queries for transformations. EF Core's IQueryable interface uses lazy evaluation by default.

### 6.5.3.0.0 Implementation

Use of LINQ and IQueryable to build up database queries, which are only executed when the data is enumerated (e.g., by calling ToListAsync()).

## 6.6.0.0.0 Bulk Processing

### 6.6.1.0.0 Required

✅ Yes

### 6.6.2.0.0 Batch Sizes

#### 6.6.2.1.0 Optimal

1,000

#### 6.6.2.2.0 Maximum

5,000

### 6.6.3.0.0 Parallelism

4

# 7.0.0.0.0 Error Handling And Recovery

## 7.1.0.0.0 Error Handling Strategies

### 7.1.1.0.0 Error Type

#### 7.1.1.1.0 Error Type

DTO Validation Failure

#### 7.1.1.2.0 Strategy

Reject and Log

#### 7.1.1.3.0 Fallback Action

Return HTTP 400 Bad Request with a structured error response detailing validation failures.

#### 7.1.1.4.0 Escalation Path

*No items available*

### 7.1.2.0.0 Error Type

#### 7.1.2.1.0 Error Type

CSV Row Processing Failure

#### 7.1.2.2.0 Strategy

Skip and Log

#### 7.1.2.3.0 Fallback Action

Log the failing row number and error, continue processing the rest of the file, and provide a summary report at the end.

#### 7.1.2.4.0 Escalation Path

- User Notification

### 7.1.3.0.0 Error Type

#### 7.1.3.1.0 Error Type

LLM Data Extraction Failure

#### 7.1.3.2.0 Strategy

Retry and Fail

#### 7.1.3.3.0 Fallback Action

After retries, update the SowDocument status to 'Failed' with error details.

#### 7.1.3.4.0 Escalation Path

- Admin Notification

## 7.2.0.0.0 Logging Requirements

### 7.2.1.0.0 Log Level

warn

### 7.2.2.0.0 Included Data

- Transformation ID
- Source Data Snippet (sanitized)
- Error Message
- StackTrace

### 7.2.3.0.0 Retention Period

30 days

### 7.2.4.0.0 Alerting

✅ Yes

## 7.3.0.0.0 Partial Success Handling

### 7.3.1.0.0 Strategy

For CSV import, a summary report is generated detailing successful imports, warnings, and failures with specific error messages per row, as required by REQ-NFR-009.

### 7.3.2.0.0 Reporting Mechanism

Downloadable report file (CSV or TXT).

### 7.3.3.0.0 Recovery Actions

- User manually corrects failed rows in the source CSV and re-uploads.

## 7.4.0.0.0 Circuit Breaking

*No items available*

## 7.5.0.0.0 Retry Strategies

- {'operation': 'LLM API Call during SOW processing', 'maxRetries': 3, 'backoffStrategy': 'exponential', 'retryConditions': ['HTTP 5xx Server Errors', 'Rate Limiting Errors (HTTP 429)', 'Request Timeouts']}

## 7.6.0.0.0 Error Notifications

### 7.6.1.0.0 Condition

#### 7.6.1.1.0 Condition

CSV import fails for more than 20% of rows.

#### 7.6.1.2.0 Recipients

- System Administrator

#### 7.6.1.3.0 Severity

medium

#### 7.6.1.4.0 Channel

Email

### 7.6.2.0.0 Condition

#### 7.6.2.1.0 Condition

SOW processing fails after all retries.

#### 7.6.2.2.0 Recipients

- System Administrator

#### 7.6.2.3.0 Severity

high

#### 7.6.2.4.0 Channel

In-App Notification

# 8.0.0.0.0 Project Specific Transformations

## 8.1.0.0.0 SOW to Project Brief Transformation

### 8.1.1.0.0 Transformation Id

PST_SOW_001

### 8.1.2.0.0 Name

SOW to Project Brief Transformation

### 8.1.3.0.0 Description

Transforms an unstructured SOW document into a structured, validated ProjectBrief entity, including extracting and vectorizing required skills. Fulfills REQ-FUNC-013.

### 8.1.4.0.0 Source

#### 8.1.4.1.0 Service

SowProcessingWorker

#### 8.1.4.2.0 Model

Raw Text from SowDocument

#### 8.1.4.3.0 Fields

- documentText

### 8.1.5.0.0 Target

#### 8.1.5.1.0 Service

Infrastructure (Database)

#### 8.1.5.2.0 Model

ProjectBrief, ProjectBriefSkill, Skill

#### 8.1.5.3.0 Fields

- scopeSummary
- skill.name
- skill.vectorEmbedding

### 8.1.6.0.0 Transformation

#### 8.1.6.1.0 Type

🔹 custom

#### 8.1.6.2.0 Logic

A multi-stage pipeline involving text extraction (Tika), structured data extraction (LLM), and vector embedding generation (LLM).

#### 8.1.6.3.0 Configuration

*No data available*

### 8.1.7.0.0 Frequency

on-demand

### 8.1.8.0.0 Criticality

critical

### 8.1.9.0.0 Dependencies

- External LLM Service

### 8.1.10.0.0 Validation

#### 8.1.10.1.0 Pre Transformation

- Ensure SowDocument status is 'PROCESSING'.

#### 8.1.10.2.0 Post Transformation

- Verify that key fields in ProjectBrief (e.g., scopeSummary) are populated.
- Update SowDocument status to 'REVIEW_PENDING' on success or 'FAILED' on error.

### 8.1.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Low to Medium (tens to hundreds per day) |
| Latency Requirement | Asynchronous, no strict latency requirement, but s... |
| Optimization Strategy | Offloaded to a background worker to not impact API... |

## 8.2.0.0.0 Dashboard Metrics Aggregation

### 8.2.1.0.0 Transformation Id

PST_DASH_002

### 8.2.2.0.0 Name

Dashboard Metrics Aggregation

### 8.2.3.0.0 Description

Aggregates transactional data from multiple tables into a single denormalized table to provide fast, real-time overviews for the admin dashboard. Fulfills REQ-FUNC-024.

### 8.2.4.0.0 Source

#### 8.2.4.1.0 Service

DashboardMetricsCalculator

#### 8.2.4.2.0 Model

Project, Invoice, Payout

#### 8.2.4.3.0 Fields

- Project.status
- Invoice.amount
- Invoice.status
- Payout.amount

### 8.2.5.0.0 Target

#### 8.2.5.1.0 Service

Infrastructure (Database)

#### 8.2.5.2.0 Model

DashboardMetrics

#### 8.2.5.3.0 Fields

- projectsByStatusCount
- pendingActionsCount
- financialMetrics

### 8.2.6.0.0 Transformation

#### 8.2.6.1.0 Type

🔹 aggregation

#### 8.2.6.2.0 Logic

SQL GROUP BY and SUM/COUNT functions executed by a scheduled job.

#### 8.2.6.3.0 Configuration

*No data available*

### 8.2.7.0.0 Frequency

batch

### 8.2.8.0.0 Criticality

high

### 8.2.9.0.0 Dependencies

*No items available*

### 8.2.10.0.0 Validation

#### 8.2.10.1.0 Pre Transformation

*No items available*

#### 8.2.10.2.0 Post Transformation

- Check that lastUpdatedAt timestamp is updated.

### 8.2.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Aggregates thousands of records. |
| Latency Requirement | < 5 minutes for batch run. |
| Optimization Strategy | Efficient SQL queries with appropriate indexes on ... |

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

Core Entity <-> DTO Mappings

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Medium

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

SOW to Project Brief Transformation Pipeline

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- Core Entity <-> DTO Mappings

### 9.2.4.0.0 Estimated Effort

Large

### 9.2.5.0.0 Risk Level

high

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Dashboard Metrics Aggregation Transformation

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- Core Entity <-> DTO Mappings

### 9.3.4.0.0 Estimated Effort

Medium

### 9.3.5.0.0 Risk Level

low

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

CSV Data Import Transformation and Validation

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

*No items available*

### 9.4.4.0.0 Estimated Effort

Medium

### 9.4.5.0.0 Risk Level

medium

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Inaccuracy or inconsistency in LLM-based SOW data extraction.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

The 'human-in-the-loop' review interface (REQ-FUNC-013) is the primary mitigation, allowing admins to correct extracted data before it's used for matching.

### 10.1.5.0.0 Contingency Plan

If extraction quality is poor, enhance prompt engineering or fine-tune the model. Fallback to manual data entry for the Project Brief.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Poor performance of Entity-to-DTO mappings for large data sets.

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Use IQueryable projections (e.g., AutoMapper's ProjectTo) to ensure only required data is pulled from the database, preventing over-fetching.

### 10.2.5.0.0 Contingency Plan

Manually write optimized DTO projection queries for performance-critical endpoints.

## 10.3.0.0.0 Risk

### 10.3.1.0.0 Risk

CSV import fails due to malformed data, leading to user frustration.

### 10.3.2.0.0 Impact

medium

### 10.3.3.0.0 Probability

high

### 10.3.4.0.0 Mitigation

Implement the 'dry run' validation feature and provide detailed, row-level error reporting as specified in REQ-NFR-009.

### 10.3.5.0.0 Contingency Plan

Provide clear documentation and a downloadable CSV template to guide users.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Technology Selection

### 11.1.2.0.0 Recommendation

Adopt a dedicated object-to-object mapping library like AutoMapper or Mapster within the .NET backend.

### 11.1.3.0.0 Justification

This will standardize and automate the repetitive task of mapping between Entities and DTOs, reducing boilerplate code, minimizing human error, and improving maintainability.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Configure mapping profiles during application startup and use dependency injection to provide the mapper instance to application services.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Performance

### 11.2.2.0.0 Recommendation

Strictly enforce the use of database projections (e.g., 'Select' in LINQ or 'ProjectTo' in AutoMapper) for all read operations that transform entities to DTOs.

### 11.2.3.0.0 Justification

This is the single most important optimization for read-heavy applications using an ORM. It prevents loading entire, often large, entity graphs into memory just to map a few properties, drastically reducing memory usage and database load.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

Establish this as a code review standard for all new repository methods or service queries.

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Error Handling

### 11.3.2.0.0 Recommendation

For the CSV import transformation, design the error report to be re-usable. The report should contain the original data for the failed row plus the error message, allowing users to fix the data in the report file itself and re-upload it.

### 11.3.3.0.0 Justification

This creates a much better user experience for handling bulk data errors, as it closes the loop and simplifies the correction process, directly addressing the risks associated with data quality in REQ-NFR-009.

### 11.3.4.0.0 Priority

🟡 medium

### 11.3.5.0.0 Implementation Notes

The import process should be able to accept the error report format as a valid input.


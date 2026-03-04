# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- Next.js 14
- PostgreSQL 16
- Serilog
- OpenTelemetry

## 1.3 Monitoring Requirements

- REQ-PERF-001: p95 API response time < 250ms
- REQ-PERF-002: LCP < 2.5 seconds
- REQ-REL-001: 99.9% uptime
- REQ-SCAL-001: Automatic horizontal scaling

## 1.4 System Architecture

ModularMonolith with EventDriven background workers

## 1.5 Environment

production

# 2.0 Log Level And Category Strategy

## 2.1 Default Log Level

INFO

## 2.2 Environment Specific Levels

### 2.2.1 Environment

#### 2.2.1.1 Environment

development

#### 2.2.1.2 Log Level

DEBUG

#### 2.2.1.3 Justification

Enables detailed diagnostics, including EF Core query logging, for developers during feature development and troubleshooting.

### 2.2.2.0 Environment

#### 2.2.2.1 Environment

production

#### 2.2.2.2 Log Level

INFO

#### 2.2.2.3 Justification

Captures significant application lifecycle events, requests, and business transactions without the performance overhead of verbose logging.

## 2.3.0.0 Component Categories

### 2.3.1.0 Component

#### 2.3.1.1 Component

BackendApi

#### 2.3.1.2 Category

🔹 Microsoft.AspNetCore

#### 2.3.1.3 Log Level

WARN

#### 2.3.1.4 Verbose Logging

❌ No

#### 2.3.1.5 Justification

Reduces noise from the web framework, focusing on warnings and errors. Request logging is handled separately.

### 2.3.2.0 Component

#### 2.3.2.1 Component

SowProcessingWorker

#### 2.3.2.2 Category

🔹 EMP.SowProcessingWorker

#### 2.3.2.3 Log Level

INFO

#### 2.3.2.4 Verbose Logging

✅ Yes

#### 2.3.2.5 Justification

Critical asynchronous process. Requires detailed logging of each processing stage (text extraction, LLM call, DB update). Verbose logging should be enabled on-demand or for failed items.

### 2.3.3.0 Component

#### 2.3.3.1 Component

Infrastructure.AI.LlmClient

#### 2.3.3.2 Category

🔹 EMP.Infrastructure.AI

#### 2.3.3.3 Log Level

DEBUG

#### 2.3.3.4 Verbose Logging

❌ No

#### 2.3.3.5 Justification

Logs sanitized requests and responses to the external LLM service to diagnose data extraction issues, but only at DEBUG level to avoid flooding production logs.

## 2.4.0.0 Sampling Strategies

*No items available*

## 2.5.0.0 Logging Approach

### 2.5.1.0 Structured

✅ Yes

### 2.5.2.0 Format

JSON

### 2.5.3.0 Standard Fields

- Timestamp
- LogLevel
- Message
- Exception
- TraceId
- SpanId
- CorrelationId
- ServiceName
- ServiceVersion
- Environment

### 2.5.4.0 Custom Fields

- UserId
- ProjectId
- SowDocumentId
- VendorId
- ClientId

# 3.0.0.0 Log Aggregation Architecture

## 3.1.0.0 Collection Mechanism

### 3.1.1.0 Type

🔹 library

### 3.1.2.0 Technology

Serilog with AWS.Logger.SeriLog Sink

### 3.1.3.0 Configuration

| Property | Value |
|----------|-------|
| Log Group | /emp/applications |
| Batch Push Interval | 5s |
| Batch Size Limit | 100 |

### 3.1.4.0 Justification

Direct integration into the .NET application. It is lightweight, efficient, and avoids the need for a separate sidecar or agent in the container, simplifying the deployment.

## 3.2.0.0 Strategy

| Property | Value |
|----------|-------|
| Approach | centralized |
| Reasoning | A centralized store (AWS CloudWatch Logs) is essen... |
| Local Retention | None |

## 3.3.0.0 Shipping Methods

- {'protocol': 'HTTP', 'destination': 'AWS CloudWatch Logs', 'reliability': 'at-least-once', 'compression': True}

## 3.4.0.0 Buffering And Batching

| Property | Value |
|----------|-------|
| Buffer Size | 10000 |
| Batch Size | 100 |
| Flush Interval | 5s |
| Backpressure Handling | Handled by the Serilog sink library. |

## 3.5.0.0 Transformation And Enrichment

- {'transformation': 'Enrichment', 'purpose': 'Add contextual properties like ServiceName, Environment, and CorrelationId to every log event.', 'stage': 'collection'}

## 3.6.0.0 High Availability

| Property | Value |
|----------|-------|
| Required | ✅ |
| Redundancy | Provided by AWS CloudWatch Logs service across mul... |
| Failover Strategy | Managed by AWS. |

# 4.0.0.0 Retention Policy Design

## 4.1.0.0 Retention Periods

### 4.1.1.0 Log Type

#### 4.1.1.1 Log Type

ApplicationLogs

#### 4.1.1.2 Retention Period

30 days

#### 4.1.1.3 Justification

Sufficient for typical operational troubleshooting and bug investigation cycles.

#### 4.1.1.4 Compliance Requirement

None

### 4.1.2.0 Log Type

#### 4.1.2.1 Log Type

SecurityAndAuditLogs

#### 4.1.2.2 Retention Period

365 days

#### 4.1.2.3 Justification

Long-term retention for security analysis and to provide an audit trail of significant events, stored in a cheaper storage tier if possible.

#### 4.1.2.4 Compliance Requirement

None

## 4.2.0.0 Compliance Requirements

*No items available*

## 4.3.0.0 Volume Impact Analysis

*Not specified*

## 4.4.0.0 Storage Tiering

### 4.4.1.0 Hot Storage

| Property | Value |
|----------|-------|
| Duration | 30 days |
| Accessibility | immediate |
| Cost | high |

### 4.4.2.0 Warm Storage

*Not specified*

### 4.4.3.0 Cold Storage

| Property | Value |
|----------|-------|
| Duration | 365 days |
| Accessibility | hours |
| Cost | low |

## 4.5.0.0 Compression Strategy

*Not specified*

## 4.6.0.0 Anonymization Requirements

*No items available*

# 5.0.0.0 Search Capability Requirements

## 5.1.0.0 Essential Capabilities

### 5.1.1.0 Capability

#### 5.1.1.1 Capability

Search by CorrelationId/TraceId

#### 5.1.1.2 Performance Requirement

< 5 seconds

#### 5.1.1.3 Justification

Essential for tracing a single request's lifecycle across the API and background workers.

### 5.1.2.0 Capability

#### 5.1.2.1 Capability

Full-text search on exception messages

#### 5.1.2.2 Performance Requirement

< 10 seconds

#### 5.1.2.3 Justification

Critical for debugging and identifying the root cause of errors.

### 5.1.3.0 Capability

#### 5.1.3.1 Capability

Filtering by custom business fields (e.g., ProjectId)

#### 5.1.3.2 Performance Requirement

< 10 seconds

#### 5.1.3.3 Justification

Allows support and operations teams to investigate issues related to a specific business entity.

## 5.2.0.0 Performance Characteristics

| Property | Value |
|----------|-------|
| Search Latency | seconds |
| Concurrent Users | 10 |
| Query Complexity | complex |
| Indexing Strategy | Native indexing provided by AWS CloudWatch Logs. |

## 5.3.0.0 Indexed Fields

### 5.3.1.0 Field

#### 5.3.1.1 Field

TraceId

#### 5.3.1.2 Index Type

Default

#### 5.3.1.3 Search Pattern

Exact Match

#### 5.3.1.4 Frequency

high

### 5.3.2.0 Field

#### 5.3.2.1 Field

CorrelationId

#### 5.3.2.2 Index Type

Default

#### 5.3.2.3 Search Pattern

Exact Match

#### 5.3.2.4 Frequency

high

### 5.3.3.0 Field

#### 5.3.3.1 Field

UserId

#### 5.3.3.2 Index Type

Default

#### 5.3.3.3 Search Pattern

Exact Match

#### 5.3.3.4 Frequency

medium

### 5.3.4.0 Field

#### 5.3.4.1 Field

ProjectId

#### 5.3.4.2 Index Type

Default

#### 5.3.4.3 Search Pattern

Exact Match

#### 5.3.4.4 Frequency

medium

## 5.4.0.0 Full Text Search

### 5.4.1.0 Required

✅ Yes

### 5.4.2.0 Fields

- Message
- Exception

### 5.4.3.0 Search Engine

AWS CloudWatch Logs Insights

### 5.4.4.0 Relevance Scoring

✅ Yes

## 5.5.0.0 Correlation And Tracing

### 5.5.1.0 Correlation Ids

- CorrelationId

### 5.5.2.0 Trace Id Propagation

W3C Trace Context (via OpenTelemetry)

### 5.5.3.0 Span Correlation

✅ Yes

### 5.5.4.0 Cross Service Tracing

✅ Yes

## 5.6.0.0 Dashboard Requirements

- {'dashboard': 'Application Health Dashboard', 'purpose': 'Provide an at-a-glance view of system health, including error rates per service, log volume, and API latency percentiles.', 'refreshInterval': '5 minutes', 'audience': 'Operations/SRE'}

# 6.0.0.0 Storage Solution Selection

## 6.1.0.0 Selected Technology

### 6.1.1.0 Primary

AWS CloudWatch Logs

### 6.1.2.0 Reasoning

It's a managed, scalable service that integrates natively with the AWS ecosystem and the chosen Serilog sink, meeting all essential requirements for aggregation and search.

### 6.1.3.0 Alternatives

- ELK Stack (Elasticsearch, Logstash, Kibana)
- Datadog
- Splunk

## 6.2.0.0 Scalability Requirements

| Property | Value |
|----------|-------|
| Expected Growth Rate | 20% MoM |
| Peak Load Handling | Handled automatically by the managed AWS service. |
| Horizontal Scaling | ✅ |

## 6.3.0.0 Cost Performance Analysis

*No items available*

## 6.4.0.0 Backup And Recovery

| Property | Value |
|----------|-------|
| Backup Frequency | On-demand |
| Recovery Time Objective | 24 hours |
| Recovery Point Objective | 1 hour |
| Testing Frequency | Annually |

## 6.5.0.0 Geo Distribution

### 6.5.1.0 Required

❌ No

### 6.5.2.0 Regions

*No items available*

### 6.5.3.0 Replication Strategy



## 6.6.0.0 Data Sovereignty

*No items available*

# 7.0.0.0 Access Control And Compliance

## 7.1.0.0 Access Control Requirements

### 7.1.1.0 Role

#### 7.1.1.1 Role

Developer

#### 7.1.1.2 Permissions

- read

#### 7.1.1.3 Log Types

- ApplicationLogs

#### 7.1.1.4 Justification

Developers need read-only access to logs in non-production environments for debugging purposes.

### 7.1.2.0 Role

#### 7.1.2.1 Role

Operator/SRE

#### 7.1.2.2 Permissions

- read
- write
- admin

#### 7.1.2.3 Log Types

- ApplicationLogs
- SecurityAndAuditLogs

#### 7.1.2.4 Justification

Operators need full access to manage log groups, retention policies, and perform deep analysis in all environments.

## 7.2.0.0 Sensitive Data Handling

- {'dataType': 'PII', 'handlingStrategy': 'mask', 'fields': ['email', 'firstName', 'lastName'], 'complianceRequirement': 'Best Practice'}

## 7.3.0.0 Encryption Requirements

### 7.3.1.0 In Transit

| Property | Value |
|----------|-------|
| Required | ✅ |
| Protocol | TLS 1.2+ |
| Certificate Management | Managed by AWS |

### 7.3.2.0 At Rest

| Property | Value |
|----------|-------|
| Required | ✅ |
| Algorithm | AES-256 |
| Key Management | AWS KMS |

## 7.4.0.0 Audit Trail

| Property | Value |
|----------|-------|
| Log Access | ✅ |
| Retention Period | 1 year |
| Audit Log Location | AWS CloudTrail |
| Compliance Reporting | ❌ |

## 7.5.0.0 Regulatory Compliance

*No items available*

## 7.6.0.0 Data Protection Measures

*No items available*

# 8.0.0.0 Project Specific Logging Config

## 8.1.0.0 Logging Config

### 8.1.1.0 Level

🔹 INFO

### 8.1.2.0 Retention

30 days

### 8.1.3.0 Aggregation

Centralized to AWS CloudWatch

### 8.1.4.0 Storage

AWS CloudWatch Logs

### 8.1.5.0 Configuration

*No data available*

## 8.2.0.0 Component Configurations

### 8.2.1.0 Component

#### 8.2.1.1 Component

BackendApi

#### 8.2.1.2 Log Level

INFO

#### 8.2.1.3 Output Format

JSON

#### 8.2.1.4 Destinations

- AWS CloudWatch Logs

#### 8.2.1.5 Sampling

##### 8.2.1.5.1 Enabled

❌ No

##### 8.2.1.5.2 Rate



#### 8.2.1.6.0 Custom Fields

- RequestPath
- RequestMethod
- StatusCode

### 8.2.2.0.0 Component

#### 8.2.2.1.0 Component

SowProcessingWorker

#### 8.2.2.2.0 Log Level

INFO

#### 8.2.2.3.0 Output Format

JSON

#### 8.2.2.4.0 Destinations

- AWS CloudWatch Logs

#### 8.2.2.5.0 Sampling

##### 8.2.2.5.1 Enabled

❌ No

##### 8.2.2.5.2 Rate



#### 8.2.2.6.0 Custom Fields

- SowDocumentId
- ProcessingStage
- ProcessingDuration

## 8.3.0.0.0 Metrics

### 8.3.1.0.0 Custom Metrics

*No data available*

## 8.4.0.0.0 Alert Rules

### 8.4.1.0.0 High API Server-Side Error Rate

#### 8.4.1.1.0 Name

High API Server-Side Error Rate

#### 8.4.1.2.0 Condition

Count of logs with LogLevel='ERROR' and StatusCode >= 500 from BackendApi > 5 per minute

#### 8.4.1.3.0 Severity

Critical

#### 8.4.1.4.0 Actions

- {'type': 'webhook', 'target': 'OnCallPager', 'configuration': {}}

#### 8.4.1.5.0 Suppression Rules

*No items available*

#### 8.4.1.6.0 Escalation Path

*No items available*

### 8.4.2.0.0 SOW Processing Failure

#### 8.4.2.1.0 Name

SOW Processing Failure

#### 8.4.2.2.0 Condition

Count of logs with LogLevel='ERROR' from SowProcessingWorker > 0

#### 8.4.2.3.0 Severity

High

#### 8.4.2.4.0 Actions

- {'type': 'email', 'target': 'dev-team@example.com', 'configuration': {}}

#### 8.4.2.5.0 Suppression Rules

*No items available*

#### 8.4.2.6.0 Escalation Path

*No items available*

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

Core Logging Setup (Serilog, Sink, Basic Enrichment)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Small

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Contextual Enrichment (TraceId, Business IDs)

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- Core Logging Setup

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

low

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Dashboard and Alerting Configuration

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- Core Logging Setup

### 9.3.4.0.0 Estimated Effort

Medium

### 9.3.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Excessive Log Volume and Cost

### 10.1.2.0.0 Impact

medium

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Set default log level to INFO. Use DEBUG level logging sparingly. Implement log retention policies to expire old logs. Monitor log ingestion volume.

### 10.1.5.0.0 Contingency Plan

Dynamically adjust log levels in production without a redeploy. Identify and silence noisy, low-value log sources.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

PII or Sensitive Data Leakage in Logs

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Implement log masking for known PII fields. Conduct regular code reviews with a focus on logging practices. Never log raw request/response bodies.

### 10.2.5.0.0 Contingency Plan

Procedure to purge sensitive data from log stores if a leak is discovered. Update masking rules.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Standardization

### 11.1.2.0.0 Recommendation

Enforce a single, consistent structured logging pattern across all backend services. All logging must be done via a centralized, injected ILogger interface.

### 11.1.3.0.0 Justification

Ensures that all logs are parseable, searchable, and contain consistent contextual data, which is critical for effective troubleshooting.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Create a shared library with Serilog configuration and common enrichers to be used by all .NET projects.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Observability

### 11.2.2.0.0 Recommendation

Ensure that the TraceId from OpenTelemetry is injected into the Serilog LogContext for every request. This is the most critical piece for log correlation.

### 11.2.3.0.0 Justification

Without a shared TraceId, it is impossible to correlate logs from a single request as it flows through the API, message queues, and background workers.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

Use middleware in ASP.NET Core and a message handler decorator in the worker to establish the LogContext from the incoming request or message headers.


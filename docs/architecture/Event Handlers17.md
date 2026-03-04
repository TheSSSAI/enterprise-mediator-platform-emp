# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Architecture Type

Modular Monolith with an Event-Driven pattern for asynchronous processing

## 1.3 Technology Stack

- .NET 8
- ASP.NET Core 8
- Message Broker (e.g., RabbitMQ, AWS SQS)

## 1.4 Bounded Contexts

- AI SOW Processing
- Project Management

# 2.0 Project Specific Events

- {'eventId': 'a4b1c2d3-e4f5-4a6b-7c8d-9e0f1a2b3c4d', 'eventName': 'SowUploaded', 'eventType': 'integration', 'category': 'AI SOW Processing', 'description': 'Published when a new Statement of Work document is successfully uploaded and is ready for asynchronous AI data extraction.', 'triggerCondition': 'A user successfully uploads an SOW file via the Application.API, and a corresponding SowDocument record is created in the database.', 'sourceContext': 'Application.API', 'targetContexts': ['SowProcessingWorker'], 'payload': {'schema': {'sowDocumentId': 'Guid', 'projectId': 'Guid', 'uploadedByUserId': 'Guid'}, 'requiredFields': ['sowDocumentId', 'projectId'], 'optionalFields': ['uploadedByUserId']}, 'frequency': 'medium', 'businessCriticality': 'critical', 'dataSource': {'database': 'PostgreSQL', 'table': 'SowDocument', 'operation': 'create'}, 'routing': {'routingKey': 'sow.uploaded.v1', 'exchange': 'sow_processing_exchange', 'queue': 'sow_processing_queue'}, 'consumers': [{'service': 'SowProcessingWorker', 'handler': 'SowUploadedEventHandler', 'processingType': 'async'}], 'dependencies': [], 'errorHandling': {'retryStrategy': 'Exponential Backoff with Jitter', 'deadLetterQueue': 'sow_processing_dlq', 'timeoutMs': 600000}}

# 3.0 Event Types And Schema Design

## 3.1 Essential Event Types

- {'eventName': 'SowUploaded', 'category': 'integration', 'description': 'Initiates the asynchronous SOW analysis and data extraction workflow.', 'priority': 'high'}

## 3.2 Schema Design

| Property | Value |
|----------|-------|
| Format | JSON |
| Reasoning | JSON is lightweight, human-readable, and natively ... |
| Consistency Approach | A shared .NET class library containing the event c... |

## 3.3 Schema Evolution

| Property | Value |
|----------|-------|
| Backward Compatibility | ✅ |
| Forward Compatibility | ❌ |
| Strategy | The schema will follow the Additive Only principle... |

## 3.4 Event Structure

### 3.4.1 Standard Fields

- EventId
- EventName
- OccurredOnUtc
- Version

### 3.4.2 Metadata Requirements

- CorrelationId to trace the request from the initial API call through the worker process.

# 4.0.0 Event Routing And Processing

## 4.1.0 Routing Mechanisms

- {'type': 'Direct Exchange with Queue Binding', 'description': 'The producer publishes to a named exchange, and the message broker routes the message directly to the queue bound with a matching routing key. This is the simplest effective pattern for a single consumer type.', 'useCase': 'Routing all SowUploaded events to the single SowProcessingWorker queue.'}

## 4.2.0 Processing Patterns

- {'pattern': 'sequential', 'applicableScenarios': ['AI SOW Processing'], 'implementation': 'The message consumer (SowProcessingWorker) will be configured with a concurrency limit of 1. This prevents a single worker instance from overwhelming the downstream LLM API or its own resources by processing multiple large documents simultaneously.'}

## 4.3.0 Filtering And Subscription

### 4.3.1 Filtering Mechanism

None (Direct Binding)

### 4.3.2 Subscription Model

Competing Consumer Pattern

### 4.3.3 Routing Keys

- sow.uploaded.v1

## 4.4.0 Handler Isolation

| Property | Value |
|----------|-------|
| Required | ✅ |
| Approach | The SowProcessingWorker is a separate, containeriz... |
| Reasoning | Isolates the resource-intensive and potentially lo... |

## 4.5.0 Delivery Guarantees

| Property | Value |
|----------|-------|
| Level | at-least-once |
| Justification | This is crucial to ensure that no uploaded SOW is ... |
| Implementation | The consumer will explicitly acknowledge a message... |

# 5.0.0 Event Storage And Replay

## 5.1.0 Persistence Requirements

| Property | Value |
|----------|-------|
| Required | ❌ |
| Duration | N/A |
| Reasoning | The system of record is the primary PostgreSQL dat... |

## 5.2.0 Event Sourcing

### 5.2.1 Necessary

❌ No

### 5.2.2 Justification

Event sourcing introduces significant complexity that is not warranted by the current requirements. A traditional state-oriented persistence model is sufficient and simpler to manage.

### 5.2.3 Scope

*No items available*

## 5.3.0 Technology Options

*No items available*

## 5.4.0 Replay Capabilities

### 5.4.1 Required

❌ No

### 5.4.2 Scenarios

- N/A

### 5.4.3 Implementation

Error handling is managed via retries and a Dead Letter Queue. Manual re-queuing of messages from the DLQ is the designated recovery path, which is sufficient for this system's needs.

## 5.5.0 Retention Policy

| Property | Value |
|----------|-------|
| Strategy | Remove on Acknowledge |
| Duration | N/A |
| Archiving Approach | No event archiving is required. The results of the... |

# 6.0.0 Dead Letter Queue And Error Handling

## 6.1.0 Dead Letter Strategy

| Property | Value |
|----------|-------|
| Approach | Utilize a dedicated Dead Letter Queue (DLQ) for th... |
| Queue Configuration | Any message that fails processing after all retrie... |
| Processing Logic | Messages in the DLQ will not be automatically proc... |

## 6.2.0 Retry Policies

- {'errorType': 'Transient Errors (e.g., LLM API unavailability, network issues, temporary database locks)', 'maxRetries': 3, 'backoffStrategy': 'exponential', 'delayConfiguration': 'Initial delay of 5 seconds, doubling with each retry.'}

## 6.3.0 Poison Message Handling

| Property | Value |
|----------|-------|
| Detection Mechanism | A message is considered poison after it fails all ... |
| Handling Strategy | An alert is triggered when a message enters the DL... |
| Alerting Required | ✅ |

## 6.4.0 Error Notification

### 6.4.1 Channels

- Email
- Monitoring System Alert (e.g., CloudWatch, Prometheus)

### 6.4.2 Severity

critical

### 6.4.3 Recipients

- System Administrators

## 6.5.0 Recovery Procedures

- {'scenario': 'A bug in the SowProcessingWorker causes all messages to fail and land in the DLQ.', 'procedure': '1. Pause the consumer service to prevent further failures. 2. Investigate logs to identify the bug. 3. Deploy a patched version of the worker service. 4. Manually move messages from the DLQ back to the main processing queue in batches. 5. Resume the consumer service and monitor its performance.', 'automationLevel': 'manual'}

# 7.0.0 Event Versioning Strategy

## 7.1.0 Schema Evolution Approach

| Property | Value |
|----------|-------|
| Strategy | Additive changes only. New versions are created fo... |
| Versioning Scheme | Semantic versioning embedded in the routing key or... |
| Migration Strategy | No in-flight migration is required. The consumer m... |

## 7.2.0 Compatibility Requirements

| Property | Value |
|----------|-------|
| Backward Compatible | ✅ |
| Forward Compatible | ❌ |
| Reasoning | The consumer should be tolerant of new, unexpected... |

## 7.3.0 Version Identification

| Property | Value |
|----------|-------|
| Mechanism | Routing Key |
| Location | N/A |
| Format | <domain>.<event_name>.v<major_version> |

## 7.4.0 Consumer Upgrade Strategy

| Property | Value |
|----------|-------|
| Approach | Blue/Green or Canary deployment. |
| Rollout Strategy | Deploy new consumer instances that can handle both... |
| Rollback Procedure | Revert the producer to emit the previous event ver... |

## 7.5.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | A shared .NET library for event contracts provides... |

# 8.0.0 Event Monitoring And Observability

## 8.1.0 Monitoring Capabilities

### 8.1.1 Capability

#### 8.1.1.1 Capability

Queue Depth Monitoring

#### 8.1.1.2 Justification

To identify processing backlogs, which could indicate a slow or failed consumer.

#### 8.1.1.3 Implementation

CloudWatch Alarms for AWS SQS or Prometheus metrics for RabbitMQ.

### 8.1.2.0 Capability

#### 8.1.2.1 Capability

DLQ Message Count

#### 8.1.2.2 Justification

To immediately detect permanent processing failures. Any message in the DLQ requires urgent attention.

#### 8.1.2.3 Implementation

CloudWatch Alarm or Prometheus alert when message count is greater than 0.

## 8.2.0.0 Tracing And Correlation

| Property | Value |
|----------|-------|
| Tracing Required | ✅ |
| Correlation Strategy | A unique Correlation ID is generated by the Applic... |
| Trace Id Propagation | This Correlation ID is passed as a message header ... |

## 8.3.0.0 Performance Metrics

### 8.3.1.0 Metric

#### 8.3.1.1 Metric

Event Processing Time (from publish to acknowledge)

#### 8.3.1.2 Threshold

p95 > 15 minutes

#### 8.3.1.3 Alerting

✅ Yes

### 8.3.2.0 Metric

#### 8.3.2.1 Metric

Number of Messages in DLQ

#### 8.3.2.2 Threshold

> 0

#### 8.3.2.3 Alerting

✅ Yes

## 8.4.0.0 Event Flow Visualization

| Property | Value |
|----------|-------|
| Required | ❌ |
| Tooling | N/A |
| Scope | The event flow is a simple, single-step producer-c... |

## 8.5.0.0 Alerting Requirements

### 8.5.1.0 Condition

#### 8.5.1.1 Condition

Message count in DLQ > 0

#### 8.5.1.2 Severity

critical

#### 8.5.1.3 Response Time

Acknowledge within 1 hour

#### 8.5.1.4 Escalation Path

- On-call System Administrator

### 8.5.2.0 Condition

#### 8.5.2.1 Condition

Queue depth > 100 for more than 10 minutes

#### 8.5.2.2 Severity

warning

#### 8.5.2.3 Response Time

Acknowledge within 4 hours

#### 8.5.2.4 Escalation Path

- System Administrator Team

# 9.0.0.0 Implementation Priority

## 9.1.0.0 Component

### 9.1.1.0 Component

Core Event Pipeline (Producer, Consumer, Broker Config) for SowUploaded

### 9.1.2.0 Priority

🔴 high

### 9.1.3.0 Dependencies

*No items available*

### 9.1.4.0 Estimated Effort

Small

## 9.2.0.0 Component

### 9.2.1.0 Component

DLQ and Retry Configuration

### 9.2.2.0 Priority

🔴 high

### 9.2.3.0 Dependencies

- Core Event Pipeline

### 9.2.4.0 Estimated Effort

Small

## 9.3.0.0 Component

### 9.3.1.0 Component

Monitoring and Alerting on Queue Metrics

### 9.3.2.0 Priority

🟡 medium

### 9.3.3.0 Dependencies

- Core Event Pipeline

### 9.3.4.0 Estimated Effort

Small

# 10.0.0.0 Risk Assessment

## 10.1.0.0 Risk

### 10.1.1.0 Risk

Poison Message Stalls Processing

### 10.1.2.0 Impact

high

### 10.1.3.0 Probability

low

### 10.1.4.0 Mitigation

A robust DLQ strategy with a limited number of retries ensures that a single bad message does not halt the processing of all subsequent messages.

## 10.2.0.0 Risk

### 10.2.1.0 Risk

Worker Service Failure

### 10.2.2.0 Impact

medium

### 10.2.3.0 Probability

low

### 10.2.4.0 Mitigation

The message broker will persist the messages until the worker service is restored. Container health checks and automated restarts will minimize downtime.

## 10.3.0.0 Risk

### 10.3.1.0 Risk

Schema Incompatibility

### 10.3.2.0 Impact

medium

### 10.3.3.0 Probability

low

### 10.3.4.0 Mitigation

Using a shared library for event contracts and enforcing a backward-compatibility-only versioning strategy minimizes this risk.

# 11.0.0.0 Recommendations

## 11.1.0.0 Category

### 11.1.1.0 Category

🔹 Idempotency

### 11.1.2.0 Recommendation

The SowUploaded event handler must be designed to be idempotent. Before starting a long-running process, it should first check the current status of the SowDocument in the database. If the status is already 'PROCESSING' or 'COMPLETED', it should acknowledge the message and exit gracefully without reprocessing.

### 11.1.3.0 Justification

At-least-once delivery guarantees that a message will not be lost, but it may be delivered more than once. Idempotency is essential to prevent duplicate processing, which could be costly and lead to inconsistent data.

### 11.1.4.0 Priority

🔴 high

## 11.2.0.0 Category

### 11.2.1.0 Category

🔹 Configuration Management

### 11.2.2.0 Recommendation

All message broker connection strings, exchange names, and queue names must be stored in external configuration (e.g., appsettings.json, environment variables) and not hard-coded in the application.

### 11.2.3.0 Justification

This follows best practices for maintainability and allows the application to be promoted through different environments (dev, staging, prod) without code changes.

### 11.2.4.0 Priority

🔴 high


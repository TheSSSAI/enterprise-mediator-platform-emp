# 1 Strategies

## 1.1 Retry

### 1.1.1 Type

🔹 Retry

### 1.1.2 Configuration

#### 1.1.2.1 Policy Name

ExternalApiTransientRetry

#### 1.1.2.2 Description

Applies to outgoing HTTP calls to external services like AWS SES and Azure.AI.OpenAI for transient, network-related failures.

#### 1.1.2.3 Retry Attempts

3

#### 1.1.2.4 Backoff Strategy

Exponential

#### 1.1.2.5 Retry Intervals

| Property | Value |
|----------|-------|
| Initial Delay | 1s |
| Factor | 2 |
| Jitter | 250ms |

#### 1.1.2.6 Error Handling Rules

- System.Net.Http.HttpRequestException[StatusCode=5xx]
- System.Net.Http.HttpRequestException[StatusCode=408]
- System.Net.Http.HttpRequestException[StatusCode=429]
- System.Threading.Tasks.TaskCanceledException

## 1.2.0.0 Retry

### 1.2.1.0 Type

🔹 Retry

### 1.2.2.0 Configuration

#### 1.2.2.1 Policy Name

DatabaseTransientRetry

#### 1.2.2.2 Description

Managed by Entity Framework Core's Execution Strategy to handle transient database connection or command issues.

#### 1.2.2.3 Retry Attempts

3

#### 1.2.2.4 Backoff Strategy

Exponential

#### 1.2.2.5 Max Delay

30s

#### 1.2.2.6 Error Handling Rules

- Npgsql.NpgsqlException[IsTransient=true]
- Microsoft.EntityFrameworkCore.DbUpdateException[IsConcurrencyConflict=true]

## 1.3.0.0 CircuitBreaker

### 1.3.1.0 Type

🔹 CircuitBreaker

### 1.3.2.0 Configuration

#### 1.3.2.1 Policy Name

OpenAiCircuitBreaker

#### 1.3.2.2 Description

Protects the system from a consistently failing or unresponsive Azure.AI.OpenAI service, preventing resource exhaustion during outages.

#### 1.3.2.3 Failure Threshold

5

#### 1.3.2.4 Break Duration

60s

#### 1.3.2.5 Error Handling Rules

- System.Net.Http.HttpRequestException[StatusCode=5xx]
- System.Threading.Tasks.TaskCanceledException

## 1.4.0.0 DeadLetter

### 1.4.1.0 Type

🔹 DeadLetter

### 1.4.2.0 Configuration

#### 1.4.2.1 Process Name

SowProcessingWorker

#### 1.4.2.2 Description

Ensures that SOW processing messages that fail repeatedly or due to a permanent error are moved to a Dead-Letter Queue (DLQ) for manual inspection, preventing infinite retry loops. Transient errors within the worker will leverage the 'ExternalApiTransientRetry' and 'DatabaseTransientRetry' policies for a limited number of attempts before considering the message 'poison'.

#### 1.4.2.3 Dead Letter Queue

sow_processing_dlq

#### 1.4.2.4 Error Handling Rules

- PermanentError:InvalidSowFileFormat
- PermanentError:UnrecoverableDocumentParsingError
- RetryExhaustion:AllTransientErrors

# 2.0.0.0 Monitoring

## 2.1.0.0 Error Types

- System.Net.Http.HttpRequestException
- Npgsql.NpgsqlException
- Microsoft.EntityFrameworkCore.DbUpdateException
- MessageProcessingPermanentException
- CircuitBreakerOpenException

## 2.2.0.0 Alerting

All exceptions are logged with a unique Correlation ID. Critical alerts are triggered via Email and Webhook for two primary events: 1) A message is published to the 'sow_processing_dlq'. 2) The 'OpenAiCircuitBreaker' transitions to the 'Open' state. Dashboards will monitor DLQ size and circuit breaker state changes.


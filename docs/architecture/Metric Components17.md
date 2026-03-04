# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- Next.js 14
- PostgreSQL 16
- pgvector
- RabbitMQ
- Redis
- Docker
- Kubernetes

## 1.3 Monitoring Components

- OpenTelemetry for .NET
- AWS CloudWatch RUM
- Centralized Log Aggregation (Serilog)
- AWS CloudWatch Metrics & RDS Performance Insights
- ASP.NET Core Health Checks

## 1.4 Requirements

- REQ-PERF-001
- REQ-PERF-002
- REQ-REL-001
- REQ-SCAL-001
- REQ-FUNC-010
- REQ-FUNC-014
- REQ-FUNC-024
- REQ-FUNC-026
- REQ-INTG-005

## 1.5 Environment

production

# 2.0 Standard System Metrics Selection

## 2.1 Hardware Utilization Metrics

### 2.1.1 container.cpu.utilization

#### 2.1.1.1 Name

container.cpu.utilization

#### 2.1.1.2 Type

🔹 gauge

#### 2.1.1.3 Unit

percent

#### 2.1.1.4 Description

CPU utilization per container instance (API, Worker, etc.).

#### 2.1.1.5 Collection

##### 2.1.1.5.1 Interval

60s

##### 2.1.1.5.2 Method

Kubernetes Metrics Server

#### 2.1.1.6.0 Thresholds

##### 2.1.1.6.1 Warning

> 75%

##### 2.1.1.6.2 Critical

> 90%

#### 2.1.1.7.0 Justification

Directly required to validate the CPU-based horizontal pod autoscaling trigger defined in REQ-SCAL-001.

### 2.1.2.0.0 container.memory.utilization

#### 2.1.2.1.0 Name

container.memory.utilization

#### 2.1.2.2.0 Type

🔹 gauge

#### 2.1.2.3.0 Unit

percent

#### 2.1.2.4.0 Description

Memory utilization per container instance.

#### 2.1.2.5.0 Collection

##### 2.1.2.5.1 Interval

60s

##### 2.1.2.5.2 Method

Kubernetes Metrics Server

#### 2.1.2.6.0 Thresholds

##### 2.1.2.6.1 Warning

> 80%

##### 2.1.2.6.2 Critical

> 95%

#### 2.1.2.7.0 Justification

Directly required to validate the memory-based horizontal pod autoscaling trigger defined in REQ-SCAL-001.

### 2.1.3.0.0 db.cpu.utilization

#### 2.1.3.1.0 Name

db.cpu.utilization

#### 2.1.3.2.0 Type

🔹 gauge

#### 2.1.3.3.0 Unit

percent

#### 2.1.3.4.0 Description

CPU utilization of the managed PostgreSQL instance.

#### 2.1.3.5.0 Collection

##### 2.1.3.5.1 Interval

60s

##### 2.1.3.5.2 Method

AWS CloudWatch (RDS)

#### 2.1.3.6.0 Thresholds

##### 2.1.3.6.1 Warning

> 70%

##### 2.1.3.6.2 Critical

> 85%

#### 2.1.3.7.0 Justification

Essential for database health monitoring, which underpins the entire system's reliability (REQ-REL-001) and performance (REQ-PERF-001).

## 2.2.0.0.0 Runtime Metrics

### 2.2.1.0.0 dotnet.gc.heap.size

#### 2.2.1.1.0 Name

dotnet.gc.heap.size

#### 2.2.1.2.0 Type

🔹 gauge

#### 2.2.1.3.0 Unit

bytes

#### 2.2.1.4.0 Description

Total managed heap size in .NET backend services.

#### 2.2.1.5.0 Technology

.NET

#### 2.2.1.6.0 Collection

##### 2.2.1.6.1 Interval

30s

##### 2.2.1.6.2 Method

OpenTelemetry .NET Runtime Instrumentation

#### 2.2.1.7.0 Criticality

medium

### 2.2.2.0.0 db.connections.active

#### 2.2.2.1.0 Name

db.connections.active

#### 2.2.2.2.0 Type

🔹 gauge

#### 2.2.2.3.0 Unit

count

#### 2.2.2.4.0 Description

Number of active connections to the PostgreSQL database.

#### 2.2.2.5.0 Technology

.NET

#### 2.2.2.6.0 Collection

##### 2.2.2.6.1 Interval

30s

##### 2.2.2.6.2 Method

AWS CloudWatch (RDS) or Npgsql Driver Metrics

#### 2.2.2.7.0 Criticality

high

## 2.3.0.0.0 Request Response Metrics

- {'name': 'http.server.request.duration', 'type': 'histogram', 'unit': 'milliseconds', 'description': 'Latency of all incoming HTTP requests to the ASP.NET Core API.', 'dimensions': ['http.method', 'http.route', 'http.status_code'], 'percentiles': ['p95', 'p99'], 'collection': {'interval': 'on_request', 'method': 'OpenTelemetry ASP.NET Core Instrumentation'}}

## 2.4.0.0.0 Availability Metrics

- {'name': 'service.uptime.percentage', 'type': 'gauge', 'unit': 'percent', 'description': 'Percentage of successful health checks over a rolling window.', 'calculation': '(successful_probes / total_probes) * 100', 'slaTarget': '99.9'}

## 2.5.0.0.0 Scalability Metrics

- {'name': 'kubernetes.deployment.replicas.available', 'type': 'gauge', 'unit': 'count', 'description': 'Number of available (running) pods for each deployment (API, Worker).', 'capacityThreshold': 'Minimum 2 for high availability', 'autoScalingTrigger': True}

# 3.0.0.0.0 Application Specific Metrics Design

## 3.1.0.0.0 Transaction Metrics

### 3.1.1.0.0 sow.processing.duration

#### 3.1.1.1.0 Name

sow.processing.duration

#### 3.1.1.2.0 Type

🔹 histogram

#### 3.1.1.3.0 Unit

seconds

#### 3.1.1.4.0 Description

End-to-end time from message consumption to final DB update for SOW processing.

#### 3.1.1.5.0 Business Context

AI SOW Processing

#### 3.1.1.6.0 Dimensions

- result

#### 3.1.1.7.0 Collection

##### 3.1.1.7.1 Interval

on_completion

##### 3.1.1.7.2 Method

Custom OpenTelemetry Activity

#### 3.1.1.8.0 Aggregation

##### 3.1.1.8.1 Functions

- p95
- avg
- count

##### 3.1.1.8.2 Window

5m

### 3.1.2.0.0 vendor.matching.search.duration

#### 3.1.2.1.0 Name

vendor.matching.search.duration

#### 3.1.2.2.0 Type

🔹 histogram

#### 3.1.2.3.0 Unit

milliseconds

#### 3.1.2.4.0 Description

Time taken to perform the semantic vector search for vendor matching.

#### 3.1.2.5.0 Business Context

AI SOW Processing

#### 3.1.2.6.0 Dimensions

- result_count

#### 3.1.2.7.0 Collection

##### 3.1.2.7.1 Interval

on_completion

##### 3.1.2.7.2 Method

Custom OpenTelemetry Activity

#### 3.1.2.8.0 Aggregation

##### 3.1.2.8.1 Functions

- p95
- avg

##### 3.1.2.8.2 Window

1m

### 3.1.3.0.0 sow.processing.jobs

#### 3.1.3.1.0 Name

sow.processing.jobs

#### 3.1.3.2.0 Type

🔹 counter

#### 3.1.3.3.0 Unit

count

#### 3.1.3.4.0 Description

Count of SOW processing jobs started, completed, or failed.

#### 3.1.3.5.0 Business Context

AI SOW Processing

#### 3.1.3.6.0 Dimensions

- status

#### 3.1.3.7.0 Collection

##### 3.1.3.7.1 Interval

on_event

##### 3.1.3.7.2 Method

Custom Metric API call

#### 3.1.3.8.0 Aggregation

##### 3.1.3.8.1 Functions

- sum

##### 3.1.3.8.2 Window

1m

## 3.2.0.0.0 Cache Performance Metrics

- {'name': 'cache.hit_ratio', 'type': 'gauge', 'unit': 'ratio', 'description': 'Ratio of cache hits to total cache lookups (hits + misses).', 'cacheType': 'Redis', 'hitRatioTarget': '> 0.85'}

## 3.3.0.0.0 External Dependency Metrics

- {'name': 'http.client.request.duration', 'type': 'histogram', 'unit': 'milliseconds', 'description': 'Latency of outbound HTTP calls to external services.', 'dependency': 'AWS SES, Azure OpenAI', 'circuitBreakerIntegration': True, 'sla': {'responseTime': '< 1000ms', 'availability': '99.9%'}}

## 3.4.0.0.0 Error Metrics

- {'name': 'http.server.requests.errors', 'type': 'counter', 'unit': 'count', 'description': 'Total count of server-side errors (HTTP 5xx).', 'errorTypes': ['500', '502', '503'], 'dimensions': ['http.route', 'exception.type'], 'alertThreshold': '> 5 errors in 5 minutes'}

## 3.5.0.0.0 Throughput And Latency Metrics

### 3.5.1.0.0 api.request.latency

#### 3.5.1.1.0 Name

api.request.latency

#### 3.5.1.2.0 Type

🔹 summary

#### 3.5.1.3.0 Unit

milliseconds

#### 3.5.1.4.0 Description

Tracks API request latency to verify against NFRs.

#### 3.5.1.5.0 Percentiles

- 0.95

#### 3.5.1.6.0 Buckets

*No items available*

#### 3.5.1.7.0 Sla Targets

##### 3.5.1.7.1 P95

< 250ms

##### 3.5.1.7.2 P99

< 1000ms

### 3.5.2.0.0 frontend.page.lcp

#### 3.5.2.1.0 Name

frontend.page.lcp

#### 3.5.2.2.0 Type

🔹 histogram

#### 3.5.2.3.0 Unit

seconds

#### 3.5.2.4.0 Description

Largest Contentful Paint for key application pages.

#### 3.5.2.5.0 Percentiles

- 0.75

#### 3.5.2.6.0 Buckets

- 0
- 2.5
- 4.0

#### 3.5.2.7.0 Sla Targets

##### 3.5.2.7.1 P75

< 2.5s

##### 3.5.2.7.2 P95



# 4.0.0.0.0 Business Kpi Identification

## 4.1.0.0.0 Critical Business Metrics

### 4.1.1.0.0 projects.count.by_status

#### 4.1.1.1.0 Name

projects.count.by_status

#### 4.1.1.2.0 Type

🔹 gauge

#### 4.1.1.3.0 Unit

count

#### 4.1.1.4.0 Description

Total number of projects, segmented by their current status.

#### 4.1.1.5.0 Business Owner

Operations

#### 4.1.1.6.0 Calculation

```sql
SELECT status, COUNT(*) FROM Project GROUP BY status
```

#### 4.1.1.7.0 Reporting Frequency

15m

#### 4.1.1.8.0 Target

N/A

### 4.1.2.0.0 sows.pending_review.count

#### 4.1.2.1.0 Name

sows.pending_review.count

#### 4.1.2.2.0 Type

🔹 gauge

#### 4.1.2.3.0 Unit

count

#### 4.1.2.4.0 Description

Number of SOWs that have been processed by AI and are awaiting human review.

#### 4.1.2.5.0 Business Owner

System Administrator

#### 4.1.2.6.0 Calculation

```sql
SELECT COUNT(*) FROM ProjectBrief WHERE status = 'PENDING_REVIEW'
```

#### 4.1.2.7.0 Reporting Frequency

15m

#### 4.1.2.8.0 Target

< 5

### 4.1.3.0.0 proposals.pending_decision.count

#### 4.1.3.1.0 Name

proposals.pending_decision.count

#### 4.1.3.2.0 Type

🔹 gauge

#### 4.1.3.3.0 Unit

count

#### 4.1.3.4.0 Description

Number of projects with submitted proposals that are awaiting a decision.

#### 4.1.3.5.0 Business Owner

System Administrator

#### 4.1.3.6.0 Calculation

```sql
SELECT COUNT(DISTINCT projectId) FROM Proposal WHERE status = 'SUBMITTED'
```

#### 4.1.3.7.0 Reporting Frequency

15m

#### 4.1.3.8.0 Target

< 10

## 4.2.0.0.0 User Engagement Metrics

*No items available*

## 4.3.0.0.0 Conversion Metrics

*No items available*

## 4.4.0.0.0 Operational Efficiency Kpis

- {'name': 'sow_processing.automation_rate', 'type': 'gauge', 'unit': 'ratio', 'description': 'Percentage of SOWs processed successfully by the worker without failure.', 'calculation': '(successful_jobs / total_jobs_started) * 100', 'benchmarkTarget': '> 0.98'}

## 4.5.0.0.0 Revenue And Cost Metrics

- {'name': 'project.profitability.net', 'type': 'gauge', 'unit': 'currency', 'description': 'Net profit (Total Invoiced - Total Paid Out) for completed projects.', 'frequency': 'daily', 'accuracy': '100%'}

## 4.6.0.0.0 Customer Satisfaction Indicators

*No items available*

# 5.0.0.0.0 Collection Interval Optimization

## 5.1.0.0.0 Sampling Frequencies

### 5.1.1.0.0 Metric Category

#### 5.1.1.1.0 Metric Category

Request/Response Latency

#### 5.1.1.2.0 Interval

on_request

#### 5.1.1.3.0 Justification

Required for accurate percentile calculations to meet REQ-PERF-001.

#### 5.1.1.4.0 Resource Impact

medium

### 5.1.2.0.0 Metric Category

#### 5.1.2.1.0 Metric Category

Hardware Utilization

#### 5.1.2.2.0 Interval

60s

#### 5.1.2.3.0 Justification

Provides sufficient granularity for autoscaling (REQ-SCAL-001) without excessive data volume.

#### 5.1.2.4.0 Resource Impact

low

### 5.1.3.0.0 Metric Category

#### 5.1.3.1.0 Metric Category

Business KPIs

#### 5.1.3.2.0 Interval

15m

#### 5.1.3.3.0 Justification

Matches the refresh interval of the DashboardMetricsCalculator job (REQ-FUNC-024).

#### 5.1.3.4.0 Resource Impact

low

## 5.2.0.0.0 High Frequency Metrics

- {'name': 'http.server.request.duration', 'interval': 'on_request', 'criticality': 'high', 'costJustification': 'Directly measures the primary performance NFR of the system (REQ-PERF-001).'}

## 5.3.0.0.0 Cardinality Considerations

- {'metricName': 'http.server.request.duration', 'estimatedCardinality': 'low-medium', 'dimensionStrategy': 'Use templated routes (e.g., /clients/{id}) instead of raw URLs to limit cardinality.', 'mitigationApproach': 'Avoid high-cardinality dimensions like user_id or specific entity_ids in metric tags.'}

## 5.4.0.0.0 Aggregation Periods

- {'metricType': 'Performance (Latency)', 'periods': ['1m', '5m', '1h'], 'retentionStrategy': 'Retain raw/high-resolution data for 1-7 days, downsample for longer-term storage.'}

## 5.5.0.0.0 Collection Methods

- {'method': 'real-time', 'applicableMetrics': ['http.server.request.duration', 'frontend.page.lcp'], 'implementation': 'OpenTelemetry Instrumentation / RUM Agent', 'performance': 'Low overhead'}

# 6.0.0.0.0 Aggregation Method Selection

## 6.1.0.0.0 Statistical Aggregations

- {'metricName': 'sow.processing.jobs', 'aggregationFunctions': ['sum', 'rate'], 'windows': ['1m', '5m'], 'justification': 'Sum provides total volume, rate shows processing throughput.'}

## 6.2.0.0.0 Histogram Requirements

- {'metricName': 'http.server.request.duration', 'buckets': ['50', '100', '200', '250', '500', '1000'], 'percentiles': ['p95', 'p99'], 'accuracy': 'High'}

## 6.3.0.0.0 Percentile Calculations

- {'metricName': 'http.server.request.duration', 'percentiles': ['p95'], 'algorithm': 't-digest', 'accuracy': 'Required for REQ-PERF-001 validation.'}

## 6.4.0.0.0 Metric Types

### 6.4.1.0.0 http.server.requests.errors

#### 6.4.1.1.0 Name

http.server.requests.errors

#### 6.4.1.2.0 Implementation

counter

#### 6.4.1.3.0 Reasoning

Errors are a monotonically increasing count, making a counter the appropriate type.

#### 6.4.1.4.0 Resets Handling

Handled by monitoring system (calculates rate).

### 6.4.2.0.0 container.cpu.utilization

#### 6.4.2.1.0 Name

container.cpu.utilization

#### 6.4.2.2.0 Implementation

gauge

#### 6.4.2.3.0 Reasoning

CPU utilization is a point-in-time value that can go up or down.

#### 6.4.2.4.0 Resets Handling

N/A

## 6.5.0.0.0 Dimensional Aggregation

- {'metricName': 'http.server.request.duration', 'dimensions': ['http.route', 'http.status_code'], 'aggregationStrategy': 'Aggregating across all dimensions provides a system-wide view, while grouping by dimensions allows for targeted problem analysis.', 'cardinalityImpact': 'Medium, managed by using templated routes.'}

## 6.6.0.0.0 Derived Metrics

- {'name': 'cache.hit.ratio', 'calculation': 'sum(rate(cache.hits.total)) / (sum(rate(cache.hits.total)) + sum(rate(cache.misses.total)))', 'sourceMetrics': ['cache.hits.total', 'cache.misses.total'], 'updateFrequency': 'on_query (e.g., 1m)'}

# 7.0.0.0.0 Storage Requirements Planning

## 7.1.0.0.0 Retention Periods

### 7.1.1.0.0 Metric Type

#### 7.1.1.1.0 Metric Type

Performance (High Resolution)

#### 7.1.1.2.0 Retention Period

14 days

#### 7.1.1.3.0 Justification

Sufficient for recent performance incident analysis.

#### 7.1.1.4.0 Compliance Requirement

None

### 7.1.2.0.0 Metric Type

#### 7.1.2.1.0 Metric Type

Business KPIs (Downsampled)

#### 7.1.2.2.0 Retention Period

1 year

#### 7.1.2.3.0 Justification

Required for year-over-year business performance analysis.

#### 7.1.2.4.0 Compliance Requirement

None

## 7.2.0.0.0 Data Resolution

### 7.2.1.0.0 Time Range

#### 7.2.1.1.0 Time Range

0-14 days

#### 7.2.1.2.0 Resolution

15s

#### 7.2.1.3.0 Query Performance

High

#### 7.2.1.4.0 Storage Optimization

Raw data for detailed analysis.

### 7.2.2.0.0 Time Range

#### 7.2.2.1.0 Time Range

14-90 days

#### 7.2.2.2.0 Resolution

5m

#### 7.2.2.3.0 Query Performance

Medium

#### 7.2.2.4.0 Storage Optimization

Downsampled via aggregation to reduce cost.

## 7.3.0.0.0 Downsampling Strategies

- {'sourceResolution': '15s', 'targetResolution': '5m', 'aggregationMethod': 'avg for gauges, sum for counters, merge for histograms', 'triggerCondition': 'Automated job after 14 days.'}

## 7.4.0.0.0 Storage Performance

| Property | Value |
|----------|-------|
| Write Latency | < 1s |
| Query Latency | < 5s for dashboard queries |
| Throughput Requirements | Support for 1,000 concurrent users (as per REQ-PER... |
| Scalability Needs | Scales with application load via managed service. |

## 7.5.0.0.0 Query Optimization

- {'queryPattern': 'Time-series dashboard widgets', 'optimizationStrategy': 'Pre-aggregation and downsampling.', 'indexingRequirements': ['timestamp', 'metric_name', 'key_dimensions']}

## 7.6.0.0.0 Cost Optimization

- {'strategy': 'Tiered Retention and Downsampling', 'implementation': 'Configure retention policies in the time-series database (e.g., Prometheus, CloudWatch).', 'expectedSavings': 'Significant (50-80%) reduction in long-term storage costs.', 'tradeoffs': 'Loss of fine-grained historical data beyond the initial retention period.'}

# 8.0.0.0.0 Project Specific Metrics Config

## 8.1.0.0.0 Standard Metrics

*No items available*

## 8.2.0.0.0 Custom Metrics

*No items available*

## 8.3.0.0.0 Dashboard Metrics

*No items available*

# 9.0.0.0.0 Implementation Priority

*No items available*

# 10.0.0.0.0 Risk Assessment

*No items available*

# 11.0.0.0.0 Recommendations

*No items available*


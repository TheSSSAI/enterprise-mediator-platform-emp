# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8/ASP.NET Core 8
- PostgreSQL 16 with pgvector
- Next.js 14/React 18
- Docker/Kubernetes
- RabbitMQ (or similar)
- AWS SES
- Azure OpenAI

## 1.3 Metrics Configuration

- Backend APM via OpenTelemetry (traces, metrics)
- Frontend RUM via AWS CloudWatch RUM (LCP, errors)
- Centralized Logging via Serilog to AWS CloudWatch
- Infrastructure Metrics via AWS CloudWatch (EKS, RDS, SQS)
- Service Health Probes via ASP.NET Core Health Checks

## 1.4 Monitoring Needs

- End-to-end API latency and error rates
- Asynchronous workflow health (queue depth, DLQ count, processing time)
- Frontend performance and user-perceived latency
- Database and container resource utilization
- External service integration health (AWS SES, Azure OpenAI)

## 1.5 Environment

production

# 2.0 Alert Condition And Threshold Design

## 2.1 Critical Metrics Alerts

### 2.1.1 Metric

#### 2.1.1.1 Metric

API Response Time (p95 Latency)

#### 2.1.1.2 Condition

value > 250ms for a sustained period of 5 minutes

#### 2.1.1.3 Threshold Type

static

#### 2.1.1.4 Value

250ms

#### 2.1.1.5 Justification

Directly validates NFR REQ-PERF-001. A sustained breach indicates a systemic performance degradation affecting user experience.

#### 2.1.1.6 Business Impact

High - Slow UI, user frustration, potential for lost transactions.

### 2.1.2.0 Metric

#### 2.1.2.1 Metric

API HTTP Server Errors (5xx Rate)

#### 2.1.2.2 Condition

rate > 5% of total requests over a 5-minute window

#### 2.1.2.3 Threshold Type

static

#### 2.1.2.4 Value

5%

#### 2.1.2.5 Justification

Indicates widespread backend failures. A rate above 5% signifies a significant service disruption impacting a large portion of users.

#### 2.1.2.6 Business Impact

Critical - Core application functionality is failing, potentially leading to data loss and severe customer impact.

### 2.1.3.0 Metric

#### 2.1.3.1 Metric

SOW Processing Queue - Dead-Letter Queue (DLQ) Size

#### 2.1.3.2 Condition

number of messages > 0

#### 2.1.3.3 Threshold Type

static

#### 2.1.3.4 Value

0

#### 2.1.3.5 Justification

Fulfills REQ-FUNC-010 monitoring. Any message in the DLQ represents a permanently failed SOW processing job that requires manual intervention. This is a direct failure of a core business process.

#### 2.1.3.6 Business Impact

High - Failure to process client SOWs, blocking new project initiation and revenue generation.

### 2.1.4.0 Metric

#### 2.1.4.1 Metric

Database CPU Utilization

#### 2.1.4.2 Condition

average utilization > 90% for a sustained period of 10 minutes

#### 2.1.4.3 Threshold Type

static

#### 2.1.4.4 Value

90%

#### 2.1.4.5 Justification

The PostgreSQL database is a critical single point of failure. Sustained high CPU indicates imminent performance collapse for the entire application.

#### 2.1.4.6 Business Impact

Critical - System-wide slowdown or outage, potential data corruption under extreme load.

### 2.1.5.0 Metric

#### 2.1.5.1 Metric

External Service Circuit Breaker State (Azure OpenAI)

#### 2.1.5.2 Condition

circuit transitions to the 'Open' state

#### 2.1.5.3 Threshold Type

static

#### 2.1.5.4 Value

Open

#### 2.1.5.5 Justification

The architecture specifies a Circuit Breaker for external services. An open state means the critical SOW data extraction (REQ-FUNC-013) and semantic search (REQ-FUNC-014) features are completely non-functional.

#### 2.1.5.6 Business Impact

High - Core AI-driven features are unavailable, impacting the platform's value proposition.

## 2.2.0.0 Threshold Strategies

*No items available*

## 2.3.0.0 Baseline Deviation Alerts

*No items available*

## 2.4.0.0 Predictive Alerts

*No items available*

## 2.5.0.0 Compound Conditions

*No items available*

# 3.0.0.0 Severity Level Classification

## 3.1.0.0 Severity Definitions

### 3.1.1.0 Level

#### 3.1.1.1 Level

🚨 Critical

#### 3.1.1.2 Criteria

Imminent or active system-wide outage. Significant data loss risk. Core business functions are non-operational for a majority of users.

#### 3.1.1.3 Business Impact

Severe revenue loss, SLA breach, major reputational damage.

#### 3.1.1.4 Customer Impact

System is unusable.

#### 3.1.1.5 Response Time

< 5 minutes (Acknowledgement)

#### 3.1.1.6 Escalation Required

✅ Yes

### 3.1.2.0 Level

#### 3.1.2.1 Level

🔴 High

#### 3.1.2.2 Criteria

Significant degradation of a core feature or business process. A component is down but redundancy may be partially effective. Risk of escalating to Critical.

#### 3.1.2.3 Business Impact

Moderate revenue impact, risk of SLA breach.

#### 3.1.2.4 Customer Impact

A key feature is non-functional or severely degraded.

#### 3.1.2.5 Response Time

< 15 minutes (Acknowledgement)

#### 3.1.2.6 Escalation Required

✅ Yes

### 3.1.3.0 Level

#### 3.1.3.1 Level

🟡 Medium

#### 3.1.3.2 Criteria

Non-critical feature degradation or performance issues. Indicates a potential future problem. A background job has failed.

#### 3.1.3.3 Business Impact

Low, operational inefficiency.

#### 3.1.3.4 Customer Impact

Minor features are failing, or the system is slow.

#### 3.1.3.5 Response Time

< 1 hour (Acknowledgement)

#### 3.1.3.6 Escalation Required

❌ No

## 3.2.0.0 Business Impact Matrix

*No items available*

## 3.3.0.0 Customer Impact Criteria

*No items available*

## 3.4.0.0 Sla Violation Severity

*No items available*

## 3.5.0.0 System Health Severity

*No items available*

# 4.0.0.0 Notification Channel Strategy

## 4.1.0.0 Channel Configuration

### 4.1.1.0 Channel

#### 4.1.1.1 Channel

pagerduty

#### 4.1.1.2 Purpose

Primary on-call alerting for urgent, actionable incidents.

#### 4.1.1.3 Applicable Severities

- Critical
- High

#### 4.1.1.4 Time Constraints

24/7

#### 4.1.1.5 Configuration

*No data available*

### 4.1.2.0 Channel

#### 4.1.2.1 Channel

slack

#### 4.1.2.2 Purpose

Real-time, high-visibility notifications for engineering teams. Used for all severity levels.

#### 4.1.2.3 Applicable Severities

- Critical
- High
- Medium
- Low
- Warning

#### 4.1.2.4 Time Constraints

24/7

#### 4.1.2.5 Configuration

##### 4.1.2.5.1 Channel

#emp-alerts-prod

### 4.1.3.0.0 Channel

#### 4.1.3.1.0 Channel

email

#### 4.1.3.2.0 Purpose

Secondary notification and summary reporting for non-urgent issues or stakeholders.

#### 4.1.3.3.0 Applicable Severities

- Medium
- Low
- Warning

#### 4.1.3.4.0 Time Constraints

Business Hours

#### 4.1.3.5.0 Configuration

##### 4.1.3.5.1 Distribution List

engineering-leads@example.com

## 4.2.0.0.0 Routing Rules

### 4.2.1.0.0 Condition

#### 4.2.1.1.0 Condition

Any

#### 4.2.1.2.0 Severity

Critical

#### 4.2.1.3.0 Alert Type

*

#### 4.2.1.4.0 Channels

- pagerduty
- slack

#### 4.2.1.5.0 Priority

🔹 1

### 4.2.2.0.0 Condition

#### 4.2.2.1.0 Condition

Any

#### 4.2.2.2.0 Severity

High

#### 4.2.2.3.0 Alert Type

*

#### 4.2.2.4.0 Channels

- pagerduty
- slack

#### 4.2.2.5.0 Priority

🔹 2

### 4.2.3.0.0 Condition

#### 4.2.3.1.0 Condition

Any

#### 4.2.3.2.0 Severity

Medium

#### 4.2.3.3.0 Alert Type

*

#### 4.2.3.4.0 Channels

- slack
- email

#### 4.2.3.5.0 Priority

🔹 3

## 4.3.0.0.0 Time Based Routing

*No items available*

## 4.4.0.0.0 Ticketing Integration

- {'system': 'jira', 'triggerConditions': ['Severity is Critical', 'Severity is High'], 'ticketPriority': 'Highest', 'autoAssignment': True}

## 4.5.0.0.0 Emergency Notifications

*No items available*

## 4.6.0.0.0 Chat Platform Integration

*No items available*

# 5.0.0.0.0 Alert Correlation Implementation

## 5.1.0.0.0 Grouping Requirements

*No items available*

## 5.2.0.0.0 Parent Child Relationships

- {'parentCondition': 'Database CPU Utilization > 90%', 'childConditions': ['API Response Time > 250ms', 'API HTTP 5xx Rate > 5%'], 'suppressionDuration': 'While parent is active', 'propagationRules': 'The parent alert (Database High CPU) is the root cause. Child alerts are symptoms and should be suppressed to reduce noise and focus response on the database issue.'}

## 5.3.0.0.0 Topology Based Correlation

*No items available*

## 5.4.0.0.0 Time Window Correlation

*No items available*

## 5.5.0.0.0 Causal Relationship Detection

*No items available*

## 5.6.0.0.0 Maintenance Window Suppression

*No items available*

# 6.0.0.0.0 False Positive Mitigation

## 6.1.0.0.0 Noise Reduction Strategies

- {'strategy': 'Sustained Threshold Breach', 'implementation': "All threshold-based alerts (e.g., latency, error rate, CPU) are configured with a 'for X minutes' condition.", 'applicableAlerts': ['API Response Time (p95 Latency)', 'API HTTP Server Errors (5xx Rate)', 'Database CPU Utilization'], 'effectiveness': 'High - Drastically reduces flapping alerts caused by transient spikes.'}

## 6.2.0.0.0 Confirmation Counts

*No items available*

## 6.3.0.0.0 Dampening And Flapping

*No items available*

## 6.4.0.0.0 Alert Validation

*No items available*

## 6.5.0.0.0 Smart Filtering

*No items available*

## 6.6.0.0.0 Quorum Based Alerting

*No items available*

# 7.0.0.0.0 On Call Management Integration

## 7.1.0.0.0 Escalation Paths

### 7.1.1.0.0 Severity

#### 7.1.1.1.0 Severity

Critical

#### 7.1.1.2.0 Escalation Levels

##### 7.1.1.2.1 Level

###### 7.1.1.2.1.1 Level

🔹 1

###### 7.1.1.2.1.2 Recipients

- Primary On-Call Engineer

###### 7.1.1.2.1.3 Escalation Time

10 minutes

###### 7.1.1.2.1.4 Requires Acknowledgment

✅ Yes

##### 7.1.1.2.2.0 Level

###### 7.1.1.2.2.1 Level

🔹 2

###### 7.1.1.2.2.2 Recipients

- Secondary On-Call Engineer
- Engineering Manager

###### 7.1.1.2.2.3 Escalation Time

15 minutes

###### 7.1.1.2.2.4 Requires Acknowledgment

✅ Yes

#### 7.1.1.3.0.0 Ultimate Escalation

Head of Engineering

### 7.1.2.0.0.0 Severity

#### 7.1.2.1.0.0 Severity

High

#### 7.1.2.2.0.0 Escalation Levels

##### 7.1.2.2.1.0 Level

###### 7.1.2.2.1.1 Level

🔹 1

###### 7.1.2.2.1.2 Recipients

- Primary On-Call Engineer

###### 7.1.2.2.1.3 Escalation Time

15 minutes

###### 7.1.2.2.1.4 Requires Acknowledgment

✅ Yes

##### 7.1.2.2.2.0 Level

###### 7.1.2.2.2.1 Level

🔹 2

###### 7.1.2.2.2.2 Recipients

- Secondary On-Call Engineer

###### 7.1.2.2.2.3 Escalation Time

20 minutes

###### 7.1.2.2.2.4 Requires Acknowledgment

✅ Yes

#### 7.1.2.3.0.0 Ultimate Escalation

Engineering Manager

## 7.2.0.0.0.0 Escalation Timeframes

*No items available*

## 7.3.0.0.0.0 On Call Rotation

*No items available*

## 7.4.0.0.0.0 Acknowledgment Requirements

### 7.4.1.0.0.0 Severity

#### 7.4.1.1.0.0 Severity

Critical

#### 7.4.1.2.0.0 Acknowledgment Timeout

10 minutes

#### 7.4.1.3.0.0 Auto Escalation

✅ Yes

#### 7.4.1.4.0.0 Requires Comment

❌ No

### 7.4.2.0.0.0 Severity

#### 7.4.2.1.0.0 Severity

High

#### 7.4.2.2.0.0 Acknowledgment Timeout

15 minutes

#### 7.4.2.3.0.0 Auto Escalation

✅ Yes

#### 7.4.2.4.0.0 Requires Comment

❌ No

## 7.5.0.0.0.0 Incident Ownership

*No items available*

## 7.6.0.0.0.0 Follow The Sun Support

*No items available*

# 8.0.0.0.0.0 Project Specific Alerts Config

## 8.1.0.0.0.0 Alerts

### 8.1.1.0.0.0 API High p95 Latency

#### 8.1.1.1.0.0 Name

API High p95 Latency

#### 8.1.1.2.0.0 Description

API p95 response time has exceeded the 250ms SLO defined in REQ-PERF-001.

#### 8.1.1.3.0.0 Condition

aws.cloudwatch.metric(name='p95_latency', service='api') > 250

#### 8.1.1.4.0.0 Threshold

250ms for 5 minutes

#### 8.1.1.5.0.0 Severity

Critical

#### 8.1.1.6.0.0 Channels

- pagerduty
- slack

#### 8.1.1.7.0.0 Correlation

##### 8.1.1.7.1.0 Group Id

api-health

##### 8.1.1.7.2.0 Suppression Rules

- is_child_of:Database High CPU

#### 8.1.1.8.0.0 Escalation

##### 8.1.1.8.1.0 Enabled

✅ Yes

##### 8.1.1.8.2.0 Escalation Time

10 minutes

##### 8.1.1.8.3.0 Escalation Path

- Primary On-Call
- Secondary On-Call

#### 8.1.1.9.0.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ✅ |
| Manual Override | ✅ |

#### 8.1.1.10.0.0 Validation

##### 8.1.1.10.1.0 Confirmation Count

0

##### 8.1.1.10.2.0 Confirmation Window

5 minutes

#### 8.1.1.11.0.0 Remediation

##### 8.1.1.11.1.0 Automated Actions

*No items available*

##### 8.1.1.11.2.0 Runbook Url

🔗 [https://runbooks.example.com/api-high-latency](https://runbooks.example.com/api-high-latency)

##### 8.1.1.11.3.0 Troubleshooting Steps

- Check APM for slow transaction traces.
- Investigate database for long-running queries.
- Check container resource utilization (CPU/Memory).

### 8.1.2.0.0.0 SOW Processing DLQ Alert

#### 8.1.2.1.0.0 Name

SOW Processing DLQ Alert

#### 8.1.2.2.0.0 Description

One or more SOW documents have failed processing permanently and require manual intervention (related to REQ-FUNC-010).

#### 8.1.2.3.0.0 Condition

aws.sqs.metric(name='ApproximateNumberOfMessagesVisible', queue='sow_processing_dlq') > 0

#### 8.1.2.4.0.0 Threshold

> 0 for 1 minute

#### 8.1.2.5.0.0 Severity

High

#### 8.1.2.6.0.0 Channels

- pagerduty
- slack

#### 8.1.2.7.0.0 Correlation

##### 8.1.2.7.1.0 Group Id

sow-processing

##### 8.1.2.7.2.0 Suppression Rules

*No items available*

#### 8.1.2.8.0.0 Escalation

##### 8.1.2.8.1.0 Enabled

✅ Yes

##### 8.1.2.8.2.0 Escalation Time

15 minutes

##### 8.1.2.8.3.0 Escalation Path

- Primary On-Call
- Secondary On-Call

#### 8.1.2.9.0.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.2.10.0.0 Validation

##### 8.1.2.10.1.0 Confirmation Count

0

##### 8.1.2.10.2.0 Confirmation Window

1 minute

#### 8.1.2.11.0.0 Remediation

##### 8.1.2.11.1.0 Automated Actions

*No items available*

##### 8.1.2.11.2.0 Runbook Url

🔗 [https://runbooks.example.com/sow-dlq-handling](https://runbooks.example.com/sow-dlq-handling)

##### 8.1.2.11.3.0 Troubleshooting Steps

- Inspect message body and metadata in DLQ for error details.
- Check logs for the SowProcessingWorker service using the trace ID from the message.
- Determine if the issue is transient (re-queue) or a permanent bug (escalate to dev).

### 8.1.3.0.0.0 High Email Bounce Rate

#### 8.1.3.1.0.0 Name

High Email Bounce Rate

#### 8.1.3.2.0.0 Description

The AWS SES bounce rate is approaching a level that could damage sender reputation (related to REQ-INTG-005).

#### 8.1.3.3.0.0 Condition

aws.ses.metric(name='BounceRate') > 0.05

#### 8.1.3.4.0.0 Threshold

5% over 1 hour

#### 8.1.3.5.0.0 Severity

High

#### 8.1.3.6.0.0 Channels

- slack

#### 8.1.3.7.0.0 Correlation

##### 8.1.3.7.1.0 Group Id

integrations

##### 8.1.3.7.2.0 Suppression Rules

*No items available*

#### 8.1.3.8.0.0 Escalation

##### 8.1.3.8.1.0 Enabled

❌ No

##### 8.1.3.8.2.0 Escalation Time



##### 8.1.3.8.3.0 Escalation Path

*No items available*

#### 8.1.3.9.0.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.3.10.0.0 Validation

##### 8.1.3.10.1.0 Confirmation Count

0

##### 8.1.3.10.2.0 Confirmation Window

1 hour

#### 8.1.3.11.0.0 Remediation

##### 8.1.3.11.1.0 Automated Actions

*No items available*

##### 8.1.3.11.2.0 Runbook Url

🔗 [https://runbooks.example.com/ses-reputation](https://runbooks.example.com/ses-reputation)

##### 8.1.3.11.3.0 Troubleshooting Steps

- Check SES console for details on bounced emails.
- Investigate if a recent data import (REQ-NFR-009) introduced invalid email addresses.
- Review recent application changes that trigger emails.

### 8.1.4.0.0.0 Dashboard Metrics Job Failure

#### 8.1.4.1.0.0 Name

Dashboard Metrics Job Failure

#### 8.1.4.2.0.0 Description

The scheduled job 'DashboardMetricsCalculator' failed to complete, resulting in stale dashboard data (related to REQ-FUNC-024).

#### 8.1.4.3.0.0 Condition

job_last_run_status == 'FAILED'

#### 8.1.4.4.0.0 Threshold

1 failure

#### 8.1.4.5.0.0 Severity

Medium

#### 8.1.4.6.0.0 Channels

- slack
- email

#### 8.1.4.7.0.0 Correlation

##### 8.1.4.7.1.0 Group Id

batch-jobs

##### 8.1.4.7.2.0 Suppression Rules

*No items available*

#### 8.1.4.8.0.0 Escalation

##### 8.1.4.8.1.0 Enabled

❌ No

##### 8.1.4.8.2.0 Escalation Time



##### 8.1.4.8.3.0 Escalation Path

*No items available*

#### 8.1.4.9.0.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.4.10.0.0 Validation

##### 8.1.4.10.1.0 Confirmation Count

0

##### 8.1.4.10.2.0 Confirmation Window

N/A

#### 8.1.4.11.0.0 Remediation

##### 8.1.4.11.1.0 Automated Actions

*No items available*

##### 8.1.4.11.2.0 Runbook Url

🔗 [https://runbooks.example.com/dashboard-job-failure](https://runbooks.example.com/dashboard-job-failure)

##### 8.1.4.11.3.0 Troubleshooting Steps

- Check logs for the DashboardMetricsCalculator service for the failed run.
- Analyze the exception to see if it's a data or code issue.
- Manually trigger the job after fixing the underlying problem.

## 8.2.0.0.0.0 Alert Groups

*No items available*

## 8.3.0.0.0.0 Notification Templates

*No items available*

# 9.0.0.0.0.0 Implementation Priority

## 9.1.0.0.0.0 Component

### 9.1.1.0.0.0 Component

Critical Service Health & Performance Alerts (API Latency, 5xx Rate, DB CPU)

### 9.1.2.0.0.0 Priority

🔴 high

### 9.1.3.0.0.0 Dependencies

- APM and Infrastructure Monitoring setup

### 9.1.4.0.0.0 Estimated Effort

Medium

### 9.1.5.0.0.0 Risk Level

low

## 9.2.0.0.0.0 Component

### 9.2.1.0.0.0 Component

Asynchronous Workflow Alerts (Queue Depth, DLQ)

### 9.2.2.0.0.0 Priority

🔴 high

### 9.2.3.0.0.0 Dependencies

- Infrastructure Monitoring setup for Message Broker

### 9.2.4.0.0.0 Estimated Effort

Medium

### 9.2.5.0.0.0 Risk Level

low

## 9.3.0.0.0.0 Component

### 9.3.1.0.0.0 Component

External Integration Alerts (Circuit Breaker, SES Bounce Rate)

### 9.3.2.0.0.0 Priority

🟡 medium

### 9.3.3.0.0.0 Dependencies

- APM setup
- AWS CloudWatch

### 9.3.4.0.0.0 Estimated Effort

Low

### 9.3.5.0.0.0 Risk Level

low

# 10.0.0.0.0.0 Risk Assessment

- {'risk': 'Alert Fatigue', 'impact': 'high', 'probability': 'medium', 'mitigation': "The alerting strategy is intentionally minimal, focusing only on actionable, critical failures. The use of 'sustained period' thresholds and alert correlation (e.g., database parent alert) is designed to significantly reduce noise.", 'contingencyPlan': 'Regularly review alert frequency and on-call incidents. Tune or suppress alerts that are not providing value or are overly noisy.'}

# 11.0.0.0.0.0 Recommendations

## 11.1.0.0.0.0 Category

### 11.1.1.0.0.0 Category

🔹 Observability

### 11.1.2.0.0.0 Recommendation

Implement distributed tracing across all backend services, ensuring the trace context is propagated through the message queue for the SOW processing workflow.

### 11.1.3.0.0.0 Justification

This is essential for debugging failures in the asynchronous SOW processing flow. It allows an operator to trace a single SOW upload from the initial API call, through the queue, to the final database update or error in the worker, drastically reducing Mean Time To Resolution (MTTR).

### 11.1.4.0.0.0 Priority

🔴 high

### 11.1.5.0.0.0 Implementation Notes

Leverage the OpenTelemetry SDK's built-in support for context propagation with messaging libraries like MassTransit or EasyNetQ.

## 11.2.0.0.0.0 Category

### 11.2.1.0.0.0 Category

🔹 Incident Response

### 11.2.2.0.0.0 Recommendation

Develop lightweight, actionable runbooks for each of the high-priority alerts defined.

### 11.2.3.0.0.0 Justification

Runbooks provide on-call engineers with immediate, standardized steps for diagnosis and remediation, which reduces stress and MTTR. This is particularly important for complex failure scenarios like the SOW processing DLQ.

### 11.2.4.0.0.0 Priority

🔴 high

### 11.2.5.0.0.0 Implementation Notes

Store runbooks in a version-controlled system (e.g., a Git repo with Markdown files) and link directly to them from the alert notifications.


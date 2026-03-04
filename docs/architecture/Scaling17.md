# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- React
- NestJS (Node.js)
- PostgreSQL (pgvector)
- Docker
- Kubernetes (AWS EKS)
- AWS (S3, SQS, SES, RDS)
- Redis

## 1.3 Architecture Patterns

- Microservices Architecture
- Event-Driven Architecture

## 1.4 Resource Needs

- Container Compute (EKS)
- Managed Database (RDS)
- Object Storage (S3)
- Message Queues (SQS)
- Distributed Cache (ElastiCache)

## 1.5 Performance Expectations

p95 API response < 250ms, LCP < 2.5s, Asynchronous SOW processing < 5 minutes.

## 1.6 Data Processing Volumes

Initial load of 1,000 concurrent users and data growth of 100GB per year.

# 2.0 Workload Characterization

## 2.1 Processing Resource Consumption

### 2.1.1 Operation

#### 2.1.1.1 Operation

API Service

#### 2.1.1.2 Cpu Pattern

bursty

#### 2.1.1.3 Cpu Utilization

| Property | Value |
|----------|-------|
| Baseline | 10-15% |
| Peak | 60-70% |
| Average | 25% |

#### 2.1.1.4 Memory Pattern

steady

#### 2.1.1.5 Memory Requirements

| Property | Value |
|----------|-------|
| Baseline | 500MB |
| Peak | 1.5GB |
| Growth | low |

#### 2.1.1.6 Io Characteristics

| Property | Value |
|----------|-------|
| Disk Iops | low |
| Network Throughput | high |
| Io Pattern | mixed |

### 2.1.2.0 Operation

#### 2.1.2.1 Operation

AI Ingestion Service (Worker)

#### 2.1.2.2 Cpu Pattern

event-driven

#### 2.1.2.3 Cpu Utilization

| Property | Value |
|----------|-------|
| Baseline | 5% |
| Peak | 80-90% |
| Average | 15% |

#### 2.1.2.4 Memory Pattern

fluctuating

#### 2.1.2.5 Memory Requirements

| Property | Value |
|----------|-------|
| Baseline | 250MB |
| Peak | 2GB |
| Growth | none |

#### 2.1.2.6 Io Characteristics

| Property | Value |
|----------|-------|
| Disk Iops | moderate |
| Network Throughput | high |
| Io Pattern | mixed |

## 2.2.0.0 Concurrency Requirements

- {'operation': 'API Service', 'maxConcurrentJobs': 1000, 'threadPoolSize': 0, 'connectionPoolSize': 200, 'queueDepth': 0}

## 2.3.0.0 Database Access Patterns

- {'accessType': 'mixed', 'connectionRequirements': 'High connection count from API services, requiring pooling (RDS Proxy).', 'queryComplexity': 'mixed', 'transactionVolume': 'medium', 'cacheHitRatio': 'N/A'}

## 2.4.0.0 Frontend Resource Demands

- {'component': 'Main Dashboard', 'renderingLoad': 'moderate', 'staticContentSize': 'medium', 'dynamicContentVolume': 'high', 'userConcurrency': 'medium'}

## 2.5.0.0 Load Patterns

- {'pattern': 'peak-trough', 'description': 'Load follows standard business hours, with peaks in the mid-morning and mid-afternoon.', 'frequency': 'daily', 'magnitude': 'high', 'predictability': 'high'}

# 3.0.0.0 Scaling Strategy Design

## 3.1.0.0 Scaling Approaches

### 3.1.1.0 Component

#### 3.1.1.1 Component

API Service

#### 3.1.1.2 Primary Strategy

horizontal

#### 3.1.1.3 Justification

Stateless nature of the API makes it ideal for horizontal scaling to handle concurrent user load (REQ-NFR-005).

#### 3.1.1.4 Limitations

- Database connection limits

#### 3.1.1.5 Implementation

Kubernetes Horizontal Pod Autoscaler (HPA)

### 3.1.2.0 Component

#### 3.1.2.1 Component

AI Ingestion Service

#### 3.1.2.2 Primary Strategy

horizontal

#### 3.1.2.3 Justification

Event-driven, stateless workers can be scaled out based on the number of SOWs in the processing queue to maintain throughput.

#### 3.1.2.4 Limitations

- Downstream API rate limits (OpenAI)
- Database write contention

#### 3.1.2.5 Implementation

Kubernetes Event-driven Autoscaling (KEDA) based on SQS queue depth.

### 3.1.3.0 Component

#### 3.1.3.1 Component

PostgreSQL Database

#### 3.1.3.2 Primary Strategy

hybrid

#### 3.1.3.3 Justification

Primary scaling is vertical (instance size) for write performance. Horizontal scaling is achieved via read replicas for read-heavy workloads like reporting and to ensure HA (REQ-NFR-002, REQ-NFR-005).

#### 3.1.3.4 Limitations

- Write performance is limited to a single primary instance.

#### 3.1.3.5 Implementation

AWS RDS instance modification and read replica creation.

## 3.2.0.0 Instance Specifications

*No items available*

## 3.3.0.0 Multithreading Considerations

*No items available*

## 3.4.0.0 Specialized Hardware

*No items available*

## 3.5.0.0 Storage Scaling

*No items available*

## 3.6.0.0 Licensing Implications

- {'software': 'PostgreSQL, NestJS, React, Docker, Kubernetes', 'licensingModel': 'Open Source', 'scalingImpact': 'None. No per-core or per-instance licensing costs.', 'costOptimization': 'N/A'}

# 4.0.0.0 Auto Scaling Trigger Metrics

## 4.1.0.0 Cpu Utilization Triggers

- {'component': 'API Service', 'scaleUpThreshold': 75, 'scaleDownThreshold': 40, 'evaluationPeriods': 3, 'dataPoints': 2, 'justification': 'Primary metric for scaling stateless web services in response to user load, as specified in REQ-NFR-005.'}

## 4.2.0.0 Memory Consumption Triggers

- {'component': 'API Service', 'scaleUpThreshold': 80, 'scaleDownThreshold': 50, 'evaluationPeriods': 2, 'triggerCondition': 'used', 'justification': 'Secondary metric for API scaling, also required by REQ-NFR-005, to handle memory-intensive requests.'}

## 4.3.0.0 Database Connection Triggers

*No items available*

## 4.4.0.0 Queue Length Triggers

- {'queueType': 'message', 'scaleUpThreshold': 50, 'scaleDownThreshold': 5, 'ageThreshold': 'N/A', 'priority': 'high'}

## 4.5.0.0 Response Time Triggers

*No items available*

## 4.6.0.0 Custom Metric Triggers

*No items available*

## 4.7.0.0 Disk Iotriggers

*No items available*

# 5.0.0.0 Scaling Limits And Safeguards

## 5.1.0.0 Instance Limits

### 5.1.1.0 Component

#### 5.1.1.1 Component

API Service

#### 5.1.1.2 Min Instances

2

#### 5.1.1.3 Max Instances

10

#### 5.1.1.4 Justification

Minimum of 2 instances ensures high availability across multiple AZs (REQ-NFR-002). Maximum of 10 prevents runaway costs.

#### 5.1.1.5 Cost Implication

Defines the baseline and maximum operational cost for the API tier.

### 5.1.2.0 Component

#### 5.1.2.1 Component

AI Ingestion Service

#### 5.1.2.2 Min Instances

1

#### 5.1.2.3 Max Instances

5

#### 5.1.2.4 Justification

Minimum of 1 instance to process low-volume traffic. Maximum of 5 to avoid overwhelming downstream services (OpenAI) and control costs.

#### 5.1.2.5 Cost Implication

Scales from a low baseline to handle bursts, with a cost cap.

## 5.2.0.0 Cooldown Periods

### 5.2.1.0 Action

#### 5.2.1.1 Action

scale-up

#### 5.2.1.2 Duration

180s

#### 5.2.1.3 Reasoning

Allows new instances time to become fully operational and start taking load before triggering another scale-up.

#### 5.2.1.4 Component

API Service

### 5.2.2.0 Action

#### 5.2.2.1 Action

scale-down

#### 5.2.2.2 Duration

300s

#### 5.2.2.3 Reasoning

Prevents 'flapping' by ensuring that a drop in traffic is sustained before removing capacity.

#### 5.2.2.4 Component

API Service

## 5.3.0.0 Scaling Step Sizes

*No items available*

## 5.4.0.0 Runaway Protection

*No items available*

## 5.5.0.0 Graceful Degradation

*No items available*

## 5.6.0.0 Resource Quotas

*No items available*

## 5.7.0.0 Workload Prioritization

*No items available*

# 6.0.0.0 Cost Optimization Strategy

## 6.1.0.0 Instance Right Sizing

*No items available*

## 6.2.0.0 Time Based Scaling

- {'schedule': 'Weekdays 9 PM - 7 AM; Weekends', 'timezone': 'UTC', 'scaleAction': 'scale-down', 'instanceCount': 1, 'justification': 'Non-production (Development, Staging) environments have predictable low usage outside of business hours.'}

## 6.3.0.0 Instance Termination Policies

*No items available*

## 6.4.0.0 Spot Instance Strategies

- {'component': 'AI Ingestion Service', 'spotPercentage': 80, 'fallbackStrategy': 'On-Demand instances', 'interruptionHandling': 'The SQS queue ensures message durability. If a Spot instance is terminated, the SOW processing message will be re-processed by another worker.', 'costSavings': 'High potential savings for the most CPU-intensive workload.'}

## 6.5.0.0 Reserved Instance Planning

- {'instanceType': 'General Purpose (e.g., m6i)', 'reservationTerm': '1-year', 'utilizationForecast': 'stable', 'baselineInstances': 2, 'paymentOption': 'partial-upfront'}

## 6.6.0.0 Resource Tracking

*No items available*

## 6.7.0.0 Cleanup Policies

*No items available*

# 7.0.0.0 Load Testing And Validation

## 7.1.0.0 Baseline Metrics

### 7.1.1.0 Metric

#### 7.1.1.1 Metric

API p95 latency

#### 7.1.1.2 Baseline Value

< 250ms

#### 7.1.1.3 Acceptable Variation

10%

#### 7.1.1.4 Measurement Method

APM

### 7.1.2.0 Metric

#### 7.1.2.1 Metric

Dashboard LCP

#### 7.1.2.2 Baseline Value

< 2.5s

#### 7.1.2.3 Acceptable Variation

15%

#### 7.1.2.4 Measurement Method

RUM

## 7.2.0.0 Validation Procedures

*No items available*

## 7.3.0.0 Synthetic Load Scenarios

### 7.3.1.0 Scenario

#### 7.3.1.1 Scenario

Sustained User Load

#### 7.3.1.2 Load Pattern

sustained

#### 7.3.1.3 Duration

60 minutes

#### 7.3.1.4 Target Metrics

- 1000 concurrent users

#### 7.3.1.5 Expected Behavior

System maintains performance NFRs (REQ-NFR-001) as API pods scale to meet demand.

### 7.3.2.0 Scenario

#### 7.3.2.1 Scenario

SOW Ingestion Burst

#### 7.3.2.2 Load Pattern

spike

#### 7.3.2.3 Duration

5 minutes

#### 7.3.2.4 Target Metrics

- 100 SOW uploads

#### 7.3.2.5 Expected Behavior

AI Ingestion worker pods scale out based on queue length, and all SOWs are processed within the NFR time (REQ-NFR-001).

## 7.4.0.0 Scaling Event Monitoring

*No items available*

## 7.5.0.0 Policy Refinement

*No items available*

## 7.6.0.0 Effectiveness Kpis

*No items available*

## 7.7.0.0 Feedback Mechanisms

*No items available*

# 8.0.0.0 Project Specific Scaling Policies

## 8.1.0.0 Policies

### 8.1.1.0 Horizontal

#### 8.1.1.1 Id

hpa-api-service

#### 8.1.1.2 Type

🔹 Horizontal

#### 8.1.1.3 Component

api-service

#### 8.1.1.4 Rules

- {'metric': 'CPUUtilizationPercentage', 'threshold': 75, 'operator': 'GREATER_THAN', 'scaleChange': 1, 'cooldown': {'scaleUpSeconds': 180, 'scaleDownSeconds': 300}, 'evaluationPeriods': 3, 'dataPointsToAlarm': 2}

#### 8.1.1.5 Safeguards

| Property | Value |
|----------|-------|
| Min Instances | 2 |
| Max Instances | 10 |
| Max Scaling Rate | N/A |
| Cost Threshold | N/A |

#### 8.1.1.6 Schedule

##### 8.1.1.6.1 Enabled

❌ No

##### 8.1.1.6.2 Timezone

*Not specified*

##### 8.1.1.6.3 Rules

*No items available*

### 8.1.2.0.0 Horizontal

#### 8.1.2.1.0 Id

keda-ai-worker

#### 8.1.2.2.0 Type

🔹 Horizontal

#### 8.1.2.3.0 Component

ai-ingestion-service

#### 8.1.2.4.0 Rules

- {'metric': 'AWSSQSQueue-ApproximateNumberOfMessagesVisible', 'threshold': 50, 'operator': 'GREATER_THAN', 'scaleChange': 1, 'cooldown': {'scaleUpSeconds': 60, 'scaleDownSeconds': 300}, 'evaluationPeriods': 2, 'dataPointsToAlarm': 2}

#### 8.1.2.5.0 Safeguards

| Property | Value |
|----------|-------|
| Min Instances | 1 |
| Max Instances | 5 |
| Max Scaling Rate | N/A |
| Cost Threshold | N/A |

#### 8.1.2.6.0 Schedule

##### 8.1.2.6.1 Enabled

❌ No

##### 8.1.2.6.2 Timezone

*Not specified*

##### 8.1.2.6.3 Rules

*No items available*

## 8.2.0.0.0 Configuration

### 8.2.1.0.0 Min Instances

1

### 8.2.2.0.0 Max Instances

10

### 8.2.3.0.0 Default Timeout

300

### 8.2.4.0.0 Region

Configurable (e.g., eu-west-1)

### 8.2.5.0.0 Resource Group

emp-production

### 8.2.6.0.0 Notification Endpoint

SNS topic

### 8.2.7.0.0 Logging Level

INFO

### 8.2.8.0.0 Vpc Id

emp-vpc

### 8.2.9.0.0 Instance Type

General Purpose (e.g., t3.large)

### 8.2.10.0.0 Enable Detailed Monitoring

true

### 8.2.11.0.0 Scaling Mode

reactive

### 8.2.12.0.0 Cost Optimization

| Property | Value |
|----------|-------|
| Spot Instances Enabled | ✅ |
| Spot Percentage | 80 |
| Reserved Instances Planned | ✅ |

### 8.2.13.0.0 Performance Targets

| Property | Value |
|----------|-------|
| Response Time | 250ms |
| Throughput | 1000 concurrent users |
| Availability | 99.9% |

## 8.3.0.0.0 Environment Specific Policies

### 8.3.1.0.0 Environment

#### 8.3.1.1.0 Environment

production

#### 8.3.1.2.0 Scaling Enabled

✅ Yes

#### 8.3.1.3.0 Aggressiveness

moderate

#### 8.3.1.4.0 Cost Priority

balanced

### 8.3.2.0.0 Environment

#### 8.3.2.1.0 Environment

staging

#### 8.3.2.2.0 Scaling Enabled

✅ Yes

#### 8.3.2.3.0 Aggressiveness

conservative

#### 8.3.2.4.0 Cost Priority

cost-optimized

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

API Service HPA Configuration

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

- EKS Cluster Setup

### 9.1.4.0.0 Estimated Effort

Low

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

AI Ingestion Service KEDA Configuration

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- EKS Cluster Setup
- KEDA Operator Installation

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

low

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Database Read Replica and RDS Proxy Setup

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- RDS Instance Provisioning

### 9.3.4.0.0 Estimated Effort

Medium

### 9.3.5.0.0 Risk Level

medium

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Runaway scaling due to misconfiguration, leading to excessive costs.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

low

### 10.1.4.0.0 Mitigation

Set sensible 'maxInstances' limits on all autoscaling groups. Implement budget alerts in AWS.

### 10.1.5.0.0 Contingency Plan

Manually scale down the component and freeze the autoscaling policy until the root cause is identified.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Scaling delay (cold starts) impacts user experience during sudden traffic spikes.

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

medium

### 10.2.4.0.0 Mitigation

Optimize container image size. Implement readiness probes to ensure pods only receive traffic when fully initialized. Consider over-provisioning during predictable peak times.

### 10.2.5.0.0 Contingency Plan

Manually scale up instances ahead of an anticipated event.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Technology

### 11.1.2.0.0 Recommendation

Use the Kubernetes Event-driven Autoscaling (KEDA) operator to scale the AI Ingestion Service based on SQS queue depth.

### 11.1.3.0.0 Justification

KEDA is the industry standard for scaling Kubernetes workloads based on external event sources like SQS. It provides a more direct and efficient scaling trigger than relying on proxy metrics like CPU.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Requires installing the KEDA operator into the EKS cluster and defining a 'ScaledObject' custom resource.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Cost Optimization

### 11.2.2.0.0 Recommendation

Utilize a mix of AWS Savings Plans for baseline compute (min API pods, RDS) and Spot Instances for the stateless, asynchronous AI Ingestion workers.

### 11.2.3.0.0 Justification

This hybrid approach provides significant cost savings. Savings Plans cover the predictable, always-on workload, while Spot Instances are ideal for fault-tolerant, interruptible workloads, which perfectly describes the AI worker.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

Configure EKS node groups with a mix of On-Demand and Spot capacity. Use node selectors or taints/tolerations to schedule worker pods onto Spot nodes.


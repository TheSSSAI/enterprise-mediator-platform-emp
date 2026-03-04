# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- React
- NestJS
- PostgreSQL (pgvector)
- Redis
- Docker
- Kubernetes (AWS EKS)
- AWS (S3, RDS, Cognito, SES)
- Terraform
- OpenAI

## 1.3 Architecture Patterns

- Microservices
- Event-Driven (for SOW processing)
- Cloud-Native

## 1.4 Data Handling Needs

- PII
- Financial Transaction Data
- Confidential SOW Documents
- Immutable Audit Logs

## 1.5 Performance Expectations

High-throughput, low-latency API responses (p95 < 250ms) and asynchronous processing of large documents.

## 1.6 Regulatory Requirements

- SOC 2 Type II
- GDPR

# 2.0 Environment Strategy

## 2.1 Environment Types

### 2.1.1 Development

#### 2.1.1.1 Type

🔹 Development

#### 2.1.1.2 Purpose

Used by developers for coding, unit testing, and feature development.

#### 2.1.1.3 Usage Patterns

- Frequent deployments
- Debugging
- Low traffic

#### 2.1.1.4 Isolation Level

complete

#### 2.1.1.5 Data Policy

Seeded, anonymized, or mock data. No production data allowed.

#### 2.1.1.6 Lifecycle Management

On-demand, may be torn down when not in use.

### 2.1.2.0 Testing

#### 2.1.2.1 Type

🔹 Testing

#### 2.1.2.2 Purpose

Used by the QA team for integration testing, E2E testing, and validation of new features.

#### 2.1.2.3 Usage Patterns

- Automated test suites
- Manual QA validation
- Stable builds

#### 2.1.2.4 Isolation Level

complete

#### 2.1.2.5 Data Policy

A sanitized and anonymized snapshot of production data.

#### 2.1.2.6 Lifecycle Management

Persistent, updated on a regular schedule from the development branch.

### 2.1.3.0 Staging

#### 2.1.3.1 Type

🔹 Staging

#### 2.1.3.2 Purpose

A pre-production environment that mirrors Production as closely as possible for UAT, performance testing, and final validation before release.

#### 2.1.3.3 Usage Patterns

- User Acceptance Testing (UAT)
- Load testing
- Pre-release validation

#### 2.1.3.4 Isolation Level

complete

#### 2.1.3.5 Data Policy

A recent, fully anonymized and masked copy of production data.

#### 2.1.3.6 Lifecycle Management

Long-lived, highly controlled, reflects the state of the upcoming production release.

### 2.1.4.0 Production

#### 2.1.4.1 Type

🔹 Production

#### 2.1.4.2 Purpose

The live environment used by all end-users (Admins, Clients, Vendors).

#### 2.1.4.3 Usage Patterns

- Live user traffic
- Real financial transactions
- 24/7 availability

#### 2.1.4.4 Isolation Level

complete

#### 2.1.4.5 Data Policy

Live, real user data.

#### 2.1.4.6 Lifecycle Management

Always-on, strictly controlled change management process.

### 2.1.5.0 DR

#### 2.1.5.1 Type

🔹 DR

#### 2.1.5.2 Purpose

Disaster Recovery environment in a secondary AWS region to ensure business continuity in case of a primary region failure.

#### 2.1.5.3 Usage Patterns

- Inactive (Warm Standby)
- Activated only during a DR event or quarterly DR tests

#### 2.1.5.4 Isolation Level

complete

#### 2.1.5.5 Data Policy

Asynchronously replicated production data.

#### 2.1.5.6 Lifecycle Management

Persistent, continuously updated from Production.

## 2.2.0.0 Promotion Strategy

### 2.2.1.0 Workflow

Code is promoted from feature branches to `develop` (Development), then to `main` (Testing -> Staging -> Production).

### 2.2.2.0 Approval Gates

- Automated tests must pass for promotion to Testing.
- QA sign-off required for promotion to Staging.
- Business owner and change advisory board (CAB) approval for promotion to Production.

### 2.2.3.0 Automation Level

automated

### 2.2.4.0 Rollback Procedure

Automated rollback via CI/CD pipeline to deploy the previous stable version. For database changes, a pre-validated rollback script is executed.

## 2.3.0.0 Isolation Strategies

- {'environment': 'All', 'isolationType': 'complete', 'implementation': 'Each environment (Dev, Test, Staging, Prod, DR) will be deployed in a separate, dedicated AWS Account. This provides the highest level of isolation for security, billing, and resource management.', 'justification': 'Prevents cross-environment interference, enhances security posture, simplifies billing, and enforces strict access control as required by SOC 2.'}

## 2.4.0.0 Scaling Approaches

### 2.4.1.0 Environment

#### 2.4.1.1 Environment

Production

#### 2.4.1.2 Scaling Type

auto

#### 2.4.1.3 Triggers

- CPU Utilization
- Memory Utilization

#### 2.4.1.4 Limits

Configured per service based on performance testing.

### 2.4.2.0 Environment

#### 2.4.2.1 Environment

Staging

#### 2.4.2.2 Scaling Type

auto

#### 2.4.2.3 Triggers

- CPU Utilization

#### 2.4.2.4 Limits

Scaled down compared to production, but auto-scaling enabled to support load testing.

### 2.4.3.0 Environment

#### 2.4.3.1 Environment

Development/Testing

#### 2.4.3.2 Scaling Type

vertical

#### 2.4.3.3 Triggers

- Manual

#### 2.4.3.4 Limits

Fixed, small number of instances to manage costs.

## 2.5.0.0 Provisioning Automation

| Property | Value |
|----------|-------|
| Tool | terraform |
| Templating | Reusable modules for common components like VPCs, ... |
| State Management | Remote state management using AWS S3 with state lo... |
| Cicd Integration | ✅ |

# 3.0.0.0 Resource Requirements Analysis

## 3.1.0.0 Workload Analysis

### 3.1.1.0 Workload Type

#### 3.1.1.1 Workload Type

API Services

#### 3.1.1.2 Expected Load

Up to 1,000 concurrent users

#### 3.1.1.3 Peak Capacity

2x expected load

#### 3.1.1.4 Resource Profile

balanced

### 3.1.2.0 Workload Type

#### 3.1.2.1 Workload Type

AI SOW Processing

#### 3.1.2.2 Expected Load

Variable, bursty

#### 3.1.2.3 Peak Capacity

Dependent on SOW complexity

#### 3.1.2.4 Resource Profile

cpu-intensive

### 3.1.3.0 Workload Type

#### 3.1.3.1 Workload Type

Database

#### 3.1.3.2 Expected Load

High read/write operations

#### 3.1.3.3 Peak Capacity

Sustained high connection count

#### 3.1.3.4 Resource Profile

io-intensive

## 3.2.0.0 Compute Requirements

### 3.2.1.0 Environment

#### 3.2.1.1 Environment

Production

#### 3.2.1.2 Instance Type

Mix of m5.xlarge (general purpose) and c5.xlarge (compute-optimized for AI workers)

#### 3.2.1.3 Cpu Cores

4

#### 3.2.1.4 Memory Gb

16

#### 3.2.1.5 Instance Count

3

#### 3.2.1.6 Auto Scaling

##### 3.2.1.6.1 Enabled

✅ Yes

##### 3.2.1.6.2 Min Instances

3

##### 3.2.1.6.3 Max Instances

10

##### 3.2.1.6.4 Scaling Triggers

- CPU > 75%
- Memory > 80%

#### 3.2.1.7.0 Justification

Provides high availability and supports horizontal scaling for 1,000+ concurrent users and bursty AI workloads.

### 3.2.2.0.0 Environment

#### 3.2.2.1.0 Environment

Staging

#### 3.2.2.2.0 Instance Type

m5.large

#### 3.2.2.3.0 Cpu Cores

2

#### 3.2.2.4.0 Memory Gb

8

#### 3.2.2.5.0 Instance Count

2

#### 3.2.2.6.0 Auto Scaling

##### 3.2.2.6.1 Enabled

✅ Yes

##### 3.2.2.6.2 Min Instances

2

##### 3.2.2.6.3 Max Instances

4

##### 3.2.2.6.4 Scaling Triggers

- CPU > 75%

#### 3.2.2.7.0 Justification

Mirrors production architecture but at a reduced scale to support UAT and performance testing while managing costs.

### 3.2.3.0.0 Environment

#### 3.2.3.1.0 Environment

Development/Testing

#### 3.2.3.2.0 Instance Type

t3.medium

#### 3.2.3.3.0 Cpu Cores

2

#### 3.2.3.4.0 Memory Gb

4

#### 3.2.3.5.0 Instance Count

2

#### 3.2.3.6.0 Auto Scaling

##### 3.2.3.6.1 Enabled

❌ No

##### 3.2.3.6.2 Min Instances

2

##### 3.2.3.6.3 Max Instances

2

##### 3.2.3.6.4 Scaling Triggers

*No items available*

#### 3.2.3.7.0 Justification

Cost-effective configuration for development and automated testing workloads with predictable load.

## 3.3.0.0.0 Storage Requirements

### 3.3.1.0.0 Environment

#### 3.3.1.1.0 Environment

Production

#### 3.3.1.2.0 Storage Type

block

#### 3.3.1.3.0 Capacity

1TB initial, auto-scaling

#### 3.3.1.4.0 Iops Requirements

10,000 Provisioned IOPS (io2)

#### 3.3.1.5.0 Throughput Requirements

500 MB/s

#### 3.3.1.6.0 Redundancy

Multi-AZ RDS

#### 3.3.1.7.0 Encryption

✅ Yes

### 3.3.2.0.0 Environment

#### 3.3.2.1.0 Environment

Production

#### 3.3.2.2.0 Storage Type

object

#### 3.3.2.3.0 Capacity

10TB initial, auto-scaling

#### 3.3.2.4.0 Iops Requirements

N/A

#### 3.3.2.5.0 Throughput Requirements

High

#### 3.3.2.6.0 Redundancy

Multi-AZ with Cross-Region Replication to DR site

#### 3.3.2.7.0 Encryption

✅ Yes

### 3.3.3.0.0 Environment

#### 3.3.3.1.0 Environment

Staging

#### 3.3.3.2.0 Storage Type

block

#### 3.3.3.3.0 Capacity

250GB

#### 3.3.3.4.0 Iops Requirements

3,000 Provisioned IOPS (gp3)

#### 3.3.3.5.0 Throughput Requirements

125 MB/s

#### 3.3.3.6.0 Redundancy

Multi-AZ RDS

#### 3.3.3.7.0 Encryption

✅ Yes

### 3.3.4.0.0 Environment

#### 3.3.4.1.0 Environment

Development/Testing

#### 3.3.4.2.0 Storage Type

block

#### 3.3.4.3.0 Capacity

100GB

#### 3.3.4.4.0 Iops Requirements

General Purpose (gp3)

#### 3.3.4.5.0 Throughput Requirements

125 MB/s

#### 3.3.4.6.0 Redundancy

Single-AZ RDS (to save cost)

#### 3.3.4.7.0 Encryption

✅ Yes

## 3.4.0.0.0 Special Hardware Requirements

*No items available*

## 3.5.0.0.0 Scaling Strategies

- {'environment': 'Production', 'strategy': 'reactive', 'implementation': 'Kubernetes Horizontal Pod Autoscaler (HPA) for application pods and Cluster Autoscaler for EKS worker nodes.', 'costOptimization': 'Use of mixed instance types and Spot Instances for non-critical, interruptible workloads like some AI processing tasks.'}

# 4.0.0.0.0 Security Architecture

## 4.1.0.0.0 Authentication Controls

### 4.1.1.0.0 Method

#### 4.1.1.1.0 Method

sso

#### 4.1.1.2.0 Scope

User access to the application

#### 4.1.1.3.0 Implementation

AWS Cognito with federation to enterprise identity providers.

#### 4.1.1.4.0 Environment

All

### 4.1.2.0.0 Method

#### 4.1.2.1.0 Method

mfa

#### 4.1.2.2.0 Scope

Admin and Finance roles

#### 4.1.2.3.0 Implementation

Enforced at the AWS Cognito User Pool level.

#### 4.1.2.4.0 Environment

All

## 4.2.0.0.0 Authorization Controls

### 4.2.1.0.0 Model

#### 4.2.1.1.0 Model

rbac

#### 4.2.1.2.0 Implementation

Enforced at the API Gateway and re-verified at the service level using JWT claims.

#### 4.2.1.3.0 Granularity

fine-grained

#### 4.2.1.4.0 Environment

All

### 4.2.2.0.0 Model

#### 4.2.2.1.0 Model

iam

#### 4.2.2.2.0 Implementation

IAM Roles for Service Accounts (IRSA) in EKS to grant pods granular permissions to AWS services (e.g., S3, SQS).

#### 4.2.2.3.0 Granularity

fine-grained

#### 4.2.2.4.0 Environment

All

## 4.3.0.0.0 Certificate Management

| Property | Value |
|----------|-------|
| Authority | external |
| Rotation Policy | Annual, automated |
| Automation | ✅ |
| Monitoring | ✅ |

## 4.4.0.0.0 Encryption Standards

### 4.4.1.0.0 Scope

#### 4.4.1.1.0 Scope

data-in-transit

#### 4.4.1.2.0 Algorithm

TLS 1.2+

#### 4.4.1.3.0 Key Management

AWS Certificate Manager (ACM)

#### 4.4.1.4.0 Compliance

- SOC 2
- GDPR

### 4.4.2.0.0 Scope

#### 4.4.2.1.0 Scope

data-at-rest

#### 4.4.2.2.0 Algorithm

AES-256

#### 4.4.2.3.0 Key Management

AWS Key Management Service (KMS) with customer-managed keys (CMKs).

#### 4.4.2.4.0 Compliance

- SOC 2
- GDPR

## 4.5.0.0.0 Access Control Mechanisms

- {'type': 'waf', 'configuration': 'AWS WAF with managed rule sets for OWASP Top 10 and rate-based rules against brute-force attacks.', 'environment': 'Production', 'rules': ['AWSManagedRulesCommonRuleSet', 'RateLimitAuthEndpoints']}

## 4.6.0.0.0 Data Protection Measures

### 4.6.1.0.0 Data Type

#### 4.6.1.1.0 Data Type

pii

#### 4.6.1.2.0 Protection Method

encryption

#### 4.6.1.3.0 Implementation

Application-level encryption for specific fields before database persistence, in addition to KMS encryption at rest.

#### 4.6.1.4.0 Compliance

- GDPR

### 4.6.2.0.0 Data Type

#### 4.6.2.1.0 Data Type

pii

#### 4.6.2.2.0 Protection Method

masking

#### 4.6.2.3.0 Implementation

A data transformation pipeline will be used to mask PII when refreshing Staging/Testing environments.

#### 4.6.2.4.0 Compliance

- GDPR

## 4.7.0.0.0 Network Security

- {'control': 'ids', 'implementation': 'AWS GuardDuty for intelligent threat detection.', 'rules': [], 'monitoring': True}

## 4.8.0.0.0 Security Monitoring

### 4.8.1.0.0 siem

#### 4.8.1.1.0 Type

🔹 siem

#### 4.8.1.2.0 Implementation

AWS Security Hub to aggregate findings from GuardDuty, Macie, and other security services.

#### 4.8.1.3.0 Frequency

continuous

#### 4.8.1.4.0 Alerting

✅ Yes

### 4.8.2.0.0 vulnerability-scanning

#### 4.8.2.1.0 Type

🔹 vulnerability-scanning

#### 4.8.2.2.0 Implementation

Static (SAST) and dynamic (DAST) analysis tools integrated into the CI/CD pipeline.

#### 4.8.2.3.0 Frequency

on-commit

#### 4.8.2.4.0 Alerting

✅ Yes

## 4.9.0.0.0 Backup Security

| Property | Value |
|----------|-------|
| Encryption | ✅ |
| Access Control | Strict IAM policies on the backup vault, preventin... |
| Offline Storage | ❌ |
| Testing Frequency | Quarterly |

## 4.10.0.0.0 Compliance Frameworks

### 4.10.1.0.0 Framework

#### 4.10.1.1.0 Framework

soc2

#### 4.10.1.2.0 Applicable Environments

- Production

#### 4.10.1.3.0 Controls

- Change Management Approval Gates
- Immutable Audit Logs
- Strict Access Control

#### 4.10.1.4.0 Audit Frequency

Annual

### 4.10.2.0.0 Framework

#### 4.10.2.1.0 Framework

gdpr

#### 4.10.2.2.0 Applicable Environments

- Production

#### 4.10.2.3.0 Controls

- Data Residency Configuration
- PII Encryption and Masking
- Data Subject Access Request (DSAR) procedures

#### 4.10.2.4.0 Audit Frequency

Annual

# 5.0.0.0.0 Network Design

## 5.1.0.0.0 Network Segmentation

### 5.1.1.0.0 Environment

#### 5.1.1.1.0 Environment

All

#### 5.1.1.2.0 Segment Type

private

#### 5.1.1.3.0 Purpose

EKS worker nodes, RDS database, ElastiCache cluster

#### 5.1.1.4.0 Isolation

virtual

### 5.1.2.0.0 Environment

#### 5.1.2.1.0 Environment

All

#### 5.1.2.2.0 Segment Type

public

#### 5.1.2.3.0 Purpose

Application Load Balancers (ALBs) and NAT Gateways

#### 5.1.2.4.0 Isolation

virtual

## 5.2.0.0.0 Subnet Strategy

### 5.2.1.0.0 Environment

#### 5.2.1.1.0 Environment

Production

#### 5.2.1.2.0 Subnet Type

private

#### 5.2.1.3.0 Cidr Block

10.0.1.0/24

#### 5.2.1.4.0 Availability Zone

us-east-1a

#### 5.2.1.5.0 Routing Table

private-rt-a

### 5.2.2.0.0 Environment

#### 5.2.2.1.0 Environment

Production

#### 5.2.2.2.0 Subnet Type

public

#### 5.2.2.3.0 Cidr Block

10.0.101.0/24

#### 5.2.2.4.0 Availability Zone

us-east-1a

#### 5.2.2.5.0 Routing Table

public-rt

## 5.3.0.0.0 Security Group Rules

### 5.3.1.0.0 Group Name

#### 5.3.1.1.0 Group Name

sg-prod-alb

#### 5.3.1.2.0 Direction

inbound

#### 5.3.1.3.0 Protocol

tcp

#### 5.3.1.4.0 Port Range

443

#### 5.3.1.5.0 Source

0.0.0.0/0

#### 5.3.1.6.0 Purpose

Allow HTTPS traffic from the internet to the load balancer.

### 5.3.2.0.0 Group Name

#### 5.3.2.1.0 Group Name

sg-prod-eks-nodes

#### 5.3.2.2.0 Direction

inbound

#### 5.3.2.3.0 Protocol

tcp

#### 5.3.2.4.0 Port Range

1024-65535

#### 5.3.2.5.0 Source

sg-prod-alb

#### 5.3.2.6.0 Purpose

Allow traffic from the ALB to the application pods.

### 5.3.3.0.0 Group Name

#### 5.3.3.1.0 Group Name

sg-prod-rds

#### 5.3.3.2.0 Direction

inbound

#### 5.3.3.3.0 Protocol

tcp

#### 5.3.3.4.0 Port Range

5432

#### 5.3.3.5.0 Source

sg-prod-eks-nodes

#### 5.3.3.6.0 Purpose

Allow database connections only from the application layer.

## 5.4.0.0.0 Connectivity Requirements

- {'source': 'Private Subnet', 'destination': 'Internet (OpenAI, Stripe APIs)', 'protocol': 'https', 'bandwidth': '10 Gbps', 'latency': 'Low'}

## 5.5.0.0.0 Network Monitoring

- {'type': 'flow-logs', 'implementation': 'VPC Flow Logs enabled and sent to CloudWatch Logs for analysis.', 'alerting': True, 'retention': '90 days'}

## 5.6.0.0.0 Bandwidth Controls

*No items available*

## 5.7.0.0.0 Service Discovery

| Property | Value |
|----------|-------|
| Method | dns |
| Implementation | Kubernetes-native CoreDNS for intra-cluster commun... |
| Health Checks | ✅ |

## 5.8.0.0.0 Environment Communication

- {'sourceEnvironment': 'Production', 'targetEnvironment': 'DR', 'communicationType': 'replication', 'securityControls': ['VPC Peering', 'Encrypted traffic']}

# 6.0.0.0.0 Data Management Strategy

## 6.1.0.0.0 Data Isolation

- {'environment': 'All', 'isolationLevel': 'complete', 'method': 'separate-instances', 'justification': 'Each environment has its own dedicated RDS instance and S3 buckets to prevent data contamination.'}

## 6.2.0.0.0 Backup And Recovery

### 6.2.1.0.0 Environment

#### 6.2.1.1.0 Environment

Production

#### 6.2.1.2.0 Backup Frequency

Daily automated snapshots

#### 6.2.1.3.0 Retention Period

35 days

#### 6.2.1.4.0 Recovery Time Objective

4 hours

#### 6.2.1.5.0 Recovery Point Objective

15 minutes

#### 6.2.1.6.0 Testing Schedule

Quarterly

### 6.2.2.0.0 Environment

#### 6.2.2.1.0 Environment

Staging

#### 6.2.2.2.0 Backup Frequency

Weekly automated snapshots

#### 6.2.2.3.0 Retention Period

14 days

#### 6.2.2.4.0 Recovery Time Objective

24 hours

#### 6.2.2.5.0 Recovery Point Objective

24 hours

#### 6.2.2.6.0 Testing Schedule

Ad-hoc

## 6.3.0.0.0 Data Masking Anonymization

- {'environment': 'Staging/Testing', 'dataType': 'PII', 'maskingMethod': 'static', 'coverage': 'complete', 'compliance': ['GDPR']}

## 6.4.0.0.0 Migration Processes

- {'sourceEnvironment': 'Staging', 'targetEnvironment': 'Production', 'migrationMethod': 'Automated CI/CD-triggered schema migration scripts', 'validation': 'Post-deployment health checks and smoke tests.', 'rollbackPlan': "Execution of pre-validated 'down' migration scripts."}

## 6.5.0.0.0 Retention Policies

- {'environment': 'Production', 'dataType': 'AuditLog', 'retentionPeriod': '7 years', 'archivalMethod': 'S3 Glacier Deep Archive', 'complianceRequirement': 'REQ-FUN-005'}

## 6.6.0.0.0 Data Classification

- {'classification': 'restricted', 'handlingRequirements': ['Encryption at rest and in transit', 'Strict access controls'], 'accessControls': ['Limited to specific IAM roles'], 'environments': ['Production']}

## 6.7.0.0.0 Disaster Recovery

- {'environment': 'Production', 'drSite': 'Secondary AWS Region', 'replicationMethod': 'asynchronous', 'failoverTime': '< 4 hours (RTO)', 'testingFrequency': 'Quarterly'}

# 7.0.0.0.0 Monitoring And Observability

## 7.1.0.0.0 Monitoring Components

### 7.1.1.0.0 Component

#### 7.1.1.1.0 Component

apm

#### 7.1.1.2.0 Tool

OpenTelemetry with AWS X-Ray

#### 7.1.1.3.0 Implementation

SDK integrated into all backend services.

#### 7.1.1.4.0 Environments

- Staging
- Production

### 7.1.2.0.0 Component

#### 7.1.2.1.0 Component

infrastructure

#### 7.1.2.2.0 Tool

Prometheus/Grafana for EKS, CloudWatch for AWS services

#### 7.1.2.3.0 Implementation

Prometheus Operator in EKS, CloudWatch agent on nodes.

#### 7.1.2.4.0 Environments

- All

### 7.1.3.0.0 Component

#### 7.1.3.1.0 Component

logs

#### 7.1.3.2.0 Tool

AWS CloudWatch Logs

#### 7.1.3.3.0 Implementation

Structured logging via JSON format from all services.

#### 7.1.3.4.0 Environments

- All

## 7.2.0.0.0 Environment Specific Thresholds

### 7.2.1.0.0 Environment

#### 7.2.1.1.0 Environment

Production

#### 7.2.1.2.0 Metric

API p99 Latency

#### 7.2.1.3.0 Warning Threshold

500ms

#### 7.2.1.4.0 Critical Threshold

1000ms

#### 7.2.1.5.0 Justification

Aligned with performance NFRs and user experience expectations.

### 7.2.2.0.0 Environment

#### 7.2.2.1.0 Environment

Production

#### 7.2.2.2.0 Metric

SQS Queue Depth (AI Processing)

#### 7.2.2.3.0 Warning Threshold

100

#### 7.2.2.4.0 Critical Threshold

500

#### 7.2.2.5.0 Justification

Indicates a backlog in the critical SOW processing workflow.

### 7.2.3.0.0 Environment

#### 7.2.3.1.0 Environment

Staging

#### 7.2.3.2.0 Metric

API p99 Latency

#### 7.2.3.3.0 Warning Threshold

1000ms

#### 7.2.3.4.0 Critical Threshold

2000ms

#### 7.2.3.5.0 Justification

Higher tolerance for staging environment used for testing.

## 7.3.0.0.0 Metrics Collection

- {'category': 'application', 'metrics': ['api_latency_p95', 'api_error_rate_5xx', 'sow_processing_duration'], 'collectionInterval': 'On request', 'retention': '30 days high-resolution, 1 year aggregated'}

## 7.4.0.0.0 Health Check Endpoints

### 7.4.1.0.0 Component

#### 7.4.1.1.0 Component

API Service

#### 7.4.1.2.0 Endpoint

/health/ready

#### 7.4.1.3.0 Check Type

readiness

#### 7.4.1.4.0 Timeout

5s

#### 7.4.1.5.0 Frequency

15s

### 7.4.2.0.0 Component

#### 7.4.2.1.0 Component

AI Worker

#### 7.4.2.2.0 Endpoint

/health/live

#### 7.4.2.3.0 Check Type

liveness

#### 7.4.2.4.0 Timeout

10s

#### 7.4.2.5.0 Frequency

30s

## 7.5.0.0.0 Logging Configuration

### 7.5.1.0.0 Environment

#### 7.5.1.1.0 Environment

Production

#### 7.5.1.2.0 Log Level

info

#### 7.5.1.3.0 Destinations

- CloudWatch Logs

#### 7.5.1.4.0 Retention

90 days

#### 7.5.1.5.0 Sampling

1.0

### 7.5.2.0.0 Environment

#### 7.5.2.1.0 Environment

Development

#### 7.5.2.2.0 Log Level

debug

#### 7.5.2.3.0 Destinations

- CloudWatch Logs

#### 7.5.2.4.0 Retention

14 days

#### 7.5.2.5.0 Sampling

1.0

## 7.6.0.0.0 Escalation Policies

### 7.6.1.0.0 Environment

#### 7.6.1.1.0 Environment

Production

#### 7.6.1.2.0 Severity

critical

#### 7.6.1.3.0 Escalation Path

- On-call Engineer (PagerDuty)
- Engineering Lead
- Head of Engineering

#### 7.6.1.4.0 Timeouts

- 10m
- 15m

#### 7.6.1.5.0 Channels

- PagerDuty
- Slack

### 7.6.2.0.0 Environment

#### 7.6.2.1.0 Environment

Staging

#### 7.6.2.2.0 Severity

critical

#### 7.6.2.3.0 Escalation Path

- Engineering Team Channel

#### 7.6.2.4.0 Timeouts

*No items available*

#### 7.6.2.5.0 Channels

- Slack

## 7.7.0.0.0 Dashboard Configurations

- {'dashboardType': 'operational', 'audience': 'SRE/DevOps Team', 'refreshInterval': '1m', 'metrics': ['EKS Cluster CPU/Memory', 'RDS CPU/Connections', 'API Latency & Error Rates', 'SQS Queue Depth']}

# 8.0.0.0.0 Project Specific Environments

## 8.1.0.0.0 Environments

*No items available*

## 8.2.0.0.0 Configuration

*No data available*

## 8.3.0.0.0 Cross Environment Policies

*No items available*

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

Production Environment Foundational Infrastructure (VPC, EKS, RDS)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Large

### 9.1.5.0.0 Risk Level

high

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

CI/CD Pipeline Automation for Deployment

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- Production Environment Foundational Infrastructure (VPC, EKS, RDS)

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Disaster Recovery Site and Replication Setup

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- Production Environment Foundational Infrastructure (VPC, EKS, RDS)

### 9.3.4.0.0 Estimated Effort

Large

### 9.3.5.0.0 Risk Level

medium

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

Data Anonymization Pipeline for Lower Environments

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

*No items available*

### 9.4.4.0.0 Estimated Effort

Medium

### 9.4.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Cloud Misconfiguration leading to security vulnerability.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Use of Infrastructure as Code (Terraform) with static analysis (e.g., tfsec) in CI/CD, peer reviews for all infrastructure changes, and regular security audits.

### 10.1.5.0.0 Contingency Plan

Isolate affected resources, analyze impact using logs and monitoring, revert configuration change, and perform post-mortem.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Uncontrolled cloud spending due to auto-scaling or over-provisioning.

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

high

### 10.2.4.0.0 Mitigation

Implement strict budgets and billing alerts in AWS. Use cost-effective instance types (e.g., Graviton, Spot for stateless workloads). Regularly review costs with FinOps tools.

### 10.2.5.0.0 Contingency Plan

Manually scale down resources, analyze cost drivers, and adjust auto-scaling policies or instance types.

## 10.3.0.0.0 Risk

### 10.3.1.0.0 Risk

Failure to meet RTO/RPO during a disaster recovery event.

### 10.3.2.0.0 Impact

high

### 10.3.3.0.0 Probability

low

### 10.3.4.0.0 Mitigation

Automate the DR failover process as much as possible. Conduct mandatory, documented DR tests on a quarterly basis to validate procedures and timings.

### 10.3.5.0.0 Contingency Plan

Execute documented manual failover procedures. Post-event analysis to automate identified bottlenecks.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Security

### 11.1.2.0.0 Recommendation

Implement AWS Control Tower to establish a multi-account landing zone. This provides baseline security governance, identity management, and compliance across all environments from the start.

### 11.1.3.0.0 Justification

Automates the setup of a secure, multi-account AWS environment, making it easier to enforce policies, manage access, and meet compliance requirements like SOC 2.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

This should be one of the first infrastructure tasks performed, as it provides the foundation for all other environment provisioning.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Cost Optimization

### 11.2.2.0.0 Recommendation

Leverage AWS Graviton (ARM-based) instances for EKS worker nodes.

### 11.2.3.0.0 Justification

Graviton instances offer significantly better price-performance for many workloads, including Node.js applications, which can lead to substantial cost savings in compute.

### 11.2.4.0.0 Priority

🟡 medium

### 11.2.5.0.0 Implementation Notes

Requires building and pushing multi-architecture Docker images (for both amd64 and arm64) to the container registry.

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Resilience

### 11.3.2.0.0 Recommendation

Use RDS Proxy between the application services and the PostgreSQL database.

### 11.3.3.0.0 Justification

Improves resilience by pooling and sharing database connections, which is highly beneficial in a serverless or containerized environment with many connections. It also enables faster and more transparent failovers for the Multi-AZ database.

### 11.3.4.0.0 Priority

🟡 medium

### 11.3.5.0.0 Implementation Notes

Requires an additional component to be provisioned via Terraform and application connection strings to be updated.


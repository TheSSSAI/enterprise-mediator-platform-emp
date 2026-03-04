# 1 System Overview

## 1.1 Analysis Date

2023-10-27

## 1.2 Technology Stack

- React (Next.js)
- NestJS (TypeScript)
- PostgreSQL (pgvector)
- Docker
- Kubernetes (AWS EKS)
- Terraform
- GitHub Actions

## 1.3 Architecture Patterns

- Modular Monolith / Microservices
- Cloud-Hosted (AWS)
- Infrastructure-as-Code

## 1.4 Data Sensitivity

- PII
- Financial Transaction Data

## 1.5 Regulatory Considerations

- GDPR
- SOC 2

## 1.6 System Criticality

business-critical

# 2.0 Build And Test Automation

## 2.1 Build Stages

### 2.1.1 Backend Build (NestJS)

#### 2.1.1.1 Type

🔹 Backend Build (NestJS)

#### 2.1.1.2 Applicable Components

- User Service
- Project Service
- Payment Service

#### 2.1.1.3 Steps

1. Install NPM dependencies. 2. Compile TypeScript to JavaScript. 3. Package into a distributable format.

#### 2.1.1.4 Trigger

On push to `main` or pull request to `main`.

#### 2.1.1.5 Justification

Creates a production-ready build of the backend application.

### 2.1.2.0 Frontend Build (Next.js)

#### 2.1.2.1 Type

🔹 Frontend Build (Next.js)

#### 2.1.2.2 Applicable Components

- Web Application

#### 2.1.2.3 Steps

1. Install NPM dependencies. 2. Build the Next.js application (static export or hybrid). 3. Optimize assets.

#### 2.1.2.4 Trigger

On push to `main` or pull request to `main`.

#### 2.1.2.5 Justification

Creates optimized, static assets and/or server components for the user interface.

### 2.1.3.0 Docker Image Build

#### 2.1.3.1 Type

🔹 Docker Image Build

#### 2.1.3.2 Applicable Components

- Backend Services
- Frontend Application

#### 2.1.3.3 Steps

1. Use multi-stage Dockerfiles for optimization. 2. Copy build artifacts into a lean production base image (e.g., node:20-alpine). 3. Tag image with Git commit SHA and version.

#### 2.1.3.4 Trigger

After successful build and test stages.

#### 2.1.3.5 Justification

Creates immutable, portable artifacts for deployment, as required by REQ-DEP-001 (containerization).

## 2.2.0.0 Test Execution

### 2.2.1.0 Test Type

#### 2.2.1.1 Test Type

Unit & Integration Tests

#### 2.2.1.2 Framework

Jest

#### 2.2.1.3 Execution Point

Post-build, within the CI pipeline for both frontend and backend.

#### 2.2.1.4 Parallelism

Tests are run in parallel by Jest workers.

#### 2.2.1.5 Justification

Fulfills REQ-NFR-006 by running unit and integration tests and enforcing code coverage gates on every commit.

### 2.2.2.0 Test Type

#### 2.2.2.1 Test Type

End-to-End (E2E) Tests

#### 2.2.2.2 Framework

Playwright

#### 2.2.2.3 Execution Point

Runs against a deployed Staging environment after a successful deployment.

#### 2.2.2.4 Parallelism

Playwright can run tests in parallel across multiple browser contexts.

#### 2.2.2.5 Justification

Validates critical user workflows in an integrated, production-like environment as per REQ-NFR-006.

## 2.3.0.0 Code Coverage

| Property | Value |
|----------|-------|
| Tool | Jest Coverage Reporter |
| Minimum Threshold | 80% |
| Enforcement | The pipeline build will fail if coverage drops bel... |
| Reporting | Coverage reports are generated and archived as bui... |
| Justification | Directly implements the code coverage enforcement ... |

# 3.0.0.0 Code Quality And Security

## 3.1.0.0 Static Analysis

- {'tool': 'ESLint', 'stage': 'Pre-build', 'purpose': 'Enforce code style and identify common programming errors in TypeScript code.', 'blocking': True}

## 3.2.0.0 Security Scanning

### 3.2.1.0 Scan Type

#### 3.2.1.1 Scan Type

Static Application Security Testing (SAST)

#### 3.2.1.2 Tool

Snyk Code / SonarQube

#### 3.2.1.3 Stage

Post-build, before artifact creation.

#### 3.2.1.4 Purpose

Scan application source code for security vulnerabilities like OWASP Top 10.

#### 3.2.1.5 Blocking

Blocks pipeline on 'High' or 'Critical' severity findings.

#### 3.2.1.6 Justification

Implements the SAST scanning requirement from REQ-NFR-003.

### 3.2.2.0 Scan Type

#### 3.2.2.1 Scan Type

Software Composition Analysis (SCA)

#### 3.2.2.2 Tool

Snyk Open Source / OWASP Dependency-Check

#### 3.2.2.3 Stage

After dependency installation.

#### 3.2.2.4 Purpose

Scan third-party libraries (NPM packages) for known vulnerabilities (CVEs).

#### 3.2.2.5 Blocking

Blocks pipeline on 'High' or 'Critical' severity findings.

#### 3.2.2.6 Justification

Addresses dependency scanning part of REQ-NFR-003 to secure the software supply chain.

## 3.3.0.0 Secret Scanning

| Property | Value |
|----------|-------|
| Tool | Git-secrets / TruffleHog |
| Stage | Pre-commit hook and in-pipeline on every commit. |
| Purpose | Prevent hardcoded secrets (API keys, credentials) ... |
| Justification | Supports REQ-NFR-003 by ensuring secrets are manag... |

# 4.0.0.0 Deployment Strategy

## 4.1.0.0 Deployment Patterns

### 4.1.1.0 Pattern

#### 4.1.1.1 Pattern

Blue-Green Deployment

#### 4.1.1.2 Environment

Production

#### 4.1.1.3 Implementation

Using Kubernetes (EKS). A new version ('green') is deployed alongside the current version ('blue'). Once the 'green' version passes health checks and E2E tests, Kubernetes Service traffic is switched from 'blue' to 'green'.

#### 4.1.1.4 Justification

Fulfills REQ-NFR-010 for zero or minimal downtime deployments and provides a rapid rollback mechanism by simply switching traffic back to 'blue'.

### 4.1.2.0 Pattern

#### 4.1.2.1 Pattern

Infrastructure as Code (IaC)

#### 4.1.2.2 Tool

Terraform

#### 4.1.2.3 Workflow

A dedicated pipeline runs 'terraform plan' on pull requests and requires manual approval to run 'terraform apply' on the `main` branch.

#### 4.1.2.4 Justification

Manages AWS infrastructure in a repeatable and version-controlled manner, as required by REQ-DEP-001 for managing Dev, Staging, and Production environments.

## 4.2.0.0 Environment Promotion

### 4.2.1.0 Source Env

#### 4.2.1.1 Source Env

Development

#### 4.2.1.2 Target Env

Staging

#### 4.2.1.3 Trigger

Successful deployment to Development and passing of all automated checks.

#### 4.2.1.4 Approval

Automated

#### 4.2.1.5 Artifact Promotion

The Docker image tag is promoted without rebuilding.

### 4.2.2.0 Source Env

#### 4.2.2.1 Source Env

Staging

#### 4.2.2.2 Target Env

Production

#### 4.2.2.3 Trigger

Successful deployment to Staging, passing of all E2E tests, and QA sign-off.

#### 4.2.2.4 Approval

Manual (via GitHub Actions environment protection rule)

#### 4.2.2.5 Artifact Promotion

The Docker image tag from Staging is promoted to Production.

## 4.3.0.0 Configuration Management

| Property | Value |
|----------|-------|
| Method | Environment variables injected into containers at ... |
| Secret Store | AWS Secrets Manager |
| Variable Store | AWS Systems Manager Parameter Store |
| Justification | Decouples configuration from the application artif... |

# 5.0.0.0 Quality Gates And Approvals

## 5.1.0.0 Automated Gates

### 5.1.1.0 Gate

#### 5.1.1.1 Gate

Unit & Integration Test Success

#### 5.1.1.2 Stage

CI

#### 5.1.1.3 Description

All tests must pass. Failure blocks the pipeline.

#### 5.1.1.4 Requirement

REQ-NFR-006

### 5.1.2.0 Gate

#### 5.1.2.1 Gate

Code Coverage Check

#### 5.1.2.2 Stage

CI

#### 5.1.2.3 Description

Code coverage must be >= 80%. Failure blocks the pipeline.

#### 5.1.2.4 Requirement

REQ-NFR-006

### 5.1.3.0 Gate

#### 5.1.3.1 Gate

Security Scan Pass

#### 5.1.3.2 Stage

CI

#### 5.1.3.3 Description

SAST and SCA scans must not report any 'Critical' or 'High' vulnerabilities.

#### 5.1.3.4 Requirement

REQ-NFR-003

### 5.1.4.0 Gate

#### 5.1.4.1 Gate

E2E Test Success

#### 5.1.4.2 Stage

CD (Post-Staging-Deploy)

#### 5.1.4.3 Description

All Playwright E2E tests against the Staging environment must pass before enabling promotion to Production.

#### 5.1.4.4 Requirement

REQ-NFR-006

## 5.2.0.0 Manual Approvals

### 5.2.1.0 Gate

#### 5.2.1.1 Gate

Production Deployment Approval

#### 5.2.1.2 Environment

Production

#### 5.2.1.3 Approvers

Lead Engineer / DevOps Team

#### 5.2.1.4 Justification

Provides a final human checkpoint to prevent accidental or unauthorized deployments to the live environment, aligning with SOC 2 change management controls (REQ-BUS-003).

### 5.2.2.0 Gate

#### 5.2.2.1 Gate

Infrastructure Change Approval

#### 5.2.2.2 Environment

All

#### 5.2.2.3 Approvers

Lead DevOps Engineer

#### 5.2.2.4 Justification

Ensures all infrastructure changes from Terraform are reviewed and understood before application to prevent costly or disruptive misconfigurations.

# 6.0.0.0 Artifact Management

## 6.1.0.0 Artifact Repository

| Property | Value |
|----------|-------|
| Type | Container Registry |
| Technology | AWS ECR (Elastic Container Registry) |
| Usage | Stores all Docker images for the frontend and back... |

## 6.2.0.0 Versioning Strategy

| Property | Value |
|----------|-------|
| Scheme | Semantic Versioning + Git Commit SHA |
| Example | 1.2.3-a1b2c3d |
| Justification | Provides both a human-readable version and a preci... |

## 6.3.0.0 Immutability

| Property | Value |
|----------|-------|
| Principle | Artifacts (Docker images) are built once and promo... |
| Enforcement | AWS ECR is configured to disallow image tag overwr... |
| Justification | Ensures that the exact code tested in Staging is w... |

## 6.4.0.0 Retention Policy

### 6.4.1.0 Policy

ECR Lifecycle Policies will be used to automatically clean up old, untagged, or development images after 30 days to manage costs.

### 6.4.2.0 Production Retention

Images tagged for production releases are retained indefinitely.

# 7.0.0.0 Rollback And Recovery

## 7.1.0.0 Application Rollback

| Property | Value |
|----------|-------|
| Method | Activate Previous Blue Deployment |
| Trigger | Automated trigger on critical monitoring alerts (e... |
| Procedure | The Kubernetes Service selector is updated to poin... |
| Rto | < 5 minutes |
| Justification | Leverages the Blue-Green deployment strategy for n... |

## 7.2.0.0 Database Rollback

| Property | Value |
|----------|-------|
| Method | Execute 'Down' Migration Scripts |
| Trigger | Manual, as a last resort in case of a critical bug... |
| Procedure | Database migration tool (e.g., TypeORM CLI, Flyway... |
| Notes | This is a high-risk operation. The primary strateg... |

# 8.0.0.0 Project Specific Pipelines

## 8.1.0.0 Pipeline Name

### 8.1.1.0 Pipeline Name

Backend CI/CD Pipeline (NestJS)

### 8.1.2.0 Trigger

Push to `main`, `develop`, or on Pull Request

### 8.1.3.0 Stages

- Checkout Code
- Install NPM Dependencies
- Run Linting & Code Formatting Checks
- Run Unit & Integration Tests (with 80% coverage gate)
- Run SCA Scan (Snyk)
- Run SAST Scan (Snyk)
- Build Docker Image
- Push Docker Image to AWS ECR
- Deploy to Development Environment
- Promote to Staging Environment (auto)
- Run E2E Tests against Staging
- Manual Approval Gate for Production
- Deploy to Production (Blue-Green)

## 8.2.0.0 Pipeline Name

### 8.2.1.0 Pipeline Name

Frontend CI/CD Pipeline (Next.js)

### 8.2.2.0 Trigger

Push to `main`, `develop`, or on Pull Request

### 8.2.3.0 Stages

- Checkout Code
- Install NPM Dependencies
- Run Linting
- Run Unit Tests
- Run SCA Scan (Snyk)
- Run SAST Scan (Snyk)
- Build Application & Docker Image
- Push Docker Image to AWS ECR
- Deploy to Development Environment
- Promote to Staging Environment (auto)
- Manual Approval Gate for Production
- Deploy to Production (Blue-Green)

## 8.3.0.0 Pipeline Name

### 8.3.1.0 Pipeline Name

Infrastructure IaC Pipeline (Terraform)

### 8.3.2.0 Trigger

Pull Request to `main` or merge to `main`

### 8.3.3.0 Stages

- Checkout Code
- Terraform Init & Validate
- Terraform Plan (on PR)
- Manual Approval Gate for Apply (on `main`)
- Terraform Apply (on `main`)

# 9.0.0.0 Risk Assessment

## 9.1.0.0 Risk

### 9.1.1.0 Risk

Pipeline Flakiness

### 9.1.2.0 Impact

medium

### 9.1.3.0 Probability

medium

### 9.1.4.0 Mitigation

Isolate E2E tests to run against a stable Staging environment, not within the main CI build. Implement retry logic for transient network failures during dependency downloads or deployments.

### 9.1.5.0 Contingency Plan

Provide mechanism for developers to re-run failed stages. Monitor pipeline success rate as a key metric.

## 9.2.0.0 Risk

### 9.2.1.0 Risk

Slow Pipeline Execution

### 9.2.2.0 Impact

medium

### 9.2.3.0 Probability

high

### 9.2.4.0 Mitigation

Utilize dependency caching (NPM packages, Docker layers). Run test, lint, and scan stages in parallel where possible. Use self-hosted runners with adequate resources.

### 9.2.5.0 Contingency Plan

Regularly profile pipeline stages to identify bottlenecks. Split monolithic pipelines into smaller, more focused workflows if necessary.


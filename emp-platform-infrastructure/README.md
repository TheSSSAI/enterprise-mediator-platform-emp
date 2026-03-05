# Enterprise Mediator Platform (EMP) - Infrastructure

This repository contains the Infrastructure as Code (IaC) definitions for the Enterprise Mediator Platform. It leverages Terraform to provision and manage AWS resources in a modular, scalable, and secure manner.

## 🏗 Architecture Overview

The infrastructure is designed as a modular monolith deployed on AWS, adhering to SOC 2 compliance requirements and high-availability standards (99.9% uptime).

### Core Components
- **Compute**: Amazon EKS (Elastic Kubernetes Service) with Managed Node Groups.
- **Networking**: VPC across multiple Availability Zones with public/private subnet isolation.
- **Database**: Amazon RDS for PostgreSQL with `pgvector` extension for semantic search support.
- **Messaging**: Amazon SQS for asynchronous workflows (SOW processing) and SNS for notifications.
- **Storage**: Amazon S3 for document storage (SOWs, artifacts) with encryption at rest.
- **Caching**: Amazon ElastiCache (Redis) for application caching.
- **Identity**: AWS Cognito for user authentication and IAM for service authorization (IRSA).
- **Security**: AWS KMS for encryption keys, Security Groups for network isolation.

## 📂 Repository Structure

```
.
├── environments/           # Environment-specific instantiations
│   ├── dev/                # Development environment configuration
│   └── prod/               # Production environment configuration
├── modules/                # Reusable Terraform modules
│   ├── eks/                # EKS Cluster and Node Groups
│   ├── elasticache/        # Redis Cluster
│   ├── kms/                # Key Management Service
│   ├── rds/                # PostgreSQL Database
│   ├── s3/                 # Storage Buckets
│   ├── ses/                # Email Service
│   ├── sqs/                # Queues
│   └── vpc/                # Networking
├── backend.tf              # Backend configuration template
├── main.tf                 # Root module entry point
├── Makefile                # Automation commands
├── variables.tf            # Global variables
└── versions.tf             # Provider and Terraform version constraints
```

## 🚀 Getting Started

### Prerequisites
- [Terraform](https://www.terraform.io/downloads.html) (v1.5+)
- [AWS CLI](https://aws.amazon.com/cli/) (v2+)
- [TFLint](https://github.com/terraform-linters/tflint)
- [Checkov](https://www.checkov.io/)

### AWS Authentication
Ensure you have active AWS credentials in your environment:
```bash
export AWS_PROFILE=emp-dev
# or
export AWS_ACCESS_KEY_ID=...
export AWS_SECRET_ACCESS_KEY=...
```

### Usage

This project uses a `Makefile` to simplify common operations.

**1. Initialize the environment:**
```bash
make init ENV=dev
```

**2. format and Validate code:**
```bash
make fmt
make validate ENV=dev
```

**3. Run Security Scans:**
```bash
make sec
```

**4. Plan Deployment:**
```bash
make plan ENV=dev
```

**5. Apply Deployment:**
```bash
make apply ENV=dev
```

## 🔐 Security & Compliance

### Policy Enforcement
- **Encryption**: All data at rest is encrypted using AWS KMS Customer Managed Keys (CMK).
- **Networking**: All compute resources reside in private subnets. No direct public access to databases.
- **IAM**: Least privilege principle applied via IAM Roles for Service Accounts (IRSA).

### Automated Scanning
We enforce security checks in the CI/CD pipeline using:
- **Checkov**: Static code analysis for IaC security misconfigurations.
- **TFLint**: Linter for Terraform code quality.

## 🛠 Module Development

When creating or modifying modules:
1.  Define inputs in `variables.tf` with descriptions and types.
2.  Define outputs in `outputs.tf` for consumption by other modules.
3.  Pin provider versions in `versions.tf`.
4.  Run `make fmt` and `make lint` before committing.

## 📝 License

Copyright © 2025 Enterprise Mediator Platform. All rights reserved.
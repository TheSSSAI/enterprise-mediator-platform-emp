# ---------------------------------------------------------------------------------------------------------------------
# ROOT MAIN.TF
# This is the primary composition root for the EMP infrastructure.
# It instantiates all modules defined in the 'modules/' directory to provision
# the complete platform infrastructure.
# ---------------------------------------------------------------------------------------------------------------------

terraform {
  required_version = ">= 1.5.0"
  backend "s3" {} # Backend configuration is injected via -backend-config or environments/dev/backend.conf
}

provider "aws" {
  region = var.aws_region

  default_tags {
    tags = local.common_tags
  }
}

locals {
  project_name = var.project_name
  environment  = var.environment
  
  common_tags = {
    Project     = local.project_name
    Environment = local.environment
    ManagedBy   = "Terraform"
    Owner       = "SRE-Team"
  }
}

# ---------------------------------------------------------------------------------------------------------------------
# 1. KEY MANAGEMENT SERVICE (KMS)
# Provisions Customer Managed Keys (CMKs) for encrypting data at rest across all services.
# ---------------------------------------------------------------------------------------------------------------------
module "kms" {
  source = "./modules/kms"

  project_name = local.project_name
  environment  = local.environment
  
  # Enable key rotation for compliance (SOC 2)
  enable_key_rotation = true
  
  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 2. VIRTUAL PRIVATE CLOUD (VPC)
# Provisions the networking layer including Subnets, NAT Gateways, and Route Tables.
# ---------------------------------------------------------------------------------------------------------------------
module "vpc" {
  source = "./modules/vpc"

  project_name = local.project_name
  environment  = local.environment
  
  vpc_cidr           = var.vpc_cidr
  availability_zones = var.availability_zones
  
  # Subnetting strategy
  public_subnet_cidrs   = var.public_subnet_cidrs
  private_subnet_cidrs  = var.private_subnet_cidrs
  database_subnet_cidrs = var.database_subnet_cidrs
  
  # NAT Gateway for private subnet outbound access (Required for EKS nodes)
  enable_nat_gateway = true
  single_nat_gateway = var.environment != "prod" # Cost savings for non-prod
  
  # EKS Requirement: DNS Hostnames support
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 3. SIMPLE STORAGE SERVICE (S3)
# Provisions buckets for SOW documents, logs, and artifacts.
# ---------------------------------------------------------------------------------------------------------------------
module "s3" {
  source = "./modules/s3"

  project_name = local.project_name
  environment  = local.environment
  
  # Secure buckets with KMS encryption
  kms_key_arn = module.kms.s3_key_arn
  
  # Bucket names derived from naming convention
  sow_bucket_name     = "${local.project_name}-${local.environment}-sow-documents"
  logs_bucket_name    = "${local.project_name}-${local.environment}-access-logs"
  artifacts_bucket_name = "${local.project_name}-${local.environment}-build-artifacts"
  
  force_destroy = var.environment != "prod"

  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 4. SIMPLE QUEUE SERVICE (SQS)
# Provisions message queues for asynchronous workflows (SOW processing, Payments).
# ---------------------------------------------------------------------------------------------------------------------
module "sqs" {
  source = "./modules/sqs"

  project_name = local.project_name
  environment  = local.environment
  
  # Queue configurations
  sow_upload_queue_name = "${local.project_name}-${local.environment}-sow-upload"
  payment_events_queue_name = "${local.project_name}-${local.environment}-payment-events"
  notification_queue_name   = "${local.project_name}-${local.environment}-notifications"
  
  # Encryption
  kms_key_id = module.kms.sqs_key_arn
  
  # Reliability configuration
  visibility_timeout_seconds = 300 # 5 minutes for processing
  message_retention_seconds  = 345600 # 4 days
  
  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 5. RELATIONAL DATABASE SERVICE (RDS)
# Provisions PostgreSQL database for core application data and vector embeddings.
# ---------------------------------------------------------------------------------------------------------------------
module "rds" {
  source = "./modules/rds"

  project_name = local.project_name
  environment  = local.environment
  
  vpc_id             = module.vpc.vpc_id
  subnet_ids         = module.vpc.database_subnets
  vpc_cidr_block     = module.vpc.vpc_cidr_block
  
  # Database Configuration
  engine               = "postgres"
  engine_version       = "16.1"
  instance_class       = var.db_instance_class
  allocated_storage    = var.db_allocated_storage
  db_name              = var.db_name
  username             = var.db_username
  password             = [REDACTED] # Injected via TF_VAR_db_password or Secrets Manager
  
  # High Availability & Security
  multi_az             = var.multi_az
  kms_key_arn          = module.kms.rds_key_arn
  storage_encrypted    = true
  deletion_protection  = var.environment == "prod"
  
  # Networking
  allowed_security_groups = [module.eks.cluster_security_group_id]
  
  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 6. ELASTICACHE (REDIS)
# Provisions Redis for application caching and session storage.
# ---------------------------------------------------------------------------------------------------------------------
module "elasticache" {
  source = "./modules/elasticache"

  project_name = local.project_name
  environment  = local.environment
  
  vpc_id             = module.vpc.vpc_id
  subnet_ids         = module.vpc.private_subnets # Place cache in private app subnets
  vpc_cidr_block     = module.vpc.vpc_cidr_block
  
  node_type          = var.cache_node_type
  num_cache_nodes    = var.cache_num_nodes
  engine_version     = "7.1"
  
  # Access Control
  allowed_security_groups = [module.eks.cluster_security_group_id]
  
  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 7. ELASTIC KUBERNETES SERVICE (EKS)
# Provisions the container orchestration platform for all microservices.
# ---------------------------------------------------------------------------------------------------------------------
module "eks" {
  source = "./modules/eks"

  project_name = local.project_name
  environment  = local.environment
  
  vpc_id          = module.vpc.vpc_id
  subnet_ids      = module.vpc.private_subnets
  
  cluster_version = var.eks_cluster_version
  
  # Node Group Configuration defined in variables
  node_groups     = var.eks_node_groups
  
  # Encryption
  kms_key_arn     = module.kms.eks_key_arn
  
  # Admin Access
  cluster_endpoint_public_access = true # Allow access from internet (restricted by CIDR in Prod)
  cluster_endpoint_private_access = true
  
  tags = local.common_tags
}

# ---------------------------------------------------------------------------------------------------------------------
# 8. SIMPLE EMAIL SERVICE (SES)
# Configures email identity for transactional emails.
# ---------------------------------------------------------------------------------------------------------------------
module "ses" {
  source = "./modules/ses"

  project_name = local.project_name
  environment  = local.environment
  
  domain_name  = var.domain_name
  
  # Route53 Zone for DKIM verification
  route53_zone_id = var.route53_zone_id
  
  tags = local.common_tags
}
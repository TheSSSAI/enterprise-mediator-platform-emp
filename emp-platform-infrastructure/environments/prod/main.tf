# ---------------------------------------------------------------------------------------------------------------------
# PRODUCTION ENVIRONMENT CONFIGURATION
# This file instantiates the infrastructure modules specifically for the Production environment.
# It enforces high availability, stronger security controls, and resource isolation.
# ---------------------------------------------------------------------------------------------------------------------

terraform {
  required_version = ">= 1.5.0"
  
  # Backend configuration should be supplied via -backend-config="backend.conf"
  backend "s3" {}
}

provider "aws" {
  region = var.aws_region

  default_tags {
    tags = local.common_tags
  }
}

locals {
  project_name = var.project_name
  environment  = "prod"
  
  common_tags = {
    Project     = local.project_name
    Environment = local.environment
    ManagedBy   = "Terraform"
    Owner       = "Platform-Team"
    CostCenter  = "Production-Infrastructure"
  }
}

# ---------------------------------------------------------------------------------------------------------------------
# MODULE INSTANTIATIONS
# Referencing modules from the root modules directory using relative paths.
# ---------------------------------------------------------------------------------------------------------------------

module "kms" {
  source = "../../modules/kms"

  project_name        = local.project_name
  environment         = local.environment
  enable_key_rotation = true
  tags                = local.common_tags
}

module "vpc" {
  source = "../../modules/vpc"

  project_name          = local.project_name
  environment           = local.environment
  vpc_cidr              = var.vpc_cidr
  availability_zones    = var.availability_zones
  public_subnet_cidrs   = var.public_subnet_cidrs
  private_subnet_cidrs  = var.private_subnet_cidrs
  database_subnet_cidrs = var.database_subnet_cidrs
  
  # Production: Highly Available Networking
  enable_nat_gateway    = true
  single_nat_gateway    = false # One NAT Gateway per AZ for HA
  enable_dns_hostnames  = true
  enable_dns_support    = true

  tags = local.common_tags
}

module "s3" {
  source = "../../modules/s3"

  project_name          = local.project_name
  environment           = local.environment
  kms_key_arn           = module.kms.s3_key_arn
  
  sow_bucket_name       = "emp-prod-sow-documents"
  logs_bucket_name      = "emp-prod-access-logs"
  artifacts_bucket_name = "emp-prod-artifacts"
  
  # Production: Prevent accidental data loss
  force_destroy         = false
  versioning_enabled    = true

  tags = local.common_tags
}

module "sqs" {
  source = "../../modules/sqs"

  project_name              = local.project_name
  environment               = local.environment
  sow_upload_queue_name     = "emp-prod-sow-upload"
  payment_events_queue_name = "emp-prod-payment-events"
  notification_queue_name   = "emp-prod-notifications"
  kms_key_id                = module.kms.sqs_key_arn
  
  visibility_timeout_seconds = 300
  message_retention_seconds  = 1209600 # 14 days for Prod

  tags = local.common_tags
}

module "rds" {
  source = "../../modules/rds"

  project_name       = local.project_name
  environment        = local.environment
  vpc_id             = module.vpc.vpc_id
  subnet_ids         = module.vpc.database_subnets
  vpc_cidr_block     = module.vpc.vpc_cidr_block
  
  engine             = "postgres"
  engine_version     = "16.1"
  instance_class     = var.db_instance_class
  allocated_storage  = var.db_allocated_storage
  db_name            = var.db_name
  username           = var.db_username
  password           = [REDACTED]
  
  # Production: High Availability
  multi_az           = true
  kms_key_arn        = module.kms.rds_key_arn
  storage_encrypted  = true
  deletion_protection = true
  backup_retention_period = 30
  
  allowed_security_groups = [module.eks.cluster_security_group_id]

  tags = local.common_tags
}

module "elasticache" {
  source = "../../modules/elasticache"

  project_name       = local.project_name
  environment        = local.environment
  vpc_id             = module.vpc.vpc_id
  subnet_ids         = module.vpc.private_subnets
  vpc_cidr_block     = module.vpc.vpc_cidr_block
  
  node_type          = var.cache_node_type
  num_cache_nodes    = var.cache_num_nodes
  engine_version     = "7.1"
  
  # Production: Automatic Failover
  automatic_failover_enabled = true
  multi_az_enabled           = true
  
  allowed_security_groups = [module.eks.cluster_security_group_id]

  tags = local.common_tags
}

module "eks" {
  source = "../../modules/eks"

  project_name    = local.project_name
  environment     = local.environment
  vpc_id          = module.vpc.vpc_id
  subnet_ids      = module.vpc.private_subnets
  
  cluster_version = var.eks_cluster_version
  node_groups     = var.eks_node_groups
  kms_key_arn     = module.kms.eks_key_arn
  
  # Production: Restricted Access
  cluster_endpoint_public_access  = true
  cluster_endpoint_public_access_cidrs = var.admin_cidr_blocks # Restrict API access
  cluster_endpoint_private_access = true

  tags = local.common_tags
}

module "ses" {
  source = "../../modules/ses"

  project_name    = local.project_name
  environment     = local.environment
  domain_name     = var.domain_name
  route53_zone_id = var.route53_zone_id

  tags = local.common_tags
}
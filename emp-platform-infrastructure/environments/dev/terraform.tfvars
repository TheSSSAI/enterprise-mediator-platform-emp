# ---------------------------------------------------------------------------------------------------------------------
# DEVELOPMENT ENVIRONMENT VARIABLES
# Configuration for the 'dev' environment.
# Optimized for cost savings while maintaining functional parity with production.
# ---------------------------------------------------------------------------------------------------------------------

project_name = "emp-platform"
environment  = "dev"
aws_region   = "us-east-1"

# Network Configuration
vpc_cidr              = "10.0.0.0/16"
availability_zones    = ["us-east-1a", "us-east-1b"] # Reduced AZs for dev
public_subnet_cidrs   = ["10.0.1.0/24", "10.0.2.0/24"]
private_subnet_cidrs  = ["10.0.3.0/24", "10.0.4.0/24"]
database_subnet_cidrs = ["10.0.5.0/24", "10.0.6.0/24"]

# Database Configuration (Cost Optimized)
db_instance_class    = "db.t3.medium"
db_allocated_storage = 20
db_name              = "emp_dev_db"
db_username          = "emp_admin"
# db_password provided via env var or secrets manager
multi_az             = false # Single AZ for dev to save costs

# Cache Configuration
cache_node_type = "cache.t3.micro"
cache_num_nodes = 1

# EKS Configuration
eks_cluster_version = "1.29"
eks_node_groups = {
  general = {
    desired_size = 2
    min_size     = 1
    max_size     = 3
    instance_types = ["t3.medium"]
    capacity_type  = "ON_DEMAND"
  }
}

# Domain Configuration
domain_name     = "dev.emp-platform.com"
route53_zone_id = "Z0123456789ABCDEF" # Placeholder ID

# Access Control
admin_cidr_blocks = ["0.0.0.0/0"] # Open access for dev convenience (or restrict to VPN)
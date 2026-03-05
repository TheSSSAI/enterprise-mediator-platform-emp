# ---------------------------------------------------------------------------------------------------------------------
# PRODUCTION ENVIRONMENT VARIABLES
# Configuration for the 'prod' environment.
# Optimized for high availability, performance, and security compliance.
# ---------------------------------------------------------------------------------------------------------------------

project_name = "emp-platform"
environment  = "prod"
aws_region   = "us-east-1"

# Network Configuration
vpc_cidr              = "10.10.0.0/16"
availability_zones    = ["us-east-1a", "us-east-1b", "us-east-1c"] # 3 AZs for HA
public_subnet_cidrs   = ["10.10.1.0/24", "10.10.2.0/24", "10.10.3.0/24"]
private_subnet_cidrs  = ["10.10.10.0/24", "10.10.11.0/24", "10.10.12.0/24"]
database_subnet_cidrs = ["10.10.20.0/24", "10.10.21.0/24", "10.10.22.0/24"]

# Database Configuration (Performance & Reliability)
db_instance_class    = "db.m6g.large" # General purpose, production grade
db_allocated_storage = 100
db_name              = "emp_prod_db"
db_username          = "emp_admin"
# db_password provided via env var or secrets manager
multi_az             = true # Required for SLA

# Cache Configuration
cache_node_type = "cache.m6g.large"
cache_num_nodes = 2 # Primary + Replica

# EKS Configuration
eks_cluster_version = "1.29"
eks_node_groups = {
  system_workloads = {
    desired_size = 2
    min_size     = 2
    max_size     = 4
    instance_types = ["m6g.large"]
    capacity_type  = "ON_DEMAND"
    labels = {
      role = "system"
    }
  }
  application_workloads = {
    desired_size = 3
    min_size     = 3
    max_size     = 10
    instance_types = ["m6g.xlarge"]
    capacity_type  = "ON_DEMAND" # Consistent performance
    labels = {
      role = "application"
    }
  }
}

# Domain Configuration
domain_name     = "app.emp-platform.com"
route53_zone_id = "Z0987654321FEDCBA" # Placeholder ID

# Access Control
# Strictly restrict control plane access to corporate VPN IPs
admin_cidr_blocks = [
  "203.0.113.0/24", # Corporate VPN
  "198.51.100.0/24" # Office HQ
]
# Public Subnets
resource "aws_subnet" "public" {
  count = length(var.availability_zones)

  vpc_id                  = aws_vpc.this.id
  cidr_block              = cidrsubnet(var.vpc_cidr, 8, count.index)
  availability_zone       = var.availability_zones[count.index]
  map_public_ip_on_launch = true

  tags = merge(
    var.tags,
    {
      Name                                          = "${var.project_name}-${var.environment}-public-${var.availability_zones[count.index]}"
      "kubernetes.io/cluster/${var.cluster_name}"   = "shared"
      "kubernetes.io/role/elb"                      = "1" # Required for AWS Load Balancer Controller to discover public subnets
    }
  )
}

# Private Subnets (App layer)
resource "aws_subnet" "private" {
  count = length(var.availability_zones)

  vpc_id                  = aws_vpc.this.id
  cidr_block              = cidrsubnet(var.vpc_cidr, 8, count.index + length(var.availability_zones))
  availability_zone       = var.availability_zones[count.index]
  map_public_ip_on_launch = false

  tags = merge(
    var.tags,
    {
      Name                                          = "${var.project_name}-${var.environment}-private-${var.availability_zones[count.index]}"
      "kubernetes.io/cluster/${var.cluster_name}"   = "shared"
      "kubernetes.io/role/internal-elb"             = "1" # Required for AWS Load Balancer Controller to discover private subnets
      "karpenter.sh/discovery"                      = var.cluster_name
    }
  )
}

# Database Subnets (Isolated)
resource "aws_subnet" "database" {
  count = length(var.availability_zones)

  vpc_id                  = aws_vpc.this.id
  cidr_block              = cidrsubnet(var.vpc_cidr, 8, count.index + (length(var.availability_zones) * 2))
  availability_zone       = var.availability_zones[count.index]
  map_public_ip_on_launch = false

  tags = merge(
    var.tags,
    {
      Name = "${var.project_name}-${var.environment}-database-${var.availability_zones[count.index]}"
    }
  )
}

resource "aws_db_subnet_group" "database" {
  name       = "${var.project_name}-${var.environment}-db-subnet-group"
  subnet_ids = aws_subnet.database[*].id

  tags = merge(
    var.tags,
    {
      Name = "${var.project_name}-${var.environment}-db-subnet-group"
    }
  )
}
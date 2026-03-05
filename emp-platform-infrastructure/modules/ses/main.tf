# Domain Identity for SES
resource "aws_ses_domain_identity" "this" {
  domain = var.domain_name
}

# DKIM Generation for Email Deliverability/Reputation
resource "aws_ses_domain_dkim" "this" {
  domain = aws_ses_domain_identity.this.domain
}

# Mail From Domain (optional, but recommended for deliverability)
resource "aws_ses_domain_mail_from" "this" {
  count            = var.enable_mail_from ? 1 : 0
  domain           = aws_ses_domain_identity.this.domain
  mail_from_domain = "bounce.${var.domain_name}"
}

# Configuration Set to capture bounce/complaint events
resource "aws_ses_configuration_set" "this" {
  name = "${var.project_name}-${var.environment}-config-set"
}
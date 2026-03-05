# DNS Verification Record
# This proves ownership of the domain to AWS SES
resource "aws_route53_record" "ses_verification" {
  count   = var.create_dns_records ? 1 : 0
  zone_id = var.route53_zone_id
  name    = "_amazonses.${var.domain_name}"
  type    = "TXT"
  ttl     = "600"
  records = [aws_ses_domain_identity.this.verification_token]
}

# DKIM CNAME Records
# Essential for preventing emails from landing in spam
resource "aws_route53_record" "ses_dkim" {
  count   = var.create_dns_records ? 3 : 0
  zone_id = var.route53_zone_id
  name    = "${aws_ses_domain_dkim.this.dkim_tokens[count.index]}._domainkey"
  type    = "CNAME"
  ttl     = "600"
  records = ["${aws_ses_domain_dkim.this.dkim_tokens[count.index]}.dkim.amazonses.com"]
}

# SPF Record (Sender Policy Framework)
# Only created if Mail From domain is configured
resource "aws_route53_record" "spf_mail_from" {
  count   = var.create_dns_records && var.enable_mail_from ? 1 : 0
  zone_id = var.route53_zone_id
  name    = aws_ses_domain_mail_from.this[0].mail_from_domain
  type    = "TXT"
  ttl     = "600"
  records = ["v=spf1 include:amazonses.com -all"]
}

# MX Record for Custom Mail From Domain
# Required to receive bounce notifications
resource "aws_route53_record" "mx_mail_from" {
  count   = var.create_dns_records && var.enable_mail_from ? 1 : 0
  zone_id = var.route53_zone_id
  name    = aws_ses_domain_mail_from.this[0].mail_from_domain
  type    = "MX"
  ttl     = "600"
  records = ["10 feedback-smtp.${var.region}.amazonses.com"]
}

# Wait for domain verification to complete
resource "aws_ses_domain_identity_verification" "this" {
  count      = var.wait_for_verification ? 1 : 0
  domain     = aws_ses_domain_identity.this.id
  depends_on = [aws_route53_record.ses_verification]
}
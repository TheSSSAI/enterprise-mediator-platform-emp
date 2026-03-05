.PHONY: init plan apply destroy fmt validate lint sec help

# Default environment
ENV ?= dev
REGION ?= us-east-1

# Terraform commands variables
TF_CMD := terraform
TF_DIR := environments/$(ENV)
TF_VARS := -var-file=terraform.tfvars

help: ## Show this help
	@echo "Enterprise Mediator Platform - Infrastructure Makefile"
	@echo "Usage: make [target] ENV=[dev|prod]"
	@echo ""
	@echo "Targets:"
	@grep -E '^[a-zA-Z0-9_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "  \033[36m%-20s\033[0m %s\n", $$1, $$2}'

init: ## Initialize Terraform for the specified environment
	@echo "Initializing Terraform for environment: $(ENV)..."
	@cd $(TF_DIR) && $(TF_CMD) init -backend-config=backend.conf -reconfigure

plan: ## Generate a Terraform plan for the specified environment
	@echo "Generating plan for environment: $(ENV)..."
	@cd $(TF_DIR) && $(TF_CMD) plan $(TF_VARS) -out=tfplan

apply: ## Apply the Terraform plan for the specified environment
	@echo "Applying configuration for environment: $(ENV)..."
	@cd $(TF_DIR) && $(TF_CMD) apply tfplan

destroy: ## Destroy infrastructure for the specified environment (WARNING: Destructive)
	@echo "WARNING: Destroying infrastructure for environment: $(ENV)..."
	@read -p "Are you sure? [y/N] " ans && [ $${ans:-N} = y ]
	@cd $(TF_DIR) && $(TF_CMD) destroy $(TF_VARS) -auto-approve

fmt: ## Format Terraform code recursively
	@echo "Formatting Terraform code..."
	@$(TF_CMD) fmt -recursive

validate: ## Validate Terraform configuration
	@echo "Validating Terraform configuration for environment: $(ENV)..."
	@cd $(TF_DIR) && $(TF_CMD) init -backend=false && $(TF_CMD) validate

lint: ## Run TFLint
	@echo "Running TFLint..."
	@tflint --init
	@tflint --recursive

sec: ## Run Checkov security scan
	@echo "Running Checkov security scan..."
	@checkov -d . --config-file .checkov.yaml

clean: ## Clean up local Terraform artifacts
	@echo "Cleaning up local artifacts..."
	@find . -type d -name ".terraform" -exec rm -rf {} +
	@find . -type f -name ".terraform.lock.hcl" -delete
	@find . -type f -name "tfplan" -delete
	@find . -type f -name "*.tfstate" -delete
	@find . -type f -name "*.tfstate.backup" -delete

bootstrap: ## Setup local development tools (MacOS/Linux)
	@echo "Installing required tools..."
	@brew install terraform tflint checkov pre-commit
	@pre-commit install
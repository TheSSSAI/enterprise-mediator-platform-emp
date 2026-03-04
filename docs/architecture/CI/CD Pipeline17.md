# 1 Pipelines

## 1.1 Backend Service CI/CD Pipeline

### 1.1.1 Id

emp-backend-cicd-v1

### 1.1.2 Name

Backend Service CI/CD Pipeline

### 1.1.3 Description

Builds, tests, scans, and deploys the .NET backend application to AWS EKS using a blue/green strategy.

### 1.1.4 Stages

#### 1.1.4.1 Build and Unit Test

##### 1.1.4.1.1 Name

Build and Unit Test

##### 1.1.4.1.2 Steps

- dotnet restore
- dotnet build --configuration Release
- dotnet test --collect:"XPlat Code Coverage"

##### 1.1.4.1.3 Environment

###### 1.1.4.1.3.1 Dotnet Version

8.0

#### 1.1.4.2.0.0 Code Quality and Security Scan

##### 1.1.4.2.1.0 Name

Code Quality and Security Scan

##### 1.1.4.2.2.0 Steps

- report-generator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
- run-sast-scanner --fail-on-critical
- dotnet list package --vulnerable --include-transitive

##### 1.1.4.2.3.0 Environment

*No data available*

##### 1.1.4.2.4.0 Quality Gates

###### 1.1.4.2.4.1 Unit Test Coverage

####### 1.1.4.2.4.1.1 Name

Unit Test Coverage

####### 1.1.4.2.4.1.2 Criteria

- line_coverage >= 80%

####### 1.1.4.2.4.1.3 Blocking

✅ Yes

###### 1.1.4.2.4.2.0 Security Vulnerabilities

####### 1.1.4.2.4.2.1 Name

Security Vulnerabilities

####### 1.1.4.2.4.2.2 Criteria

- sast_critical_vulnerabilities == 0
- dependency_critical_vulnerabilities == 0

####### 1.1.4.2.4.2.3 Blocking

✅ Yes

#### 1.1.4.3.0.0.0 Package Container Image

##### 1.1.4.3.1.0.0 Name

Package Container Image

##### 1.1.4.3.2.0.0 Steps

- dotnet publish -c Release -o ./publish
- docker build -t $ECR_REPO/emp-backend:$GIT_COMMIT -f ./Dockerfile ./publish
- aws ecr get-login-password --region $AWS_REGION | docker login --username AWS --password-stdin $ECR_REPO
- docker push $ECR_REPO/emp-backend:$GIT_COMMIT

##### 1.1.4.3.3.0.0 Environment

###### 1.1.4.3.3.1.0 Ecr Repo

123456789012.dkr.ecr.us-east-1.amazonaws.com

###### 1.1.4.3.3.2.0 Aws Region

us-east-1

#### 1.1.4.4.0.0.0 Deploy to Staging

##### 1.1.4.4.1.0.0 Name

Deploy to Staging

##### 1.1.4.4.2.0.0 Steps

- helm upgrade --install emp-backend-staging ./charts/emp-backend --namespace staging --set image.tag=$GIT_COMMIT
- run-database-migrations --env staging

##### 1.1.4.4.3.0.0 Environment

###### 1.1.4.4.3.1.0 Kube Context

eks-staging-cluster

#### 1.1.4.5.0.0.0 End-to-End Testing on Staging

##### 1.1.4.5.1.0.0 Name

End-to-End Testing on Staging

##### 1.1.4.5.2.0.0 Steps

- npx playwright test --config=playwright.staging.config.js

##### 1.1.4.5.3.0.0 Environment

###### 1.1.4.5.3.1.0 Base Url

🔗 [https://staging.emp.com](https://staging.emp.com)

##### 1.1.4.5.4.0.0 Quality Gates

- {'name': 'E2E Test Suite', 'criteria': ['all_tests_passed == true'], 'blocking': True}

#### 1.1.4.6.0.0.0 Manual Approval for Production

##### 1.1.4.6.1.0.0 Name

Manual Approval for Production

##### 1.1.4.6.2.0.0 Steps

- await-manual-approval --reason "Deploying version $GIT_COMMIT to Production"

##### 1.1.4.6.3.0.0 Environment

###### 1.1.4.6.3.1.0 Notify Channel

prod-deployment-approvals

#### 1.1.4.7.0.0.0 Deploy to Production (Blue/Green)

##### 1.1.4.7.1.0.0 Name

Deploy to Production (Blue/Green)

##### 1.1.4.7.2.0.0 Steps

- helm upgrade --install emp-backend-prod-green ./charts/emp-backend --namespace production --set image.tag=$GIT_COMMIT --set service.color=green
- run-smoke-tests --endpoint http://emp-backend-green.production.svc.cluster.local
- kubectl patch service emp-backend-prod-service -p '{"spec":{"selector":{"color":"green"}}}'
- run-database-migrations --env production

##### 1.1.4.7.3.0.0 Environment

###### 1.1.4.7.3.1.0 Kube Context

eks-production-cluster

## 1.2.0.0.0.0.0 Frontend Application CI/CD Pipeline

### 1.2.1.0.0.0.0 Id

emp-frontend-cicd-v1

### 1.2.2.0.0.0.0 Name

Frontend Application CI/CD Pipeline

### 1.2.3.0.0.0.0 Description

Builds, tests, scans, and deploys the Next.js frontend application to AWS S3 and CloudFront.

### 1.2.4.0.0.0.0 Stages

#### 1.2.4.1.0.0.0 Build and Unit Test

##### 1.2.4.1.1.0.0 Name

Build and Unit Test

##### 1.2.4.1.2.0.0 Steps

- npm ci
- npm run test

##### 1.2.4.1.3.0.0 Environment

###### 1.2.4.1.3.1.0 Node Env

test

#### 1.2.4.2.0.0.0 Code Quality and Security Scan

##### 1.2.4.2.1.0.0 Name

Code Quality and Security Scan

##### 1.2.4.2.2.0.0 Steps

- npm run lint
- npm audit --audit-level=critical

##### 1.2.4.2.3.0.0 Environment

*No data available*

##### 1.2.4.2.4.0.0 Quality Gates

- {'name': 'Linting and Security', 'criteria': ['lint_errors == 0', 'critical_vulnerabilities == 0'], 'blocking': True}

#### 1.2.4.3.0.0.0 Package Static Assets

##### 1.2.4.3.1.0.0 Name

Package Static Assets

##### 1.2.4.3.2.0.0 Steps

- npm run build

##### 1.2.4.3.3.0.0 Environment

###### 1.2.4.3.3.1.0 Node Env

production

###### 1.2.4.3.3.2.0 Api Url

🔗 [https://api.emp.com](https://api.emp.com)

#### 1.2.4.4.0.0.0 Deploy and Test on Staging

##### 1.2.4.4.1.0.0 Name

Deploy and Test on Staging

##### 1.2.4.4.2.0.0 Steps

- aws s3 sync ./out s3://$S3_BUCKET_STAGING
- npx playwright test --config=playwright.staging.config.js

##### 1.2.4.4.3.0.0 Environment

###### 1.2.4.4.3.1.0 S3 Bucket Staging

emp-frontend-staging

###### 1.2.4.4.3.2.0 Base Url

🔗 [https://staging.emp.com](https://staging.emp.com)

##### 1.2.4.4.4.0.0 Quality Gates

- {'name': 'E2E Test Suite', 'criteria': ['all_tests_passed == true'], 'blocking': True}

#### 1.2.4.5.0.0.0 Manual Approval for Production

##### 1.2.4.5.1.0.0 Name

Manual Approval for Production

##### 1.2.4.5.2.0.0 Steps

- await-manual-approval --reason "Deploying frontend version $GIT_COMMIT to Production"

##### 1.2.4.5.3.0.0 Environment

*No data available*

#### 1.2.4.6.0.0.0 Deploy to Production

##### 1.2.4.6.1.0.0 Name

Deploy to Production

##### 1.2.4.6.2.0.0 Steps

- aws s3 sync ./out s3://$S3_BUCKET_PRODUCTION
- aws cloudfront create-invalidation --distribution-id $CF_DISTRIBUTION_ID --paths "/*"

##### 1.2.4.6.3.0.0 Environment

###### 1.2.4.6.3.1.0 S3 Bucket Production

emp-frontend-production

###### 1.2.4.6.3.2.0 Cf Distribution Id

E1234567890ABC

## 1.3.0.0.0.0.0 Infrastructure (IaC) Pipeline

### 1.3.1.0.0.0.0 Id

emp-iac-cicd-v1

### 1.3.2.0.0.0.0 Name

Infrastructure (IaC) Pipeline

### 1.3.3.0.0.0.0 Description

Manages and applies infrastructure changes using Terraform for all environments.

### 1.3.4.0.0.0.0 Stages

#### 1.3.4.1.0.0.0 Initialize and Validate

##### 1.3.4.1.1.0.0 Name

Initialize and Validate

##### 1.3.4.1.2.0.0 Steps

- terraform init -backend-config=backend.tfvars
- terraform validate
- tflint --module

##### 1.3.4.1.3.0.0 Environment

*No data available*

#### 1.3.4.2.0.0.0 Plan Staging

##### 1.3.4.2.1.0.0 Name

Plan Staging

##### 1.3.4.2.2.0.0 Steps

- terraform workspace select staging
- terraform plan -out=staging.tfplan

##### 1.3.4.2.3.0.0 Environment

*No data available*

#### 1.3.4.3.0.0.0 Apply Staging

##### 1.3.4.3.1.0.0 Name

Apply Staging

##### 1.3.4.3.2.0.0 Steps

- await-manual-approval --reason "Applying Terraform plan to Staging"
- terraform apply staging.tfplan

##### 1.3.4.3.3.0.0 Environment

*No data available*

#### 1.3.4.4.0.0.0 Plan Production

##### 1.3.4.4.1.0.0 Name

Plan Production

##### 1.3.4.4.2.0.0 Steps

- terraform workspace select production
- terraform plan -out=production.tfplan

##### 1.3.4.4.3.0.0 Environment

*No data available*

#### 1.3.4.5.0.0.0 Apply Production

##### 1.3.4.5.1.0.0 Name

Apply Production

##### 1.3.4.5.2.0.0 Steps

- await-manual-approval --reason "Applying Terraform plan to Production"
- terraform apply production.tfplan

##### 1.3.4.5.3.0.0 Environment

*No data available*

# 2.0.0.0.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Artifact Repository | AWS ECR (123456789012.dkr.ecr.us-east-1.amazonaws.... |
| Default Branch | main |
| Retention Policy | Keep artifacts for last 20 successful builds |
| Notification Channel | slack#emp-deployments |


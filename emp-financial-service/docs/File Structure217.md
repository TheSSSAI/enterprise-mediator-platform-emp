# 1 Dependency Levels

## 1.1 Level

### 1.1.1 Level

🔹 0

### 1.1.2 Files

- EnterpriseMediator.Financial.sln
- src/EnterpriseMediator.Financial.Domain/EnterpriseMediator.Financial.Domain.csproj
- src/EnterpriseMediator.Financial.Application/EnterpriseMediator.Financial.Application.csproj
- src/EnterpriseMediator.Financial.Infrastructure/EnterpriseMediator.Financial.Infrastructure.csproj
- src/EnterpriseMediator.Financial.Web.API/EnterpriseMediator.Financial.Web.API.csproj
- tests/EnterpriseMediator.Financial.IntegrationTests/EnterpriseMediator.Financial.IntegrationTests.csproj
- tests/EnterpriseMediator.Financial.UnitTests/EnterpriseMediator.Financial.UnitTests.csproj
- docker-compose.yml
- Dockerfile
- nuget.config
- global.json
- .gitignore
- .editorconfig
- .dockerignore
- src/EnterpriseMediator.Financial.Web.API/appsettings.json
- src/EnterpriseMediator.Financial.Web.API/appsettings.Development.json
- src/EnterpriseMediator.Financial.Web.API/Properties/launchSettings.json
- src/EnterpriseMediator.Financial.Domain/Enums/InvoiceStatus.cs
- src/EnterpriseMediator.Financial.Domain/Enums/TransactionType.cs
- src/EnterpriseMediator.Financial.Domain/ValueObjects/Money.cs
- src/EnterpriseMediator.Financial.Domain/ValueObjects/Currency.cs

## 1.2.0 Level

### 1.2.1 Level

🔹 1

### 1.2.2 Files

- src/EnterpriseMediator.Financial.Domain/Events/InvoicePaidEvent.cs
- src/EnterpriseMediator.Financial.Domain/Events/PayoutProcessedEvent.cs
- src/EnterpriseMediator.Financial.Domain/Entities/Invoice.cs
- src/EnterpriseMediator.Financial.Domain/Entities/Transaction.cs
- src/EnterpriseMediator.Financial.Domain/Entities/Payout.cs

## 1.3.0 Level

### 1.3.1 Level

🔹 2

### 1.3.2 Files

- src/EnterpriseMediator.Financial.Domain/Interfaces/IPaymentGateway.cs
- src/EnterpriseMediator.Financial.Domain/Interfaces/IPayoutGateway.cs
- src/EnterpriseMediator.Financial.Domain/Interfaces/IFinancialRepository.cs
- src/EnterpriseMediator.Financial.Domain/Services/InvoiceCalculationService.cs

## 1.4.0 Level

### 1.4.1 Level

🔹 3

### 1.4.2 Files

- src/EnterpriseMediator.Financial.Application/DTOs/InvoiceDto.cs
- src/EnterpriseMediator.Financial.Application/Common/Behaviors/LoggingBehavior.cs
- src/EnterpriseMediator.Financial.Application/Common/Behaviors/IdempotencyBehavior.cs
- src/EnterpriseMediator.Financial.Application/Common/Behaviors/TransactionalOutboxBehavior.cs
- src/EnterpriseMediator.Financial.Application/Features/Invoices/Commands/GenerateInvoice/GenerateInvoiceCommand.cs
- src/EnterpriseMediator.Financial.Application/Features/Payouts/Commands/InitiatePayout/InitiatePayoutCommand.cs
- src/EnterpriseMediator.Financial.Application/Features/Ledger/Queries/GetTransactionHistory/GetTransactionHistoryQuery.cs

## 1.5.0 Level

### 1.5.1 Level

🔹 4

### 1.5.2 Files

- src/EnterpriseMediator.Financial.Application/Features/Invoices/Commands/GenerateInvoice/GenerateInvoiceHandler.cs
- src/EnterpriseMediator.Financial.Application/Features/Payments/EventHandlers/StripeWebhookHandler.cs

## 1.6.0 Level

### 1.6.1 Level

🔹 5

### 1.6.2 Files

- src/EnterpriseMediator.Financial.3110[REDACTED].cs
- src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/StripeSettings.cs
- src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/WiseSettings.cs
- src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/InvoiceConfiguration.cs
- src/EnterpriseMediator.Financial.Infrastructure/Persistence/Configurations/MoneyConfiguration.cs
- src/EnterpriseMediator.Financial.Infrastructure/Messaging/MassTransitExtensions.cs

## 1.7.0 Level

### 1.7.1 Level

🔹 6

### 1.7.2 Files

- src/EnterpriseMediator.Financial.Infrastructure/Persistence/FinancialDbContext.cs
- src/EnterpriseMediator.Financial.Infrastructure/Interceptors/FinancialAuditInterceptor.cs

## 1.8.0 Level

### 1.8.1 Level

🔹 7

### 1.8.2 Files

- src/EnterpriseMediator.Financial.Infrastructure/Gateways/Stripe/StripePaymentAdapter.cs
- src/EnterpriseMediator.Financial.Infrastructure/Gateways/Wise/WisePayoutAdapter.cs

## 1.9.0 Level

### 1.9.1 Level

🔹 8

### 1.9.2 Files

- src/EnterpriseMediator.Financial.Web.API/Controllers/InvoicesController.cs
- src/EnterpriseMediator.Financial.Web.API/Controllers/Webhooks/StripeWebhookController.cs
- src/EnterpriseMediator.Financial.Web.API/Controllers/LedgerController.cs

## 1.10.0 Level

### 1.10.1 Level

🔹 9

### 1.10.2 Files

- src/EnterpriseMediator.Financial.Web.API/Program.cs

# 2.0.0 Total Files

53

# 3.0.0 Generation Order

- 0
- 1
- 2
- 3
- 4
- 5
- 6
- 7
- 8
- 9


# 1 Dependency Levels

## 1.1 Level

### 1.1.1 Level

🔹 0

### 1.1.2 Files

- .dockerignore
- .editorconfig
- .gitignore
- .vscode/launch.json
- Directory.Build.props
- Directory.Packages.props
- EnterpriseMediator.UserManagement.sln
- global.json
- src/EnterpriseMediator.UserManagement.API/EnterpriseMediator.UserManagement.API.csproj
- src/EnterpriseMediator.UserManagement.Application/EnterpriseMediator.UserManagement.Application.csproj
- src/EnterpriseMediator.UserManagement.Domain/EnterpriseMediator.UserManagement.Domain.csproj
- src/EnterpriseMediator.UserManagement.Infrastructure/EnterpriseMediator.UserManagement.Infrastructure.csproj
- tests/EnterpriseMediator.UserManagement.IntegrationTests/EnterpriseMediator.UserManagement.IntegrationTests.csproj
- tests/EnterpriseMediator.UserManagement.UnitTests/EnterpriseMediator.UserManagement.UnitTests.csproj

## 1.2.0 Level

### 1.2.1 Level

🔹 1

### 1.2.2 Files

- src/EnterpriseMediator.UserManagement.Domain/Enums/UserType.cs
- src/EnterpriseMediator.UserManagement.Domain/ValueObjects/Address.cs
- src/EnterpriseMediator.UserManagement.Domain/ValueObjects/PaymentInfo.cs

## 1.3.0 Level

### 1.3.1 Level

🔹 2

### 1.3.2 Files

- src/EnterpriseMediator.UserManagement.Domain/Events/UserAnonymizedEvent.cs
- src/EnterpriseMediator.UserManagement.Domain/Events/VendorCreatedEvent.cs
- src/EnterpriseMediator.UserManagement.Domain/Events/VendorProfileUpdated.cs
- src/EnterpriseMediator.UserManagement.Domain/Events/VendorVettingStatusChangedEvent.cs
- src/EnterpriseMediator.UserManagement.Domain/Aggregates/Vendor/VendorSkill.cs
- src/EnterpriseMediator.UserManagement.Domain/Aggregates/User/User.cs
- src/EnterpriseMediator.UserManagement.Domain/Aggregates/Client/Client.cs
- src/EnterpriseMediator.UserManagement.Domain/Aggregates/Vendor/Vendor.cs
- src/EnterpriseMediator.UserManagement.Domain/Interfaces/IUserRepository.cs
- src/EnterpriseMediator.UserManagement.Domain/Interfaces/IVendorRepository.cs
- src/EnterpriseMediator.UserManagement.Domain/Interfaces/IClientRepository.cs

## 1.4.0 Level

### 1.4.1 Level

🔹 3

### 1.4.2 Files

- src/EnterpriseMediator.UserManagement.Application/Configuration/UserManagementSettings.cs
- src/EnterpriseMediator.UserManagement.Application/Interfaces/IInternalUserService.cs
- src/EnterpriseMediator.UserManagement.Application/Interfaces/IAuditServiceAdapter.cs
- src/EnterpriseMediator.UserManagement.Application/Interfaces/ICurrentUserService.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Users/Commands/AnonymizeUser/AnonymizeUserCommand.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Internal/Queries/GetUserRole/GetUserRoleQuery.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Clients/Commands/CreateClient/CreateClientCommand.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Vendors/Commands/CreateVendor/CreateVendorCommand.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Vendors/Commands/UpdateProfile/UpdateVendorProfileCommand.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Clients/Queries/GetClientDetails/GetClientDetailsQuery.cs

## 1.5.0 Level

### 1.5.1 Level

🔹 4

### 1.5.2 Files

- src/EnterpriseMediator.UserManagement.Application/Common/Behaviors/ValidationBehavior.cs
- src/EnterpriseMediator.UserManagement.Application/Behaviors/AuditLoggingBehavior.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Users/Commands/AnonymizeUser/AnonymizeUserHandler.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Internal/Queries/GetUserRole/GetUserRoleHandler.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Clients/Commands/CreateClient/CreateClientHandler.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Vendors/Commands/UpdateProfile/UpdateVendorProfileHandler.cs
- src/EnterpriseMediator.UserManagement.Application/Features/Vendors/Commands/UpdateProfile/UpdateVendorProfileValidator.cs

## 1.6.0 Level

### 1.6.1 Level

🔹 5

### 1.6.2 Files

- src/EnterpriseMediator.UserManagement.Infrastructure/Persistence/Configurations/UserConfiguration.cs
- src/EnterpriseMediator.UserManagement.Infrastructure/Persistence/Configurations/VendorConfiguration.cs
- src/EnterpriseMediator.UserManagement.4277[REDACTED].cs

## 1.7.0 Level

### 1.7.1 Level

🔹 6

### 1.7.2 Files

- src/EnterpriseMediator.UserManagement.Infrastructure/Persistence/Repositories/UserRepository.cs
- src/EnterpriseMediator.UserManagement.Infrastructure/Persistence/Repositories/VendorRepository.cs
- src/EnterpriseMediator.UserManagement.Infrastructure/Services/AuditServiceAdapter.cs
- src/EnterpriseMediator.UserManagement.Infrastructure/Services/DomainEventDispatcher.cs

## 1.8.0 Level

### 1.8.1 Level

🔹 7

### 1.8.2 Files

- src/EnterpriseMediator.UserManagement.API/Middleware/GlobalExceptionHandler.cs
- src/EnterpriseMediator.UserManagement.API/Controllers/InternalUsersController.cs
- src/EnterpriseMediator.UserManagement.API/Controllers/ClientsController.cs
- src/EnterpriseMediator.UserManagement.API/Controllers/VendorsController.cs

## 1.9.0 Level

### 1.9.1 Level

🔹 8

### 1.9.2 Files

- src/EnterpriseMediator.UserManagement.API/appsettings.json
- src/EnterpriseMediator.UserManagement.API/appsettings.Development.json
- src/EnterpriseMediator.UserManagement.API/Properties/launchSettings.json
- src/EnterpriseMediator.UserManagement.API/Dockerfile
- src/EnterpriseMediator.UserManagement.API/Program.cs

# 2.0.0 Total Files

68

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


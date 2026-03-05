#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EnterpriseMediator.UserManagement.API/EnterpriseMediator.UserManagement.API.csproj", "src/EnterpriseMediator.UserManagement.API/"]
COPY ["src/EnterpriseMediator.UserManagement.Application/EnterpriseMediator.UserManagement.Application.csproj", "src/EnterpriseMediator.UserManagement.Application/"]
COPY ["src/EnterpriseMediator.UserManagement.Domain/EnterpriseMediator.UserManagement.Domain.csproj", "src/EnterpriseMediator.UserManagement.Domain/"]
COPY ["src/EnterpriseMediator.UserManagement.Infrastructure/EnterpriseMediator.UserManagement.Infrastructure.csproj", "src/EnterpriseMediator.UserManagement.Infrastructure/"]
RUN dotnet restore "./src/EnterpriseMediator.UserManagement.API/EnterpriseMediator.UserManagement.API.csproj"
COPY . .
WORKDIR "/src/src/EnterpriseMediator.UserManagement.API"
RUN dotnet build "./EnterpriseMediator.UserManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EnterpriseMediator.UserManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseMediator.UserManagement.API.dll"]
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EnterpriseMediator.Financial.Web.API/EnterpriseMediator.Financial.Web.API.csproj", "src/EnterpriseMediator.Financial.Web.API/"]
COPY ["src/EnterpriseMediator.Financial.Infrastructure/EnterpriseMediator.Financial.Infrastructure.csproj", "src/EnterpriseMediator.Financial.Infrastructure/"]
COPY ["src/EnterpriseMediator.Financial.Application/EnterpriseMediator.Financial.Application.csproj", "src/EnterpriseMediator.Financial.Application/"]
COPY ["src/EnterpriseMediator.Financial.Domain/EnterpriseMediator.Financial.Domain.csproj", "src/EnterpriseMediator.Financial.Domain/"]
RUN dotnet restore "./src/EnterpriseMediator.Financial.Web.API/EnterpriseMediator.Financial.Web.API.csproj"
COPY . .
WORKDIR "/src/src/EnterpriseMediator.Financial.Web.API"
RUN dotnet build "./EnterpriseMediator.Financial.Web.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EnterpriseMediator.Financial.Web.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseMediator.Financial.Web.API.dll"]
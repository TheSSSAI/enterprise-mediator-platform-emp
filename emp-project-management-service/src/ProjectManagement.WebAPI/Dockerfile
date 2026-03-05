#Base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ProjectManagement.WebAPI/ProjectManagement.WebAPI.csproj", "src/ProjectManagement.WebAPI/"]
COPY ["src/ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj", "src/ProjectManagement.Infrastructure/"]
COPY ["src/ProjectManagement.Application/ProjectManagement.Application.csproj", "src/ProjectManagement.Application/"]
COPY ["src/ProjectManagement.Domain/ProjectManagement.Domain.csproj", "src/ProjectManagement.Domain/"]
RUN dotnet restore "src/ProjectManagement.WebAPI/ProjectManagement.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/ProjectManagement.WebAPI"
RUN dotnet build "ProjectManagement.WebAPI.csproj" -c Release -o /app/build

#Publish stage
FROM build AS publish
RUN dotnet publish "ProjectManagement.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseMediator.ProjectManagement.WebAPI.dll"]
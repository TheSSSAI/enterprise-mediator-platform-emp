# Stage 1: Base Runtime Image
# Using ASP.NET Core 8.0 Runtime on Linux
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Switch to non-root user for security (default in .NET 8 images, but explicit is better)
USER app

# Stage 2: Build Image
# Using .NET SDK 8.0 for compilation
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first to leverage Docker layer caching for NuGet restore
# Level 0
COPY ["src/Emp.ApiGateway.Application/Emp.ApiGateway.Application.csproj", "src/Emp.ApiGateway.Application/"]
# Level 1
COPY ["src/Emp.ApiGateway.Infrastructure/Emp.ApiGateway.Infrastructure.csproj", "src/Emp.ApiGateway.Infrastructure/"]
# Level 2
COPY ["src/Emp.ApiGateway.Web/Emp.ApiGateway.Web.csproj", "src/Emp.ApiGateway.Web/"]

# Restore dependencies
RUN dotnet restore "src/Emp.ApiGateway.Web/Emp.ApiGateway.Web.csproj"

# Copy the rest of the source code
# We copy everything because the dockerignore should filter out bin/obj/etc
COPY . .

# Build the Web project
WORKDIR "/src/src/Emp.ApiGateway.Web"
RUN dotnet build "Emp.ApiGateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Emp.ApiGateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final Production Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define entrypoint
ENTRYPOINT ["dotnet", "Emp.ApiGateway.Web.dll"]
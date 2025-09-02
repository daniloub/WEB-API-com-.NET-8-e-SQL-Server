# Multi-stage Dockerfile for .NET 8 Web API

# 1) Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY WebApi/WebApi.sln ./WebApi.sln
COPY WebApi/Application/Application.csproj WebApi/Application/
COPY WebApi/Domain/Domain.csproj WebApi/Domain/
COPY WebApi/Infrastructure/Infrastructure.csproj WebApi/Infrastructure/
COPY WebApi/Web/Web.csproj WebApi/Web/
RUN dotnet restore WebApi/Web/Web.csproj

# Copy the rest of the source and build
COPY . .
WORKDIR /src/WebApi/Web
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 2) Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Web.dll"]



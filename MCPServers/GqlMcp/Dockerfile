# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy Shared project files for restore
COPY Shared/Shared.csproj Shared/

# Copy project file for restore
COPY GqlMcp/GqlMcp.csproj GqlMcp/

# Restore dependencies (cached layer)
RUN dotnet restore "GqlMcp/GqlMcp.csproj"

# Copy remaining source files
COPY Shared/ Shared/
COPY GqlMcp/ GqlMcp/

# Build and publish
RUN dotnet publish "GqlMcp/GqlMcp.csproj" -c Release -o /app/publish --runtime linux-x64 --self-contained false 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY GqlMcp/appsettings.json ./
EXPOSE 4549
ENTRYPOINT ["dotnet", "GqlMcp.dll"]
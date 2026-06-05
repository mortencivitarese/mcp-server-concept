# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy Shared project files for restore
COPY Shared/Shared.csproj Shared/

# Copy project file for restore
COPY EthMcp/EthMcp.csproj EthMcp/

# Restore dependencies (cached layer)
RUN dotnet restore "EthMcp/EthMcp.csproj"

# Copy remaining source files
COPY Shared/ Shared/
COPY EthMcp/ EthMcp/

# Build and publish
RUN dotnet publish "EthMcp/EthMcp.csproj" -c Release -o /app/publish --runtime linux-x64 --self-contained false 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY EthMcp/appsettings.json ./
EXPOSE 4553
ENTRYPOINT ["dotnet", "EthMcp.dll"]
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy Shared project files for restore
COPY Shared/Shared.csproj Shared/

# Copy project file for restore
COPY SqliteMcp/SqliteMcp.csproj SqliteMcp/

# Restore dependencies (cached layer)
RUN dotnet restore "SqliteMcp/SqliteMcp.csproj"

# Copy remaining source files
COPY Shared/ Shared/
COPY SqliteMcp/ SqliteMcp/

# Build and publish
RUN dotnet publish "SqliteMcp/SqliteMcp.csproj" -c Release -o /app/publish --runtime linux-x64 --self-contained false 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY SqliteMcp/appsettings.json ./
EXPOSE 4551
ENTRYPOINT ["dotnet", "SqliteMcp.dll"]
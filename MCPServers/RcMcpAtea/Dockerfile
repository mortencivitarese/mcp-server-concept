# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy Shared project files for restore
COPY Shared/Shared.csproj Shared/

# Copy project file for restore
COPY RcMcpAtea/RcMcpAtea.csproj RcMcpAtea/

# Restore dependencies (cached layer)
RUN dotnet restore "RcMcpAtea/RcMcpAtea.csproj"

# Copy remaining source files
COPY Shared/ Shared/
COPY RcMcpAtea/ RcMcpAtea/

# Build and publish
RUN dotnet publish "RcMcpAtea/RcMcpAtea.csproj" -c Release -o /app/publish --runtime linux-x64 --self-contained false 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY RcMcpAtea/appsettings.json ./
EXPOSE 4548
ENTRYPOINT ["dotnet", "RcMcpAtea.dll"]
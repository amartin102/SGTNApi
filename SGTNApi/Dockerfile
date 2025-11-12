# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["SGTNApi/SGTNApi.csproj", "SGTNApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Restore dependencies
RUN dotnet restore "SGTNApi/SGTNApi.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/SGTNApi"
RUN dotnet build "SGTNApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "SGTNApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Define entry point
ENTRYPOINT ["dotnet", "SGTNApi.dll"]

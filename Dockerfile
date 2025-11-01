# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files before restore (better caching)
COPY ["Emocare.API/Emocare.API.csproj", "Emocare.API/"]
COPY ["Emocare.Application/Emocare.Application.csproj", "Emocare.Application/"]
COPY ["Emocare.Core/Emocare.Domain.csproj", "Emocare.Core/"]
COPY ["Emocare.Shared/Emocare.Shared.csproj", "Emocare.Shared/"]
COPY ["Emocare.Infrastructure/Emocare.Infrastructure.csproj", "Emocare.Infrastructure/"]

RUN dotnet restore "Emocare.API/Emocare.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/Emocare.API"
RUN dotnet build "Emocare.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Emocare.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final Runtime Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Emocare.API.dll"]

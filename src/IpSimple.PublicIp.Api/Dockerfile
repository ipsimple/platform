FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Accept APP_VERSION as a build argument
ARG APP_VERSION=unknown

LABEL org.opencontainers.image.title="IpSimple App"
LABEL org.opencontainers.image.description="A reliable and scalable public IP address API, designed for seamless integration into any application. It offers unlimited usage, compatibility with IPv4 and IPv6, high availability, open-source transparency, privacy focus, and future-proof reliability. Use cases include network management, cloud infrastructure, security applications, and developer tools."
LABEL org.opencontainers.image.url="https://github.com/ipsimple/platform"
LABEL org.opencontainers.image.authors="ipsimple org"
LABEL org.opencontainers.image.licenses="MIT"
LABEL org.opencontainers.image.vendor="ipsimple org"
LABEL org.opencontainers.image.source="https://github.com/ipsimple/platform"
LABEL org.opencontainers.image.version="${APP_VERSION}"

# Set the APP_VERSION environment variable
ENV APP_VERSION=${APP_VERSION}

USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["IpSimple.PublicIp.Api/IpSimple.PublicIp.Api.csproj", "IpSimple.PublicIp.Api/"]
COPY ["IpSimple.Domain/IpSimple.Domain.csproj", "IpSimple.Domain/"]
COPY ["IpSimple.Extensions/IpSimple.Extensions.csproj", "IpSimple.Extensions/"]

# Restore dependencies
RUN dotnet restore "./IpSimple.PublicIp.Api/IpSimple.PublicIp.Api.csproj"

# Copy source code
COPY . .
WORKDIR "/src/IpSimple.PublicIp.Api"
RUN dotnet build "./IpSimple.PublicIp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IpSimple.PublicIp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IpSimple.PublicIp.Api.dll"]

FROM ghcr.io/ipsimple/aspnet:8.0 AS base

LABEL org.opencontainers.image.title="IpSimple App"
LABEL org.opencontainers.image.description="A reliable and scalable public IP address API, designed for seamless integration into any application. It offers unlimited usage, compatibility with IPv4 and IPv6, high availability, open-source transparency, privacy focus, and future-proof reliability. Use cases include network management, cloud infrastructure, security applications, and developer tools."
LABEL org.opencontainers.image.url="https://github.com/ipsimple/platform"
LABEL org.opencontainers.image.authors="ipsimple org"
LABEL org.opencontainers.image.licenses="MIT"
LABEL org.opencontainers.image.vendor="ipsimple org"
LABEL org.opencontainers.image.source="https://github.com/ipsimple/platform"

USER app
WORKDIR /app
EXPOSE 8080

FROM ghcr.io/ipsimple/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IpSimple.PublicIp.Api/IpSimple.PublicIp.Api.csproj", "IpSimple.PublicIp.Api/"]
RUN dotnet restore "./IpSimple.PublicIp.Api/IpSimple.PublicIp.Api.csproj"
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

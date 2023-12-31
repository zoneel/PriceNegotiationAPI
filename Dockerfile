﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["PriceNegotiationAPI/PriceNegotiationAPI.csproj", "PriceNegotiationAPI/"]
COPY ["PriceNegotiationAPI.Domain/PriceNegotiationAPI.Domain.csproj", "PriceNegotiationAPI.Domain/"]
COPY ["PriceNegotiationAPI.Infrastructure/PriceNegotiationAPI.Infrastructure.csproj", "PriceNegotiationAPI.Infrastructure/"]
COPY ["PriceNegotiationAPI.Application/PriceNegotiationAPI.Application.csproj", "PriceNegotiationAPI.Application/"]
RUN dotnet restore "PriceNegotiationAPI/PriceNegotiationAPI.csproj"
COPY . .
WORKDIR "PriceNegotiationAPI"
RUN dotnet build "PriceNegotiationAPI.csproj" -c $BUILD_CONFIGURATION -o /build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PriceNegotiationAPI.csproj" -c $BUILD_CONFIGURATION -o /publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceNegotiationAPI.dll"]

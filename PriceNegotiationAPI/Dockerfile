FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PriceNegotiationAPI/PriceNegotiationAPI.csproj", "PriceNegotiationAPI/"]
COPY ["PriceNegotiationAPI.Application/PriceNegotiationAPI.Application.csproj", "PriceNegotiationAPI.Application/"]
COPY ["PriceNegotiationAPI.Domain/PriceNegotiationAPI.Domain.csproj", "PriceNegotiationAPI.Domain/"]
COPY ["PriceNegotiationAPI.Infrastructure/PriceNegotiationAPI.Infrastructure.csproj", "PriceNegotiationAPI.Infrastructure/"]
RUN dotnet restore "PriceNegotiationAPI/PriceNegotiationAPI.csproj"
COPY . .
WORKDIR "/src/PriceNegotiationAPI"
RUN dotnet build "PriceNegotiationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PriceNegotiationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceNegotiationAPI.dll"]

# cd /
# docker build --pull --rm -f "src/Services/Catalog/Catalog.FunctionalTests/Dockerfile" -t eshop-catalog-functionaltests src

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /src
COPY "BuildingBlocks/EventBus/EventBus/EventBus.csproj" "BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj" "BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj"
COPY "BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj"
COPY "Services/Catalog/Catalog.API/Catalog.API.csproj" "Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY "Services/Catalog/Catalog.FunctionalTests/Catalog.FunctionalTests.csproj" "Services/Catalog/Catalog.FunctionalTests/Catalog.FunctionalTests.csproj"
COPY "Services/Catalog/Catalog.UnitTests/Catalog.UnitTests.csproj" "Services/Catalog/Catalog.UnitTests/Catalog.UnitTests.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"

COPY /src/Services/Catalog/Catalog.API/Proto /app/Proto
COPY /src/Services/Catalog/Catalog.API/eshop.pfx .
WORKDIR /src/Services/Catalog/Catalog.FunctionalTests

RUN apt-get update && apt-get install -y netcat
RUN dotnet tool install -g dotnet-reportgenerator-globaltool

#FROM build as unittest
#WORKDIR /src/Services/Catalog/Catalog.UnitTests
#RUN ["dotnet", "test"]
#RUN ["cat", "Unit tests runned"]

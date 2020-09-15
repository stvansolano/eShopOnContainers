# cd /
# docker build --pull --rm -f "src/Services/Catalog/Catalog.FunctionalTests/Dockerfile" -t eshop-catalog-functionaltests src

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /src
COPY "BuildingBlocks/EventBus/EventBus/" "BuildingBlocks/EventBus/EventBus/"
COPY "BuildingBlocks/EventBus/EventBusRabbitMQ/" "BuildingBlocks/EventBus/EventBusRabbitMQ/"
COPY "BuildingBlocks/EventBus/EventBusServiceBus/" "BuildingBlocks/EventBus/EventBusServiceBus/"
COPY "BuildingBlocks/EventBus/IntegrationEventLogEF" "BuildingBlocks/EventBus/IntegrationEventLogEF/"
COPY "Services/Catalog/Catalog.API/" "Services/Catalog/Catalog.API/"
COPY "Services/Catalog/Catalog.FunctionalTests/" "Services/Catalog/Catalog.FunctionalTests/"
COPY "Services/Catalog/Catalog.UnitTests/" "Services/Catalog/Catalog.UnitTests/"

COPY "NuGet.config" "NuGet.config"

WORKDIR /src/Services/Catalog/Catalog.FunctionalTests

RUN apt-get update && apt-get install -y netcat
RUN dotnet tool install -g dotnet-reportgenerator-globaltool

#FROM build as unittest
#WORKDIR /src/Services/Catalog/Catalog.UnitTests
#RUN ["dotnet", "test"]
#RUN ["cat", "Unit tests runned"]

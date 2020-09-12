# cd /
# docker build --pull --rm -f "src/Services/Catalog/Catalog.FunctionalTests/Dockerfile" -t eshop-catalog-functionaltests src

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .

FROM base AS final
WORKDIR /app
COPY --from=build /src/Services/Catalog/Catalog.API/Proto /app/Proto
COPY --from=build /src/Services/Catalog/Catalog.API/eshop.pfx .

#FROM build as unittest
#WORKDIR /src/Services/Catalog/Catalog.UnitTests
#RUN ["dotnet", "test"]
#RUN ["cat", "Unit tests runned"]


FROM build as functionaltest
WORKDIR /src/Services/Catalog/Catalog.FunctionalTests

RUN apt-get update && apt-get install -y netcat

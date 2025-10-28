# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Rest.Api" "JDev.Tuteee.DAL" "JDev.Tuteee.Rest.ApiClient"

COPY JDev.Tuteee.Rest.Api ./JDev.Tuteee.Rest.Api/
COPY JDev.Tuteee.DAL ./JDev.Tuteee.DAL/
COPY JDev.Tuteee.Rest.ApiClient ./JDev.Tuteee.Rest.ApiClient/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj \
    && dotnet build --no-restore --configuration Release ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj \
    && dotnet publish --no-build -o out ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj

FROM infra.registry.johngould.net/bao-dotnet:0.1.3

WORKDIR /app

COPY --from=build /app/out .
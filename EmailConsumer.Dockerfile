FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.EmailConsumer"
RUN mkdir "JDev.Tuteee.Grpc.Api.Messages"
RUN mkdir "JDev.Tuteee.Rest.ApiClient"
RUN mkdir "JDev.Tuteee.CustomTypes"

COPY JDev.Tuteee.EmailConsumer ./JDev.Tuteee.EmailConsumer/
COPY JDev.Tuteee.Grpc.Api.Messages ./JDev.Tuteee.Grpc.Api.Messages/
COPY JDev.Tuteee.Rest.ApiClient ./JDev.Tuteee.Rest.ApiClient/
COPY JDev.Tuteee.CustomTypes ./JDev.Tuteee.CustomTypes/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

FROM infra.registry.johngould.net/bao-dotnet:0.1.3

WORKDIR /app

COPY --from=build /app/out .

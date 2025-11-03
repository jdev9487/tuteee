FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Grpc.Api" "JDev.Tuteee.Grpc.Api.Messages" "JDev.Tuteee.DAL" "JDev.Tuteee.Protos" "JDev.Tuteee.CustomTypes"

COPY JDev.Tuteee.Grpc.Api ./JDev.Tuteee.Grpc.Api/
COPY JDev.Tuteee.Grpc.Api.Messages ./JDev.Tuteee.Grpc.Api.Messages/
COPY JDev.Tuteee.DAL ./JDev.Tuteee.DAL/
COPY JDev.Tuteee.Protos ./JDev.Tuteee.Protos/
COPY JDev.Tuteee.CustomTypes ./JDev.Tuteee.CustomTypes/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj \
    && dotnet build --no-restore --configuration Release ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj \
    && dotnet publish --no-build -o out ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

FROM infra.registry.johngould.net/bao-dotnet:0.1.3

WORKDIR /app

COPY --from=build /app/out .

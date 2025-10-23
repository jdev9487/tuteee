FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Grpc.Api"
RUN mkdir "JDev.Tuteee.Grpc.Api.Messages"
RUN mkdir "JDev.Tuteee.DAL"
RUN mkdir "JDev.Tuteee.Protos"

COPY JDev.Tuteee.Grpc.Api ./JDev.Tuteee.Grpc.Api/
COPY JDev.Tuteee.Grpc.Api.Messages ./JDev.Tuteee.Grpc.Api.Messages/
COPY JDev.Tuteee.DAL ./JDev.Tuteee.DAL/
COPY JDev.Tuteee.Protos ./JDev.Tuteee.Protos/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

FROM infra.registry.johngould.net/bao-dotnet:0.1.3

WORKDIR /app

COPY --from=build /app/out .
COPY JDev.Tuteee.Grpc.Api/bao-initialise.sh .

ENTRYPOINT ["sh", "bao-initialise.sh"]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.EmailConsumer"
RUN mkdir "JDev.Tuteee.Grpc.Api.Messages"

COPY JDev.Tuteee.EmailConsumer ./JDev.Tuteee.EmailConsumer/
COPY JDev.Tuteee.Grpc.Api.Messages ./JDev.Tuteee.Grpc.Api.Messages/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=openbao/openbao:2.4.1 /bin/bao /bin
COPY --from=build /app/out .
COPY JDev.Tuteee.EmailConsumer/initialise-bao.sh /app/initialise-bao.sh

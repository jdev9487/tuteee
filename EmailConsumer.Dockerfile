FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.EmailConsumer"
RUN mkdir "JDev.Tuteee.Grpc.Api.Messages"

COPY JDev.Tuteee.EmailConsumer ./JDev.Tuteee.EmailConsumer/
COPY JDev.Tuteee.Grpc.Api.Messages ./JDev.Tuteee.Grpc.Api.Messages/
COPY nuget.config nuget.config

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

RUN dotnet publish -o out ./JDev.Tuteee.EmailConsumer/JDev.Tuteee.EmailConsumer.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JDev.Tuteee.EmailConsumer.dll"]

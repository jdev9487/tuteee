FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Grpc.Api"
RUN mkdir "JDev.Tuteee.DAL"
RUN mkdir "JDev.Tuteee.Protos"

COPY JDev.Tuteee.Grpc.Api ./JDev.Tuteee.Grpc.Api/
COPY JDev.Tuteee.DAL ./JDev.Tuteee.DAL/
COPY JDev.Tuteee.Protos ./JDev.Tuteee.Protos/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.Grpc.Api/JDev.Tuteee.Grpc.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JDev.Tuteee.Grpc.Api.dll"]

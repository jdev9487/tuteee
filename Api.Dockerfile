FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Api"
RUN mkdir "JDev.Tuteee.ApiClient"

COPY JDev.Tuteee.Api ./JDev.Tuteee.Api/
COPY JDev.Tuteee.ApiClient ./JDev.Tuteee.ApiClient/

RUN dotnet restore ./JDev.Tuteee.Api/JDev.Tuteee.Api.csproj

RUN dotnet publish -o out ./JDev.Tuteee.Api/JDev.Tuteee.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JDev.Tuteee.Api.dll"]

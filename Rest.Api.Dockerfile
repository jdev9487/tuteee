FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Rest.Api"
RUN mkdir "JDev.Tuteee.DAL"
RUN mkdir "JDev.Tuteee.Rest.ApiClient"

COPY JDev.Tuteee.Rest.Api ./JDev.Tuteee.Rest.Api/
COPY JDev.Tuteee.DAL ./JDev.Tuteee.DAL/
COPY JDev.Tuteee.Rest.ApiClient ./JDev.Tuteee.Rest.ApiClient/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.Rest.Api/JDev.Tuteee.Rest.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENV ConnectionStrings__Tuteee my-connection-string

ENTRYPOINT ["dotnet", "JDev.Tuteee.Rest.Api.dll"]

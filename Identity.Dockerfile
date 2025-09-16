FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Identity"
RUN mkdir "JDev.Tuteee.Rest.ApiClient"
RUN mkdir "JDev.Tuteee.Protos"

COPY JDev.Tuteee.Identity ./JDev.Tuteee.Identity/
COPY JDev.Tuteee.Rest.ApiClient ./JDev.Tuteee.Rest.ApiClient/
COPY JDev.Tuteee.Protos ./JDev.Tuteee.Protos/

COPY nuget.config .

RUN dotnet restore --config-file nuget.config ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

RUN dotnet publish -o out ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JDev.Tuteee.Identity.dll"]

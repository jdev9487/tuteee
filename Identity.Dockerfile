FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Identity"
RUN mkdir "JDev.Tuteee.Rest.ApiClient"
RUN mkdir "JDev.Tuteee.Protos"

COPY JDev.Tuteee.Identity ./JDev.Tuteee.Identity/
COPY JDev.Tuteee.Rest.ApiClient ./JDev.Tuteee.Rest.ApiClient/
COPY JDev.Tuteee.Protos ./JDev.Tuteee.Protos/

COPY nuget.config .

RUN dotnet restore --configfile nuget.config ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

RUN dotnet build --no-restore --configuration Release ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

RUN dotnet publish --no-build -o out ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=openbao/openbao:2.4.1 /bin/bao /bin
COPY --from=build /app/out .
COPY JDev.Tuteee.Identity/initialise.sh /app/initialise.sh

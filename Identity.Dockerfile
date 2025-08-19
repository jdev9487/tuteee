FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.Identity"
RUN mkdir "JDev.Tuteee.ApiClient"

COPY JDev.Tuteee.Identity ./JDev.Tuteee.Identity/
COPY JDev.Tuteee.ApiClient ./JDev.Tuteee.ApiClient/

RUN dotnet restore ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

RUN dotnet publish -o out ./JDev.Tuteee.Identity/JDev.Tuteee.Identity.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JDev.Tuteee.Identity.dll"]

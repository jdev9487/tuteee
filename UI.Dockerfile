FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN mkdir "JDev.Tuteee.UI"
RUN mkdir "JDev.Tuteee.ApiClient"

COPY JDev.Tuteee.UI ./JDev.Tuteee.UI/
COPY JDev.Tuteee.ApiClient ./JDev.Tuteee.ApiClient/

RUN dotnet restore ./JDev.Tuteee.UI/JDev.Tuteee.UI.csproj

RUN dotnet publish -o out ./JDev.Tuteee.UI/JDev.Tuteee.UI.csproj

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/out/wwwroot .
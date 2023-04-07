FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY ["Clients/Client.Web/Client.Web.csproj", "Clients/Client.Web/"]
COPY ["Clients/Client.Web.Utilities/Client.Web.Utilities.csproj", "Clients/Client.Web.Utilities/"]
COPY ["Clients/Clients.Web.Components/Clients.Web.Components.csproj", "Clients/Clients.Web.Components/"]

RUN dotnet restore "Clients/Client.Web/Client.Web.csproj"

COPY . . 

WORKDIR "/app/Clients/Client.Web"

RUN dotnet build "Client.Web.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Client.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine as final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY Clients/Client.Web/nginx.conf /etc/nginx/nginx.conf
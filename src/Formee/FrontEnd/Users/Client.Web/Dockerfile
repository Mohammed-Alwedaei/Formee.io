FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY ["FrontEnd/Users/Client.Web/Client.Web.csproj", "FrontEnd/Users/Client.Web/"]
COPY ["FrontEnd/Users/Web.Utilities/Client.Web.Utilities.csproj", "FrontEnd/Users/Web.Utilities/"]
COPY ["FrontEnd/Users/Web.Components/Clients.Web.Components.csproj", "FrontEnd/Users/Web.Components/"]

RUN dotnet restore "FrontEnd/Users/Client.Web/Client.Web.csproj"

COPY . . 

WORKDIR "/app/FrontEnd/Users/Client.Web"

RUN dotnet build "Client.Web.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Client.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine as final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY FrontEnd/Users/Client.Web/nginx.conf /etc/nginx/nginx.conf
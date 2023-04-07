#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Notifications/Presentation/Presentation.csproj", "Services/Notifications/Presentation/"]
COPY ["Services/Notifications/Infrastructure/Infrastructure.csproj", "Services/Notifications/Infrastructure/"]
COPY ["Services/Notifications/Application/Application.csproj", "Services/Notifications/Application/"]
COPY ["Services/Notifications/Domain/Domain.csproj", "Services/Notifications/Domain/"]
RUN dotnet restore "Services/Notifications/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Notifications/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
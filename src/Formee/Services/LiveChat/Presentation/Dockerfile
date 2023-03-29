#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/LiveChat/Presentation/Presentation.csproj", "Services/LiveChat/Presentation/"]
COPY ["Services/LiveChat/Infrastructure/Infrastructure.csproj", "Services/LiveChat/Infrastructure/"]
COPY ["Services/LiveChat/Application/Application.csproj", "Services/LiveChat/Application/"]
COPY ["Services/LiveChat/Domain/Domain.csproj", "Services/LiveChat/Domain/"]
RUN dotnet restore "Services/LiveChat/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/Services/LiveChat/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
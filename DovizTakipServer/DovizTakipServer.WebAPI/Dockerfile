#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["DovizTakipServer/DovizTakipServer.WebAPI/DovizTakipServer.WebAPI.csproj", "DovizTakipServer.WebAPI/"]
COPY ["DovizTakipServer/DovizTakipServer.Application/DovizTakipServer.Application.csproj", "DovizTakipServer.Application/"]
COPY ["DovizTakipServer/DovizTakipServer.Domain/DovizTakipServer.Domain.csproj", "DovizTakipServer.Domain/"]
COPY ["DovizTakipServer/DovizTakipServer.Infrastructure/DovizTakipServer.Infrastructure.csproj", "DovizTakipServer.Infrastructure/"]

WORKDIR /src
RUN dotnet restore "DovizTakipServer.WebAPI/DovizTakipServer.WebAPI.csproj"

COPY ["DovizTakipServer/DovizTakipServer.WebAPI/", "DovizTakipServer.WebAPI/"]
COPY ["DovizTakipServer/DovizTakipServer.Application/", "DovizTakipServer.Application/"]
COPY ["DovizTakipServer/DovizTakipServer.Domain/", "DovizTakipServer.Domain/"]
COPY ["DovizTakipServer/DovizTakipServer.Infrastructure/", "DovizTakipServer.Infrastructure/"]

WORKDIR "/src/DovizTakipServer.WebAPI/"
RUN dotnet build "./DovizTakipServer.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DovizTakipServer.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DovizTakipServer.WebAPI.dll"]

#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/User/BelleZone.User.Api/BelleZone.User.Api.csproj", "src/User/BelleZone.User.Api/"]
COPY ["aspire/dotnet-ecommerce.ServiceDefaults/dotnet-ecommerce.ServiceDefaults.csproj", "aspire/dotnet-ecommerce.ServiceDefaults/"]
RUN dotnet restore "./src/User/BelleZone.User.Api/BelleZone.User.Api.csproj"
COPY . .
WORKDIR "/src/src/User/BelleZone.User.Api"
RUN dotnet build "./BelleZone.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BelleZone.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BelleZone.User.Api.dll"]
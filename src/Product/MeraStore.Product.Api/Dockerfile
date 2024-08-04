#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Product/MeraStore.Product.Api/MeraStore.Product.Api.csproj", "src/Product/MeraStore.Product.Api/"]
COPY ["aspire/dotnet-ecommerce.ServiceDefaults/dotnet-ecommerce.ServiceDefaults.csproj", "aspire/dotnet-ecommerce.ServiceDefaults/"]
COPY ["src/Product/MeraStore.Product.Models/MeraStore.Product.Models.csproj", "src/Product/MeraStore.Product.Models/"]
COPY ["src/Product/MeraStore.Product.Service/MeraStore.Product.Services.csproj", "src/Product/MeraStore.Product.Service/"]
RUN dotnet restore "./src/Product/MeraStore.Product.Api/MeraStore.Product.Api.csproj"
COPY . .
WORKDIR "/src/src/Product/MeraStore.Product.Api"
RUN dotnet build "./MeraStore.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeraStore.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeraStore.Product.Api.dll"]
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/eStore/eStore.csproj", "src/eStore/"]
COPY ["src/Modules/Products/Products.Application/Products.Application.csproj", "src/Modules/Products/Products.Application/"]
COPY ["src/Modules/Products/Products.Domain/Products.Domain.csproj", "src/Modules/Products/Products.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
COPY ["src/Modules/Products/Products.Infrastructure/Products.Infrastructure.csproj", "src/Modules/Products/Products.Infrastructure/"]
COPY ["src/Modules/Users/Users.Application/Users.Application.csproj", "src/Modules/Users/Users.Application/"]
COPY ["src/Modules/Users/Users.Domain/Users.Domain.csproj", "src/Modules/Users/Users.Domain/"]
COPY ["src/Modules/Users/Users.Infrastructure/Users.Infrastructure.csproj", "src/Modules/Users/Users.Infrastructure/"]
RUN dotnet restore "./src/eStore/eStore.csproj"
COPY . .
WORKDIR "/src/src/eStore"
RUN dotnet build "./eStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eStore.dll"]
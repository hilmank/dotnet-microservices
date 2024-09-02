#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Services/Userapp/UserApp.API/UserApp.API.csproj", "Services/Userapp/UserApp.API/"]4
COPY ["Services/Userapp/UserApp.Application/UserApp.Application.csproj", "Services/Userapp/UserApp.Application/"]4
COPY ["Services/Userapp/UserApp.Core/UserApp.Core.csproj", "Services/Userapp/UserApp.Core/"]
COPY ["Services/Userapp/UserApp.Infrastructure/UserApp.Infrastructure.csproj", "Services/Userapp/UserApp.Infrastructure/"]
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj", "Infrastructure/Common.Logging/"]
RUN dotnet restore "./Services/Userapp/UserApp.API/UserApp.API.csproj"
COPY . .

# Command to run when the container starts
CMD ["ls", "-R", "/app"]

WORKDIR "/src/Services/Userapp/UserApp.API"
RUN dotnet build "./UserApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "UserApp.API.dll"]

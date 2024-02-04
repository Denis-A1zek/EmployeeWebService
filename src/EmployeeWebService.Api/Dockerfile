#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EmployeeWebService.Api/EmployeeWebService.Api.csproj", "src/EmployeeWebService.Api/"]
COPY ["src/EmployeeWebService.Application/EmployeeWebService.Application.csproj", "src/EmployeeWebService.Application/"]
COPY ["src/EmployeeWebService.Domain/EmployeeWebService.Domain.csproj", "src/EmployeeWebService.Domain/"]
COPY ["src/EmployeeWebService.Data/EmployeeWebService.Data.csproj", "src/EmployeeWebService.Data/"]
RUN dotnet restore "./src/EmployeeWebService.Api/./EmployeeWebService.Api.csproj"
COPY . .
WORKDIR "/src/src/EmployeeWebService.Api"
RUN dotnet build "./EmployeeWebService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeWebService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeWebService.Api.dll"]
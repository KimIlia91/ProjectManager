#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PM.WebApi/PM.WebApi.csproj", "PM.WebApi/"]
COPY ["PM.Infrastructure/PM.Infrastructure.csproj", "PM.Infrastructure/"]
COPY ["PM.Logic/PM.Application.csproj", "PM.Logic/"]
COPY ["PM.Domain/PM.Domain.csproj", "PM.Domain/"]
RUN dotnet restore "PM.WebApi/PM.WebApi.csproj"
COPY . .
WORKDIR "/src/PM.WebApi"
RUN dotnet build "PM.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PM.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PM.WebApi.dll"]